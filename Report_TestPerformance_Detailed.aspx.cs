using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;

partial class Report_TestPerformance_Detailed : System.Web.UI.Page
{


    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL_Division();
            FillDDL_AcadYear();
            FillDDL_TestCategories();
            ControlVisibility("Search");
           // SetSearchPanel_UserControl();     
            ddlStandard.Items.Insert(0, "Select");
            ddlStandard.SelectedIndex = 0;
            ddlTest.Items.Insert(0, "Select");
            ddlTest.SelectedIndex = 0;
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
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the specified ASP.NET
        //     server control at run time. 

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

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
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

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
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

    private void FillDDL_AcadYear()
    {
        DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcadYear, dsAcadYear, "Description", "Id");
        ddlAcadYear.Items.Insert(0, "Select");
        ddlAcadYear.SelectedIndex = 0;
    }

    private void FillDDL_Search_Centre()
    {
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);

        //BindDDL(ddlCentre, dsCentre, "Center_Name", "Center_Code");
        //ddlCentre.Items.Insert(0, "Select");
        //ddlCentre.SelectedIndex = 0;
        BindListBox(ddlCentre, dsCentre, "Center_Name", "Center_Code");
        ddlCentre.Items.Insert(0, "All");
    }


    private void FillDDL_TestCategories()
    {
        DataSet dsTestCategory = ProductController.GetAllActiveTestCategory();
        BindDDL(ddlTestCategory, dsTestCategory, "TestCategory_Name", "TestCategory_Id");
        ddlTestCategory.Items.Insert(0, "Select");
        ddlTestCategory.SelectedIndex = 0;

        if (dsTestCategory.Tables[0].Rows.Count > 0)
        {
            try
            {
                ddlTestCategory.SelectedValue = "002";
            }
            catch
            {
            }
        }
    }


    private void FillDDL_TestName()
    {
        string DivCode = "",AcadYear="",CourseCode="",TestCategoryId="",FromDate="",ToDate="";

        DivCode = ddlDivision.SelectedValue;
        AcadYear = ddlAcadYear.SelectedValue;
        CourseCode = ddlStandard.SelectedValue;
        TestCategoryId = ddlTestCategory.SelectedValue;

        string CenterCode = "";

        for (int CenterCnt = 0; CenterCnt <= ddlCentre.Items.Count - 1; CenterCnt++)
        {
            // CenterCode = CenterCode + ddlCentre.Items[CenterCnt].Value + ",";
            if (ddlCentre.Items[CenterCnt].Selected == true)
            {
                CenterCode = CenterCode + ddlCentre.Items[CenterCnt].Value + ",";
            }
        }

        string DateRange = id_date_range_picker_1.Value;
        if (DateRange != "")
        {
            FromDate = (DateRange.Substring(0, DateRange.Length - 13)).ToString();
            ToDate = DateRange.Substring(DateRange.Length - 10);
        }

        DataSet dsTestName = ProductController.GetTestScheduleBy_Division_Year_Standard(DivCode, AcadYear, CourseCode, "", "", TestCategoryId, "", "", FromDate, ToDate, 0, 0, 3, CenterCode);
        BindDDL(ddlTest, dsTestName, "Test_Name", "PKey");
        ddlTest.Items.Insert(0, "Select");
        ddlTest.SelectedIndex = 0;
    }

    protected void btnTestName_Click(object sender, System.EventArgs e)
    {
        FillDDL_TestName();
    }


    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_Standard();
        FillDDL_Search_Centre();
        FillDDL_TestName();        
    }

    protected void ddlAcadYear_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_Standard();
        FillDDL_TestName();         
    }

    protected void btnExport_Click(object sender, System.EventArgs e)
    {
        //Response.Clear();

        //Response.AddHeader("content-disposition", "attachment;filename=Report_TestPerformance.xls");

        //Response.Charset = "";


        //Response.ContentType = "application/vnd.xls";

        //System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        //dlGridReport1.RenderControl(htmlWrite);

        //Response.Write(stringWrite.ToString());

        //Response.End();

        ////////////////////////////////////////////////////////////////////////
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Students Report_TestPerformance Details_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='6'>Test Performance Detailed</b></TD></TR><TR><TD Colspan='2'><b>Division : " + lblDivision_Result.Text.ToString() + "</b></TD><TD Colspan='2'><b>Acad Year : " + lblAcadYear_Result.Text.ToString() + "</b></TD><TD Colspan='2'><b>Course : " + lblStandard_Result.Text.ToString() + "</b></TD></TR><TR><TD Colspan='2'><b>Test Category : " + lblTestCategory_Result.Text.ToString() + "</b></TD><TD Colspan='2'><b>Test Name : " + lblTestName_Result.Text.ToString() + "</b></TD><TD Colspan='2'><b>Test Date : " + lblTestDate_Result.Text.ToString() + "</b></TD></TR>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)
        dlGridReport1.RenderControl(oHtmlTextWriter1);
        //string style = @"<style> td { mso-number-format:\@;} </style>";
        //Response.Write(style);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
    }
    public Report_TestPerformance_Detailed()
    {
        Load += Page_Load;
    }


    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlAcadYear.SelectedIndex = 0;
        ddlCentre.Items.Clear();
        ddlStandard.SelectedIndex = 0;
        //ddlTestCategory.SelectedIndex = 0;
        ddlTest.Items.Clear();
        ddlTest.Items.Insert(0, "Select");
        ddlTest.SelectedIndex = 0;
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Division");            
            return;
        }
        if (ddlAcadYear.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Acad Year");
            return;
        }
        if (ddlStandard.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Course");
            return;
        }
        if (ddlTestCategory.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Test Category");
            return;
        }

        if (ddlTest.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Test");
            return;
        }

        string CenterCode = "", Userid="";

        for (int Cnt = 0; Cnt <= ddlCentre.Items.Count - 1; Cnt++)
        {
            if (ddlCentre.Items[Cnt].Selected == true)
            {
                CenterCode = CenterCode + ddlCentre.Items[Cnt].Value + ",";
            }
        }

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");
        Userid = lblHeader_User_Code.Text;

        DataSet dsTestReport = ProductController.Get_Rpt_test_Performance_Detailed(CenterCode, ddlTest.SelectedValue, Userid, 1);

        if (dsTestReport != null)
        {
            if (dsTestReport.Tables.Count == 2)
            {

                if (dsTestReport.Tables[0].Rows.Count > 0)
                {
                    ControlVisibility("Result");

                    dlGridReport1.DataSource = dsTestReport.Tables[0];
                    dlGridReport1.DataBind();

                    lbltotalcount.Text = dsTestReport.Tables[0].Rows.Count.ToString();
                    lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
                    lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
                    lblStandard_Result.Text = ddlStandard.SelectedItem.ToString();
                    lblTestCategory_Result.Text = ddlTestCategory.SelectedItem.ToString();
                    lblTestName_Result.Text = ddlTest.SelectedItem.ToString();
                    lblTestDate_Result.Text = dsTestReport.Tables[1].Rows[0]["TestDate"].ToString();
                }
                else
                {
                    Show_Error_Success_Box("E", "Records not found");
                    return;
                }

            }
            else
            {
                Show_Error_Success_Box("E", "Records not found");
                return;
            }
        }
      


    }
    
    protected void ddlStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_TestName();  
    }

    protected void ddlTestCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_TestName();
    }
}
