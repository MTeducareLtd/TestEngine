using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

public partial class Rpt_PaperCorrector_and_TeacherDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ControlVisibility("Search");
                FillDDL_Division();
                FillDDL_AcadYear();              
            }
        }
        catch (Exception ex)
        {
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
        BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
        ddlDivision.Items.Insert(0, "Select");
        ddlDivision.SelectedIndex = 0;


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
    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
        FillDDL_Search_Center();
        Clear_Error_Success_Box();
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

        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        DataSet dsCenter = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);

        BindListBox(ddlCenter, dsCenter, "Center_Name", "Center_Code");
        ddlCenter.Items.Insert(0, "All");       

    }

    private void FillDDL_Standard()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        DataSet dsStandard = ProductController.GetAllActive_Standard_ForYear(Div_Code, YearName);
        //BindDDL(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");
        //ddlStandard.Items.Insert(0, "Select");
        //ddlStandard.SelectedIndex = 0;
        BindListBox(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");
    }

    protected void ddlAcadYear_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
        Clear_Error_Success_Box();
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
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlAcadYear.SelectedIndex = 0;
        //ddlStandard.Items.Clear();
        FillDDL_Standard();
        ddlCenter.Items.Clear();
        id_date_range_picker_1.Value = "";
        Clear_Error_Success_Box();
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        Clear_Error_Success_Box();
    }
    protected void HLExport_Click(object sender, EventArgs e)
    {
        

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Paper_corrector_Details_And_Teacher_Details_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='13'>Paper Corrector Details and Teacher Details</b></TD></TR><TR style='color: #fff; background: black;text-align:Left;'><TD Colspan='4'>Division-" + lblDivision_Result.Text + "</td><TD Colspan='4'>Academic Year-" + lblAcadYear_Result.Text + "</td><TD Colspan='5'>Centers-" + lblCentre_Result.Text + "</td></tr><TR style='color: #fff; background: black;text-align:left;'><TD Colspan='4'>Course-" + lblStandard_Result.Text + "</td><TD Colspan='4'>Test Period-" + lblPeriod.Text + "</td> <TD Colspan='5'></td></tr>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)
        dlGridDisplay.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();

    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Fill_Grid();
    }
    private void Fill_Grid()
    {
        try
        {
            //Validate if all information is entered correctly
            if (ddlDivision.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0001");
                ddlDivision.Focus();
                return;
            }
            
            if (ddlAcadYear.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select AcadYear");
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
                    Center_Code = Center_Code + ddlCenter.Items[CenterCnt].Value + ",";
                    Center_Name = Center_Name + ddlCenter.Items[CenterCnt].Text + ",";
                }
            }
                        
            if (CenterSelCnt == 0)
            {
                Center_Code = "%%";
                Center_Name = "All";
            }
            else
            {
                Center_Code = Common.RemoveComma(Center_Code);
                Center_Name = Common.RemoveComma(Center_Name);
            }

            string DivisionCode = "";
            string Course = "", CourseName = "";
            DivisionCode = ddlDivision.SelectedValue;

            for (int CourseCnt = 0; CourseCnt <= ddlStandard.Items.Count - 1; CourseCnt++)
            {
                if (ddlStandard.Items[CourseCnt].Selected == true)
                {
                    Course = Course + ddlStandard.Items[CourseCnt].Value + ",";
                    CourseName = CourseName + ddlStandard.Items[CourseCnt].Text + ",";
                }
            }

           // 


            if (Course == "")
            {
                Course = "%%";
                CourseName = "All";
            }
            else
            {
                Course = Common.RemoveComma(Course);
                CourseName = Common.RemoveComma(CourseName);
            }

            string AcademicYear = "";
            AcademicYear = ddlAcadYear.SelectedItem.Text;

            //string Course = "";
            //Course = ddlCourse.SelectedValue;


            string DateRange = "";
            DateRange = id_date_range_picker_1.Value;


            string FromDate="", ToDate="";

            if (DateRange != "")
            {
                FromDate = DateRange.Substring(0, 10);
                ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;
            }


           
            //lblstatus.Text = ddlcriteria.SelectedValue;

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            Label lblHeader_DBName = default(Label);
            lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

            if (string.IsNullOrEmpty(lblHeader_User_Code.Text))
                Response.Redirect("Default.aspx");

            DataSet dsGrid = null;
            dsGrid = ProductController.Get_RPTPaperCorrector_Details_And_TeacherDetails(ddlDivision.SelectedValue, Course, Center_Code, AcademicYear, FromDate, ToDate, lblHeader_User_Code.Text, 1);
            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    if (dsGrid.Tables[1].Rows.Count >1 )
                    {
                        ControlVisibility("Result");
                        dlGridDisplay.DataSource = dsGrid.Tables[1];
                        dlGridDisplay.DataBind();
                        lbltotalcount.Text = dsGrid.Tables[1].Rows.Count.ToString();
                        // BtnAuthorization.Visible = true;
                        lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
                        lblStandard_Result.Text = CourseName;
                        lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
                        lblCentre_Result.Text = Center_Name;
                        lblPeriod.Text = dsGrid.Tables[0].Rows[0]["TestPeriod"].ToString();                       
                    }
                    else
                    {
                        dlGridDisplay.DataSource = null;
                        dlGridDisplay.DataBind();
                        // lbltotalcount.Text = "0";
                        ControlVisibility("Search");
                        Show_Error_Success_Box("E", "Record not found ");
                        lbltotalcount.Text = "0";
                        // BtnAuthorization.Visible = false;
                        
                    }
                }
                else
                {
                    dlGridDisplay.DataSource = null;
                    dlGridDisplay.DataBind();
                    // lbltotalcount.Text = "0";
                    Show_Error_Success_Box("E", "Record not found ");
                    lbltotalcount.Text = "0";
                    // BtnAuthorization.Visible = false;
                }

            }
            else
            {
                dlGridDisplay.DataSource = null;
                dlGridDisplay.DataBind();
                // lbltotalcount.Text = "0";
                Show_Error_Success_Box("E", "Record not found ");
                lbltotalcount.Text = "0";
                //  BtnAuthorization.Visible = false;
            }


        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            //BtnAuthorization.Visible = false;
            return;

        }



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
}