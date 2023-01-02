using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.OleDb;
using System.Web.UI;
using System.Net.Mail;
using System.Net;

public partial class Rpt_Studentwise_Absentisum_Detailed : System.Web.UI.Page
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
        else if (Mode == "Manage")
        {
  
            DivResultPanel.Visible = false;
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
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Standard();
        FillDDL_Search_Centre();
        Clear_Error_Success_Box();
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
    protected void ddlAcadYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Standard();
    }
    private void FillDDL_Batch()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        string CentreCode = null;
        CentreCode = ddlCentre.SelectedValue;


        DataSet dsBatch = ProductController.GetAllActive_Batch_ForStandard(Div_Code, YearName, StandardCode, CentreCode);
        BindListBox(ddlBatch, dsBatch, "Batch_Name", "Batch_Code");
        //ddlBatch.SelectedIndex = 0;
    }
    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
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
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivision.Focus();
            return;
        }

        if (ddlAcadYear.SelectedIndex == 0 || ddlAcadYear.SelectedIndex == -1)
        {
            Show_Error_Success_Box("E", "0002");
            ddlAcadYear.Focus();
            return;
        }
        if (ddlStandard.SelectedIndex == 0 || ddlStandard.SelectedIndex == -1)
        {
            Show_Error_Success_Box("E", "0003");
            ddlStandard.Focus();
            return;
        }
        //if (ddlCentre.SelectedIndex == 0 || ddlCentre.SelectedIndex == -1)
        //{
        //    Show_Error_Success_Box("E", "0006");
        //    ddlCentre.Focus();
        //    return;
        //}

        string Center_Code = "";
        string Center_Name = "";
        int CenterCnt = 0;
        int CenterSelCnt = 0;
        
        for (CenterCnt = 0; CenterCnt <= ddlCentre.Items.Count - 1; CenterCnt++)
        {
            if (ddlCentre.Items[CenterCnt].Selected == true)
            {
                CenterSelCnt = CenterSelCnt + 1;
            }
        }



        if (CenterSelCnt == 0)
        {
            //When all is selected   
            Show_Error_Success_Box("E", "Select atleast one center");
            ddlCentre.Focus();
            return;

        }
        else
        {
            for (CenterCnt = 0; CenterCnt <= ddlCentre.Items.Count - 1; CenterCnt++)
            {
                if (ddlCentre.Items[CenterCnt].Selected == true)
                {
                    Center_Code = Center_Code + ddlCentre.Items[CenterCnt].Value + ",";
                    Center_Name = Center_Name + ddlCentre.Items[CenterCnt].Text + ",";
                }
            }
            Center_Code = Common.RemoveComma(Center_Code);
            Center_Name = Common.RemoveComma(Center_Name);
        }
     
        //if (ddlBatch.SelectedIndex == 0 || ddlBatch.SelectedIndex == -1)
        //{
        //    Show_Error_Success_Box("E", "0015");
        //    ddlBatch.Focus();
        //    return;
        //}

        string BatchCode = "";
        int BatchCnt = 0;
        int BatchSelCnt = 0;
        for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
        {
            if (ddlBatch.Items[BatchCnt].Selected == true)
            {
                BatchSelCnt = BatchSelCnt + 1;
            }
        }

        if (BatchSelCnt == 0)
        {
            ////When all is selected
            //for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            //{
            //    BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
            //}
            ////if (Strings.Right(BatchCode, 1) == ",")
            ////    BatchCode = Strings.Left(BatchCode, Strings.Len(BatchCode) - 1);
            //BatchCode = Common.RemoveComma(BatchCode);
            Show_Error_Success_Box("E", "Select atleast one Batch");
            return;
        }
        else
        {
            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                if (ddlBatch.Items[BatchCnt].Selected == true)
                {
                    BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
                }
            }
            //if (Strings.Right(BatchCode, 1) == ",")
            //    BatchCode = Strings.Left(BatchCode, Strings.Len(BatchCode) - 1);
            BatchCode = Common.RemoveComma(BatchCode);
        }

        if (ddlRollNo.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Roll No");
            ddlRollNo.Focus();
            return;
        }
        //if (id_date_range_picker_1.se == 0)
        //{
        //    Show_Error_Success_Box("E", "0012");
        //    ddlRollNo.Focus();
        //    return;
        //}

        

        //string TestType_ID = "";
        //int TypeCnt = 0;
        //int TypeSelCnt = 0;
        //for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
        //{
        //    if (ddlTestType.Items[TypeCnt].Selected == true)
        //    {
        //        TypeSelCnt = TypeSelCnt + 1;
        //    }
        //}

        //if (TypeSelCnt == 0)
        //{
        //    //When all is selected
        //    for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
        //    {
        //        TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
        //    }
        //    //if (Strings.Right(TestType_ID, 1) == ",")
        //    //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);
        //    TestType_ID = Common.RemoveComma(TestType_ID);
        //}
        //else
        //{
        //    for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
        //    {
        //        if (ddlTestType.Items[TypeCnt].Selected == true)
        //        {
        //            TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
        //        }
        //    }
        //    //if (Strings.Right(TestType_ID, 1) == ",")
        //    //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);
        //    TestType_ID = Common.RemoveComma(TestType_ID);
        //}

        ControlVisibility("Result");

        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        string ROll_No = null;
        ROll_No = ddlRollNo.SelectedValue;

        //string TestName = null;
        //if (string.IsNullOrEmpty(txtTestName.Text.Trim()))
        //{
        //    TestName = "%";
        //}
        //else
        //{
        //    TestName = "%" + txtTestName.Text.Trim();
        //}

        string DateRange = null;
        DateRange = id_date_range_picker_1.Value;

        string FromDate = null;
        string ToDate = null;
        //FromDate = Strings.Left(DateRange, 10);
        //if (DateRange != "")
        //{
        //    FromDate = DateRange.Substring(0, 10);
        //}
        //if (string.IsNullOrEmpty(FromDate))
        //    FromDate = System.DateTime.Now.ToString("dd MMM yyyy");

        //// ToDate = Strings.Right(DateRange, 10);
        //if (DateRange != "")
        //{
        //    ToDate = DateRange.Substring(DateRange.Length - 10);//DateRange.Substring(DateRange.Length, 10);
        //}
        //if (string.IsNullOrEmpty(ToDate))
        //    ToDate = System.DateTime.Now.ToString("dd MMM yyyy");


        if (DateRange != "")
        {
            FromDate = DateRange.Substring(0, 10);//Strings.Left(Report_Period, 10);            
            DateTime result = DateTime.ParseExact(FromDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            FromDate = result.ToString("yyyy-MM-dd");
        }
        if (string.IsNullOrEmpty(FromDate))
        {

            FromDate = "2010-01-01";
        }
        if (DateRange != "")
        {
            ToDate = DateRange.Substring(DateRange.Length - 10);//Strings.Right(Report_Period, 10);            
            DateTime result = DateTime.ParseExact(ToDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            ToDate = result.ToString("yyyy-MM-dd");

        }
        if (string.IsNullOrEmpty(ToDate))
        {
            ToDate = System.DateTime.Now.ToString("yyyy-MM-dd");
        }



       //string CenterCode = "";
       //  string CenterName = "";
       // int CenterCnt = 0;
       // int CenterSelCnt = 0;
       // for (CenterCnt = 0; CenterCnt <= ddlBatch.Items.Count - 1; CenterCnt++)
       // {
       //     if (ddlCentre.Items[CenterCnt].Selected == true)
       //     {
       //         CenterSelCnt = CenterSelCnt + 1;
       //     }
       // }

       // if (CenterSelCnt == 0)
       // {
       //     //When all is selected
       //     for (CenterCnt = 0; CenterCnt <= ddlCentre.Items.Count - 1; CenterCnt++)
       //     {
       //         CenterCode = BatchCode + ddlCentre.Items[CenterCnt].Value + ",";
       //     }
       //     //if (Strings.Right(BatchCode, 1) == ",")
       //     //    BatchCode = Strings.Left(BatchCode, Strings.Len(BatchCode) - 1);
       //     CenterCode = Common.RemoveComma(CenterCode);
       //     CenterName = Common.RemoveComma(CenterName);

       // }
       // else
       // {
       //     for (CenterCnt = 0; CenterCnt <= ddlCentre.Items.Count - 1; CenterCnt++)
       //     {
       //         if (ddlCentre.Items[CenterCnt].Selected == true)
       //         {
       //             CenterCode = CenterCode + ddlCentre.Items[CenterCnt].Value + ",";
       //             CenterName = CenterName + ddlCentre.Items[CenterCnt].Value + ",";

       //         }
       //     }
       //     //if (Strings.Right(BatchCode, 1) == ",")
       //     //    BatchCode = Strings.Left(BatchCode, Strings.Len(BatchCode) - 1);
       //     CenterCode = Common.RemoveComma(CenterCode);
       //     CenterName = Common.RemoveComma(CenterName);

       // }




        

        DataSet dsGrid = ProductController.Report_MonthlyReport_Card(DivisionCode, StandardCode, BatchCode, YearName, ROll_No, Center_Code, FromDate, ToDate);




            if (dsGrid != null)
            {
                if (dsGrid.Tables.Count != 0)
                {
                    dlGridDisplay.DataSource = dsGrid;
                    dlGridDisplay.DataBind();

                    dsPrint.DataSource = dsGrid;
                    dsPrint.DataBind();
                   
                }
                else
                {
                    dlGridDisplay.DataSource = null;
                    dlGridDisplay.DataBind();

                    dsPrint.DataSource = null;
                    dsPrint.DataBind();
                }
            }
            else
            {
                dlGridDisplay.DataSource = null;
                dlGridDisplay.DataBind();

                dsPrint.DataSource = null;
                dsPrint.DataBind();
           
            }
            if (dsGrid.Tables[1].Rows.Count != 0)
            {
                DivResult.Visible = true;
                btnPrint.Visible=true;
                HLExport.Visible = true;
                lblPrintrollNo.Text = Convert.ToString(dsGrid.Tables[1].Rows[0]["RollNo"]);
                lblRoll_No.Text = Convert.ToString(dsGrid.Tables[1].Rows[0]["RollNo"]);
                lblPrintstudent.Text = Convert.ToString(dsGrid.Tables[1].Rows[0]["StudentName"]);
                lblStudent_name.Text = Convert.ToString(dsGrid.Tables[1].Rows[0]["StudentName"]);
                lbltotalapplicable_Test.Text = Convert.ToString(dsGrid.Tables[2].Rows[0]["Applicable_Test"]);
                lbltotaltest.Text = Convert.ToString(dsGrid.Tables[2].Rows[0]["Applicable_Test"]);
                lbltotalapplicable_Test.Text = Convert.ToString(dsGrid.Tables[2].Rows[0]["Applicable_Test"]);
                lbltotal_presentTest.Text = Convert.ToString(dsGrid.Tables[2].Rows[0]["Present_Test"]);
                lbltotalpresent_test.Text = Convert.ToString(dsGrid.Tables[2].Rows[0]["Present_Test"]);
                lblabsentpersent.Text = Convert.ToString(dsGrid.Tables[2].Rows[0]["Absent%"]);
                lblabsent_percentage.Text = Convert.ToString(dsGrid.Tables[2].Rows[0]["Absent%"]);
                lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);


                lblperiod.Text = id_date_range_picker_1.Value;
                lblCentre_Result.Text = Center_Name;

                lblPrintperiod.Text = id_date_range_picker_1.Value;
                lblPrint_Center.Text = Center_Name;
            }
            else
            {
                btnPrint.Visible = false;
                HLExport.Visible = false;
                Show_Error_Success_Box("E","No Record Found");
            }

            
        
       
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlAcadYear.SelectedIndex = 0;
     
        ddlStandard.Items.Clear();
        ddlCentre.Items.Clear();
        Msg_Error.Visible = false;
        lblerror.Text = "";
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        ddlBatch.Items.Clear();
        ddlRollNo.Items.Clear();
      
        id_date_range_picker_1.Value = "";
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        DivSearchPanel.Visible = true;
        DivResultPanel.Visible = false;
        DivResult.Visible = false;
    }
    protected void ddlCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
        Clear_Error_Success_Box();
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
        BindListBox(ddlCentre, dsCentre, "Center_Name", "Center_Code");

 
    }
  
    protected void HLExport_Click(object sender, EventArgs e)
    {
        dlGridDisplay.Visible = true;
        DivResult.Visible = true;

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Rpt_MonthlyReport_Card_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='text-align:center;'><TD Colspan='12'>Rpt Monthly Report Card</b></TD></TR><TR style='text-align:center;'><TD Colspan='1'>Student Name</td><TD Colspan='2'>" + lblStudent_name.Text + "</td><TD Colspan='1'>Roll No</td><TD Colspan='2'>" + lblRoll_No.Text + "</td></tr><TR style=';text-align:center;'><TD Colspan='1'>Period</td><TD Colspan='2'>" + lblperiod.Text + "</td><TD Colspan='1'>Centre</td><TD Colspan='2'>" + lblCentre_Result.Text + "</td></tr>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)
        
        dlGridDisplay.RenderControl(oHtmlTextWriter1);
        DivResult.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();


        dlGridDisplay.Visible = false;
    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDdlRollNo();
        Clear_Error_Success_Box();
    }
    protected void ddlStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Batch();
        Clear_Error_Success_Box();
    }


    private void FillDdlRollNo()
    {
        string BatchCode = null;
        BatchCode = ddlBatch.SelectedValue;

        DataSet dsRollNO = ProductController.GetRollNumber_batchcode(BatchCode);
        BindDDL(ddlRollNo, dsRollNO, "RollNo", "RollNo");
        ddlRollNo.Items.Insert(0, "Select");
      
    }

}