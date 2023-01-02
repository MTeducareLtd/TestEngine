using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShoppingCart.BL;
using System.Globalization;

public partial class Rpt_Test_Supervisor_Payment_Summary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();
        }
    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;


        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;


        }

        Clear_Error_Success_Box();
    }

    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }

    private void Show_Error_Success_Box(string BoxType, string Error_Code)
    {
        if (BoxType == "E")
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox.Update();
        }
        else
        {
            Msg_Success.Visible = true;
            Msg_Error.Visible = false;
            lblSuccess.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox.Update();
        }
    }
    private void FillDDL_Division()
    {
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        if (string.IsNullOrEmpty(lblHeader_User_Code.Text))
            Response.Redirect("Default.aspx");

        DataSet dsDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, "", "", "2", lblHeader_DBName.Text);
        //BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
        //ddlDivision.Items.Insert(0, "Select");
        //ddlDivision.SelectedIndex = 0;
        BindListBox(ddlDivision, dsDivision, "Division_Name", "Division_Code");

    }

    private void FillDDL_AcadYear()
    {
        DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcadYear, dsAcadYear, "Description", "Id");
        ddlAcadYear.Items.Insert(0, "Select");
        ddlAcadYear.SelectedIndex = 0;


    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        if (ds != null)
        {
            if (ds.Tables.Count != 0)
            {
                ddl.DataSource = ds;
                ddl.DataTextField = txtField;
                ddl.DataValueField = valField;
                ddl.DataBind();
            }
        }
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Standard();
        FillDDL_Search_Center();
        Clear_Error_Success_Box();
    }

    private void FillDDL_Standard()
    {
        string Div_Code = "";
        // Div_Code = ddlDivision.SelectedValue;
        for (int DivCnt = 0; DivCnt <= ddlDivision.Items.Count - 1; DivCnt++)
        {
            if (ddlDivision.Items[DivCnt].Selected == true)
            {
                Div_Code = Div_Code + ddlDivision.Items[DivCnt].Value + ",";
            }
        }
        if (Div_Code != "")
        {
            Div_Code = Common.RemoveComma(Div_Code);
        }

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        DataSet dsStandard = ProductController.GetAllActive_Standard_ForYear(Div_Code, YearName);
        //BindDDL(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");
        //ddlStandard.Items.Insert(0, "Select");
        //ddlStandard.SelectedIndex = 0;
        BindListBox(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");
        ddlStandard.Items.Insert(0, "All");
    }

    private void FillDDL_Search_Center()
    {
        ddlCenter.Items.Clear();
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        string Div_Code = "";
        //Div_Code = ddlDivision.SelectedValue;
        for (int DivCnt = 0; DivCnt <= ddlDivision.Items.Count - 1; DivCnt++)
        {
            if (ddlDivision.Items[DivCnt].Selected == true)
            {
                Div_Code = Div_Code + ddlDivision.Items[DivCnt].Value + ",";
            }
        }

        DataSet dsCenter = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);

        BindListBox(ddlCenter, dsCenter, "Center_Name", "Center_Code");
        ddlCenter.Items.Insert(0, "All");

    }

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        if (ds != null)
        {
            if (ds.Tables.Count != 0)
            {
                ddl.DataSource = ds;
                ddl.DataTextField = txtField;
                ddl.DataValueField = valField;
                ddl.DataBind();
            }
        }

    }

    protected void ddlAcadYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Standard();
        Clear_Error_Success_Box();
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Fill_Grid();
    }



    public decimal GetRateValue(object a)
    {

        decimal rate = 0;
        if (a.GetType().FullName != "System.DBNull")
        {
            rate = Convert.ToDecimal(a);
        }
        return rate;
    }

    private void Fill_Grid()
    {
        try
        {


            ////Validate if all information is entered correctly
            //if (ddlDivision.SelectedIndex == 0)
            //{
            //    Show_Error_Success_Box("E", "0001");
            //    ddlDivision.Focus();
            //    return;
            //}
            string Div_Code = "", Div_Name = "", CourseCode = "", CourseName = "";
            //Div_Code = ddlDivision.SelectedValue;
            for (int DivCnt = 0; DivCnt <= ddlDivision.Items.Count - 1; DivCnt++)
            {
                if (ddlDivision.Items[DivCnt].Selected == true)
                {
                    Div_Code = Div_Code + ddlDivision.Items[DivCnt].Value + ",";
                    Div_Name = Div_Name + ddlDivision.Items[DivCnt].Text + ",";
                }
            }

            if (Div_Code == "")
            {
                Show_Error_Success_Box("E", "Select atleast one division");
                return;
            }

            if (ddlAcadYear.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0002");
                ddlAcadYear.Focus();
                return;
            }

            string Center_Code = "";
            string Center_Name = "";
            int CenterCnt = 0;
            int CenterSelCnt = 0;

            for (CenterCnt = 0; CenterCnt <= ddlCenter.Items.Count - 1; CenterCnt++)
            {
                if (ddlCenter.Items[CenterCnt].Selected == true)
                {
                    CenterSelCnt = CenterSelCnt + 1;
                }
            }

            if (CenterSelCnt == 0)
            {
                //When all is selected   
                Show_Error_Success_Box("E", "0006");
                ddlCenter.Focus();
                return;
            }
            else
            {
                for (CenterCnt = 0; CenterCnt <= ddlCenter.Items.Count - 1; CenterCnt++)
                {
                    if (ddlCenter.Items[CenterCnt].Selected == true)
                    {
                        Center_Code = Center_Code + ddlCenter.Items[CenterCnt].Value + ",";
                        Center_Name = Center_Name + ddlCenter.Items[CenterCnt].Text + ",";
                    }
                }
                Center_Code = Common.RemoveComma(Center_Code);
                Center_Name = Common.RemoveComma(Center_Name);
            }

            //if (ddlStandard.SelectedIndex == 0)
            //{
            //    Show_Error_Success_Box("E", "0003");
            //    ddlStandard.Focus();
            //    return;
            //}

            for (int DivCnt = 0; DivCnt <= ddlStandard.Items.Count - 1; DivCnt++)
            {
                if (ddlStandard.Items[DivCnt].Selected == true)
                {
                    CourseCode = CourseCode + ddlStandard.Items[DivCnt].Value + ",";
                    CourseName = CourseName + ddlStandard.Items[DivCnt].Text + ",";
                }
            }

            if (CourseCode == "")
            {
                Show_Error_Success_Box("E", "Select atleast one course");
                return;
            }

            if (id_date_range_picker_1.Value == "")
            {
                Show_Error_Success_Box("E", "Select Date Range");
                id_date_range_picker_1.Focus();
                return;
            }


            ControlVisibility("Result");
            Div_Code = Common.RemoveComma(Div_Code);
            Div_Name = Common.RemoveComma(Div_Name);
            CourseCode = Common.RemoveComma(CourseCode);
            CourseName = Common.RemoveComma(CourseName);
            string DivisionCode = Div_Code;
            //DivisionCode = ddlDivision.SelectedValue;
            //Div_Code = ddlDivision.SelectedValue;


            string AcademicYear = "";
            AcademicYear = ddlAcadYear.SelectedItem.Text;

            string Course = CourseCode;
            //Course = ddlStandard.SelectedValue;

            string DateRange = "";
            DateRange = id_date_range_picker_1.Value;

            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;

            DateTime fdt = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            DateTime tdt = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            lblStandard_Result.Text = CourseName;

            lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
            lblCentre_Result.Text = Center_Name;
            lblPeriod.Text = fdt.ToString("dd MMM yyyy") + " - " + tdt.ToString("dd MMM yyyy");

            DataSet dsGrid = null;
            dsGrid = ProductController.Get_Test_Supervisor_Payment_Summary(DivisionCode, AcademicYear, Course, Center_Code, fdt, tdt, "1");
            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    if (dsGrid.Tables[0].Rows.Count != 0)
                    {
                        lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
                        dlGridExport.DataSource = dsGrid;
                        dlGridExport.DataBind();
                    }
                    else
                    {

                        Show_Error_Success_Box("E", "Record not found ");
                        lbltotalcount.Text = "0";
                        dlGridExport.DataSource = dsGrid;
                        dlGridExport.DataBind();
                    }
                }
                else
                {

                    Show_Error_Success_Box("E", "Record not found ");
                    lbltotalcount.Text = "0";
                    //dlGridExport.DataSource = dsGrid;
                    //dlGridExport.DataBind();
                }

            }
            else
            {
                dlGridExport.DataSource = dsGrid;
                dlGridExport.DataBind();
                Show_Error_Success_Box("E", "Record not found ");
                lbltotalcount.Text = "0";
            }

        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            return;

        }



    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        Clear_Error_Success_Box();
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        //ddlDivision.SelectedIndex = 0;
        FillDDL_Division();
        ddlAcadYear.SelectedIndex = 0;
        ddlStandard.Items.Clear();
        ddlCenter.Items.Clear();
        id_date_range_picker_1.Value = "";
        Clear_Error_Success_Box();
    }
    protected void BtnClose_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }
    protected void HLExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Paper_Checker_Payment_Done_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='10'>Paper Checker Payment Done</b></TD></TR><TR style='color: #fff; background: black;text-align:left;'><TD Colspan='3'>Division - " + lblDivision_Result.Text + "</td><TD Colspan='3'>Academic Year - " + lblAcadYear_Result.Text + "</td><TD Colspan='4'>Centers - " + lblCentre_Result.Text + "</td></tr><TR style='color: #fff; background: black;text-align:left;'><TD Colspan='3'>Course - " + lblStandard_Result.Text + "</td><TD Colspan='7'>Test Period - " + lblPeriod.Text + "</td></tr>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)
        dlGridExport.RenderControl(oHtmlTextWriter1);
        string style = @"<style> td { mso-number-format:\@;} </style>";
        Response.Write(style);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();

        dlGridExport.Visible = false;
    }

    protected void ddlCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        int count = ddlCenter.GetSelectedIndices().Length;

        if (ddlCenter.SelectedValue == "All")
        {
            ddlCenter.Items.Clear();
            ddlCenter.Items.Insert(0, "All");
            ddlCenter.SelectedIndex = 0;

        }
        else if (count == 0)
        {
            FillDDL_Search_Center();
            //BindCenter();
        }
        else
        {

        }
    }
}