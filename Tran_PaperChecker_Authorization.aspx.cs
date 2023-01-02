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
using System.IO;


public partial class Tran_PaperChecker_Authorization : System.Web.UI.Page
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
        BindDDL(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");
        ddlStandard.Items.Insert(0, "Select");
        ddlStandard.SelectedIndex = 0;
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


    protected void BtnSearch_Click(object sender, EventArgs e)
    {        
        Fill_Grid();
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlAcadYear.SelectedIndex = 0;        
        ddlStandard.Items.Clear();
        ddlCenter.Items.Clear();
        id_date_range_picker_1.Value = "";
        Clear_Error_Success_Box();
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        Clear_Error_Success_Box();
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
                Show_Error_Success_Box("E", "0002");
                ddlAcadYear.Focus();
                return;
            }

            if (ddlStandard.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0003");
                ddlStandard.Focus();
                return;
            }
            if (id_date_range_picker_1.Value == "")
            {
                Show_Error_Success_Box("E", "Select Date Range");
                id_date_range_picker_1.Focus();
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


            ControlVisibility("Result");
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            
            string AcademicYear = "";
            AcademicYear = ddlAcadYear.SelectedItem.Text;

            string Course = "";
            Course = ddlStandard.SelectedValue;

            string DateRange = "";
            DateRange = id_date_range_picker_1.Value;


            string FromDate, ToDate;
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;


            DateTime fdt = DateTime.ParseExact(FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            DateTime tdt = DateTime.ParseExact(ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);


            lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
            lblStandard_Result.Text = ddlStandard.SelectedItem.ToString();

            lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
            lblCentre_Result.Text = Center_Name;
            lblPeriod.Text = fdt.ToString("dd MMM yyyy") + " - " + tdt.ToString("dd MMM yyyy");

            DataSet dsGrid = null;
            dsGrid = ProductController.Get_UnAuthorizedAnswerSheet_PaperChecker(DivisionCode, AcademicYear, Course, Center_Code, fdt, tdt, "1");
            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    if (dsGrid.Tables[0].Rows.Count != 0)
                    {
                        dlGridDisplay.DataSource = dsGrid;
                        dlGridDisplay.DataBind();
                        lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
                        BtnAuthorization.Visible = true;
                        dlGridExport.DataSource = dsGrid;
                        dlGridExport.DataBind();
                    }
                    else
                    {
                        dlGridDisplay.DataSource = null;
                        dlGridDisplay.DataBind();
                        Show_Error_Success_Box("E", "Record not found ");
                        lbltotalcount.Text = "0";
                        BtnAuthorization.Visible = false;
                        dlGridExport.DataSource = dsGrid;
                        dlGridExport.DataBind();
                    }
                }
                else
                {
                    dlGridDisplay.DataSource = null;
                    dlGridDisplay.DataBind();
                    Show_Error_Success_Box("E", "Record not found ");
                    lbltotalcount.Text = "0";
                    BtnAuthorization.Visible = false;
                    dlGridExport.DataSource = dsGrid;
                    dlGridExport.DataBind();
                }

            }
            else
            {
                dlGridDisplay.DataSource = null;
                dlGridDisplay.DataBind();
                dlGridExport.DataSource = dsGrid;
                dlGridExport.DataBind();
                Show_Error_Success_Box("E", "Record not found ");
                lbltotalcount.Text = "0";
                BtnAuthorization.Visible = false;                
            }    

        }
        catch( Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString();
            UpdatePanelMsgBox.Update();
            BtnAuthorization.Visible = false;   
            return;

        }



    }
    protected void BtnClose_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }
    protected void BtnAuthorization_Click(object sender, EventArgs e)
    {
        try
        {
            bool flag = false;
            foreach (DataListItem dtlItem in dlGridDisplay.Items)
            {
                CheckBox chkTest = (CheckBox)dtlItem.FindControl("chkTest");
                if (chkTest != null && chkTest.Checked == true)
                {
                    flag = true;
                    break;
                }
            }
            if (flag == false)
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Please select atleast one Test";
                UpdatePanelMsgBox.Update();
                return;
            }
            else
            {
                Label lblHeader_User_Code = default(Label);
                lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                string CreatedBy = null;
                CreatedBy = lblHeader_User_Code.Text;
                
                

                string DivisionCode = null;
                DivisionCode = ddlDivision.SelectedValue;


                string AcademicYear = "";
                AcademicYear = ddlAcadYear.SelectedItem.Text;

                string Course = "";
                Course = ddlStandard.SelectedValue;

                
                int ResultId = 0;


                foreach (DataListItem dtlItem in dlGridDisplay.Items)
                {
                    CheckBox chkTest = (CheckBox)dtlItem.FindControl("chkTest");
                    if (chkTest.Checked)
                    {                        
                        Label lblTest_ID = (Label)dtlItem.FindControl("lblTest_ID");
                        Label lblBatchCode = (Label)dtlItem.FindControl("lblBatchCode");
                        Label lblPartner_Code = (Label)dtlItem.FindControl("lblPartner_Code");
                        Label lblSlab_Code = (Label)dtlItem.FindControl("lblSlab_Code");
                        Label lblBagDispatch_ID = (Label)dtlItem.FindControl("lblBagDispatch_ID");
                        Label lblCenter = (Label)dtlItem.FindControl("lblCenter");
                        Label lblRate = (Label)dtlItem.FindControl("lblRate");
                        Label lblAmount = (Label)dtlItem.FindControl("lblAmount");
                        decimal Rate = 0;
                        decimal Amount = 0;
                        if (lblRate.Text != "")
                        {
                            Rate = Convert.ToDecimal(lblRate.Text);
                        }

                        if (lblAmount.Text != "")
                        {
                            Amount = Convert.ToDecimal(lblAmount.Text);
                        }
                        Label lblConductNo = (Label)dtlItem.FindControl("lblConductNo");
                        
                        ResultId = ProductController.AnswerSheet_Issue_Authorised(DivisionCode, AcademicYear, Course, lblCenter.Text, lblTest_ID.Text, lblBatchCode.Text, lblConductNo.Text,
                                   lblPartner_Code.Text, lblSlab_Code.Text, Amount, Rate, 1, CreatedBy, DateTime.Now, lblBagDispatch_ID.Text, 0, "", DateTime.Now, 0, "", DateTime.Now, 1);
                    
                    }
                    
                }
                                
                Fill_Grid();
                Show_Error_Success_Box("S", "Authorisation done successfully");
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
    protected void chkTestAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlGridDisplay.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkTest");
            if (chkitemck.Visible == true)
            {
                chkitemck.Checked = s.Checked;
            }
        }
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


    public decimal GetTotal(object a, object b)
    {
        decimal amount = 0;
        decimal rate = 0;
        int quantity = 0;

        
        if (a.GetType().FullName != "System.DBNull")
        {
            rate = Convert.ToDecimal(a);
        }
        if (b.GetType().FullName != "System.DBNull")
        {
            quantity = Convert.ToInt32(b);
        }         

       
        amount = rate * quantity;
        return amount;
    }
    protected void HLExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Trans_PaperChecker_Authorisation_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='9'>Trans_PaperChecker_Authorisation</b></TD></TR><TR style='color: #fff; background: black;text-align:center;'><TD Colspan='1'>Division</td><TD Colspan='2'>" + lblDivision_Result.Text + "</td><TD Colspan='1'>Academic Year</td><TD Colspan='2'>" + lblAcadYear_Result.Text + "</td><TD Colspan='1'>Centers</td><TD Colspan='2'>" + lblCentre_Result.Text + "</td></tr><TR style='color: #fff; background: black;text-align:center;'><TD Colspan='1'>Course</td><TD Colspan='2'>" + lblStandard_Result.Text + "</td><TD Colspan='1'>Test Period</td><TD Colspan='5' style='color: #fff; background: black;text-align:left;'>" + lblPeriod.Text + "</td></tr>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)
        dlGridExport.RenderControl(oHtmlTextWriter1);
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