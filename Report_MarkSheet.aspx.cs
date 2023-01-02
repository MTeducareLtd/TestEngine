using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Mail;
using System.Net;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic;
using System.Web.UI;
using System.Web;

partial class Report_MarkSheet : System.Web.UI.Page
{

    protected void BtnSearch_Click(object sender, System.EventArgs e)
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

        if (ddlCentre.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0006");
            ddlCentre.Focus();
            return;
        }

        if (ddlStandard.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0003");
            ddlStandard.Focus();
            return;
        }

        if (ddlTestCategory.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0012");
            ddlTestCategory.Focus();
            return;
        }



        string TestType_ID = "";
        string TestType_Name = "";
        int TypeCnt = 0;
        int TypeSelCnt = 0;




        string Test_ID = "";
        string Test_Name = "";
        int TestCnt = 0;
        int TestSelCnt = 0;





        for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
        {
            if (ddlTestType.Items[TypeCnt].Selected == true)
            {
                TypeSelCnt = TypeSelCnt + 1;
            }
        }

        if (TypeSelCnt == 0)
        {
            //When all is selected
            //When all is selected
            Show_Error_Success_Box("E", "Select At Least One Test Type");
            ddlStudentName.Focus();
            return;
            //if (Strings.Right(TestType_ID, 1) == ",")
            //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);
            //if (Strings.Right(TestType_Name, 1) == ",")
            //    TestType_Name = Strings.Left(TestType_Name, Strings.Len(TestType_Name) - 1);
        }
        else
        {
            for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
            {
                if (ddlTestType.Items[TypeCnt].Selected == true)
                {
                    TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
                    TestType_Name = TestType_Name + ddlTestType.Items[TypeCnt].ToString() + ",";
                }
            }
            TestType_ID = Common.RemoveComma(TestType_ID);
            TestType_Name = Common.RemoveComma(TestType_Name);
            //if (Strings.Right(TestType_ID, 1) == ",")
            //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);
            //if (Strings.Right(TestType_Name, 1) == ",")
            //    TestType_Name = Strings.Left(TestType_Name, Strings.Len(TestType_Name) - 1);
        }


        for (TestCnt = 0; TestCnt <= ddlTestName.Items.Count - 1; TestCnt++)
        {
            if (ddlTestName.Items[TestCnt].Selected == true)
            {
                TestSelCnt = TestSelCnt + 1;
            }
        }

        if (TestSelCnt == 0)
        {
            //When all is selected
            Show_Error_Success_Box("E", "Select At Least One Test");
            ddlStudentName.Focus();
            return;

        }
        else
        {
            for (TypeCnt = 0; TypeCnt <= ddlTestName.Items.Count - 1; TypeCnt++)
            {
                if (ddlTestName.Items[TypeCnt].Selected == true)
                {
                    Test_ID = Test_ID + ddlTestName.Items[TypeCnt].Value + ",";
                    Test_Name = Test_Name + ddlTestName.Items[TypeCnt].ToString() + ",";
                }
            }
            Test_ID = Common.RemoveComma(Test_ID);
            Test_Name = Common.RemoveComma(Test_Name);
            //if (Strings.Right(Test_ID, 1) == ",")
            //    Test_ID = Strings.Left(Test_ID, Strings.Len(Test_ID) - 1);
            //if (Strings.Right(Test_Name, 1) == ",")
            //    Test_Name = Strings.Left(Test_Name, Strings.Len(Test_Name) - 1);
        }


        if (ddlStudentName.SelectedIndex == 0 | ddlStudentName.Items.Count == 0)
        {
            Show_Error_Success_Box("E", "0030");
            ddlStudentName.Focus();
            return;
        }


        TestType_ID = "";
        TestType_Name = "";
        TypeCnt = 0;
        TypeSelCnt = 0;




        Test_ID = "";
        Test_Name = "";
        TestCnt = 0;
        TestSelCnt = 0;


        string BatchCode = "";
        int BatchCnt = 0;
        int BatchSelCnt = 0;

        string FromDate = null;
        string ToDate = null;

        //string Report_Period = id_date_range_picker_1.Value.ToString();
        //if (Report_Period != "")
        //{
        //    FromDate = Report_Period.Substring(0, 10);//Strings.Left(Report_Period, 10);
        //}
        //if (string.IsNullOrEmpty(FromDate))
        //{
        //    //FromDate = System.DateTime.Now.ToString("01 Jan 2010");

        //    FromDate = "01 Jan 2010";
        //}
        //if (Report_Period != "")
        //{
        //    ToDate = ToDate.Substring(ToDate.Length - 10);//Strings.Right(Report_Period, 10);
        //}
        //if (string.IsNullOrEmpty(ToDate))
        //{
        //    ToDate = System.DateTime.Now.ToString("dd MMM yyyy");
        //}




        string Report_Period = id_date_range_picker_1.Value.ToString();
        if (Report_Period != "")
        {
            FromDate = Report_Period.Substring(0, 10);//Strings.Left(Report_Period, 10);            
            DateTime result = DateTime.ParseExact(FromDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            FromDate = result.ToString("yyyy-MM-dd");
        }
        if (string.IsNullOrEmpty(FromDate))
        {

            FromDate = "2010-01-01";
        }
        if (Report_Period != "")
        {
            ToDate = Report_Period.Substring(Report_Period.Length - 10);//Strings.Right(Report_Period, 10);            
            DateTime result = DateTime.ParseExact(ToDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            ToDate = result.ToString("yyyy-MM-dd");

        }
        if (string.IsNullOrEmpty(ToDate))
        {
            ToDate = System.DateTime.Now.ToString("yyyy-MM-dd");
        }



        for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
        {
            if (ddlBatch.Items[BatchCnt].Selected == true)
            {
                BatchSelCnt = BatchSelCnt + 1;
            }
        }

        if (BatchSelCnt == 0)
        {
            //When all is selected
            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
            }
            //if (Strings.Right(BatchCode, 1) == ",")
            //    BatchCode = Strings.Left(BatchCode, Strings.Len(BatchCode) - 1);
            BatchCode = Common.RemoveComma(BatchCode);
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

            BatchCode = Common.RemoveComma(BatchCode);
        }

        for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
        {
            if (ddlTestType.Items[TypeCnt].Selected == true)
            {
                TypeSelCnt = TypeSelCnt + 1;
            }
        }

        if (TypeSelCnt == 0)
        {
            //When all is selected
            for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
            {
                TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
                TestType_Name = TestType_Name + ddlTestType.Items[TypeCnt].ToString() + ",";
            }
            TestType_ID = Common.RemoveComma(TestType_ID);
            TestType_Name = Common.RemoveComma(TestType_Name);
            //if (Strings.Right(TestType_ID, 1) == ",")
            //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);
            //if (Strings.Right(TestType_Name, 1) == ",")
            //    TestType_Name = Strings.Left(TestType_Name, Strings.Len(TestType_Name) - 1);
        }
        else
        {
            for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
            {
                if (ddlTestType.Items[TypeCnt].Selected == true)
                {
                    TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
                    TestType_Name = TestType_Name + ddlTestType.Items[TypeCnt].ToString() + ",";
                }
            }
            TestType_ID = Common.RemoveComma(TestType_ID);
            TestType_Name = Common.RemoveComma(TestType_Name);
            //if (Strings.Right(TestType_ID, 1) == ",")
            //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);
            //if (Strings.Right(TestType_Name, 1) == ",")
            //    TestType_Name = Strings.Left(TestType_Name, Strings.Len(TestType_Name) - 1);
        }


        for (TestCnt = 0; TestCnt <= ddlTestName.Items.Count - 1; TestCnt++)
        {
            if (ddlTestName.Items[TestCnt].Selected == true)
            {
                TestSelCnt = TestSelCnt + 1;
            }
        }

        if (TestSelCnt == 0)
        {
            //When all is selected
            for (TypeCnt = 0; TypeCnt <= ddlTestName.Items.Count - 1; TypeCnt++)
            {
                Test_ID = Test_ID + ddlTestName.Items[TypeCnt].Value + ",";
                Test_Name = Test_Name + ddlTestName.Items[TypeCnt].ToString() + ",";
            }
            Test_ID = Common.RemoveComma(Test_ID);
            Test_Name = Common.RemoveComma(Test_Name);
            //if (Strings.Right(Test_ID, 1) == ",")
            //    Test_ID = Strings.Left(Test_ID, Strings.Len(Test_ID) - 1);
            //if (Strings.Right(Test_Name, 1) == ",")
            //    Test_Name = Strings.Left(Test_Name, Strings.Len(Test_Name) - 1);
        }
        else
        {
            for (TypeCnt = 0; TypeCnt <= ddlTestName.Items.Count - 1; TypeCnt++)
            {
                if (ddlTestName.Items[TypeCnt].Selected == true)
                {
                    Test_ID = Test_ID + ddlTestName.Items[TypeCnt].Value + ",";
                    Test_Name = Test_Name + ddlTestName.Items[TypeCnt].ToString() + ",";
                }
            }
            Test_ID = Common.RemoveComma(Test_ID);
            Test_Name = Common.RemoveComma(Test_Name);
            //if (Strings.Right(Test_ID, 1) == ",")
            //    Test_ID = Strings.Left(Test_ID, Strings.Len(Test_ID) - 1);
            //if (Strings.Right(Test_Name, 1) == ",")
            //    Test_Name = Strings.Left(Test_Name, Strings.Len(Test_Name) - 1);
        }

        ControlVisibility("Result");

        string SBEntryCode = null;
        SBEntryCode = ddlStudentName.SelectedValue.ToString();

        lblTestID_Result.Text = Test_ID;

        //For MCQ Type test
        if (ddlTestCategory.SelectedValue == "002")
        {
            DataSet dsGrid = ProductController.Report_Test_MCQ_Test_Subject_Student_Rank(Test_ID, SBEntryCode, 1, FromDate, ToDate);
            dlGridSummaryReport.DataSource = dsGrid.Tables[0];
            dlGridSummaryReport.DataBind();

            dlGridDetailsofAnswering.DataSource = dsGrid.Tables[1];
            dlGridDetailsofAnswering.DataBind();
            dlGridDetailsofAnswering.Visible = true;

            dlGridOverallToppers.DataSource = dsGrid.Tables[2];
            dlGridOverallToppers.DataBind();

            lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count - 1);

            DivResult_MCQ.Visible = true;
        }
        else
        {
            DataSet dsGrid = ProductController.Report_Test_NonMCQ_Test_Subject_Student_Rank(Test_ID, SBEntryCode, 1, FromDate, ToDate);
            dlGridSummaryReport.DataSource = dsGrid.Tables[0];
            dlGridSummaryReport.DataBind();

            dlGridDetailsofAnswering.DataSource = null;
            dlGridDetailsofAnswering.DataBind();
            dlGridDetailsofAnswering.Visible = false;

            dlGridOverallToppers.DataSource = dsGrid.Tables[1];
            dlGridOverallToppers.DataBind();

            lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count - 1);

            DivResult_MCQ.Visible = true;
        }

        lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
        lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
        lblStandard_Result.Text = ddlStandard.SelectedItem.ToString();
        lblTestCategory_Result.Text = ddlTestCategory.SelectedItem.ToString();
        lblCentre_Result.Text = ddlCentre.SelectedItem.ToString();
        lblStudentName_Result.Text = ddlStudentName.SelectedItem.ToString();
        lblRollNo_Result.Text = txtRollNo.Text;
        lblTestType_Result.Text = TestType_Name;
        lblTestName_Result.Text = Test_Name;
        lblTestPeriod.Text = id_date_range_picker_1.Value.ToString();

    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();
            FillDDL_TestCategories();
            FillDDL_TestTypes();
            //FillAttendanceType()
            //FillEntityType()
        }
    }

    private void FillDDL_TestCategories()
    {
        DataSet dsTestCategory = ProductController.GetAllActiveTestCategory();
        BindDDL(ddlTestCategory, dsTestCategory, "TestCategory_Name", "TestCategory_Id");
        ddlTestCategory.Items.Insert(0, "Select");
        ddlTestCategory.SelectedIndex = 0;

    }

    private void FillDDL_TestTypes()
    {
        DataSet dsTestType = ProductController.GetAllActiveTestType();
        BindListBox(ddlTestType, dsTestType, "TestType_Name", "TestType_Id");

    }

    private void FillDDL_TestName()
    {
        ddlTestName.Items.Clear();

        //Validate if all information is entered correctly
        if (ddlDivision.SelectedIndex == 0)
        {
            //Show_Error_Success_Box("E", "0001")
            //ddlDivision.Focus()
            return;
        }

        if (ddlAcadYear.SelectedIndex == 0)
        {
            //Show_Error_Success_Box("E", "0002")
            //ddlAcadYear.Focus()
            return;
        }

        if (ddlCentre.SelectedIndex == 0)
        {
            //Show_Error_Success_Box("E", "0006")
            //ddlCentre.Focus()
            return;
        }

        if (ddlStandard.SelectedIndex == 0)
        {
            //Show_Error_Success_Box("E", "0003")
            //ddlStandard.Focus()
            return;
        }

        if (ddlTestCategory.SelectedIndex == 0)
        {
            //Show_Error_Success_Box("E", "0012")
            //ddlTestCategory.Focus()
            return;
        }

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
            //When all is selected
            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
            }

            BatchCode = Common.RemoveComma(BatchCode);
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
            BatchCode = Common.RemoveComma(BatchCode);

        }

        string TestType_ID = "";
        int TypeCnt = 0;
        int TypeSelCnt = 0;
        for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
        {
            if (ddlTestType.Items[TypeCnt].Selected == true)
            {
                TypeSelCnt = TypeSelCnt + 1;
            }
        }

        if (TypeSelCnt == 0)
        {
            //When all is selected
            for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
            {
                TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
            }
            //if (Strings.Right(TestType_ID, 1) == ",")
            //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);
            TestType_ID = Common.RemoveComma(TestType_ID);
        }
        else
        {
            for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
            {
                if (ddlTestType.Items[TypeCnt].Selected == true)
                {
                    TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
                }
            }
            TestType_ID = Common.RemoveComma(TestType_ID);

        }

        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        string TestName = null;
        TestName = "%";

        //string DateRange = null;
        //DateRange = id_date_range_picker_1.Value;
        //string FromDate = null;
        //string ToDate = null;
        //if (DateRange != "")
        //{
        //    FromDate = DateRange.Substring(0, DateRange.Length);
        //}
        ////FromDate = Strings.Left(DateRange, 10);
        //if (string.IsNullOrEmpty(FromDate))
        //    FromDate = System.DateTime.Now.ToString("dd MMM yyyy");
        //if (DateRange != "")
        //{
        //    ToDate = DateRange.Substring(DateRange.Length - 10);
        //}
        ////ToDate = Strings.Right(DateRange, 10);
        //if (string.IsNullOrEmpty(ToDate))
        //    ToDate = System.DateTime.Now.ToString("dd MMM yyyy");


        string DateRange = "";
        DateRange = id_date_range_picker_1.Value;

        string FromDate, ToDate;
        if (id_date_range_picker_1.Value == "")
        {
            FromDate = "";
            ToDate = "";
        }
        else
        {
            FromDate = DateRange.Substring(0, 10);
            ToDate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;
        }

        string Centre_Code = null;
        Centre_Code = ddlCentre.SelectedItem.Value;

        DataSet dsTestName = ProductController.GetTestScheduleBy_Division_Year_Standard_Centre(DivisionCode, YearName, StandardCode, BatchCode, "01,02", ddlTestCategory.SelectedValue, TestType_ID, TestName, FromDate, ToDate,
        1, 0, Centre_Code, 2);

        BindListBox(ddlTestName, dsTestName, "Test_Name", "PKey");
        if (dsTestName.Tables[0].Rows.Count > 0)
        {
            ddlTestName.Items.Insert(0, "All");
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
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
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

    //Protected Sub BtnCloseAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCloseAdd.Click
    //    ControlVisibility("Result")
    //    Clear_AddPanel()
    //End Sub

    //Private Sub Clear_AddPanel()
    //    lblTestPKey_Edit.Text = ""
    //    dlGridDisplay_StudAttendance.DataSource = Nothing
    //    dlGridDisplay_StudAttendance.DataBind()

    //    lblSummary_BatchStrength.Text = ""
    //    lblSummary_ExemptCount.Text = ""
    //    lblSummary_PresentCount.Text = ""
    //    lblSummary_PresentPercent.Text = ""
    //    lblSummary_AbsentCount.Text = ""
    //    lblSummary_AbsentPercent.Text = ""
    //    lblSummary_NMCount.Text = ""

    //    dlGridStudent.DataSource = Nothing
    //    dlGridStudent.DataBind()
    //End Sub

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
        FillDDL_Search_Centre();
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

        BindDDL(ddlCentre, dsCentre, "Center_Name", "Center_Code");
        ddlCentre.Items.Insert(0, "Select");
        ddlCentre.SelectedIndex = 0;
    }

    private void FillDDL_Standard()
    {
        ddlStudentName.Items.Clear();
        ddlStandard.Items.Clear();
        ddlBatch.Items.Clear();
        txtRollNo.Text = "";

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
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
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



    protected void ddlStandard_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Batch();
        FillDDL_TestName();
        Clear_Error_Success_Box();
    }

    private void FillDDL_Batch()
    {
        ddlStudentName.Items.Clear();
        ddlBatch.Items.Clear();
        txtRollNo.Text = "";

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


    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
        //Clear_AddPanel()
    }

    //Private Sub FillAttendanceType()
    //    Dim dsAttendanceType As DataSet = ProductController.GetAllTestAttendanceActionType()
    //    BindDDL(ddlAttendanceType, dsAttendanceType, "Action_Name", "Action_Id")
    //    ddlAttendanceType.Items.Insert(0, "[ All ]")
    //    ddlAttendanceType.SelectedIndex = 0
    //End Sub

    //Private Sub FillEntityType()
    //    Dim Action_Id As String
    //    Action_Id = ddlAttendanceType.SelectedValue

    //    Dim Flag As Integer
    //    If ddlAttendanceType.SelectedIndex = 0 Then
    //        Flag = 2
    //    Else
    //        Flag = 1
    //    End If

    //    Dim dsEntityType As DataSet = ProductController.GetAllTestAttendanceEntityType(Action_Id, Flag)
    //    BindDDL(ddlEntityType, dsEntityType, "Entity_Name", "Entity_Id")
    //    ddlEntityType.Items.Insert(0, "Select")

    //    If ddlEntityType.Items.Count = 2 Then
    //        ddlEntityType.SelectedIndex = 1
    //    Else
    //        ddlEntityType.SelectedIndex = 0
    //    End If

    //End Sub

    //Protected Sub btnSearchAttendance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchAttendance.Click
    //    Clear_Error_Success_Box()

    //    'Validation
    //    If ddlEntityType.SelectedIndex = 0 Then
    //        Show_Error_Success_Box("E", "0021")
    //        ddlEntityType.Focus()
    //        Exit Sub
    //    End If

    //    DivResultPanelLevel2.Visible = True

    //    Dim TestPKey As String
    //    TestPKey = lblTestPKey_Edit.Text

    //    Dim ActionFlag As Integer

    //    If ddlEntityType.SelectedValue = "001" Then

    //        If ddlAttendanceType.SelectedValue = "001" Then
    //            ActionFlag = 1
    //        ElseIf ddlAttendanceType.SelectedValue = "002" Then
    //            ActionFlag = 2
    //        ElseIf ddlAttendanceType.SelectedValue = "003" Then
    //            ActionFlag = 3
    //        Else
    //            ActionFlag = 0
    //        End If

    //        Dim dsStudent As DataSet = ProductController.GetStudent_ForTest_ByTestPKey(TestPKey, ActionFlag)
    //        dlGridDisplay_StudAttendance.DataSource = dsStudent
    //        dlGridDisplay_StudAttendance.DataBind()

    //        If dsStudent.Tables(1).Rows.Count > 0 Then
    //            Dim ActualBatchStrength As Long
    //            ActualBatchStrength = dsStudent.Tables(1).Rows(0)("BatchStrength") - dsStudent.Tables(1).Rows(0)("ExemptCount")

    //            Dim PresentPercent As Single
    //            If ActualBatchStrength <> 0 Then
    //                PresentPercent = Math.Round(100 * dsStudent.Tables(1).Rows(0)("PresentCount") / ActualBatchStrength, 1)
    //            Else
    //                PresentPercent = 0
    //            End If

    //            Dim AbsentPercent As Single
    //            If ActualBatchStrength <> 0 Then
    //                AbsentPercent = Math.Round(100 * dsStudent.Tables(1).Rows(0)("AbsentCount") / ActualBatchStrength, 1)
    //            Else
    //                AbsentPercent = 0
    //            End If

    //            lblSummary_BatchStrength.Text = dsStudent.Tables(1).Rows(0)("BatchStrength")
    //            lblSummary_ExemptCount.Text = dsStudent.Tables(1).Rows(0)("ExemptCount")
    //            lblSummary_PresentCount.Text = dsStudent.Tables(1).Rows(0)("PresentCount")
    //            lblSummary_PresentPercent.Text = "[ " + PresentPercent.ToString + " %]"
    //            lblSummary_AbsentCount.Text = dsStudent.Tables(1).Rows(0)("AbsentCount")
    //            lblSummary_AbsentPercent.Text = "[ " + AbsentPercent.ToString + " %]"
    //            lblSummary_NMCount.Text = dsStudent.Tables(1).Rows(0)("NotMarkedCount")
    //        End If

    //        dlGridDisplay_StudAttendance.Visible = True
    //    Else
    //        dlGridDisplay_StudAttendance.Visible = False
    //    End If

    //    UpdatePanel_StudAttendance_Result.Update()
    //End Sub

    //Protected Sub ddlAttendanceType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAttendanceType.SelectedIndexChanged
    //    If ddlAttendanceType.SelectedIndex = 0 Or ddlEntityType.SelectedIndex = 0 Then
    //        btnAddAttendance.Visible = False
    //    Else
    //        btnAddAttendance.Visible = True
    //    End If
    //    FillEntityType()
    //End Sub

    //Protected Sub ddlEntityType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEntityType.SelectedIndexChanged
    //    If ddlAttendanceType.SelectedIndex = 0 Or ddlEntityType.SelectedIndex = 0 Then
    //        btnAddAttendance.Visible = False
    //    Else
    //        btnAddAttendance.Visible = True
    //    End If
    //End Sub

    //Protected Sub btnAddAttendance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddAttendance.Click
    //    FillGridStudent()
    //    ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Pop", "openModalStudAttend();", True)
    //End Sub

    //Private Sub FillGridStudent()
    //    Dim TestPKey As String
    //    TestPKey = lblTestPKey_Edit.Text

    //    Dim ActionFlag As Integer

    //    If ddlAttendanceType.SelectedValue = "001" Then
    //        lblStudAttend_Header.Text = "Mark Student Exemption"
    //        ActionFlag = 1
    //        lblStudAttend_Action.Text = "E"
    //    ElseIf ddlAttendanceType.SelectedValue = "002" Then
    //        lblStudAttend_Header.Text = "Mark Student Absent"
    //        ActionFlag = 2
    //        lblStudAttend_Action.Text = "A"
    //    ElseIf ddlAttendanceType.SelectedValue = "003" Then
    //        lblStudAttend_Header.Text = "Mark Student Present"
    //        ActionFlag = 3
    //        lblStudAttend_Action.Text = "P"
    //    End If

    //    Dim dsStudent As DataSet = ProductController.GetStudent_ForTest_ByTestPKey(TestPKey, ActionFlag)
    //    dlGridStudent.DataSource = dsStudent
    //    dlGridStudent.DataBind()
    //    UpdatePanel_StudAttendance.Update()

    //End Sub

    //Protected Sub btnStudAttend_Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStudAttend_Save.Click
    //    Dim TestPKey As String
    //    TestPKey = lblTestPKey_Edit.Text

    //    Dim StudCnt As Integer
    //    Dim SBEntryCode As String = ""
    //    Dim NotSel_SBEntryCode As String = ""

    //    For Each dtlItem As DataListItem In dlGridStudent.Items
    //        Dim chkStudent As CheckBox = DirectCast(dtlItem.FindControl("chkStudent"), CheckBox)
    //        Dim lblSBEntryCode As Label = DirectCast(dtlItem.FindControl("lblSBEntryCode"), Label)
    //        If chkStudent.Checked = True Then
    //            StudCnt = StudCnt + 1
    //            SBEntryCode = SBEntryCode & lblSBEntryCode.Text & ","
    //        Else
    //            NotSel_SBEntryCode = NotSel_SBEntryCode & lblSBEntryCode.Text & ","
    //        End If
    //    Next
    //    If Right(SBEntryCode, 1) = "," Then SBEntryCode = Left(SBEntryCode, Len(SBEntryCode) - 1)
    //    If Right(NotSel_SBEntryCode, 1) = "," Then NotSel_SBEntryCode = Left(NotSel_SBEntryCode, Len(NotSel_SBEntryCode) - 1)

    //    Dim lblHeader_User_Code As Label
    //    lblHeader_User_Code = CType(Master.FindControl("lblHeader_User_Code"), Label)

    //    Dim CreatedBy As String
    //    CreatedBy = lblHeader_User_Code.Text

    //    Dim ActionFlag As String
    //    ActionFlag = lblStudAttend_Action.Text

    //    Dim ResultId As Integer
    //    'Mark exemption/absent/present for those students who are selected
    //    ResultId = ProductController.Insert_StudentTestAttendace(TestPKey, ActionFlag, SBEntryCode, CreatedBy)

    //    'Mark NA for those students who are not selected
    //    ResultId = ProductController.Insert_StudentTestAttendace(TestPKey, "N", NotSel_SBEntryCode, CreatedBy)

    //    'Close the Add Panel and go to Search Grid
    //    If ResultId = 1 Then
    //        btnSearchAttendance_Click(sender, e)
    //        btnStudAttend_Close_Click(sender, e)
    //    End If
    //End Sub

    //Protected Sub btnStudAttend_Close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStudAttend_Close.Click
    //    'Close the modal box
    //End Sub

    protected void ddlTestType_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_TestName();
        Clear_Error_Success_Box();
    }

    protected void ddlTestCategory_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_TestName();
        Clear_Error_Success_Box();
    }

    protected void ddlBatch_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_TestName();
        ddlStudentName.Items.Clear();
        txtRollNo.Text = "";
        Clear_Error_Success_Box();
    }

    protected void ddlCentre_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Batch();
        FillDDL_TestName();
        Clear_Error_Success_Box();
    }

    protected void btnStudentName_Click(object sender, System.EventArgs e)
    {
        ddlStudentName.Items.Clear();
        ddlStudentRollNo.Items.Clear();
        txtRollNo.Text = "";

        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string Centre_Code = null;
        Centre_Code = ddlCentre.SelectedValue;

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

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        if (BatchSelCnt == 0)
        {
            //When all is selected
            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
            }
            BatchCode = Common.RemoveComma(BatchCode);
            //if (Strings.Right(BatchCode, 1) == ",")
            //    BatchCode = Strings.Left(BatchCode, Strings.Len(BatchCode) - 1);
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

            BatchCode = Common.RemoveComma(BatchCode);
        }


        string TestType_ID = "";
        string TestType_Name = "";
        int TypeCnt = 0;
        int TypeSelCnt = 0;




        string Test_ID = "";
        string Test_Name = "";
        int TestCnt = 0;
        int TestSelCnt = 0;
        for (TestCnt = 0; TestCnt <= ddlTestName.Items.Count - 1; TestCnt++)
        {
            if (ddlTestName.Items[TestCnt].Selected == true)
            {
                TestSelCnt = TestSelCnt + 1;
            }
        }

        if (TestSelCnt == 0)
        {
            //When all is selected
            Show_Error_Success_Box("E", "Select At Least One Test");
            ddlStudentName.Focus();
            return;

        }
        else
        {
            for (TypeCnt = 0; TypeCnt <= ddlTestName.Items.Count - 1; TypeCnt++)
            {
                if (ddlTestName.Items[TypeCnt].Selected == true)
                {
                    Test_ID = Test_ID + ddlTestName.Items[TypeCnt].Value + ",";
                    Test_Name = Test_Name + ddlTestName.Items[TypeCnt].ToString() + ",";
                }
            }
            Test_ID = Common.RemoveComma(Test_ID);
            Test_Name = Common.RemoveComma(Test_Name);
            //if (Strings.Right(Test_ID, 1) == ",")
            //    Test_ID = Strings.Left(Test_ID, Strings.Len(Test_ID) - 1);
            //if (Strings.Right(Test_Name, 1) == ",")
            //    Test_Name = Strings.Left(Test_Name, Strings.Len(Test_Name) - 1);
        }


        if (ddlTestName.SelectedItem.Text == "All")
        {
            for (TypeCnt = 0; TypeCnt <= ddlTestName.Items.Count - 1; TypeCnt++)
            {

                Test_ID = Test_ID + ddlTestName.Items[TypeCnt].Value + ",";
                Test_Name = Test_Name + ddlTestName.Items[TypeCnt].ToString() + ",";

            }

        }


        DataSet dsStudent = ProductController.GetStudent_ForTest_ByDivision_Centre_Standard_TestWise(Div_Code, YearName, Centre_Code, StandardCode, BatchCode, "CDB", 1, Test_ID);
        BindDDL(ddlStudentName, dsStudent, "StudentName", "SBEntryCode");
        ddlStudentName.Items.Insert(0, "Select");
        ddlStudentName.SelectedIndex = 0;

        BindDDL(ddlStudentRollNo, dsStudent, "SBEntryCode", "RollNo");
        ddlStudentRollNo.Items.Insert(0, "Select");
        ddlStudentRollNo.SelectedIndex = 0;

        BindDDL(ddlStudentEMailId, dsStudent, "SBEntryCode", "ParentsEMailId");
        ddlStudentEMailId.Items.Insert(0, "Select");
        ddlStudentEMailId.SelectedIndex = 0;



        Clear_Error_Success_Box();
    }

    protected void ddlStudentName_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        txtRollNo.Text = ddlStudentRollNo.Items[ddlStudentName.SelectedIndex].Value;
        Clear_Error_Success_Box();
    }

    protected void btnPrint_Click(object sender, System.EventArgs e)
    {
        FillGridStudent();
        btnStudSelect_Mail.Visible = false;
        btnStudSelect_Print.Visible = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalStudSelection();", true);
    }

    private void FillGridStudent()
    {
        dlGridStudSelect.DataSource = null;
        dlGridStudSelect.DataBind();

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] {
			new DataColumn("RollNo", typeof(string)),
			new DataColumn("StudentName", typeof(string)),
			new DataColumn("StudentSelFlag", typeof(int)),
			new DataColumn("SBEntryCode", typeof(string)),
			new DataColumn("ParentEMailId", typeof(string))
		});

        int Cnt = 0;
        int StudentSelFlag = 0;
        for (Cnt = 1; Cnt <= ddlStudentName.Items.Count - 1; Cnt++)
        {
            if (ddlStudentName.Items[Cnt].Value == ddlStudentName.SelectedValue.ToString())
            {
                StudentSelFlag = 1;
            }
            else
            {
                StudentSelFlag = 0;
            }
            dt.Rows.Add(ddlStudentRollNo.Items[Cnt].Value, ddlStudentName.Items[Cnt].Text, 0, ddlStudentName.Items[Cnt].Value, ddlStudentEMailId.Items[Cnt].Value);
        }

        dlGridStudSelect.DataSource = dt;
        dlGridStudSelect.DataBind();

    }

    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    //Required to verify that the control is rendered properly on page
    //}

    private void PrintStudentResult(object sender, System.EventArgs e)
    {

        string list = ProductController.GetDivisiononlyScience();
        string[] strlist = list.Split(',');


        bool flag = false;

        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");



        //DataSet dsDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, "", "", "2", lblHeader_DBName.Text);
        //foreach (DataRow dr in dsDivision.Tables[0].Rows)
        //{
        //    if (strlist != null)
        //    {
        //        foreach (string item in strlist)
        //        {
        //            if (item.ToString() == dr["Division_Code"].ToString())
        //            {
        //                flag = true;
        //            }
        //        }
        //    }
        //}

        string Division_Code = ddlDivision.SelectedValue;

        if (Division_Code == "E0" || Division_Code == "T0")
        {
            PrintDataforsci(sender, e);
        }
        else
        {
            //use old function only
            PrintDataforstateboard_Old(sender, e);

        }

    }

    protected void btnStudSelect_Print_Click(object sender, System.EventArgs e)
    {
        PrintStudentResult(sender, e);
    }

    protected void btnStudSelect_Close_Click(object sender, System.EventArgs e)
    {
        //Do nothing
    }

    public void All_Student_ChkBox_Selected(object sender, System.EventArgs e)
    {
        //Change checked status of a hidden check box
        chkStudentAllHidden.Checked = !(chkStudentAllHidden.Checked);

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlGridStudSelect.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkStudent");

            chkitemck.Checked = chkStudentAllHidden.Checked;
        }

    }

    protected void btnEmail_Click(object sender, System.EventArgs e)
    {
        FillGridStudent();
        btnStudSelect_Mail.Visible = true;
        btnStudSelect_Print.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalStudSelection();", true);
    }

    protected void btnStudSelect_Mail_Click(object sender, System.EventArgs e)
    {
        try
        {
            string list = ProductController.GetDivisiononlyScience();
            string[] strlist = list.Split(',');


            bool flag = false;

            Label lblHeader_Company_Code = default(Label);
            lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            Label lblHeader_DBName = default(Label);
            lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");



            DataSet dsDivision = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, "", "", "2", lblHeader_DBName.Text);
            foreach (DataRow dr in dsDivision.Tables[0].Rows)
            {
                if (strlist != null)
                {
                    foreach (string item in strlist)
                    {
                        if (item.ToString() == dr["Division_Code"].ToString())
                        {
                            flag = true;
                        }
                    }
                }
            }

            if (flag)
            {

                MailStudentResult(sender, e);
            }
            else
            {
                MailStateBoardStudentResult(sender, e);
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    private void MailStudentResult(object sender, System.EventArgs e)
    {
        //Dim Path As String = "Report_Marksheet_Print.aspx?Test_Id=" & Replace(Test_Id, "%", "%25") & "&SBEntryCode=" & SBEntryCode
        //ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", "open('" + Path + "');", True)
        try
        {
            string Test_Id = null;
            Test_Id = lblTestID_Result.Text;
            string FromDate = null;
            string ToDate = null;

            string Report_Period = lblTestPeriod.Text.ToString();
            if (Report_Period != "")
            {
                FromDate = Report_Period.Substring(0, 10);//Strings.Left(Report_Period, 10);
            }
            if (string.IsNullOrEmpty(FromDate))
            {
                //FromDate = System.DateTime.Now.ToString("01 Jan 2010");

                FromDate = "01 Jan 2010";
            }
            if (Report_Period != "")
            {
                ToDate = Report_Period.Substring(Report_Period.Length - 10);//Strings.Right(Report_Period, 10);
            }
            if (string.IsNullOrEmpty(ToDate))
            {
                ToDate = System.DateTime.Now.ToString("dd MMM yyyy");
            }




            // Create a Document object



            dynamic TitleFont = FontFactory.GetFont("Arial", 16, Font.BOLD);
            dynamic subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
            dynamic boldTableFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
            dynamic endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
            dynamic bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);


            // Open the Document for writing


            //For each item selected in Grid run the following things
            foreach (DataListItem dtlItem in dlGridStudSelect.Items)
            {
                dynamic document = new Document(PageSize.A4, 50, 50, 25, 25);
                // Create a new PdfWriter object, specifying the output stream
                dynamic output = new MemoryStream();
                dynamic writer = PdfWriter.GetInstance(document, output);
                document.Open();

                CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
                Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
                Label lblStudentRollNo = (Label)dtlItem.FindControl("lblStudentRollNo");
                Label lblStudentName = (Label)dtlItem.FindControl("lblStudentName");
                Label lblStudentEmail = (Label)dtlItem.FindControl("lblStudentEmail");

                if (chkStudent.Checked == true & !string.IsNullOrEmpty(lblStudentEmail.Text.Trim()))
                {
                    //For MCQ Type test
                    if (ddlTestCategory.SelectedValue == "002")
                    {
                        DataSet dsGrid = ProductController.Report_Test_MCQ_Test_Subject_Student_Rank(Test_Id, lblSBEntryCode.Text, 1, FromDate, ToDate);
                        dlPrint_Summary.DataSource = dsGrid.Tables[0];
                        dlPrint_Summary.DataBind();

                        dlPrint_Answering.DataSource = dsGrid.Tables[1];
                        dlPrint_Answering.DataBind();

                        dlPrint_Topper.DataSource = dsGrid.Tables[2];
                        dlPrint_Topper.DataBind();
                    }
                    else
                    {
                        DataSet dsGrid = ProductController.Report_Test_NonMCQ_Test_Subject_Student_Rank(Test_Id, lblSBEntryCode.Text, 1, FromDate, ToDate);
                        dlPrint_Summary.DataSource = dsGrid.Tables[0];
                        dlPrint_Summary.DataBind();

                        dlPrint_Answering.DataSource = null;
                        dlPrint_Answering.DataBind();

                        dlPrint_Topper.DataSource = dsGrid.Tables[1];
                        dlPrint_Topper.DataBind();
                    }

                    lblPrint_Center.Text = ddlCentre.SelectedItem.ToString();
                    lblPrint_StudentName.Text = lblStudentName.Text;
                    lblPrint_RollNo.Text = lblStudentRollNo.Text;

                    float YPos = 0;
                    YPos = 780;

                    dynamic logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/LEPL-LOGO.jpg"));
                    logo.SetAbsolutePosition(25, YPos);
                    logo.ScalePercent(60);
                    document.Add(logo);

                    BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                    PdfContentByte cb = writer.DirectContent;
                    cb.BeginText();
                    cb.SetTextMatrix(380, YPos + 20);
                    cb.SetFontAndSize(bf, 16);

                    cb.SetLineWidth(0.5f);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                    cb.ShowText("STATEMENT OF MARKS");
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                    YPos = YPos - 0;

                    cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                    cb.SetLineWidth(0.5f);
                    cb.MoveTo(20, YPos);
                    cb.LineTo(570, YPos);
                    cb.Stroke();

                    YPos = YPos - 15;

                    cb.SetTextMatrix(25, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Name of Student : ");

                    cb.SetTextMatrix(120, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                    cb.ShowText(lblPrint_StudentName.Text);

                    cb.SetTextMatrix(325, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                    cb.ShowText("Roll No : ");

                    cb.SetTextMatrix(375, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                    cb.ShowText(lblPrint_RollNo.Text);

                    cb.SetTextMatrix(425, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                    cb.ShowText("Centre : ");

                    cb.SetTextMatrix(475, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                    cb.ShowText(lblPrint_Center.Text);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                    float TableStartYPos = 0;
                    cb.MoveTo(20, YPos - 10);
                    cb.LineTo(570, YPos - 10);
                    cb.Stroke();
                    TableStartYPos = YPos - 10;

                    YPos = YPos - 25;

                    float Col0Left = 0;
                    float Col1Left = 0;
                    float Col2Left = 0;
                    float Col3Left = 0;
                    float Col4Left = 0;
                    float Col5Left = 0;
                    float Col6Left = 0;
                    float Col7Left = 0;
                    float Col8Left = 0;

                    Col0Left = 25;
                    Col1Left = Col0Left + 65;
                    Col2Left = Col1Left + 80;
                    Col3Left = Col2Left + 130;
                    Col4Left = Col3Left + 40;
                    Col5Left = Col4Left + 45;
                    Col6Left = Col5Left + 45;
                    Col7Left = Col6Left + 60;
                    Col8Left = 570;
                    //Col6Left + 60

                    cb.SetTextMatrix(Col0Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Test Date");

                    cb.SetTextMatrix(Col1Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Test Name");

                    cb.SetTextMatrix(Col2Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Subject");

                    cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth("Attend", false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Attend");

                    cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth("Score", false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Score");

                    cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth("Out Of", false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Out Of");

                    cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth("Centre Rank", false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Centre Rank");

                    cb.SetTextMatrix((Col7Left + ((Col8Left - Col7Left) / 2) - (cb.GetEffectiveStringWidth("Overall Rank", false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Overall Rank");


                    cb.MoveTo(20, YPos - 5);
                    cb.LineTo(570, YPos - 5);
                    cb.Stroke();

                    foreach (DataListItem dtlItem1 in dlPrint_Summary.Items)
                    {
                        Label lblDLTestDate = (Label)dtlItem1.FindControl("lblDLTestDate");
                        Label lblDLTestName = (Label)dtlItem1.FindControl("lblDLTestName");
                        Label lblDLSubject = (Label)dtlItem1.FindControl("lblDLSubject");
                        Label lblDLMarksObtd = (Label)dtlItem1.FindControl("lblDLMarksObtd");
                        Label lblDLMarksOutOf = (Label)dtlItem1.FindControl("lblDLMarksOutOf");
                        Label lblDLPercent = (Label)dtlItem1.FindControl("lblDLPercent");
                        Label lblDLCentreRank = (Label)dtlItem1.FindControl("lblDLCentreRank");
                        Label lblDLOvarllRank = (Label)dtlItem1.FindControl("lblDLOvarllRank");
                        Label lblDLAttendStatus = (Label)dtlItem1.FindControl("lblDLAttendStatus");
                        if (chkOverallRankFlag.Checked == false)
                        {
                            lblDLOvarllRank.Text = "-";
                        }

                        YPos = YPos - 20;
                        cb.SetTextMatrix(Col0Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLTestDate.Text);

                        cb.SetTextMatrix(Col1Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLTestName.Text);

                        cb.SetTextMatrix(Col2Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLSubject.Text);

                        cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth(lblDLAttendStatus.Text, false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLAttendStatus.Text);

                        cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth(lblDLMarksObtd.Text, false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLMarksObtd.Text);

                        cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth(lblDLMarksOutOf.Text, false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLMarksOutOf.Text);

                        cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth(lblDLCentreRank.Text, false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLCentreRank.Text);

                        cb.SetTextMatrix((Col7Left + ((Col8Left - Col7Left) / 2) - (cb.GetEffectiveStringWidth(lblDLOvarllRank.Text, false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLOvarllRank.Text);

                        cb.MoveTo(20, YPos - 5);
                        cb.LineTo(570, YPos - 5);
                        cb.Stroke();
                    }

                    cb.MoveTo(20, TableStartYPos);
                    cb.LineTo(20, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col1Left - 5, TableStartYPos);
                    cb.LineTo(Col1Left - 5, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col2Left - 5, TableStartYPos);
                    cb.LineTo(Col2Left - 5, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col3Left, TableStartYPos);
                    cb.LineTo(Col3Left, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col4Left, TableStartYPos);
                    cb.LineTo(Col4Left, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col5Left, TableStartYPos);
                    cb.LineTo(Col5Left, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col6Left, TableStartYPos);
                    cb.LineTo(Col6Left, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col7Left, TableStartYPos);
                    cb.LineTo(Col7Left, YPos - 5);
                    cb.Stroke();
                    cb.MoveTo(Col8Left, TableStartYPos);
                    cb.LineTo(Col8Left, YPos - 5);
                    cb.Stroke();


                    YPos = YPos - 25;
                    document.NewPage();

                    //For MCQ Type test
                    if (ddlTestCategory.SelectedValue == "002")
                    {
                        cb.SetTextMatrix(25, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                        cb.ShowText("Details of Answering");
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                        cb.MoveTo(20, YPos - 10);
                        cb.LineTo(570, YPos - 10);
                        cb.Stroke();
                        TableStartYPos = YPos - 10;

                        YPos = YPos - 25;

                        Col0Left = 25;
                        Col1Left = Col0Left + 65;
                        Col2Left = Col1Left + 70;
                        Col3Left = Col2Left + 65;
                        Col4Left = Col3Left + 40;
                        Col5Left = Col4Left + 100;
                        Col6Left = Col5Left + 100;
                        Col7Left = 570;
                        //Col6Left + 60

                        cb.SetTextMatrix(Col0Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Test Name");

                        cb.SetTextMatrix(Col1Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Subject");

                        cb.SetTextMatrix(Col2Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Status");

                        cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth("Count", false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Count");

                        cb.SetTextMatrix(Col4Left + 5, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Que No");

                        //cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth("Que No - Moderate", False) / 2)), YPos)
                        //cb.SetFontAndSize(bf, 10)
                        //cb.ShowText("Que No - Moderate")

                        //cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth("Que No - Difficult", False) / 2)), YPos)
                        //cb.SetFontAndSize(bf, 10)
                        //cb.ShowText("Que No - Difficult")

                        cb.MoveTo(20, YPos - 5);
                        cb.LineTo(570, YPos - 5);
                        cb.Stroke();

                        foreach (DataListItem dtlItem1 in dlPrint_Answering.Items)
                        {
                            Label lblDLTestName = (Label)dtlItem1.FindControl("lblDLTestName");
                            Label lblDLSubjectName = (Label)dtlItem1.FindControl("lblDLSubjectName");
                            Label lblDLResultStatus = (Label)dtlItem1.FindControl("lblDLResultStatus");
                            Label lblDLResultCount = (Label)dtlItem1.FindControl("lblDLResultCount");
                            Label lblDLEasy = (Label)dtlItem1.FindControl("lblDLEasy");
                            Label lblDLModerate = (Label)dtlItem1.FindControl("lblDLModerate");
                            Label lblDLDifficult = (Label)dtlItem1.FindControl("lblDLDifficult");

                            YPos = YPos - 20;
                            cb.SetTextMatrix(Col0Left, YPos);
                            cb.SetFontAndSize(bf, 10);
                            cb.ShowText(lblDLTestName.Text);

                            cb.SetTextMatrix(Col1Left, YPos);
                            cb.SetFontAndSize(bf, 10);
                            //cb.ShowText(Strings.Left(lblDLSubjectName.Text, 10));
                            //cb.ShowText(lblDLSubjectName.Text.Substring(0,10));
                            cb.ShowText(lblDLSubjectName.Text);


                            cb.SetTextMatrix(Col2Left, YPos);
                            cb.SetFontAndSize(bf, 10);
                            cb.ShowText(lblDLResultStatus.Text);

                            cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth(lblDLResultCount.Text, false) / 2)), YPos);
                            cb.SetFontAndSize(bf, 10);
                            cb.ShowText(lblDLResultCount.Text);

                            //cb.SetTextMatrix(Col4Left + 5, YPos)
                            //cb.SetFontAndSize(bf, 10)
                            //cb.ShowText(lblDLEasy.Text)

                            float Yp1PDF = 0;
                            float ActPos = 0;
                            string TotalMatter = null;
                            string DummyMatter = null;
                            string PrintMatter = null;
                            dynamic SplitMatter = null;

                            Yp1PDF = YPos;
                            ActPos = YPos;
                            TotalMatter = lblDLEasy.Text + lblDLModerate.Text + lblDLDifficult.Text;

                            int Cnt = 0;
                            Cnt = 0;

                            //TRIPTY Start


                            if (!string.IsNullOrEmpty((TotalMatter.Trim())))
                            {
                                Yp1PDF = Yp1PDF + 20;

                                DummyMatter = TotalMatter;
                                SplitMatter = TotalMatter.Split(Environment.NewLine.ToCharArray()); //Strings.Split(TotalMatter, Constants.vbCrLf);JAyant
                                for (int EntCnt = 0; EntCnt <= Cnt; EntCnt++)
                                {
                                    TotalMatter = SplitMatter[EntCnt];
                                Again1PDF:
                                    Yp1PDF = Yp1PDF - 20;
                                    PrintMatter = "";
                                    for (int ChrCnt = 1; ChrCnt <= (TotalMatter.Length); ChrCnt++)
                                    {
                                        if (string.IsNullOrEmpty((TotalMatter.Trim())))
                                        {
                                            PrintMatter = "";
                                        }
                                        else
                                        {
                                            PrintMatter = TotalMatter.Substring(0, ChrCnt); //PrintMatter = Strings.Left(TotalMatter, ChrCnt);
                                        }
                                        if (cb.GetEffectiveStringWidth(PrintMatter, true) >= (Col7Left - Col4Left - 10))
                                        {
                                            //Search for last blank space
                                            //ChrCnt = PrintMatter.LastIndexOf(',');//Strings.InStrRev(PrintMatter, ",");
                                            //PrintMatter = TotalMatter.Substring(0, ChrCnt);//PrintMatter = Strings.Left(TotalMatter, ChrCnt);

                                            //cb.SetTextMatrix(Col4Left + 5, Yp1PDF);
                                            //cb.SetFontAndSize(bf, 10);
                                            //cb.ShowText(PrintMatter);

                                            //TotalMatter = TotalMatter.Substring(TotalMatter.Length - (TotalMatter.Length) - ChrCnt); //Strings.Right(TotalMatter, Strings.Len(TotalMatter) - ChrCnt);

                                            ChrCnt = PrintMatter.LastIndexOf(',');//Strings.InStrRev(PrintMatter, ",");
                                            PrintMatter = TotalMatter.Substring(0, ChrCnt);

                                            cb.SetTextMatrix(Col4Left + 5, Yp1PDF);
                                            cb.SetFontAndSize(bf, 10);
                                            cb.ShowText(PrintMatter);

                                            TotalMatter = TotalMatter.Substring(PrintMatter.Length, TotalMatter.Length - ChrCnt);



                                            goto Again1PDF;
                                        }
                                    }

                                    cb.SetTextMatrix(Col4Left + 5, Yp1PDF);
                                    cb.SetFontAndSize(bf, 10);
                                    cb.ShowText(PrintMatter);
                                }
                            }





                            YPos = Yp1PDF;

                            //cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth(lblDLModerate.Text, False) / 2)), YPos)
                            //cb.SetFontAndSize(bf, 10)
                            //cb.ShowText(lblDLModerate.Text)

                            //cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth(lblDLDifficult.Text, False) / 2)), YPos)
                            //cb.SetFontAndSize(bf, 10)
                            //cb.ShowText(lblDLDifficult.Text)

                            cb.MoveTo(20, YPos - 5);
                            cb.LineTo(570, YPos - 5);
                            cb.Stroke();
                        }

                        cb.MoveTo(20, TableStartYPos);
                        cb.LineTo(20, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col1Left - 5, TableStartYPos);
                        cb.LineTo(Col1Left - 5, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col2Left - 5, TableStartYPos);
                        cb.LineTo(Col2Left - 5, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col3Left, TableStartYPos);
                        cb.LineTo(Col3Left, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col4Left, TableStartYPos);
                        cb.LineTo(Col4Left, YPos - 5);
                        cb.Stroke();

                        //cb.MoveTo(Col5Left, TableStartYPos)
                        //cb.LineTo(Col5Left, YPos - 5)
                        //cb.Stroke()

                        //cb.MoveTo(Col6Left, TableStartYPos)
                        //cb.LineTo(Col6Left, YPos - 5)
                        //cb.Stroke()

                        cb.MoveTo(Col7Left, TableStartYPos);
                        cb.LineTo(Col7Left, YPos - 5);
                        cb.Stroke();
                    }

                    if (chkOverallRankFlag.Checked == true)
                    {


                        YPos = YPos - 25;

                        cb.SetTextMatrix(25, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                        cb.ShowText("Overall Toppers");
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                        cb.MoveTo(20, YPos - 10);
                        cb.LineTo(570, YPos - 10);
                        cb.Stroke();
                        TableStartYPos = YPos - 10;

                        YPos = YPos - 25;

                        Col0Left = 25;
                        Col1Left = Col0Left + 65;
                        Col2Left = Col1Left + 70;
                        Col3Left = Col2Left + 205;
                        Col4Left = Col3Left + 150;
                        Col5Left = 570;
                        //Col6Left + 60

                        cb.SetTextMatrix(Col0Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Test Name");

                        cb.SetTextMatrix(Col1Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Subject");

                        cb.SetTextMatrix(Col2Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Name of Student");

                        cb.SetTextMatrix(Col3Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Centre");

                        cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth("Score", false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText("Score");

                        cb.MoveTo(20, YPos - 5);
                        cb.LineTo(570, YPos - 5);
                        cb.Stroke();

                        foreach (DataListItem dtlItem1 in dlPrint_Topper.Items)
                        {
                            Label lblDLTestName = (Label)dtlItem1.FindControl("lblDLTestName");
                            Label lblDLSubject = (Label)dtlItem1.FindControl("lblDLSubject");
                            Label lblDLStudentName = (Label)dtlItem1.FindControl("lblDLStudentName");
                            Label lblDLCentre = (Label)dtlItem1.FindControl("lblDLCentre");
                            Label lblDLScore = (Label)dtlItem1.FindControl("lblDLScore");

                            YPos = YPos - 20;
                            cb.SetTextMatrix(Col0Left, YPos);
                            cb.SetFontAndSize(bf, 10);
                            cb.ShowText(lblDLTestName.Text);

                            cb.SetTextMatrix(Col1Left, YPos);
                            cb.SetFontAndSize(bf, 10);
                            //cb.ShowText(Strings.Left(lblDLSubject.Text, 10));
                            //cb.ShowText(lblDLSubject.Text.Substring(0, 10));

                            cb.ShowText(lblDLSubject.Text);
                            cb.SetTextMatrix(Col2Left, YPos);
                            cb.SetFontAndSize(bf, 10);
                            cb.ShowText(lblDLStudentName.Text);

                            cb.SetTextMatrix(Col3Left, YPos);
                            cb.SetFontAndSize(bf, 10);
                            cb.ShowText(lblDLCentre.Text);

                            cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth(lblDLScore.Text, false) / 2)), YPos);
                            cb.SetFontAndSize(bf, 10);
                            cb.ShowText(lblDLScore.Text);

                            cb.MoveTo(20, YPos - 5);
                            cb.LineTo(570, YPos - 5);
                            cb.Stroke();
                        }

                        cb.MoveTo(20, TableStartYPos);
                        cb.LineTo(20, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col1Left - 5, TableStartYPos);
                        cb.LineTo(Col1Left - 5, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col2Left - 5, TableStartYPos);
                        cb.LineTo(Col2Left - 5, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col3Left - 5, TableStartYPos);
                        cb.LineTo(Col3Left - 5, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col4Left, TableStartYPos);
                        cb.LineTo(Col4Left, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col5Left, TableStartYPos);
                        cb.LineTo(Col5Left, YPos - 5);
                        cb.Stroke();

                    }
                    YPos = YPos - 25;


                    //document.Add(New Paragraph("STATEMENT OF MARKS", TitleFont))

                    //Dim orderInfoTable = New PdfPTable(2)
                    //orderInfoTable.HorizontalAlignment = 0
                    //orderInfoTable.SpacingBefore = 10
                    //orderInfoTable.SpacingAfter = 10
                    //orderInfoTable.DefaultCell.Border = 1
                    //orderInfoTable.SetWidths(New Integer() {1, 4})

                    //orderInfoTable.AddCell(New Phrase("Order:", boldTableFont))
                    //orderInfoTable.AddCell(lblPrint_RollNo.Text)
                    //orderInfoTable.AddCell(New Phrase("Price:", boldTableFont))
                    //'orderInfoTable.AddCell(Convert.ToDecimal(txtTotalPrice.Text).ToString("c"))
                    //orderInfoTable.AddCell(lblPrint_StudentName.Text)

                    //document.Add(orderInfoTable)


                    cb.EndText();


                    writer.CloseStream = false;
                    document.Close();
                    output.Position = 0;


                    //EMail code should come over here
                    //Dim bytes As Byte() = MemoryStream.ToArray()
                    //memoryStream.Close()


                    string userid = "", Password = "", Host = "", SSL = "", MailType = "";
                    int Port = 0;
                    DataSet dsCRoom = ProductController.GetMailDetails_ByCenter(ddlCentre.SelectedValue.ToString().Trim(), "Transactional");


                    if (dsCRoom.Tables[0].Rows.Count > 0)
                    {

                        userid = Convert.ToString(dsCRoom.Tables[0].Rows[0]["UserId"]);
                        Password = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Password"]);
                        Host = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Host"]);
                        Port = Convert.ToInt32(Convert.ToString(dsCRoom.Tables[0].Rows[0]["Port"]));
                        SSL = Convert.ToString(dsCRoom.Tables[0].Rows[0]["EnableSSl"]);
                        MailType = Convert.ToString(dsCRoom.Tables[0].Rows[0]["MailType"]);

                        //////

                        MailMessage Msg = new MailMessage(userid, lblStudentEmail.Text.Trim());

                        string CurTimeFrame = null;
                        CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

                        // Subject of e-mail
                        Msg.Subject = "Statement of Marks for " + lblStudentName.Text;
                        Msg.Body += "Dear Parent <br/><br/>Please find enclosed a PDF file containing Statement of Marks for your ward " + lblStudentName.Text + " for " + lblStandard_Result.Text + " standard at MT Educare.";
                        string Att_Name = "StatementOfMarks.pdf";
                        Msg.Attachments.Add(new Attachment(output, Att_Name));

                        Msg.IsBodyHtml = true;

                        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
                        string Userid = cookie.Values["UserID"];

                        bool value = System.Convert.ToBoolean(SSL);
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = Host;
                        smtp.EnableSsl = value;
                        NetworkCredential NetworkCred = new NetworkCredential(userid, Password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = Port;

                        int resultid = 0;
                        try
                        {
                            smtp.Timeout = 20000;
                            smtp.Send(Msg);

                            resultid = ProductController.Insert_Mailog(lblStudentEmail.Text.Trim(), Msg.Subject.ToString().Trim(), Msg.Body.ToString().Trim(), 1, Att_Name, "1", Userid, 1, ddlCentre.SelectedValue.ToString().Trim(), MailType);

                        }
                        catch (Exception ex)
                        {
                            resultid = ProductController.Insert_Mailog(lblStudentEmail.Text.Trim(), Msg.Subject.ToString().Trim(), Msg.Body.ToString().Trim(), 1, Att_Name, "2", Userid, 1, ddlCentre.SelectedValue.ToString().Trim(), MailType);
                        }


                        //
                    }
                    else
                    {

                    }




                    //output.Close();

                    //Response.Clear()
                    //Response.ContentType = "application/pdf"
                    //Response.AddHeader("Con   tent-Disposition", String.Format("attachment;filename=StudentMarkSheet{0}.pdf", CurTimeFrame))
                    //Response.ContentType = "application/pdf"
                    //Response.Buffer = True
                    //Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    //Response.BinaryWrite(bytes)
                    //Response.[End]()
                    //Response.Close();


                }
            }




            //Response.ContentType = "application/pdf"
            //Response.AddHeader("Content-Disposition", String.Format("attachment;filename=StudentMarkSheet{0}.pdf", CurTimeFrame))
            //Response.BinaryWrite(output.ToArray())

            Show_Error_Success_Box("S", "PDF File generated successfully.");
            BtnSearch_Click(sender, e);
            //btnStudSelect_Close_Click(sender, e);        
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    private void MailStateBoardStudentResult(object sender, System.EventArgs e)
    {
        try
        {
            //Dim Path As String = "Report_Marksheet_Print.aspx?Test_Id=" & Replace(Test_Id, "%", "%25") & "&SBEntryCode=" & SBEntryCode
            //ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", "open('" + Path + "');", True)

            string Test_Id = null;
            Test_Id = lblTestID_Result.Text;
            string FromDate = null;
            string ToDate = null;

            string Report_Period = lblTestPeriod.Text.ToString();
            if (Report_Period != "")
            {
                FromDate = Report_Period.Substring(0, 10);//Strings.Left(Report_Period, 10);
            }
            if (string.IsNullOrEmpty(FromDate))
            {
                //FromDate = System.DateTime.Now.ToString("01 Jan 2010");

                FromDate = "01 Jan 2010";
            }
            if (Report_Period != "")
            {
                ToDate = Report_Period.Substring(Report_Period.Length - 10);//Strings.Right(Report_Period, 10);
            }
            if (string.IsNullOrEmpty(ToDate))
            {
                ToDate = System.DateTime.Now.ToString("dd MMM yyyy");
            }




            // Create a Document object



            dynamic TitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
            dynamic subTitleFont = FontFactory.GetFont("Arial", 10, Font.BOLD);
            dynamic boldTableFont = FontFactory.GetFont("Arial", 8, Font.BOLD);
            dynamic endingMessageFont = FontFactory.GetFont("Arial", 7, Font.ITALIC);
            dynamic bodyFont = FontFactory.GetFont("Arial", 9, Font.NORMAL);


            // Open the Document for writing


            //For each item selected in Grid run the following things
            foreach (DataListItem dtlItem in dlGridStudSelect.Items)
            {
                dynamic document = new Document(PageSize.A4, 50, 50, 25, 25);
                // Create a new PdfWriter object, specifying the output stream
                dynamic output = new MemoryStream();
                dynamic writer = PdfWriter.GetInstance(document, output);
                document.Open();

                CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
                Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
                Label lblStudentRollNo = (Label)dtlItem.FindControl("lblStudentRollNo");
                Label lblStudentName = (Label)dtlItem.FindControl("lblStudentName");
                Label lblStudentEmail = (Label)dtlItem.FindControl("lblStudentEmail");

                if (chkStudent.Checked == true & !string.IsNullOrEmpty(lblStudentEmail.Text.Trim()))
                {
                    //For MCQ Type test
                    if (ddlTestCategory.SelectedValue == "002")
                    {
                        DataSet dsGrid = ProductController.Report_Test_MCQ_Test_Subject_Student_Rank(Test_Id, lblSBEntryCode.Text, 1, FromDate, ToDate);
                        dlPrint_Summary.DataSource = dsGrid.Tables[0];
                        dlPrint_Summary.DataBind();

                        dlPrint_Answering.DataSource = dsGrid.Tables[1];
                        dlPrint_Answering.DataBind();

                        dlPrint_Topper.DataSource = dsGrid.Tables[2];
                        dlPrint_Topper.DataBind();
                    }
                    else
                    {
                        DataSet dsGrid = ProductController.Report_Test_NonMCQ_Test_Subject_Student_Rank(Test_Id, lblSBEntryCode.Text, 1, FromDate, ToDate);
                        dlPrint_Summary.DataSource = dsGrid.Tables[0];
                        dlPrint_Summary.DataBind();

                        dlPrint_Answering.DataSource = null;
                        dlPrint_Answering.DataBind();

                        dlPrint_Topper.DataSource = dsGrid.Tables[1];
                        dlPrint_Topper.DataBind();
                    }

                    lblPrint_Center.Text = ddlCentre.SelectedItem.ToString();
                    lblPrint_StudentName.Text = lblStudentName.Text;
                    lblPrint_RollNo.Text = lblStudentRollNo.Text;

                    float YPos = 0;
                    YPos = 780;

                    dynamic logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/logo.jpg"));
                    logo.SetAbsolutePosition(25, YPos);
                    logo.ScalePercent(60);
                    document.Add(logo);

                    BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                    PdfContentByte cb = writer.DirectContent;
                    cb.BeginText();
                    cb.SetTextMatrix(380, YPos + 20);
                    cb.SetFontAndSize(bf, 14);

                    cb.SetLineWidth(0.5f);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                    cb.ShowText("STATEMENT OF MARKS");
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                    YPos = YPos - 0;

                    cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                    cb.SetLineWidth(0.5f);
                    cb.MoveTo(20, YPos);
                    cb.LineTo(570, YPos);
                    cb.Stroke();

                    YPos = YPos - 15;

                    cb.SetTextMatrix(25, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Name of Student : ");

                    cb.SetTextMatrix(120, YPos);
                    cb.SetFontAndSize(bf, 9);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                    cb.ShowText(lblPrint_StudentName.Text);

                    cb.SetTextMatrix(325, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                    cb.ShowText("Roll No : ");

                    cb.SetTextMatrix(375, YPos);
                    cb.SetFontAndSize(bf, 9);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                    cb.ShowText(lblPrint_RollNo.Text);

                    cb.SetTextMatrix(425, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                    cb.ShowText("Centre : ");

                    cb.SetTextMatrix(475, YPos);
                    cb.SetFontAndSize(bf, 9);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                    cb.ShowText(lblPrint_Center.Text);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                    float TableStartYPos = 0;
                    cb.MoveTo(20, YPos - 10);
                    cb.LineTo(570, YPos - 10);
                    cb.Stroke();
                    TableStartYPos = YPos - 10;

                    YPos = YPos - 25;

                    float Col0Left = 0;
                    float Col1Left = 0;
                    float Col2Left = 0;
                    float Col3Left = 0;
                    float Col4Left = 0;
                    float Col5Left = 0;
                    float Col6Left = 0;
                    float Col7Left = 0;
                    float Col8Left = 0;
                    float Col9Left = 0;
                    float Col10Left = 0;

                    Col0Left = 25;
                    Col1Left = Col0Left + 65;
                    Col2Left = Col1Left + 130;
                    Col3Left = Col2Left + 90;
                    Col4Left = Col3Left + 33;
                    Col5Left = Col4Left + 35;
                    Col6Left = Col5Left + 35;
                    Col7Left = Col6Left + 35;
                    Col8Left = Col7Left + 35;
                    Col9Left = Col8Left + 35;
                    Col10Left = 570;
                    //Col6Left + 60

                    cb.SetTextMatrix(Col0Left, YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("Test Date");

                    cb.SetTextMatrix(Col1Left, YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("Test Name");

                    cb.SetTextMatrix(Col2Left, YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("Subject");

                    cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth("Attend", false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("Attend");

                    cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth("Score", false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("Score");

                    cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth("Out Of", false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("Out Of");


                    cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth("%", false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("%");

                    cb.SetTextMatrix((Col7Left + ((Col8Left - Col7Left) / 2) - (cb.GetEffectiveStringWidth("Rank*", false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("Rank*");

                    cb.SetTextMatrix((Col8Left + ((Col9Left - Col8Left) / 2) - (cb.GetEffectiveStringWidth("Highest", false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("Highest");


                    cb.SetTextMatrix((Col9Left + ((Col10Left - Col9Left) / 2) - (cb.GetEffectiveStringWidth("Overall Rank", false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("Overall Rank");


                    cb.MoveTo(20, YPos - 5);
                    cb.LineTo(570, YPos - 5);
                    cb.Stroke();

                    foreach (DataListItem dtlItem1 in dlPrint_Summary.Items)
                    {
                        Label lblDLTestDate = (Label)dtlItem1.FindControl("lblDLTestDate");
                        Label lblDLTestName = (Label)dtlItem1.FindControl("lblDLTestName");
                        Label lblDLSubject = (Label)dtlItem1.FindControl("lblDLSubject");
                        Label lblDLMarksObtd = (Label)dtlItem1.FindControl("lblDLMarksObtd");
                        Label lblDLMarksOutOf = (Label)dtlItem1.FindControl("lblDLMarksOutOf");
                        Label lblDLPercent = (Label)dtlItem1.FindControl("lblDLPercent");
                        Label lblDLCentreRank = (Label)dtlItem1.FindControl("lblDLCentreRank");
                        Label lblDLOvarllRank = (Label)dtlItem1.FindControl("lblDLOvarllRank");
                        Label lblDlCenter_Highest_Mark = (Label)dtlItem1.FindControl("lblDlCenter_Highest_Mark");
                        Label lblDLAttendStatus = (Label)dtlItem1.FindControl("lblDLAttendStatus");

                        if (chkOverallRankFlag.Checked == false)
                        {
                            lblDLOvarllRank.Text = "-";
                        }

                        YPos = YPos - 20;
                        cb.SetTextMatrix(Col0Left, YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText(lblDLTestDate.Text);

                        cb.SetTextMatrix(Col1Left, YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText(lblDLTestName.Text);

                        cb.SetTextMatrix(Col2Left, YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText(lblDLSubject.Text);

                        cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth(lblDLAttendStatus.Text, false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText(lblDLAttendStatus.Text);


                        cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth(lblDLMarksObtd.Text, false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText(lblDLMarksObtd.Text);

                        cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth(lblDLMarksOutOf.Text, false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText(lblDLMarksOutOf.Text);

                        cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth(lblDLPercent.Text, false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText(lblDLPercent.Text);


                        cb.SetTextMatrix((Col7Left + ((Col8Left - Col7Left) / 2) - (cb.GetEffectiveStringWidth(lblDLCentreRank.Text, false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText(lblDLCentreRank.Text);



                        cb.SetTextMatrix((Col8Left + ((Col9Left - Col8Left) / 2) - (cb.GetEffectiveStringWidth(lblDlCenter_Highest_Mark.Text, false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText(lblDlCenter_Highest_Mark.Text);


                        cb.SetTextMatrix((Col9Left + ((Col10Left - Col9Left) / 2) - (cb.GetEffectiveStringWidth(lblDLOvarllRank.Text, false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText(lblDLOvarllRank.Text);


                        cb.MoveTo(20, YPos - 5);
                        cb.LineTo(570, YPos - 5);
                        cb.Stroke();
                    }

                    cb.MoveTo(20, TableStartYPos);
                    cb.LineTo(20, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col1Left - 5, TableStartYPos);
                    cb.LineTo(Col1Left - 5, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col2Left - 5, TableStartYPos);
                    cb.LineTo(Col2Left - 5, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col3Left, TableStartYPos);
                    cb.LineTo(Col3Left, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col4Left, TableStartYPos);
                    cb.LineTo(Col4Left, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col5Left, TableStartYPos);
                    cb.LineTo(Col5Left, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col6Left, TableStartYPos);
                    cb.LineTo(Col6Left, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col7Left, TableStartYPos);
                    cb.LineTo(Col7Left, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col8Left, TableStartYPos);
                    cb.LineTo(Col8Left, YPos - 5);
                    cb.Stroke();


                    cb.MoveTo(Col9Left, TableStartYPos);
                    cb.LineTo(Col9Left, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col10Left, TableStartYPos);
                    cb.LineTo(Col10Left, YPos - 5);
                    cb.Stroke();


                    YPos = YPos - 25;
                    cb.Stroke();

                    //For MCQ Type test
                    if (ddlTestCategory.SelectedValue == "002")
                    {
                        cb.SetTextMatrix(25, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                        cb.ShowText("Details of Answering");
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                        cb.MoveTo(20, YPos - 10);
                        cb.LineTo(570, YPos - 10);
                        cb.Stroke();
                        TableStartYPos = YPos - 10;

                        YPos = YPos - 25;

                        Col0Left = 25;
                        Col1Left = Col0Left + 100;
                        Col2Left = Col1Left + 100;
                        Col3Left = Col2Left + 65;
                        Col4Left = Col3Left + 40;
                        Col5Left = Col4Left + 100;
                        Col6Left = Col5Left + 100;
                        Col7Left = 570;
                        //Col6Left + 60

                        cb.SetTextMatrix(Col0Left, YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText("Test Name");

                        cb.SetTextMatrix(Col1Left, YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText("Subject");

                        cb.SetTextMatrix(Col2Left, YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText("Status");

                        cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth("Count", false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText("Count");

                        cb.SetTextMatrix(Col4Left + 5, YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText("Que No");

                        //cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth("Que No - Moderate", False) / 2)), YPos)
                        //cb.SetFontAndSize(bf, 10)
                        //cb.ShowText("Que No - Moderate")

                        //cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth("Que No - Difficult", False) / 2)), YPos)
                        //cb.SetFontAndSize(bf, 10)
                        //cb.ShowText("Que No - Difficult")

                        cb.MoveTo(20, YPos - 5);
                        cb.LineTo(570, YPos - 5);
                        cb.Stroke();

                        foreach (DataListItem dtlItem1 in dlPrint_Answering.Items)
                        {
                            Label lblDLTestName = (Label)dtlItem1.FindControl("lblDLTestName");
                            Label lblDLSubjectName = (Label)dtlItem1.FindControl("lblDLSubjectName");
                            Label lblDLResultStatus = (Label)dtlItem1.FindControl("lblDLResultStatus");
                            Label lblDLResultCount = (Label)dtlItem1.FindControl("lblDLResultCount");
                            Label lblDLEasy = (Label)dtlItem1.FindControl("lblDLEasy");
                            Label lblDLModerate = (Label)dtlItem1.FindControl("lblDLModerate");
                            Label lblDLDifficult = (Label)dtlItem1.FindControl("lblDLDifficult");

                            YPos = YPos - 20;
                            cb.SetTextMatrix(Col0Left, YPos);
                            cb.SetFontAndSize(bf, 8);
                            cb.ShowText(lblDLTestName.Text);

                            cb.SetTextMatrix(Col1Left, YPos);
                            cb.SetFontAndSize(bf, 8);
                            //cb.ShowText(Strings.Left(lblDLSubjectName.Text, 10));
                            //cb.ShowText(lblDLSubjectName.Text.Substring(0,10));
                            cb.ShowText(lblDLSubjectName.Text);


                            cb.SetTextMatrix(Col2Left, YPos);
                            cb.SetFontAndSize(bf, 8);
                            cb.ShowText(lblDLResultStatus.Text);

                            cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth(lblDLResultCount.Text, false) / 2)), YPos);
                            cb.SetFontAndSize(bf, 8);
                            cb.ShowText(lblDLResultCount.Text);

                            //cb.SetTextMatrix(Col4Left + 5, YPos)
                            //cb.SetFontAndSize(bf, 10)
                            //cb.ShowText(lblDLEasy.Text)

                            float Yp1PDF = 0;
                            float ActPos = 0;
                            string TotalMatter = null;
                            string DummyMatter = null;
                            string PrintMatter = null;
                            dynamic SplitMatter = null;

                            Yp1PDF = YPos;
                            ActPos = YPos;
                            TotalMatter = lblDLEasy.Text + lblDLModerate.Text + lblDLDifficult.Text;

                            int Cnt = 0;
                            Cnt = 0;

                            //TRIPTY Start


                            if (!string.IsNullOrEmpty((TotalMatter.Trim())))
                            {
                                Yp1PDF = Yp1PDF + 20;

                                DummyMatter = TotalMatter;
                                SplitMatter = TotalMatter.Split(Environment.NewLine.ToCharArray()); //Strings.Split(TotalMatter, Constants.vbCrLf);JAyant
                                for (int EntCnt = 0; EntCnt <= Cnt; EntCnt++)
                                {
                                    TotalMatter = SplitMatter[EntCnt];
                                Again1PDF:
                                    Yp1PDF = Yp1PDF - 20;
                                    PrintMatter = "";
                                    for (int ChrCnt = 1; ChrCnt <= (TotalMatter.Length); ChrCnt++)
                                    {
                                        if (string.IsNullOrEmpty((TotalMatter.Trim())))
                                        {
                                            PrintMatter = "";
                                        }
                                        else
                                        {
                                            PrintMatter = TotalMatter.Substring(0, ChrCnt); //PrintMatter = Strings.Left(TotalMatter, ChrCnt);
                                        }
                                        if (cb.GetEffectiveStringWidth(PrintMatter, true) >= (Col7Left - Col4Left - 10))
                                        {
                                            //Search for last blank space
                                            //ChrCnt = PrintMatter.LastIndexOf(',');//Strings.InStrRev(PrintMatter, ",");
                                            //PrintMatter = TotalMatter.Substring(0, ChrCnt);//PrintMatter = Strings.Left(TotalMatter, ChrCnt);

                                            //cb.SetTextMatrix(Col4Left + 5, Yp1PDF);
                                            //cb.SetFontAndSize(bf, 10);
                                            //cb.ShowText(PrintMatter);

                                            //TotalMatter = TotalMatter.Substring(TotalMatter.Length - (TotalMatter.Length) - ChrCnt); //Strings.Right(TotalMatter, Strings.Len(TotalMatter) - ChrCnt);

                                            ChrCnt = PrintMatter.LastIndexOf(',');//Strings.InStrRev(PrintMatter, ",");
                                            PrintMatter = TotalMatter.Substring(0, ChrCnt);

                                            cb.SetTextMatrix(Col4Left + 5, Yp1PDF);
                                            cb.SetFontAndSize(bf, 10);
                                            cb.ShowText(PrintMatter);

                                            TotalMatter = TotalMatter.Substring(PrintMatter.Length, TotalMatter.Length - ChrCnt);



                                            goto Again1PDF;
                                        }
                                    }

                                    cb.SetTextMatrix(Col4Left + 5, Yp1PDF);
                                    cb.SetFontAndSize(bf, 8);
                                    cb.ShowText(PrintMatter);
                                }
                            }





                            YPos = Yp1PDF;

                            //cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth(lblDLModerate.Text, False) / 2)), YPos)
                            //cb.SetFontAndSize(bf, 10)
                            //cb.ShowText(lblDLModerate.Text)

                            //cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth(lblDLDifficult.Text, False) / 2)), YPos)
                            //cb.SetFontAndSize(bf, 10)
                            //cb.ShowText(lblDLDifficult.Text)

                            cb.MoveTo(20, YPos - 5);
                            cb.LineTo(570, YPos - 5);
                            cb.Stroke();
                        }

                        cb.MoveTo(20, TableStartYPos);
                        cb.LineTo(20, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col1Left - 5, TableStartYPos);
                        cb.LineTo(Col1Left - 5, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col2Left - 5, TableStartYPos);
                        cb.LineTo(Col2Left - 5, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col3Left, TableStartYPos);
                        cb.LineTo(Col3Left, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col4Left, TableStartYPos);
                        cb.LineTo(Col4Left, YPos - 5);
                        cb.Stroke();

                        //cb.MoveTo(Col5Left, TableStartYPos)
                        //cb.LineTo(Col5Left, YPos - 5)
                        //cb.Stroke()

                        //cb.MoveTo(Col6Left, TableStartYPos)
                        //cb.LineTo(Col6Left, YPos - 5)
                        //cb.Stroke()

                        cb.MoveTo(Col7Left, TableStartYPos);
                        cb.LineTo(Col7Left, YPos - 5);
                        cb.Stroke();
                    }

                    if (chkOverallRankFlag.Checked == true)
                    {


                        YPos = YPos - 25;

                        cb.SetTextMatrix(25, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                        cb.ShowText("Overall Toppers");
                        cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                        cb.MoveTo(20, YPos - 10);
                        cb.LineTo(780, YPos - 10);
                        cb.Stroke();
                        TableStartYPos = YPos - 10;

                        YPos = YPos - 25;

                        Col0Left = 25;
                        Col1Left = Col0Left + 100;
                        Col2Left = Col1Left + 100;
                        Col3Left = Col2Left + 170;
                        Col4Left = Col3Left + 125;
                        Col5Left = 570;
                        //Col6Left + 60

                        cb.SetTextMatrix(Col0Left, YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText("Test Name");

                        cb.SetTextMatrix(Col1Left, YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText("Subject");

                        cb.SetTextMatrix(Col2Left, YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText("Name of Student");

                        cb.SetTextMatrix(Col3Left, YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText("Centre");

                        cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth("Score", false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText("Score");

                        cb.MoveTo(20, YPos - 5);
                        cb.LineTo(570, YPos - 5);
                        cb.Stroke();

                        foreach (DataListItem dtlItem1 in dlPrint_Topper.Items)
                        {
                            Label lblDLTestName = (Label)dtlItem1.FindControl("lblDLTestName");
                            Label lblDLSubject = (Label)dtlItem1.FindControl("lblDLSubject");
                            Label lblDLStudentName = (Label)dtlItem1.FindControl("lblDLStudentName");
                            Label lblDLCentre = (Label)dtlItem1.FindControl("lblDLCentre");
                            Label lblDLScore = (Label)dtlItem1.FindControl("lblDLScore");

                            YPos = YPos - 20;
                            cb.SetTextMatrix(Col0Left, YPos);
                            cb.SetFontAndSize(bf, 8);
                            cb.ShowText(lblDLTestName.Text);

                            cb.SetTextMatrix(Col1Left, YPos);
                            cb.SetFontAndSize(bf, 8);
                            //cb.ShowText(Strings.Left(lblDLSubject.Text, 10));
                            //cb.ShowText(lblDLSubject.Text.Substring(0, 10));

                            cb.ShowText(lblDLSubject.Text);
                            cb.SetTextMatrix(Col2Left, YPos);
                            cb.SetFontAndSize(bf, 8);
                            cb.ShowText(lblDLStudentName.Text);

                            cb.SetTextMatrix(Col3Left, YPos);
                            cb.SetFontAndSize(bf, 8);
                            cb.ShowText(lblDLCentre.Text);

                            cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth(lblDLScore.Text, false) / 2)), YPos);
                            cb.SetFontAndSize(bf, 8);
                            cb.ShowText(lblDLScore.Text);

                            cb.MoveTo(20, YPos - 5);
                            cb.LineTo(570, YPos - 5);
                            cb.Stroke();
                        }

                        cb.MoveTo(20, TableStartYPos);
                        cb.LineTo(20, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col1Left - 5, TableStartYPos);
                        cb.LineTo(Col1Left - 5, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col2Left - 5, TableStartYPos);
                        cb.LineTo(Col2Left - 5, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col3Left - 5, TableStartYPos);
                        cb.LineTo(Col3Left - 5, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col4Left, TableStartYPos);
                        cb.LineTo(Col4Left, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col5Left, TableStartYPos);
                        cb.LineTo(Col5Left, YPos - 5);
                        cb.Stroke();

                    }
                    YPos = YPos - 25;


                    //document.Add(New Paragraph("STATEMENT OF MARKS", TitleFont))

                    //Dim orderInfoTable = New PdfPTable(2)
                    //orderInfoTable.HorizontalAlignment = 0
                    //orderInfoTable.SpacingBefore = 10
                    //orderInfoTable.SpacingAfter = 10
                    //orderInfoTable.DefaultCell.Border = 1
                    //orderInfoTable.SetWidths(New Integer() {1, 4})

                    //orderInfoTable.AddCell(New Phrase("Order:", boldTableFont))
                    //orderInfoTable.AddCell(lblPrint_RollNo.Text)
                    //orderInfoTable.AddCell(New Phrase("Price:", boldTableFont))
                    //'orderInfoTable.AddCell(Convert.ToDecimal(txtTotalPrice.Text).ToString("c"))
                    //orderInfoTable.AddCell(lblPrint_StudentName.Text)

                    //document.Add(orderInfoTable)


                    cb.EndText();


                    writer.CloseStream = false;
                    document.Close();
                    output.Position = 0;


                    //EMail code should come over here
                    //Dim bytes As Byte() = MemoryStream.ToArray()
                    //memoryStream.Close()

                    MailMessage Msg = new MailMessage();
                    MailAddress fromMail = new MailAddress("mtsttdept@gmail.com");
                    string CurTimeFrame = null;
                    CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

                    // Sender e-mail address.
                    Msg.From = fromMail;

                    // Recipient e-mail address.
                    Msg.To.Add(new MailAddress(lblStudentEmail.Text.Trim()));

                    // Subject of e-mail
                    Msg.Subject = "Statement of Marks for " + lblStudentName.Text;
                    Msg.Body += "Dear Parent <br/><br/>Please find enclosed a PDF file containing Statement of Marks for your ward " + lblStudentName.Text + " for " + lblStandard_Result.Text + " standard at MT Educare.";

                    Msg.Attachments.Add(new Attachment(output, "StatementOfMarks.pdf"));

                    Msg.IsBodyHtml = true;
                    string sSmtpServer = "";
                    sSmtpServer = "smtp.gmail.com";
                    SmtpClient a = new SmtpClient();
                    a.Host = sSmtpServer;

                    a.EnableSsl = true;


                    NetworkCredential NetworkCred = new NetworkCredential();
                    NetworkCred.UserName = "mtsttdept@gmail.com";
                    NetworkCred.Password = "science11";
                    a.UseDefaultCredentials = true;
                    a.Credentials = NetworkCred;
                    a.Port = 587;
                    a.Timeout = 20000;
                    a.Send(Msg);

                    output.Close();

                    //Response.Clear()
                    //Response.ContentType = "application/pdf"
                    //Response.AddHeader("Content-Disposition", String.Format("attachment;filename=StudentMarkSheet{0}.pdf", CurTimeFrame))
                    //Response.ContentType = "application/pdf"
                    //Response.Buffer = True
                    //Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    //Response.BinaryWrite(bytes)
                    //Response.[End]()
                    Response.Close();


                }
            }




            //Response.ContentType = "application/pdf"
            //Response.AddHeader("Content-Disposition", String.Format("attachment;filename=StudentMarkSheet{0}.pdf", CurTimeFrame))
            //Response.BinaryWrite(output.ToArray())

            Show_Error_Success_Box("S", "PDF File generated successfully.");

            //btnStudSelect_Close_Click(sender, e);
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }


    public Report_MarkSheet()
    {
        Load += Page_Load;
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        ddlDivision.SelectedIndex = 0;
        ddlAcadYear.SelectedIndex = 0;
        ddlTestCategory.SelectedIndex = 0;
        ddlStandard.Items.Clear();
        ddlTestType.Items.Clear();
        ddlCentre.Items.Clear();
        ddlBatch.Items.Clear();
        txtRollNo.Text = "";
        ddlTestName.Items.Clear();
        id_date_range_picker_1.Value = "";
    }


    private void PrintDataforsci(object sender, EventArgs e)
    {

        //Dim Path As String = "Report_Marksheet_Print.aspx?Test_Id=" & Replace(Test_Id, "%", "%25") & "&SBEntryCode=" & SBEntryCode
        //ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", "open('" + Path + "');", True)

        string Test_Id = null;
        Test_Id = lblTestID_Result.Text;


        string FromDate = null;
        string ToDate = null;

        string Report_Period = lblTestPeriod.Text.ToString();
        if (Report_Period != "")
        {
            FromDate = Report_Period.Substring(0, 10);//Strings.Left(Report_Period, 10);
        }
        if (string.IsNullOrEmpty(FromDate))
        {
            //FromDate = System.DateTime.Now.ToString("01 Jan 2010");

            FromDate = "01 Jan 2010";
        }
        if (Report_Period != "")
        {
            ToDate = Report_Period.Substring(Report_Period.Length - 10);//Strings.Right(Report_Period, 10);
        }
        if (string.IsNullOrEmpty(ToDate))
        {
            ToDate = System.DateTime.Now.ToString("dd MMM yyyy");
        }


        // Create a Document object
        dynamic document = new Document(PageSize.A4, 50, 50, 25, 25);

        // Create a new PdfWriter object, specifying the output stream
        dynamic output = new MemoryStream();
        dynamic writer = PdfWriter.GetInstance(document, output);


        dynamic TitleFont = FontFactory.GetFont("Arial", 16, Font.BOLD);
        dynamic subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
        dynamic boldTableFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
        dynamic endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
        dynamic bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);


        // Open the Document for writing
        document.Open();

        //For each item selected in Grid run the following things
        foreach (DataListItem dtlItem in dlGridStudSelect.Items)
        {
            CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
            Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
            Label lblStudentRollNo = (Label)dtlItem.FindControl("lblStudentRollNo");
            Label lblStudentName = (Label)dtlItem.FindControl("lblStudentName");

            if (chkStudent.Checked == true)
            {
                //For MCQ Type test
                if (ddlTestCategory.SelectedValue == "002")
                {
                    DataSet dsGrid = ProductController.Report_Test_MCQ_Test_Subject_Student_Rank(Test_Id, lblSBEntryCode.Text, 1, FromDate, ToDate);
                    dlPrint_Summary.DataSource = dsGrid.Tables[0];
                    dlPrint_Summary.DataBind();

                    dlPrint_Answering.DataSource = dsGrid.Tables[1];
                    dlPrint_Answering.DataBind();

                    dlPrint_Topper.DataSource = dsGrid.Tables[2];
                    dlPrint_Topper.DataBind();
                }
                else  //subjective
                {
                    DataSet dsGrid = ProductController.Report_Test_NonMCQ_Test_Subject_Student_Rank(Test_Id, lblSBEntryCode.Text, 1, FromDate, ToDate);
                    dlPrint_Summary.DataSource = dsGrid.Tables[0];
                    dlPrint_Summary.DataBind();

                    dlPrint_Answering.DataSource = null;
                    dlPrint_Answering.DataBind();

                    dlPrint_Topper.DataSource = dsGrid.Tables[1];
                    dlPrint_Topper.DataBind();
                }

                lblPrint_Center.Text = ddlCentre.SelectedItem.ToString();
                //  lblPrint_StudentName.Text = lblStudentName.Text;
                //  lblPrint_RollNo.Text = lblStudentRollNo.Text;

                float YPos = 0;
                YPos = 780;

                dynamic logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/LEPL-LOGO.jpg"));
                logo.SetAbsolutePosition(25, YPos);
                logo.ScalePercent(60);
                document.Add(logo);

                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                PdfContentByte cb = writer.DirectContent;
                cb.BeginText();
                cb.SetTextMatrix(380, YPos + 20);
                cb.SetFontAndSize(bf, 16);

                cb.SetLineWidth(0.5f);
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                cb.ShowText("STATEMENT OF MARKS");
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                YPos = YPos - 0;

                cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                cb.SetLineWidth(0.5f);
                cb.MoveTo(20, YPos);
                cb.LineTo(570, YPos);
                cb.Stroke();

                YPos = YPos - 15;

                cb.SetTextMatrix(25, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.ShowText("Name of Student : ");

                cb.SetTextMatrix(120, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                cb.ShowText(lblStudentName.Text);

                cb.SetTextMatrix(325, YPos);
                cb.SetFontAndSize(bf, 10);
                //  cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                cb.ShowText("Roll No : ");

                cb.SetTextMatrix(375, YPos);
                cb.SetFontAndSize(bf, 10);
                // cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                cb.ShowText(lblStudentRollNo.Text);

                cb.SetTextMatrix(425, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                cb.ShowText("Centre : ");

                cb.SetTextMatrix(475, YPos);
                cb.SetFontAndSize(bf, 10);
                // cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                cb.ShowText(lblPrint_Center.Text);
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                float TableStartYPos = 0;
                cb.MoveTo(20, YPos - 10);
                cb.LineTo(570, YPos - 10);
                cb.Stroke();
                TableStartYPos = YPos - 10;

                YPos = YPos - 25;

                float Col0Left = 0;
                float Col1Left = 0;
                float Col2Left = 0;
                float Col3Left = 0;
                float Col4Left = 0;
                float Col5Left = 0;
                float Col6Left = 0;
                float Col7Left = 0;
                float Col8Left = 0;

                Col0Left = 25;
                Col1Left = Col0Left + 65;
                Col2Left = Col1Left + 80;
                Col3Left = Col2Left + 130;
                Col4Left = Col3Left + 40;
                Col5Left = Col4Left + 45;
                Col6Left = Col5Left + 45;
                Col7Left = Col6Left + 60;
                Col8Left = 570;
                //Col6Left + 60

                cb.SetTextMatrix(Col0Left, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.ShowText("Test Date");

                cb.SetTextMatrix(Col1Left, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.ShowText("Test Name");

                cb.SetTextMatrix(Col2Left, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.ShowText("Subject");

                cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth("Attend", false) / 2)), YPos);
                cb.SetFontAndSize(bf, 10);
                cb.ShowText("Attend");

                cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth("Score", false) / 2)), YPos);
                cb.SetFontAndSize(bf, 10);
                cb.ShowText("Score");

                cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth("Out Of", false) / 2)), YPos);
                cb.SetFontAndSize(bf, 10);
                cb.ShowText("Out Of");

                cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth("Centre Rank", false) / 2)), YPos);
                cb.SetFontAndSize(bf, 10);
                cb.ShowText("Centre Rank");

                cb.SetTextMatrix((Col7Left + ((Col8Left - Col7Left) / 2) - (cb.GetEffectiveStringWidth("Overall Rank", false) / 2)), YPos);
                cb.SetFontAndSize(bf, 10);
                cb.ShowText("Overall Rank");

                cb.MoveTo(20, YPos - 5);
                cb.LineTo(570, YPos - 5);
                cb.Stroke();

                foreach (DataListItem dtlItem1 in dlPrint_Summary.Items)
                {

                    if (YPos < 40)
                    {
                        cb.EndText();
                        cb.MoveTo(20, TableStartYPos);
                        cb.LineTo(20, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col1Left - 5, TableStartYPos);
                        cb.LineTo(Col1Left - 5, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col2Left - 5, TableStartYPos);
                        cb.LineTo(Col2Left - 5, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col3Left, TableStartYPos);
                        cb.LineTo(Col3Left, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col4Left, TableStartYPos);
                        cb.LineTo(Col4Left, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col5Left, TableStartYPos);
                        cb.LineTo(Col5Left, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col6Left, TableStartYPos);
                        cb.LineTo(Col6Left, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col7Left, TableStartYPos);
                        cb.LineTo(Col7Left, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col8Left, TableStartYPos);
                        cb.LineTo(Col8Left, YPos);

                        document.NewPage();
                        YPos = 780;
                        TableStartYPos = 780;
                        // cb.BeginText();
                        cb.SetTextMatrix(380, YPos + 20);
                        cb.SetFontAndSize(bf, 16);

                        cb.SetLineWidth(0.5f);
                        //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                        cb.Stroke();
                        // cb.EndText();
                    }
                    Label lblDLTestDate = (Label)dtlItem1.FindControl("lblDLTestDate");
                    Label lblDLTestName = (Label)dtlItem1.FindControl("lblDLTestName");
                    Label lblDLSubject = (Label)dtlItem1.FindControl("lblDLSubject");
                    Label lblDLMarksObtd = (Label)dtlItem1.FindControl("lblDLMarksObtd");
                    Label lblDLMarksOutOf = (Label)dtlItem1.FindControl("lblDLMarksOutOf");
                    Label lblDLPercent = (Label)dtlItem1.FindControl("lblDLPercent");
                    Label lblDLCentreRank = (Label)dtlItem1.FindControl("lblDLCentreRank");
                    Label lblDLOvarllRank = (Label)dtlItem1.FindControl("lblDLOvarllRank");
                    Label lblDLAttendStatus = (Label)dtlItem1.FindControl("lblDLAttendStatus");
                    //cb.BeginText();

                    if (chkOverallRankFlag.Checked == false)
                    {
                        lblDLOvarllRank.Text = "-";
                    }


                    YPos = YPos - 20;
                    cb.SetTextMatrix(Col0Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblDLTestDate.Text);



                    float Yp1PDF = 0;
                    float ActPos = 0;
                    string TotalMatter = null;
                    string DummyMatter = null;
                    string PrintMatter = null;
                    dynamic SplitMatter = null;

                    Yp1PDF = YPos;
                    ActPos = YPos;
                    TotalMatter = lblDLTestName.Text; ;

                    int Cnt = 0;
                    Cnt = 0;




                    if (!string.IsNullOrEmpty((TotalMatter.Trim())))
                    {
                        Yp1PDF = Yp1PDF + 20;

                        DummyMatter = TotalMatter;
                        SplitMatter = TotalMatter.Split(Environment.NewLine.ToCharArray());//Strings.Split(TotalMatter, Constants.vbCrLf);//chk by jayant



                        for (int EntCnt = 0; EntCnt <= Cnt; EntCnt++)
                        {
                            TotalMatter = SplitMatter[EntCnt];
                        Again1PDF:
                            Yp1PDF = Yp1PDF - 20;
                            PrintMatter = "";
                            for (int ChrCnt = 1; ChrCnt <= (TotalMatter.Length); ChrCnt++)
                            {
                                if (string.IsNullOrEmpty((TotalMatter.Trim())))
                                {
                                    PrintMatter = "";
                                }
                                else
                                {
                                    PrintMatter = TotalMatter.Substring(0, ChrCnt); //Strings.Left(TotalMatter, ChrCnt);
                                }


                                if (cb.GetEffectiveStringWidth(PrintMatter, true) >= (Col2Left - Col1Left - 10))
                                {
                                    //Search for last blank space

                                    //ChrCnt = PrintMatter.IndexOf(',');//Strings.InStrRev(PrintMatter, ",");
                                    //PrintMatter = TotalMatter.Substring(0, ChrCnt);//Strings.Left(TotalMatter, ChrCnt);


                                    ChrCnt = PrintMatter.LastIndexOf(' ');//Strings.InStrRev(PrintMatter, ",");
                                    PrintMatter = TotalMatter.Substring(0, ChrCnt);

                                    cb.SetTextMatrix(Col1Left, Yp1PDF);
                                    cb.SetFontAndSize(bf, 10);
                                    cb.ShowText(PrintMatter);

                                    TotalMatter = TotalMatter.Substring(PrintMatter.Length, TotalMatter.Length - ChrCnt);

                                    //TotalMatter = TotalMatter.Substring(0,TotalMatter.Length - ChrCnt);//Strings.Right(TotalMatter, Strings.Len(TotalMatter) - ChrCnt);





                                    goto Again1PDF;
                                }
                            }

                            cb.SetTextMatrix(Col1Left, Yp1PDF);
                            cb.SetFontAndSize(bf, 10);
                            cb.ShowText(PrintMatter);
                        }
                    }
                    //cb.SetTextMatrix(Col1Left, YPos);
                    //cb.SetFontAndSize(bf, 10);
                    //if (lblDLTestName.Text.Length > 17)
                    //{
                    //    cb.ShowText(lblDLTestName.Text.Substring(0, 17));
                    //}
                    //else
                    //{

                    //    cb.ShowText(lblDLTestName.Text);
                    //}
                    cb.SetTextMatrix(Col2Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblDLSubject.Text);


                    cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth(lblDLAttendStatus.Text, false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblDLAttendStatus.Text);

                    cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth(lblDLMarksObtd.Text, false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblDLMarksObtd.Text);

                    cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth(lblDLMarksOutOf.Text, false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblDLMarksOutOf.Text);

                    cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth(lblDLCentreRank.Text, false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblDLCentreRank.Text);

                    cb.SetTextMatrix((Col7Left + ((Col8Left - Col7Left) / 2) - (cb.GetEffectiveStringWidth(lblDLOvarllRank.Text, false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblDLOvarllRank.Text);
                    YPos = Yp1PDF;
                    cb.MoveTo(20, YPos - 5);
                    cb.LineTo(570, YPos - 5);

                    cb.Stroke();

                    //if (YPos < 25)
                    //{
                    //    cb.EndText();
                    //    document.NewPage();
                    //    YPos = 780;
                    //    cb.BeginText();
                    //    cb.SetTextMatrix(380, YPos + 20);
                    //    cb.SetFontAndSize(bf, 16);

                    //    cb.SetLineWidth(0.5f);
                    //    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                    //}


                }

                cb.MoveTo(20, TableStartYPos);
                cb.LineTo(20, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col1Left - 5, TableStartYPos);
                cb.LineTo(Col1Left - 5, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col2Left - 5, TableStartYPos);
                cb.LineTo(Col2Left - 5, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col3Left, TableStartYPos);
                cb.LineTo(Col3Left, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col4Left, TableStartYPos);
                cb.LineTo(Col4Left, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col5Left, TableStartYPos);
                cb.LineTo(Col5Left, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col6Left, TableStartYPos);
                cb.LineTo(Col6Left, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col7Left, TableStartYPos);
                cb.LineTo(Col7Left, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col8Left, TableStartYPos);
                cb.LineTo(Col8Left, YPos - 5);

                YPos = YPos - 25;

                cb.Stroke();
                cb.EndText();

                //For MCQ Type test
                if (ddlTestCategory.SelectedValue == "002")
                {

                    cb.BeginText();
                    cb.SetTextMatrix(25, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                    cb.ShowText("Details of Answering");
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                    cb.MoveTo(20, YPos - 10);
                    cb.LineTo(570, YPos - 10);
                    cb.Stroke();
                    TableStartYPos = YPos - 10;

                    YPos = YPos - 25;

                    Col0Left = 25;
                    Col1Left = Col0Left + 65;
                    Col2Left = Col1Left + 70;
                    Col3Left = Col2Left + 65;
                    Col4Left = Col3Left + 40;
                    //Col5Left = Col4Left + 100;
                    //Col6Left = Col5Left + 100;
                    Col7Left = 570;
                    //Col6Left + 60

                    cb.SetTextMatrix(Col0Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Test Name");

                    cb.SetTextMatrix(Col1Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Subject");

                    cb.SetTextMatrix(Col2Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Status");

                    cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth("Count", false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Count");

                    cb.SetTextMatrix(Col4Left + 5, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Que No");

                    //cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth("Que No - Moderate", False) / 2)), YPos)
                    //cb.SetFontAndSize(bf, 10)
                    //cb.ShowText("Que No - Moderate")

                    //cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth("Que No - Difficult", False) / 2)), YPos)
                    //cb.SetFontAndSize(bf, 10)
                    //cb.ShowText("Que No - Difficult")
                    cb.EndText();

                    cb.MoveTo(20, YPos - 5);
                    cb.LineTo(570, YPos - 5);
                    cb.Stroke();
                    cb.BeginText();
                    foreach (DataListItem dtlItem1 in dlPrint_Answering.Items)
                    {

                        if (YPos < 40)
                        {
                            cb.EndText();
                            cb.MoveTo(20, TableStartYPos);
                            cb.LineTo(20, YPos - 5);
                            cb.Stroke();

                            cb.MoveTo(Col1Left - 5, TableStartYPos);
                            cb.LineTo(Col1Left - 5, YPos - 5);
                            cb.Stroke();

                            cb.MoveTo(Col2Left - 5, TableStartYPos);
                            cb.LineTo(Col2Left - 5, YPos - 5);
                            cb.Stroke();

                            cb.MoveTo(Col3Left, TableStartYPos);
                            cb.LineTo(Col3Left, YPos - 5);
                            cb.Stroke();

                            cb.MoveTo(Col4Left, TableStartYPos);
                            cb.LineTo(Col4Left, YPos - 5);
                            cb.Stroke();

                            //cb.MoveTo(Col5Left, TableStartYPos)
                            //cb.LineTo(Col5Left, YPos - 5)
                            //cb.Stroke()

                            //cb.MoveTo(Col6Left, TableStartYPos)
                            //cb.LineTo(Col6Left, YPos - 5)
                            //cb.Stroke()

                            cb.MoveTo(Col7Left, TableStartYPos);
                            cb.LineTo(Col7Left, YPos - 5);
                            // YPos = YPos - 25;
                            cb.Stroke();
                            document.NewPage();
                            YPos = 780;
                            TableStartYPos = 780;

                            cb.MoveTo(20, TableStartYPos);
                            cb.LineTo(570, YPos);
                            cb.Stroke();
                            // cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                            //   cb.Stroke();
                            cb.BeginText();
                        }

                        YPos = YPos - 20;

                        Label lblDLTestName = (Label)dtlItem1.FindControl("lblDLTestName");
                        Label lblDLSubjectName = (Label)dtlItem1.FindControl("lblDLSubjectName");
                        Label lblDLResultStatus = (Label)dtlItem1.FindControl("lblDLResultStatus");
                        Label lblDLResultCount = (Label)dtlItem1.FindControl("lblDLResultCount");
                        Label lblDLEasy = (Label)dtlItem1.FindControl("lblDLEasy");
                        Label lblDLModerate = (Label)dtlItem1.FindControl("lblDLModerate");
                        Label lblDLDifficult = (Label)dtlItem1.FindControl("lblDLDifficult");
                        cb.BeginText();

                        cb.SetTextMatrix(Col0Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLTestName.Text);

                        cb.SetTextMatrix(Col1Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        //cb.ShowText(Strings.Left(lblDLSubjectName.Text, 10));
                        //cb.ShowText(lblDLSubjectName.Text.Substring(0, 10));
                        cb.ShowText(lblDLSubjectName.Text);

                        cb.SetTextMatrix(Col2Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLResultStatus.Text);

                        cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth(lblDLResultCount.Text, false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLResultCount.Text);

                        //cb.SetTextMatrix(Col4Left + 5, YPos);
                        //cb.SetFontAndSize(bf, 10);
                        //cb.ShowText(lblDLEasy.Text);

                        float Yp1PDF = 0;
                        float ActPos = 0;
                        string TotalMatter = null;
                        string DummyMatter = null;
                        string PrintMatter = null;
                        dynamic SplitMatter = null;

                        Yp1PDF = YPos;
                        ActPos = YPos;
                        TotalMatter = lblDLEasy.Text + lblDLModerate.Text + lblDLDifficult.Text;

                        int Cnt = 0;
                        Cnt = 0;




                        if (!string.IsNullOrEmpty((TotalMatter.Trim())))
                        {
                            Yp1PDF = Yp1PDF + 20;

                            DummyMatter = TotalMatter;
                            SplitMatter = TotalMatter.Split(Environment.NewLine.ToCharArray());//Strings.Split(TotalMatter, Constants.vbCrLf);//chk by jayant



                            for (int EntCnt = 0; EntCnt <= Cnt; EntCnt++)
                            {
                                TotalMatter = SplitMatter[EntCnt];
                            Again1PDF:
                                Yp1PDF = Yp1PDF - 20;
                                PrintMatter = "";
                                for (int ChrCnt = 1; ChrCnt <= (TotalMatter.Length); ChrCnt++)
                                {
                                    if (string.IsNullOrEmpty((TotalMatter.Trim())))
                                    {
                                        PrintMatter = "";
                                    }
                                    else
                                    {
                                        PrintMatter = TotalMatter.Substring(0, ChrCnt); //Strings.Left(TotalMatter, ChrCnt);
                                    }


                                    if (cb.GetEffectiveStringWidth(PrintMatter, true) >= (Col7Left - Col4Left - 10))
                                    {
                                        //Search for last blank space

                                        //ChrCnt = PrintMatter.IndexOf(',');//Strings.InStrRev(PrintMatter, ",");
                                        //PrintMatter = TotalMatter.Substring(0, ChrCnt);//Strings.Left(TotalMatter, ChrCnt);


                                        ChrCnt = PrintMatter.LastIndexOf(',');//Strings.InStrRev(PrintMatter, ",");
                                        PrintMatter = TotalMatter.Substring(0, ChrCnt);

                                        cb.SetTextMatrix(Col4Left + 5, Yp1PDF);
                                        cb.SetFontAndSize(bf, 10);
                                        cb.ShowText(PrintMatter);

                                        TotalMatter = TotalMatter.Substring(PrintMatter.Length, TotalMatter.Length - ChrCnt);

                                        //TotalMatter = TotalMatter.Substring(0,TotalMatter.Length - ChrCnt);//Strings.Right(TotalMatter, Strings.Len(TotalMatter) - ChrCnt);





                                        goto Again1PDF;
                                    }
                                }

                                cb.SetTextMatrix(Col4Left + 5, Yp1PDF);
                                cb.SetFontAndSize(bf, 10);
                                cb.ShowText(PrintMatter);
                            }
                        }

                        YPos = Yp1PDF;

                        //cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth(lblDLModerate.Text, False) / 2)), YPos)
                        //cb.SetFontAndSize(bf, 10)
                        //cb.ShowText(lblDLModerate.Text)

                        //cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth(lblDLDifficult.Text, False) / 2)), YPos)
                        //cb.SetFontAndSize(bf, 10)
                        //cb.ShowText(lblDLDifficult.Text)
                        cb.EndText();
                        cb.MoveTo(20, YPos - 5);
                        cb.LineTo(570, YPos - 5);
                        cb.Stroke();
                        cb.BeginText();
                    }

                    cb.EndText();
                    cb.MoveTo(20, TableStartYPos);
                    cb.LineTo(20, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col1Left - 5, TableStartYPos);
                    cb.LineTo(Col1Left - 5, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col2Left - 5, TableStartYPos);
                    cb.LineTo(Col2Left - 5, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col3Left, TableStartYPos);
                    cb.LineTo(Col3Left, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col4Left, TableStartYPos);
                    cb.LineTo(Col4Left, YPos - 5);
                    cb.Stroke();

                    //cb.MoveTo(Col5Left, TableStartYPos)
                    //cb.LineTo(Col5Left, YPos - 5)
                    //cb.Stroke()

                    //cb.MoveTo(Col6Left, TableStartYPos)
                    //cb.LineTo(Col6Left, YPos - 5)
                    //cb.Stroke()

                    cb.MoveTo(Col7Left, TableStartYPos);
                    cb.LineTo(Col7Left, YPos - 5);
                    YPos = YPos - 25;
                    cb.Stroke();



                }

                if (chkOverallRankFlag.Checked == true)
                {
                    cb.BeginText();
                    cb.SetTextMatrix(25, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                    cb.ShowText("Overall Toppers");
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);


                    cb.MoveTo(20, YPos - 10);
                    cb.LineTo(570, YPos - 10);
                    cb.Stroke();
                    TableStartYPos = YPos - 10;

                    YPos = YPos - 25;

                    Col0Left = 25;
                    Col1Left = Col0Left + 65;
                    Col2Left = Col1Left + 70;
                    Col3Left = Col2Left + 205;
                    Col4Left = Col3Left + 150;
                    Col5Left = 570;
                    //Col6Left + 60

                    cb.SetTextMatrix(Col0Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Test Name");

                    cb.SetTextMatrix(Col1Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Subject");

                    cb.SetTextMatrix(Col2Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Name of Student");

                    cb.SetTextMatrix(Col3Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Centre");

                    cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth("Score", false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Score");

                    cb.MoveTo(20, YPos - 5);
                    cb.LineTo(570, YPos - 5);
                    cb.Stroke();

                    foreach (DataListItem dtlItem1 in dlPrint_Topper.Items)
                    {


                        if (YPos < 40)
                        {
                            cb.EndText();
                            document.NewPage();
                            YPos = 800;
                            cb.BeginText();

                            cb.MoveTo(20, YPos - 10);
                            cb.LineTo(570, YPos - 10);

                            TableStartYPos = YPos - 10;

                            // YPos = YPos - 25;
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                            //cb.ShowText("Overall Toppers");
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                            cb.Stroke();
                        }

                        YPos = YPos - 25;
                        //else
                        //{
                        Label lblDLTestName = (Label)dtlItem1.FindControl("lblDLTestName");
                        Label lblDLSubject = (Label)dtlItem1.FindControl("lblDLSubject");
                        Label lblDLStudentName = (Label)dtlItem1.FindControl("lblDLStudentName");
                        Label lblDLCentre = (Label)dtlItem1.FindControl("lblDLCentre");
                        Label lblDLScore = (Label)dtlItem1.FindControl("lblDLScore");
                        cb.BeginText();
                        cb.SetTextMatrix(25, YPos);
                        // cb.SetFontAndSize(bf, 10);



                        cb.SetTextMatrix(Col0Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLTestName.Text);

                        cb.SetTextMatrix(Col1Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        //cb.ShowText(Strings.Left(lblDLSubject.Text, 10));
                        //cb.ShowText(lblDLSubject.Text.Substring(0, 10));
                        cb.ShowText(lblDLSubject.Text);

                        cb.SetTextMatrix(Col2Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLStudentName.Text);

                        cb.SetTextMatrix(Col3Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLCentre.Text);

                        cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth(lblDLScore.Text, false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLScore.Text);


                        cb.MoveTo(20, YPos - 5);
                        cb.LineTo(570, YPos - 5);
                        cb.Stroke();


                        cb.MoveTo(20, TableStartYPos);
                        cb.LineTo(20, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col1Left - 5, TableStartYPos);
                        cb.LineTo(Col1Left - 5, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col2Left - 5, TableStartYPos);
                        cb.LineTo(Col2Left - 5, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col3Left - 5, TableStartYPos);
                        cb.LineTo(Col3Left - 5, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col4Left, TableStartYPos);
                        cb.LineTo(Col4Left, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col5Left, TableStartYPos);
                        cb.LineTo(Col5Left, YPos - 5);
                        cb.Stroke();

                    }



                    cb.EndText();


                    //Create New Page for new student


                }

            }
            document.NewPage();

        }

        document.Close();

        string CurTimeFrame = null;
        CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=StudentMarkSheet{0}.pdf", CurTimeFrame));
        Response.BinaryWrite(output.ToArray());

        Show_Error_Success_Box("S", "PDF File generated successfully.");

        btnStudSelect_Close_Click(sender, e);
    }

    private void PrintDataforstateboard(object sender, EventArgs e)
    {

        //Dim Path As String = "Report_Marksheet_Print.aspx?Test_Id=" & Replace(Test_Id, "%", "%25") & "&SBEntryCode=" & SBEntryCode
        //ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", "open('" + Path + "');", True)

        string Test_Id = null;
        Test_Id = lblTestID_Result.Text;


        string FromDate = null;
        string ToDate = null;

        string Report_Period = lblTestPeriod.Text.ToString();
        if (Report_Period != "")
        {
            FromDate = Report_Period.Substring(0, 10);//Strings.Left(Report_Period, 10);
        }
        if (string.IsNullOrEmpty(FromDate))
        {
            //FromDate = System.DateTime.Now.ToString("01 Jan 2010");

            FromDate = "01 Jan 2010";
        }
        if (Report_Period != "")
        {
            ToDate = Report_Period.Substring(Report_Period.Length - 10);//Strings.Right(Report_Period, 10);
        }
        if (string.IsNullOrEmpty(ToDate))
        {
            ToDate = System.DateTime.Now.ToString("dd MMM yyyy");
        }


        // Create a Document object
        dynamic document = new Document(PageSize.A4, 50, 50, 25, 25);

        // Create a new PdfWriter object, specifying the output stream
        dynamic output = new MemoryStream();
        dynamic writer = PdfWriter.GetInstance(document, output);


        dynamic TitleFont = FontFactory.GetFont("Arial", 16, Font.BOLD);
        dynamic subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
        dynamic boldTableFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
        dynamic endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
        dynamic bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);


        // Open the Document for writing
        document.Open();

        //For each item selected in Grid run the following things
        foreach (DataListItem dtlItem in dlGridStudSelect.Items)
        {
            CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
            Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
            Label lblStudentRollNo = (Label)dtlItem.FindControl("lblStudentRollNo");
            Label lblStudentName = (Label)dtlItem.FindControl("lblStudentName");

            if (chkStudent.Checked == true)
            {
                //For MCQ Type test
                if (ddlTestCategory.SelectedValue == "002")
                {
                    DataSet dsGrid = ProductController.Report_Test_MCQ_Test_Subject_Student_Rank(Test_Id, lblSBEntryCode.Text, 1, FromDate, ToDate);
                    dlPrint_Summary.DataSource = dsGrid.Tables[0];
                    dlPrint_Summary.DataBind();

                    dlPrint_Answering.DataSource = dsGrid.Tables[1];
                    dlPrint_Answering.DataBind();

                    dlPrint_Topper.DataSource = dsGrid.Tables[2];
                    dlPrint_Topper.DataBind();
                }
                else  //subjective
                {
                    DataSet dsGrid = ProductController.Report_Test_NonMCQ_Test_Subject_Student_Rank(Test_Id, lblSBEntryCode.Text, 1, FromDate, ToDate);
                    dlPrint_Summary.DataSource = dsGrid.Tables[0];
                    dlPrint_Summary.DataBind();

                    dlPrint_Answering.DataSource = null;
                    dlPrint_Answering.DataBind();

                    dlPrint_Topper.DataSource = dsGrid.Tables[1];
                    dlPrint_Topper.DataBind();
                }

                lblPrint_Center.Text = ddlCentre.SelectedItem.ToString();
                //  lblPrint_StudentName.Text = lblStudentName.Text;
                //  lblPrint_RollNo.Text = lblStudentRollNo.Text;

                float YPos = 0;
                YPos = 780;

                dynamic logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/logo.jpg"));
                logo.SetAbsolutePosition(25, YPos);
                logo.ScalePercent(60);
                document.Add(logo);

                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                PdfContentByte cb = writer.DirectContent;
                cb.BeginText();
                cb.SetTextMatrix(380, YPos + 20);
                cb.SetFontAndSize(bf, 16);

                cb.SetLineWidth(0.5f);
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                cb.ShowText("STATEMENT OF MARKS");
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                YPos = YPos - 0;

                cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                cb.SetLineWidth(0.5f);
                cb.MoveTo(20, YPos);
                cb.LineTo(570, YPos);
                cb.Stroke();

                YPos = YPos - 15;

                cb.SetTextMatrix(25, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.ShowText("Name of Student : ");

                cb.SetTextMatrix(120, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                cb.ShowText(lblStudentName.Text);

                cb.SetTextMatrix(325, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                cb.ShowText("Roll No : ");

                cb.SetTextMatrix(375, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                cb.ShowText(lblStudentRollNo.Text);

                cb.SetTextMatrix(425, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                cb.ShowText("Centre : ");

                cb.SetTextMatrix(475, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                cb.ShowText(lblPrint_Center.Text);
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                float TableStartYPos = 0;
                cb.MoveTo(20, YPos - 10);
                cb.LineTo(570, YPos - 10);
                cb.Stroke();
                TableStartYPos = YPos - 10;

                YPos = YPos - 25;

                float Col0Left = 0;
                float Col1Left = 0;
                float Col2Left = 0;
                float Col3Left = 0;
                float Col4Left = 0;
                float Col5Left = 0;
                float Col6Left = 0;
                float Col7Left = 0;
                float Col8Left = 0;

                Col0Left = 25;
                Col1Left = Col0Left + 65;
                Col2Left = Col1Left + 80;
                Col3Left = Col2Left + 130;
                Col4Left = Col3Left + 40;
                Col5Left = Col4Left + 45;
                Col6Left = Col5Left + 45;
                Col7Left = Col6Left + 60;
                Col8Left = 570;
                //Col6Left + 60

                cb.SetTextMatrix(Col0Left, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.ShowText("Test Date");

                cb.SetTextMatrix(Col1Left, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.ShowText("Test Name");

                cb.SetTextMatrix(Col2Left, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.ShowText("Subject");

                cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth("Attend", false) / 2)), YPos);
                cb.SetFontAndSize(bf, 10);
                cb.ShowText("Attend");

                cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth("Score", false) / 2)), YPos);
                cb.SetFontAndSize(bf, 10);
                cb.ShowText("Score");

                cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth("Out Of", false) / 2)), YPos);
                cb.SetFontAndSize(bf, 10);
                cb.ShowText("Out Of");

                cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth("Centre Rank", false) / 2)), YPos);
                cb.SetFontAndSize(bf, 10);
                cb.ShowText("Centre Rank");

                cb.SetTextMatrix((Col7Left + ((Col8Left - Col7Left) / 2) - (cb.GetEffectiveStringWidth("Overall Rank", false) / 2)), YPos);
                cb.SetFontAndSize(bf, 10);
                cb.ShowText("Overall Rank");

                cb.MoveTo(20, YPos - 5);
                cb.LineTo(570, YPos - 5);
                cb.Stroke();

                foreach (DataListItem dtlItem1 in dlPrint_Summary.Items)
                {

                    if (YPos < 40)
                    {
                        cb.EndText();
                        cb.MoveTo(20, TableStartYPos);
                        cb.LineTo(20, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col1Left - 5, TableStartYPos);
                        cb.LineTo(Col1Left - 5, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col2Left - 5, TableStartYPos);
                        cb.LineTo(Col2Left - 5, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col3Left, TableStartYPos);
                        cb.LineTo(Col3Left, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col4Left, TableStartYPos);
                        cb.LineTo(Col4Left, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col5Left, TableStartYPos);
                        cb.LineTo(Col5Left, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col6Left, TableStartYPos);
                        cb.LineTo(Col6Left, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col7Left, TableStartYPos);
                        cb.LineTo(Col7Left, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col8Left, TableStartYPos);
                        cb.LineTo(Col8Left, YPos);

                        document.NewPage();
                        YPos = 780;
                        TableStartYPos = 780;
                        // cb.BeginText();
                        cb.SetTextMatrix(380, YPos + 20);
                        cb.SetFontAndSize(bf, 16);

                        cb.SetLineWidth(0.5f);
                        //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                        cb.Stroke();
                        // cb.EndText();
                    }
                    Label lblDLTestDate = (Label)dtlItem1.FindControl("lblDLTestDate");
                    Label lblDLTestName = (Label)dtlItem1.FindControl("lblDLTestName");
                    Label lblDLSubject = (Label)dtlItem1.FindControl("lblDLSubject");
                    Label lblDLMarksObtd = (Label)dtlItem1.FindControl("lblDLMarksObtd");
                    Label lblDLMarksOutOf = (Label)dtlItem1.FindControl("lblDLMarksOutOf");
                    Label lblDLPercent = (Label)dtlItem1.FindControl("lblDLPercent");
                    Label lblDLCentreRank = (Label)dtlItem1.FindControl("lblDLCentreRank");
                    Label lblDLOvarllRank = (Label)dtlItem1.FindControl("lblDLOvarllRank");
                    Label lblDLAttendStatus = (Label)dtlItem1.FindControl("lblDLAttendStatus");
                    //cb.BeginText();
                    if (chkOverallRankFlag.Checked == false)
                    {
                        lblDLOvarllRank.Text = "-";
                    }


                    YPos = YPos - 20;
                    cb.SetTextMatrix(Col0Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblDLTestDate.Text);

                    float Yp1PDF = 0;
                    float ActPos = 0;
                    string TotalMatter = null;
                    string DummyMatter = null;
                    string PrintMatter = null;
                    dynamic SplitMatter = null;

                    Yp1PDF = YPos;
                    ActPos = YPos;
                    TotalMatter = lblDLTestName.Text; ;

                    int Cnt = 0;
                    Cnt = 0;




                    if (!string.IsNullOrEmpty((TotalMatter.Trim())))
                    {
                        Yp1PDF = Yp1PDF + 20;

                        DummyMatter = TotalMatter;
                        SplitMatter = TotalMatter.Split(Environment.NewLine.ToCharArray());//Strings.Split(TotalMatter, Constants.vbCrLf);//chk by jayant



                        for (int EntCnt = 0; EntCnt <= Cnt; EntCnt++)
                        {
                            TotalMatter = SplitMatter[EntCnt];
                        Again1PDF:
                            Yp1PDF = Yp1PDF - 20;
                            PrintMatter = "";
                            for (int ChrCnt = 1; ChrCnt <= (TotalMatter.Length); ChrCnt++)
                            {
                                if (string.IsNullOrEmpty((TotalMatter.Trim())))
                                {
                                    PrintMatter = "";
                                }
                                else
                                {
                                    PrintMatter = TotalMatter.Substring(0, ChrCnt); //Strings.Left(TotalMatter, ChrCnt);
                                }


                                if (cb.GetEffectiveStringWidth(PrintMatter, true) >= (Col2Left - Col1Left - 10))
                                {
                                    //Search for last blank space

                                    //ChrCnt = PrintMatter.IndexOf(',');//Strings.InStrRev(PrintMatter, ",");
                                    //PrintMatter = TotalMatter.Substring(0, ChrCnt);//Strings.Left(TotalMatter, ChrCnt);


                                    ChrCnt = PrintMatter.LastIndexOf(' ');//Strings.InStrRev(PrintMatter, ",");
                                    PrintMatter = TotalMatter.Substring(0, ChrCnt);

                                    cb.SetTextMatrix(Col1Left, Yp1PDF);
                                    cb.SetFontAndSize(bf, 10);
                                    cb.ShowText(PrintMatter);

                                    TotalMatter = TotalMatter.Substring(PrintMatter.Length, TotalMatter.Length - ChrCnt);

                                    //TotalMatter = TotalMatter.Substring(0,TotalMatter.Length - ChrCnt);//Strings.Right(TotalMatter, Strings.Len(TotalMatter) - ChrCnt);





                                    goto Again1PDF;
                                }
                            }

                            cb.SetTextMatrix(Col1Left, Yp1PDF);
                            cb.SetFontAndSize(bf, 10);
                            cb.ShowText(PrintMatter);
                        }
                    }



                    //cb.SetTextMatrix(Col1Left, YPos);
                    //cb.SetFontAndSize(bf, 10);


                    ////int len = lblDLTestName.Text.Length;

                    //if (lblDLTestName.Text.Length > 17)
                    //{
                    //   cb.ShowText(lblDLTestName.Text.Substring(0, 17));



                    //}
                    //else
                    //{

                    //    cb.ShowText(lblDLTestName.Text);
                    //}

                    cb.SetTextMatrix(Col2Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblDLSubject.Text);


                    cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth(lblDLAttendStatus.Text, false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblDLAttendStatus.Text);

                    cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth(lblDLMarksObtd.Text, false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblDLMarksObtd.Text);

                    cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth(lblDLMarksOutOf.Text, false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblDLMarksOutOf.Text);

                    cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth(lblDLCentreRank.Text, false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblDLCentreRank.Text);

                    cb.SetTextMatrix((Col7Left + ((Col8Left - Col7Left) / 2) - (cb.GetEffectiveStringWidth(lblDLOvarllRank.Text, false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText(lblDLOvarllRank.Text);
                    YPos = Yp1PDF;

                    cb.MoveTo(20, YPos - 5);
                    cb.LineTo(570, YPos - 5);

                    cb.Stroke();

                    //if (YPos < 25)
                    //{
                    //    cb.EndText();
                    //    document.NewPage();
                    //    YPos = 780;
                    //    cb.BeginText();
                    //    cb.SetTextMatrix(380, YPos + 20);
                    //    cb.SetFontAndSize(bf, 16);

                    //    cb.SetLineWidth(0.5f);
                    //    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                    //}


                }

                cb.MoveTo(20, TableStartYPos);
                cb.LineTo(20, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col1Left - 5, TableStartYPos);
                cb.LineTo(Col1Left - 5, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col2Left - 5, TableStartYPos);
                cb.LineTo(Col2Left - 5, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col3Left, TableStartYPos);
                cb.LineTo(Col3Left, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col4Left, TableStartYPos);
                cb.LineTo(Col4Left, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col5Left, TableStartYPos);
                cb.LineTo(Col5Left, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col6Left, TableStartYPos);
                cb.LineTo(Col6Left, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col7Left, TableStartYPos);
                cb.LineTo(Col7Left, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col8Left, TableStartYPos);
                cb.LineTo(Col8Left, YPos - 5);

                YPos = YPos - 25;

                cb.Stroke();
                cb.EndText();

                //For MCQ Type test
                if (ddlTestCategory.SelectedValue == "002")
                {

                    cb.BeginText();
                    cb.SetTextMatrix(25, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                    cb.ShowText("Details of Answering");
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                    cb.MoveTo(20, YPos - 10);
                    cb.LineTo(570, YPos - 10);
                    cb.Stroke();
                    TableStartYPos = YPos - 10;

                    YPos = YPos - 25;

                    Col0Left = 25;
                    Col1Left = Col0Left + 65;
                    Col2Left = Col1Left + 70;
                    Col3Left = Col2Left + 65;
                    Col4Left = Col3Left + 40;
                    //Col5Left = Col4Left + 100;
                    //Col6Left = Col5Left + 100;
                    Col7Left = 570;
                    //Col6Left + 60

                    cb.SetTextMatrix(Col0Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Test Name");

                    cb.SetTextMatrix(Col1Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Subject");

                    cb.SetTextMatrix(Col2Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Status");

                    cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth("Count", false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Count");

                    cb.SetTextMatrix(Col4Left + 5, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Que No");

                    //cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth("Que No - Moderate", False) / 2)), YPos)
                    //cb.SetFontAndSize(bf, 10)
                    //cb.ShowText("Que No - Moderate")

                    //cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth("Que No - Difficult", False) / 2)), YPos)
                    //cb.SetFontAndSize(bf, 10)
                    //cb.ShowText("Que No - Difficult")
                    cb.EndText();

                    cb.MoveTo(20, YPos - 5);
                    cb.LineTo(570, YPos - 5);
                    cb.Stroke();
                    cb.BeginText();
                    foreach (DataListItem dtlItem1 in dlPrint_Answering.Items)
                    {

                        if (YPos < 40)
                        {
                            cb.EndText();
                            cb.MoveTo(20, TableStartYPos);
                            cb.LineTo(20, YPos - 5);
                            cb.Stroke();

                            cb.MoveTo(Col1Left - 5, TableStartYPos);
                            cb.LineTo(Col1Left - 5, YPos - 5);
                            cb.Stroke();

                            cb.MoveTo(Col2Left - 5, TableStartYPos);
                            cb.LineTo(Col2Left - 5, YPos - 5);
                            cb.Stroke();

                            cb.MoveTo(Col3Left, TableStartYPos);
                            cb.LineTo(Col3Left, YPos - 5);
                            cb.Stroke();

                            cb.MoveTo(Col4Left, TableStartYPos);
                            cb.LineTo(Col4Left, YPos - 5);
                            cb.Stroke();

                            //cb.MoveTo(Col5Left, TableStartYPos)
                            //cb.LineTo(Col5Left, YPos - 5)
                            //cb.Stroke()

                            //cb.MoveTo(Col6Left, TableStartYPos)
                            //cb.LineTo(Col6Left, YPos - 5)
                            //cb.Stroke()

                            cb.MoveTo(Col7Left, TableStartYPos);
                            cb.LineTo(Col7Left, YPos - 5);
                            // YPos = YPos - 25;
                            cb.Stroke();
                            document.NewPage();
                            YPos = 780;
                            TableStartYPos = 780;

                            cb.MoveTo(20, TableStartYPos);
                            cb.LineTo(570, YPos);
                            cb.Stroke();
                            // cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                            //   cb.Stroke();
                            cb.BeginText();
                        }

                        YPos = YPos - 20;

                        Label lblDLTestName = (Label)dtlItem1.FindControl("lblDLTestName");
                        Label lblDLSubjectName = (Label)dtlItem1.FindControl("lblDLSubjectName");
                        Label lblDLResultStatus = (Label)dtlItem1.FindControl("lblDLResultStatus");
                        Label lblDLResultCount = (Label)dtlItem1.FindControl("lblDLResultCount");
                        Label lblDLEasy = (Label)dtlItem1.FindControl("lblDLEasy");
                        Label lblDLModerate = (Label)dtlItem1.FindControl("lblDLModerate");
                        Label lblDLDifficult = (Label)dtlItem1.FindControl("lblDLDifficult");
                        cb.BeginText();

                        cb.SetTextMatrix(Col0Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLTestName.Text);

                        cb.SetTextMatrix(Col1Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        //cb.ShowText(Strings.Left(lblDLSubjectName.Text, 10));
                        //cb.ShowText(lblDLSubjectName.Text.Substring(0, 10));
                        cb.ShowText(lblDLSubjectName.Text);

                        cb.SetTextMatrix(Col2Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLResultStatus.Text);

                        cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth(lblDLResultCount.Text, false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLResultCount.Text);

                        //cb.SetTextMatrix(Col4Left + 5, YPos);
                        //cb.SetFontAndSize(bf, 10);
                        //cb.ShowText(lblDLEasy.Text);

                        float Yp1PDF = 0;
                        float ActPos = 0;
                        string TotalMatter = null;
                        string DummyMatter = null;
                        string PrintMatter = null;
                        dynamic SplitMatter = null;

                        Yp1PDF = YPos;
                        ActPos = YPos;
                        TotalMatter = lblDLEasy.Text + lblDLModerate.Text + lblDLDifficult.Text;

                        int Cnt = 0;
                        Cnt = 0;




                        if (!string.IsNullOrEmpty((TotalMatter.Trim())))
                        {
                            Yp1PDF = Yp1PDF + 20;

                            DummyMatter = TotalMatter;
                            SplitMatter = TotalMatter.Split(Environment.NewLine.ToCharArray());//Strings.Split(TotalMatter, Constants.vbCrLf);//chk by jayant



                            for (int EntCnt = 0; EntCnt <= Cnt; EntCnt++)
                            {
                                TotalMatter = SplitMatter[EntCnt];
                            Again1PDF:
                                Yp1PDF = Yp1PDF - 20;
                                PrintMatter = "";
                                for (int ChrCnt = 1; ChrCnt <= (TotalMatter.Length); ChrCnt++)
                                {
                                    if (string.IsNullOrEmpty((TotalMatter.Trim())))
                                    {
                                        PrintMatter = "";
                                    }
                                    else
                                    {
                                        PrintMatter = TotalMatter.Substring(0, ChrCnt); //Strings.Left(TotalMatter, ChrCnt);
                                    }


                                    if (cb.GetEffectiveStringWidth(PrintMatter, true) >= (Col7Left - Col4Left - 10))
                                    {
                                        //Search for last blank space

                                        //ChrCnt = PrintMatter.IndexOf(',');//Strings.InStrRev(PrintMatter, ",");
                                        //PrintMatter = TotalMatter.Substring(0, ChrCnt);//Strings.Left(TotalMatter, ChrCnt);


                                        ChrCnt = PrintMatter.LastIndexOf(',');//Strings.InStrRev(PrintMatter, ",");
                                        PrintMatter = TotalMatter.Substring(0, ChrCnt);

                                        cb.SetTextMatrix(Col4Left + 5, Yp1PDF);
                                        cb.SetFontAndSize(bf, 10);
                                        cb.ShowText(PrintMatter);

                                        TotalMatter = TotalMatter.Substring(PrintMatter.Length, TotalMatter.Length - ChrCnt);

                                        //TotalMatter = TotalMatter.Substring(0,TotalMatter.Length - ChrCnt);//Strings.Right(TotalMatter, Strings.Len(TotalMatter) - ChrCnt);





                                        goto Again1PDF;
                                    }
                                }

                                cb.SetTextMatrix(Col4Left + 5, Yp1PDF);
                                cb.SetFontAndSize(bf, 10);
                                cb.ShowText(PrintMatter);
                            }
                        }

                        YPos = Yp1PDF;

                        //cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth(lblDLModerate.Text, False) / 2)), YPos)
                        //cb.SetFontAndSize(bf, 10)
                        //cb.ShowText(lblDLModerate.Text)

                        //cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth(lblDLDifficult.Text, False) / 2)), YPos)
                        //cb.SetFontAndSize(bf, 10)
                        //cb.ShowText(lblDLDifficult.Text)
                        cb.EndText();
                        cb.MoveTo(20, YPos - 5);
                        cb.LineTo(570, YPos - 5);
                        cb.Stroke();
                        cb.BeginText();
                    }

                    cb.EndText();
                    cb.MoveTo(20, TableStartYPos);
                    cb.LineTo(20, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col1Left - 5, TableStartYPos);
                    cb.LineTo(Col1Left - 5, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col2Left - 5, TableStartYPos);
                    cb.LineTo(Col2Left - 5, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col3Left, TableStartYPos);
                    cb.LineTo(Col3Left, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col4Left, TableStartYPos);
                    cb.LineTo(Col4Left, YPos - 5);
                    cb.Stroke();

                    //cb.MoveTo(Col5Left, TableStartYPos)
                    //cb.LineTo(Col5Left, YPos - 5)
                    //cb.Stroke()

                    //cb.MoveTo(Col6Left, TableStartYPos)
                    //cb.LineTo(Col6Left, YPos - 5)
                    //cb.Stroke()

                    cb.MoveTo(Col7Left, TableStartYPos);
                    cb.LineTo(Col7Left, YPos - 5);
                    YPos = YPos - 25;
                    cb.Stroke();



                }

                if (chkOverallRankFlag.Checked == true)
                {
                    cb.BeginText();
                    cb.SetTextMatrix(25, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                    cb.ShowText("Overall Toppers");
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);


                    cb.MoveTo(20, YPos - 10);
                    cb.LineTo(570, YPos - 10);
                    cb.Stroke();
                    TableStartYPos = YPos - 10;

                    YPos = YPos - 25;

                    Col0Left = 25;
                    Col1Left = Col0Left + 65;
                    Col2Left = Col1Left + 70;
                    Col3Left = Col2Left + 205;
                    Col4Left = Col3Left + 150;
                    Col5Left = 570;
                    //Col6Left + 60

                    cb.SetTextMatrix(Col0Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Test Name");

                    cb.SetTextMatrix(Col1Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Subject");

                    cb.SetTextMatrix(Col2Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Name of Student");

                    cb.SetTextMatrix(Col3Left, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Centre");

                    cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth("Score", false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.ShowText("Score");

                    cb.MoveTo(20, YPos - 5);
                    cb.LineTo(570, YPos - 5);
                    cb.Stroke();

                    foreach (DataListItem dtlItem1 in dlPrint_Topper.Items)
                    {


                        if (YPos < 40)
                        {
                            cb.EndText();
                            document.NewPage();
                            YPos = 800;
                            cb.BeginText();

                            cb.MoveTo(20, YPos - 10);
                            cb.LineTo(570, YPos - 10);

                            TableStartYPos = YPos - 10;

                            // YPos = YPos - 25;
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                            //cb.ShowText("Overall Toppers");
                            cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                            cb.Stroke();
                        }

                        YPos = YPos - 25;
                        //else
                        //{
                        Label lblDLTestName = (Label)dtlItem1.FindControl("lblDLTestName");
                        Label lblDLSubject = (Label)dtlItem1.FindControl("lblDLSubject");
                        Label lblDLStudentName = (Label)dtlItem1.FindControl("lblDLStudentName");
                        Label lblDLCentre = (Label)dtlItem1.FindControl("lblDLCentre");
                        Label lblDLScore = (Label)dtlItem1.FindControl("lblDLScore");
                        cb.BeginText();
                        cb.SetTextMatrix(25, YPos);
                        // cb.SetFontAndSize(bf, 10);



                        cb.SetTextMatrix(Col0Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLTestName.Text);

                        cb.SetTextMatrix(Col1Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        //cb.ShowText(Strings.Left(lblDLSubject.Text, 10));
                        //cb.ShowText(lblDLSubject.Text.Substring(0, 10));
                        cb.ShowText(lblDLSubject.Text);

                        cb.SetTextMatrix(Col2Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLStudentName.Text);

                        cb.SetTextMatrix(Col3Left, YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLCentre.Text);

                        cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth(lblDLScore.Text, false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 10);
                        cb.ShowText(lblDLScore.Text);


                        cb.MoveTo(20, YPos - 5);
                        cb.LineTo(570, YPos - 5);
                        cb.Stroke();


                        cb.MoveTo(20, TableStartYPos);
                        cb.LineTo(20, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col1Left - 5, TableStartYPos);
                        cb.LineTo(Col1Left - 5, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col2Left - 5, TableStartYPos);
                        cb.LineTo(Col2Left - 5, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col3Left - 5, TableStartYPos);
                        cb.LineTo(Col3Left - 5, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col4Left, TableStartYPos);
                        cb.LineTo(Col4Left, YPos - 5);
                        cb.Stroke();

                        cb.MoveTo(Col5Left, TableStartYPos);
                        cb.LineTo(Col5Left, YPos - 5);
                        cb.Stroke();

                    }



                    cb.EndText();


                    //Create New Page for new student


                }

            }
            document.NewPage();

        }

        document.Close();

        string CurTimeFrame = null;
        CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=StudentMarkSheet{0}.pdf", CurTimeFrame));
        Response.BinaryWrite(output.ToArray());

        Show_Error_Success_Box("S", "PDF File generated successfully.");

        btnStudSelect_Close_Click(sender, e);
    }

    private void PrintDataforstateboard_Old(object sender, EventArgs e)
    {

        //Dim Path As String = "Report_Marksheet_Print.aspx?Test_Id=" & Replace(Test_Id, "%", "%25") & "&SBEntryCode=" & SBEntryCode
        //ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", "open('" + Path + "');", True)

        string Test_Id = null;
        Test_Id = lblTestID_Result.Text;


        string FromDate = null;
        string ToDate = null;

        string Report_Period = lblTestPeriod.Text.ToString();
        if (Report_Period != "")
        {
            FromDate = Report_Period.Substring(0, 10);//Strings.Left(Report_Period, 10);
        }
        if (string.IsNullOrEmpty(FromDate))
        {
            //FromDate = System.DateTime.Now.ToString("01 Jan 2010");

            FromDate = "01 Jan 2010";
        }
        if (Report_Period != "")
        {
            ToDate = Report_Period.Substring(Report_Period.Length - 10);//Strings.Right(Report_Period, 10);
        }
        if (string.IsNullOrEmpty(ToDate))
        {
            ToDate = System.DateTime.Now.ToString("dd MMM yyyy");
        }


        // Create a Document object
        dynamic document = new Document(PageSize.A4, 50, 50, 25, 25);

        // Create a new PdfWriter object, specifying the output stream
        dynamic output = new MemoryStream();
        dynamic writer = PdfWriter.GetInstance(document, output);


        dynamic TitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
        dynamic subTitleFont = FontFactory.GetFont("Arial", 10, Font.BOLD);
        dynamic boldTableFont = FontFactory.GetFont("Arial", 8, Font.BOLD);
        dynamic endingMessageFont = FontFactory.GetFont("Arial", 7, Font.ITALIC);
        dynamic bodyFont = FontFactory.GetFont("Arial", 9, Font.NORMAL);


        // Open the Document for writing
        document.Open();

        //For each item selected in Grid run the following things
        foreach (DataListItem dtlItem in dlGridStudSelect.Items)
        {
            CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
            Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
            Label lblStudentRollNo = (Label)dtlItem.FindControl("lblStudentRollNo");
            Label lblStudentName = (Label)dtlItem.FindControl("lblStudentName");

            if (chkStudent.Checked == true)
            {
                //For MCQ Type test
                if (ddlTestCategory.SelectedValue == "002")
                {
                    DataSet dsGrid = ProductController.Report_Test_MCQ_Test_Subject_Student_Rank(Test_Id, lblSBEntryCode.Text, 1, FromDate, ToDate);
                    dlPrint_Summary.DataSource = dsGrid.Tables[0];
                    dlPrint_Summary.DataBind();

                    dlPrint_Answering.DataSource = dsGrid.Tables[1];
                    dlPrint_Answering.DataBind();

                    dlPrint_Topper.DataSource = dsGrid.Tables[2];
                    dlPrint_Topper.DataBind();
                }
                else
                {
                    DataSet dsGrid = ProductController.Report_Test_NonMCQ_Test_Subject_Student_Rank(Test_Id, lblSBEntryCode.Text, 1, FromDate, ToDate);
                    dlPrint_Summary.DataSource = dsGrid.Tables[0];
                    dlPrint_Summary.DataBind();

                    dlPrint_Answering.DataSource = null;
                    dlPrint_Answering.DataBind();

                    dlPrint_Topper.DataSource = dsGrid.Tables[1];
                    dlPrint_Topper.DataBind();
                }

                lblPrint_Center.Text = ddlCentre.SelectedItem.ToString();
                //lblPrint_StudentName.Text = lblStudentName.Text;
                //lblPrint_RollNo.Text = lblStudentRollNo.Text;

                float YPos = 0;
                YPos = 780;

                dynamic logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/logo.jpg"));
                logo.SetAbsolutePosition(25, YPos);
                logo.ScalePercent(60);
                document.Add(logo);

                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);



                PdfContentByte cb = writer.DirectContent;
                cb.BeginText();
                cb.SetTextMatrix(380, YPos + 20);
                cb.SetFontAndSize(bf, 16);

                cb.SetLineWidth(0.5f);
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);

                cb.ShowText("STATEMENT OF MARKS");
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                YPos = YPos - 0;

                cb.SetColorStroke(new CMYKColor(1f, 1f, 1f, 1f));
                cb.SetLineWidth(0.5f);
                cb.MoveTo(20, YPos);
                cb.LineTo(570, YPos);
                cb.Stroke();

                YPos = YPos - 15;

                cb.SetTextMatrix(25, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.ShowText("Name of Student : ");

                cb.SetTextMatrix(120, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                cb.ShowText(lblStudentName.Text);

                cb.SetTextMatrix(325, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                cb.ShowText("Roll No : ");


                cb.SetTextMatrix(375, YPos);
                cb.SetFontAndSize(bf, 9);
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                cb.ShowText(lblPrint_RollNo.Text);

                cb.SetTextMatrix(425, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);
                cb.ShowText("Centre : ");

                cb.SetTextMatrix(475, YPos);
                cb.SetFontAndSize(bf, 10);
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                cb.ShowText(lblPrint_Center.Text);
                cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                float TableStartYPos = 0;
                cb.MoveTo(20, YPos - 10);
                cb.LineTo(570, YPos - 10);
                cb.Stroke();
                TableStartYPos = YPos - 10;

                YPos = YPos - 25;

                float Col0Left = 0;
                float Col1Left = 0;
                float Col2Left = 0;
                float Col3Left = 0;
                float Col4Left = 0;
                float Col5Left = 0;
                float Col6Left = 0;
                float Col7Left = 0;
                float Col8Left = 0;
                float Col9Left = 0;
                float Col10Left = 0;

                Col0Left = 25;
                Col1Left = Col0Left + 65;
                Col2Left = Col1Left + 130;
                Col3Left = Col2Left + 90;
                Col4Left = Col3Left + 33;
                Col5Left = Col4Left + 35;
                Col6Left = Col5Left + 35;
                Col7Left = Col6Left + 35;
                Col8Left = Col7Left + 35;
                Col9Left = Col8Left + 35;
                Col10Left = 570;
                //Col6Left + 60

                cb.SetTextMatrix(Col0Left, YPos);
                cb.SetFontAndSize(bf, 8);
                cb.ShowText("Test Date");

                cb.SetTextMatrix(Col1Left, YPos);
                cb.SetFontAndSize(bf, 8);
                cb.ShowText("Test Name");

                cb.SetTextMatrix(Col2Left, YPos);
                cb.SetFontAndSize(bf, 8);
                cb.ShowText("Subject");

                cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth("Attend", false) / 2)), YPos);
                cb.SetFontAndSize(bf, 8);
                cb.ShowText("Attend");

                cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth("Score", false) / 2)), YPos);
                cb.SetFontAndSize(bf, 8);
                cb.ShowText("Score");

                cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth("Out Of", false) / 2)), YPos);
                cb.SetFontAndSize(bf, 8);
                cb.ShowText("Out Of");


                cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth("%", false) / 2)), YPos);
                cb.SetFontAndSize(bf, 8);
                cb.ShowText("%");

                cb.SetTextMatrix((Col7Left + ((Col8Left - Col7Left) / 2) - (cb.GetEffectiveStringWidth("Rank*", false) / 2)), YPos);
                cb.SetFontAndSize(bf, 8);
                cb.ShowText("Rank*");

                cb.SetTextMatrix((Col8Left + ((Col9Left - Col8Left) / 2) - (cb.GetEffectiveStringWidth("Highest", false) / 2)), YPos);
                cb.SetFontAndSize(bf, 8);
                cb.ShowText("Highest");


                cb.SetTextMatrix((Col9Left + ((Col10Left - Col9Left) / 2) - (cb.GetEffectiveStringWidth("Overall Rank", false) / 2)), YPos);
                cb.SetFontAndSize(bf, 8);
                cb.ShowText("Overall Rank");



                cb.MoveTo(20, YPos - 5);
                cb.LineTo(570, YPos - 5);
                cb.Stroke();

                foreach (DataListItem dtlItem1 in dlPrint_Summary.Items)
                {


                    if (YPos < 40)
                    {
                        cb.EndText();
                        cb.MoveTo(20, TableStartYPos);
                        cb.LineTo(20, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col1Left - 5, TableStartYPos);
                        cb.LineTo(Col1Left - 5, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col2Left - 5, TableStartYPos);
                        cb.LineTo(Col2Left - 5, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col3Left, TableStartYPos);
                        cb.LineTo(Col3Left, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col4Left, TableStartYPos);
                        cb.LineTo(Col4Left, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col5Left, TableStartYPos);
                        cb.LineTo(Col5Left, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col6Left, TableStartYPos);
                        cb.LineTo(Col6Left, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col7Left, TableStartYPos);
                        cb.LineTo(Col7Left, YPos);
                        cb.Stroke();

                        cb.MoveTo(Col8Left, TableStartYPos);
                        cb.LineTo(Col8Left, YPos);

                        document.NewPage();
                        YPos = 780;
                        TableStartYPos = 780;
                        // cb.BeginText();
                        cb.SetTextMatrix(380, YPos + 20);
                        cb.SetFontAndSize(bf, 16);

                        cb.SetLineWidth(0.5f);
                        //cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                        cb.Stroke();
                        // cb.EndText();
                    }

                    Label lblDLTestDate = (Label)dtlItem1.FindControl("lblDLTestDate");
                    Label lblDLTestName = (Label)dtlItem1.FindControl("lblDLTestName");
                    Label lblDLSubject = (Label)dtlItem1.FindControl("lblDLSubject");
                    Label lblDLMarksObtd = (Label)dtlItem1.FindControl("lblDLMarksObtd");
                    Label lblDLMarksOutOf = (Label)dtlItem1.FindControl("lblDLMarksOutOf");
                    Label lblDLPercent = (Label)dtlItem1.FindControl("lblDLPercent");
                    Label lblDLCentreRank = (Label)dtlItem1.FindControl("lblDLCentreRank");
                    Label lblDLOvarllRank = (Label)dtlItem1.FindControl("lblDLOvarllRank");
                    Label lblDlCenter_Highest_Mark = (Label)dtlItem1.FindControl("lblDlCenter_Highest_Mark");
                    Label lblDLAttendStatus = (Label)dtlItem1.FindControl("lblDLAttendStatus");




                    if (chkOverallRankFlag.Checked == false)
                    {
                        lblDLOvarllRank.Text = "-";
                    }




                    YPos = YPos - 20;
                    cb.SetTextMatrix(Col0Left, YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText(lblDLTestDate.Text);

                    cb.SetTextMatrix(Col1Left, YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText(lblDLTestName.Text);





                    float Yp1PDF = 0;
                    float ActPos = 0;
                    string TotalMatter = null;
                    string DummyMatter = null;
                    string PrintMatter = null;
                    dynamic SplitMatter = null;

                    Yp1PDF = YPos;
                    ActPos =
                        YPos;
                    TotalMatter = lblDLSubject.Text;

                    int Cnt = 0;
                    Cnt = 0;


                    if (lblDLSubject.Text.Contains(" "))
                    {

                        if (!string.IsNullOrEmpty((TotalMatter.Trim())))
                        {
                            Yp1PDF = Yp1PDF + 20;

                            DummyMatter = TotalMatter;
                            SplitMatter = TotalMatter.Split(Environment.NewLine.ToCharArray());//Strings.Split(TotalMatter, Constants.vbCrLf);//chk by jayant



                            for (int EntCnt = 0; EntCnt <= Cnt; EntCnt++)
                            {
                                TotalMatter = SplitMatter[EntCnt];
                            Again1PDF:
                                Yp1PDF = Yp1PDF - 20;
                                PrintMatter = "";
                                for (int ChrCnt = 1; ChrCnt <= (TotalMatter.Length); ChrCnt++)
                                {
                                    if (string.IsNullOrEmpty((TotalMatter.Trim())))
                                    {
                                        PrintMatter = "";
                                    }
                                    else
                                    {
                                        PrintMatter = TotalMatter.Substring(0, ChrCnt); //Strings.Left(TotalMatter, ChrCnt);
                                    }


                                    if (cb.GetEffectiveStringWidth(PrintMatter, true) >= (Col3Left - Col2Left - 10))
                                    {
                                        //Search for last blank space

                                        //ChrCnt = PrintMatter.IndexOf(',');//Strings.InStrRev(PrintMatter, ",");
                                        //PrintMatter = TotalMatter.Substring(0, ChrCnt);//Strings.Left(TotalMatter, ChrCnt);


                                        ChrCnt = PrintMatter.LastIndexOf(' ');//Strings.InStrRev(PrintMatter, ",");
                                        PrintMatter = TotalMatter.Substring(0, ChrCnt);

                                        cb.SetTextMatrix(Col2Left, Yp1PDF);
                                        cb.SetFontAndSize(bf, 8);
                                        cb.ShowText(PrintMatter);

                                        TotalMatter = TotalMatter.Substring(PrintMatter.Length, TotalMatter.Length - ChrCnt);

                                        //TotalMatter = TotalMatter.Substring(0,TotalMatter.Length - ChrCnt);//Strings.Right(TotalMatter, Strings.Len(TotalMatter) - ChrCnt);





                                        goto Again1PDF;
                                    }
                                }

                                cb.SetTextMatrix(Col2Left, Yp1PDF);
                                cb.SetFontAndSize(bf, 8);
                                cb.ShowText(PrintMatter);
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty((TotalMatter.Trim())))
                        {
                            Yp1PDF = Yp1PDF + 20;

                            DummyMatter = TotalMatter;
                            SplitMatter = TotalMatter.Split(Environment.NewLine.ToCharArray());//Strings.Split(TotalMatter, Constants.vbCrLf);//chk by jayant



                            for (int EntCnt = 0; EntCnt <= Cnt; EntCnt++)
                            {
                                TotalMatter = SplitMatter[EntCnt];
                            Again1PDF:
                                Yp1PDF = Yp1PDF - 20;
                                PrintMatter = "";
                                for (int ChrCnt = 1; ChrCnt <= (TotalMatter.Length); ChrCnt++)
                                {
                                    if (string.IsNullOrEmpty((TotalMatter.Trim())))
                                    {
                                        PrintMatter = "";
                                    }
                                    else
                                    {
                                        PrintMatter = TotalMatter.Substring(0, ChrCnt); //Strings.Left(TotalMatter, ChrCnt);
                                    }


                                    if (cb.GetEffectiveStringWidth(PrintMatter, true) >= (Col3Left - Col2Left - 10))
                                    {
                                        //Search for last blank space

                                        //ChrCnt = PrintMatter.IndexOf(',');//Strings.InStrRev(PrintMatter, ",");
                                        //PrintMatter = TotalMatter.Substring(0, ChrCnt);//Strings.Left(TotalMatter, ChrCnt);


                                        ChrCnt = PrintMatter.LastIndexOf('_');//Strings.InStrRev(PrintMatter, ",");
                                        PrintMatter = TotalMatter.Substring(0, ChrCnt);

                                        cb.SetTextMatrix(Col2Left, Yp1PDF);
                                        cb.SetFontAndSize(bf, 8);
                                        cb.ShowText(PrintMatter);

                                        TotalMatter = TotalMatter.Substring(PrintMatter.Length, TotalMatter.Length - ChrCnt);

                                        //TotalMatter = TotalMatter.Substring(0,TotalMatter.Length - ChrCnt);//Strings.Right(TotalMatter, Strings.Len(TotalMatter) - ChrCnt);





                                        goto Again1PDF;
                                    }
                                }

                                cb.SetTextMatrix(Col2Left, Yp1PDF);
                                cb.SetFontAndSize(bf, 8);
                                cb.ShowText(PrintMatter);
                            }
                        }
                    }

                    cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth(lblDLAttendStatus.Text, false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText(lblDLAttendStatus.Text);


                    cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth(lblDLMarksObtd.Text, false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText(lblDLMarksObtd.Text);

                    cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth(lblDLMarksOutOf.Text, false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText(lblDLMarksOutOf.Text);

                    cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth(lblDLPercent.Text, false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText(lblDLPercent.Text);


                    cb.SetTextMatrix((Col7Left + ((Col8Left - Col7Left) / 2) - (cb.GetEffectiveStringWidth(lblDLCentreRank.Text, false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText(lblDLCentreRank.Text);



                    cb.SetTextMatrix((Col8Left + ((Col9Left - Col8Left) / 2) - (cb.GetEffectiveStringWidth(lblDlCenter_Highest_Mark.Text, false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText(lblDlCenter_Highest_Mark.Text);


                    cb.SetTextMatrix((Col9Left + ((Col10Left - Col9Left) / 2) - (cb.GetEffectiveStringWidth(lblDLOvarllRank.Text, false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText(lblDLOvarllRank.Text);


                    //lblDLOvarllRank.Text

                    //cb.MoveTo(20, YPos - 5);
                    //cb.LineTo(570, YPos - 5);
                    //cb.Stroke();
                    YPos = Yp1PDF;

                    cb.MoveTo(20, YPos - 5);
                    cb.LineTo(570, YPos - 5);

                    cb.Stroke();
                }



                cb.MoveTo(20, TableStartYPos);
                cb.LineTo(20, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col1Left - 5, TableStartYPos);
                cb.LineTo(Col1Left - 5, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col2Left - 5, TableStartYPos);
                cb.LineTo(Col2Left - 5, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col3Left, TableStartYPos);
                cb.LineTo(Col3Left, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col4Left, TableStartYPos);
                cb.LineTo(Col4Left, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col5Left, TableStartYPos);
                cb.LineTo(Col5Left, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col6Left, TableStartYPos);
                cb.LineTo(Col6Left, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col7Left, TableStartYPos);
                cb.LineTo(Col7Left, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col8Left, TableStartYPos);
                cb.LineTo(Col8Left, YPos - 5);
                cb.Stroke();


                cb.MoveTo(Col9Left, TableStartYPos);
                cb.LineTo(Col9Left, YPos - 5);
                cb.Stroke();

                cb.MoveTo(Col10Left, TableStartYPos);
                cb.LineTo(Col10Left, YPos - 5);
                cb.Stroke();


                YPos = YPos - 25;
                cb.EndText();
                //if(YPos >YPos - 25)
                //{

                //document.NewPage();
                //}

                //For MCQ Type test
                if (ddlTestCategory.SelectedValue == "002")
                {


                    cb.SetTextMatrix(25, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                    cb.ShowText("Details of Answering");
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                    cb.MoveTo(20, YPos - 10);
                    cb.LineTo(570, YPos - 10);
                    cb.Stroke();
                    TableStartYPos = YPos - 10;

                    YPos = YPos - 25;

                    Col0Left = 25;
                    Col1Left = Col0Left + 100;
                    Col2Left = Col1Left + 100;
                    Col3Left = Col2Left + 65;
                    Col4Left = Col3Left + 40;
                    Col5Left = Col4Left + 100;
                    Col6Left = Col5Left + 100;
                    Col7Left = 570;
                    //Col6Left + 60

                    cb.SetTextMatrix(Col0Left, YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("Test Name");

                    cb.SetTextMatrix(Col1Left, YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("Subject");

                    cb.SetTextMatrix(Col2Left, YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("Status");

                    cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth("Count", false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("Count");

                    cb.SetTextMatrix(Col4Left + 5, YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("Que No");

                    //cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth("Que No - Moderate", False) / 2)), YPos)
                    //cb.SetFontAndSize(bf, 10)
                    //cb.ShowText("Que No - Moderate")

                    //cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth("Que No - Difficult", False) / 2)), YPos)
                    //cb.SetFontAndSize(bf, 10)
                    //cb.ShowText("Que No - Difficult")

                    cb.MoveTo(20, YPos - 5);
                    cb.LineTo(570, YPos - 5);
                    cb.Stroke();

                    foreach (DataListItem dtlItem1 in dlPrint_Answering.Items)
                    {

                        Label lblDLTestName = (Label)dtlItem1.FindControl("lblDLTestName");
                        Label lblDLSubjectName = (Label)dtlItem1.FindControl("lblDLSubjectName");
                        Label lblDLResultStatus = (Label)dtlItem1.FindControl("lblDLResultStatus");
                        Label lblDLResultCount = (Label)dtlItem1.FindControl("lblDLResultCount");
                        Label lblDLEasy = (Label)dtlItem1.FindControl("lblDLEasy");
                        Label lblDLModerate = (Label)dtlItem1.FindControl("lblDLModerate");
                        Label lblDLDifficult = (Label)dtlItem1.FindControl("lblDLDifficult");

                        YPos = YPos - 20;
                        cb.SetTextMatrix(Col0Left, YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText(lblDLTestName.Text);

                        cb.SetTextMatrix(Col1Left, YPos);
                        cb.SetFontAndSize(bf, 8);
                        //cb.ShowText(Strings.Left(lblDLSubjectName.Text, 10));
                        //cb.ShowText(lblDLSubjectName.Text.Substring(0, 10));
                        cb.ShowText(lblDLSubjectName.Text);

                        cb.SetTextMatrix(Col2Left, YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText(lblDLResultStatus.Text);

                        cb.SetTextMatrix((Col3Left + ((Col4Left - Col3Left) / 2) - (cb.GetEffectiveStringWidth(lblDLResultCount.Text, false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText(lblDLResultCount.Text);

                        //cb.SetTextMatrix(Col4Left + 5, YPos)
                        //cb.SetFontAndSize(bf, 10)
                        //cb.ShowText(lblDLEasy.Text)

                        float Yp1PDF = 0;
                        float ActPos = 0;
                        string TotalMatter = null;
                        string DummyMatter = null;
                        string PrintMatter = null;
                        dynamic SplitMatter = null;

                        Yp1PDF = YPos;
                        ActPos = YPos;
                        TotalMatter = lblDLEasy.Text + lblDLModerate.Text + lblDLDifficult.Text;

                        int Cnt = 0;
                        Cnt = 0;




                        if (!string.IsNullOrEmpty((TotalMatter.Trim())))
                        {
                            Yp1PDF = Yp1PDF + 20;

                            DummyMatter = TotalMatter;
                            SplitMatter = TotalMatter.Split(Environment.NewLine.ToCharArray());//Strings.Split(TotalMatter, Constants.vbCrLf);//chk by jayant



                            for (int EntCnt = 0; EntCnt <= Cnt; EntCnt++)
                            {
                                TotalMatter = SplitMatter[EntCnt];
                            Again1PDF:
                                Yp1PDF = Yp1PDF - 20;
                                PrintMatter = "";
                                for (int ChrCnt = 1; ChrCnt <= (TotalMatter.Length); ChrCnt++)
                                {
                                    if (string.IsNullOrEmpty((TotalMatter.Trim())))
                                    {
                                        PrintMatter = "";
                                    }
                                    else
                                    {
                                        PrintMatter = TotalMatter.Substring(0, ChrCnt); //Strings.Left(TotalMatter, ChrCnt);
                                    }
                                    if (cb.GetEffectiveStringWidth(PrintMatter, true) >= (Col7Left - Col4Left - 10))
                                    {
                                        //Search for last blank space

                                        ChrCnt = PrintMatter.LastIndexOf(',');//Strings.InStrRev(PrintMatter, ",");
                                        PrintMatter = TotalMatter.Substring(0, ChrCnt);

                                        cb.SetTextMatrix(Col4Left + 5, Yp1PDF);
                                        cb.SetFontAndSize(bf, 10);
                                        cb.ShowText(PrintMatter);

                                        TotalMatter = TotalMatter.Substring(PrintMatter.Length, TotalMatter.Length - ChrCnt);


                                        //ChrCnt = PrintMatter.IndexOf(',');//Strings.InStrRev(PrintMatter, ",");
                                        //PrintMatter = TotalMatter.Substring(0, ChrCnt);//Strings.Left(TotalMatter, ChrCnt);

                                        //cb.SetTextMatrix(Col4Left + 5, Yp1PDF);
                                        //cb.SetFontAndSize(bf, 8);
                                        //cb.ShowText(PrintMatter);

                                        //TotalMatter = TotalMatter.Substring(TotalMatter.Length - ChrCnt);//Strings.Right(TotalMatter, Strings.Len(TotalMatter) - ChrCnt);
                                        goto Again1PDF;
                                    }
                                }

                                cb.SetTextMatrix(Col4Left + 5, Yp1PDF);
                                cb.SetFontAndSize(bf, 8);
                                cb.ShowText(PrintMatter);
                            }
                        }

                        YPos = Yp1PDF;

                        //cb.SetTextMatrix((Col5Left + ((Col6Left - Col5Left) / 2) - (cb.GetEffectiveStringWidth(lblDLModerate.Text, False) / 2)), YPos)
                        //cb.SetFontAndSize(bf, 10)
                        //cb.ShowText(lblDLModerate.Text)

                        //cb.SetTextMatrix((Col6Left + ((Col7Left - Col6Left) / 2) - (cb.GetEffectiveStringWidth(lblDLDifficult.Text, False) / 2)), YPos)
                        //cb.SetFontAndSize(bf, 10)
                        //cb.ShowText(lblDLDifficult.Text)

                        cb.MoveTo(20, YPos - 5);
                        cb.LineTo(570, YPos - 5);
                        cb.Stroke();
                    }


                    cb.MoveTo(20, TableStartYPos);
                    cb.LineTo(20, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col1Left - 5, TableStartYPos);
                    cb.LineTo(Col1Left - 5, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col2Left - 5, TableStartYPos);
                    cb.LineTo(Col2Left - 5, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col3Left, TableStartYPos);
                    cb.LineTo(Col3Left, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col4Left, TableStartYPos);
                    cb.LineTo(Col4Left, YPos - 5);
                    cb.Stroke();

                    //cb.MoveTo(Col5Left, TableStartYPos)
                    //cb.LineTo(Col5Left, YPos - 5)
                    //cb.Stroke()

                    //cb.MoveTo(Col6Left, TableStartYPos)
                    //cb.LineTo(Col6Left, YPos - 5)
                    //cb.Stroke()

                    cb.MoveTo(Col7Left, TableStartYPos);
                    cb.LineTo(Col7Left, YPos - 5);
                    cb.Stroke();
                }

                if (chkOverallRankFlag.Checked == true)
                {

                    cb.BeginText();
                    YPos = YPos - 25;

                    cb.SetTextMatrix(25, YPos);
                    cb.SetFontAndSize(bf, 10);
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE);
                    cb.ShowText("Overall Toppers");
                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL);

                    cb.MoveTo(20, YPos - 10);
                    cb.LineTo(570, YPos - 10);
                    cb.Stroke();
                    TableStartYPos = YPos - 10;

                    YPos = YPos - 25;

                    Col0Left = 25;
                    Col1Left = Col0Left + 100;
                    Col2Left = Col1Left + 100;
                    Col3Left = Col2Left + 170;
                    Col4Left = Col3Left + 125;
                    Col5Left = 570;
                    //Col6Left + 60

                    cb.SetTextMatrix(Col0Left, YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("Test Name");

                    cb.SetTextMatrix(Col1Left, YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("Subject");

                    cb.SetTextMatrix(Col2Left, YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("Name of Student");

                    cb.SetTextMatrix(Col3Left, YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("Centre");

                    cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth("Score", false) / 2)), YPos);
                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText("Score");

                    cb.MoveTo(20, YPos - 5);
                    cb.LineTo(570, YPos - 5);
                    cb.Stroke();

                    foreach (DataListItem dtlItem1 in dlPrint_Topper.Items)
                    {

                        Label lblDLTestName = (Label)dtlItem1.FindControl("lblDLTestName");
                        Label lblDLSubject = (Label)dtlItem1.FindControl("lblDLSubject");
                        Label lblDLStudentName = (Label)dtlItem1.FindControl("lblDLStudentName");
                        Label lblDLCentre = (Label)dtlItem1.FindControl("lblDLCentre");
                        Label lblDLScore = (Label)dtlItem1.FindControl("lblDLScore");

                        YPos = YPos - 20;
                        //cb.SetTextMatrix(Col0Left, YPos);
                        //cb.SetFontAndSize(bf, 8);
                        //cb.ShowText(lblDLTestName.Text);

                        //cb.SetTextMatrix(Col1Left, YPos);
                        //cb.SetFontAndSize(bf, 8);
                        ////cb.ShowText(Strings.Left(lblDLSubject.Text, 10));
                        ////cb.ShowText(lblDLSubject.Text.Substring(0, 10));
                        //cb.ShowText(lblDLSubject.Text);

                        float Yp1PDF = 0;
                        float ActPos = 0;
                        string TotalMatter = null;
                        string DummyMatter = null;
                        string PrintMatter = null;
                        dynamic SplitMatter = null;




                        Yp1PDF = YPos;
                        ActPos = YPos;
                        TotalMatter = lblDLTestName.Text;

                        int Cnt = 0;
                        Cnt = 0;




                        if (!string.IsNullOrEmpty((TotalMatter.Trim())))
                        {
                            Yp1PDF = Yp1PDF + 20;

                            DummyMatter = TotalMatter;
                            SplitMatter = TotalMatter.Split(Environment.NewLine.ToCharArray());//Strings.Split(TotalMatter, Constants.vbCrLf);//chk by jayant



                            for (int EntCnt = 0; EntCnt <= Cnt; EntCnt++)
                            {
                                TotalMatter = SplitMatter[EntCnt];
                            Again1PDF:
                                Yp1PDF = Yp1PDF - 20;
                                PrintMatter = "";
                                for (int ChrCnt = 1; ChrCnt <= (TotalMatter.Length); ChrCnt++)
                                {
                                    if (string.IsNullOrEmpty((TotalMatter.Trim())))
                                    {
                                        PrintMatter = "";
                                    }
                                    else
                                    {
                                        PrintMatter = TotalMatter.Substring(0, ChrCnt); //Strings.Left(TotalMatter, ChrCnt);
                                    }


                                    if (cb.GetEffectiveStringWidth(PrintMatter, true) >= (90))
                                    {
                                        //Search for last blank space

                                        //ChrCnt = PrintMatter.IndexOf(',');//Strings.InStrRev(PrintMatter, ",");
                                        //PrintMatter = TotalMatter.Substring(0, ChrCnt);//Strings.Left(TotalMatter, ChrCnt);


                                        ChrCnt = PrintMatter.LastIndexOf(' ');//Strings.InStrRev(PrintMatter, ",");
                                        PrintMatter = TotalMatter.Substring(0, ChrCnt);

                                        cb.SetTextMatrix(Col0Left, Yp1PDF);
                                        cb.SetFontAndSize(bf, 8);
                                        cb.ShowText(PrintMatter);

                                        TotalMatter = TotalMatter.Substring(PrintMatter.Length, TotalMatter.Length - ChrCnt);

                                        //TotalMatter = TotalMatter.Substring(0,TotalMatter.Length - ChrCnt);//Strings.Right(TotalMatter, Strings.Len(TotalMatter) - ChrCnt);





                                        goto Again1PDF;
                                    }
                                }

                                cb.SetTextMatrix(Col0Left, Yp1PDF);
                                cb.SetFontAndSize(bf, 8);
                                cb.ShowText(PrintMatter);
                            }
                        }





                        Yp1PDF = YPos;
                        ActPos = YPos;
                        TotalMatter = lblDLSubject.Text;


                        Cnt = 0;


                        if (lblDLSubject.Text.Contains(" "))
                        {

                            if (!string.IsNullOrEmpty((TotalMatter.Trim())))
                            {
                                Yp1PDF = Yp1PDF + 20;

                                DummyMatter = TotalMatter;
                                SplitMatter = TotalMatter.Split(Environment.NewLine.ToCharArray());//Strings.Split(TotalMatter, Constants.vbCrLf);//chk by jayant



                                for (int EntCnt = 0; EntCnt <= Cnt; EntCnt++)
                                {
                                    TotalMatter = SplitMatter[EntCnt];
                                Again1PDF:
                                    Yp1PDF = Yp1PDF - 20;
                                    PrintMatter = "";
                                    for (int ChrCnt = 1; ChrCnt <= (TotalMatter.Length); ChrCnt++)
                                    {
                                        if (string.IsNullOrEmpty((TotalMatter.Trim())))
                                        {
                                            PrintMatter = "";
                                        }
                                        else
                                        {
                                            PrintMatter = TotalMatter.Substring(0, ChrCnt); //Strings.Left(TotalMatter, ChrCnt);
                                        }


                                        if (cb.GetEffectiveStringWidth(PrintMatter, true) >= (Col1Left - Col0Left - 10))
                                        {
                                            //Search for last blank space

                                            //ChrCnt = PrintMatter.IndexOf(',');//Strings.InStrRev(PrintMatter, ",");
                                            //PrintMatter = TotalMatter.Substring(0, ChrCnt);//Strings.Left(TotalMatter, ChrCnt);


                                            ChrCnt = PrintMatter.LastIndexOf(' ');//Strings.InStrRev(PrintMatter, ",");
                                            PrintMatter = TotalMatter.Substring(0, ChrCnt);

                                            cb.SetTextMatrix(Col1Left, Yp1PDF);
                                            cb.SetFontAndSize(bf, 8);
                                            cb.ShowText(PrintMatter);

                                            TotalMatter = TotalMatter.Substring(PrintMatter.Length, TotalMatter.Length - ChrCnt);

                                            //TotalMatter = TotalMatter.Substring(0,TotalMatter.Length - ChrCnt);//Strings.Right(TotalMatter, Strings.Len(TotalMatter) - ChrCnt);





                                            goto Again1PDF;
                                        }
                                    }

                                    cb.SetTextMatrix(Col1Left, Yp1PDF);
                                    cb.SetFontAndSize(bf, 8);
                                    cb.ShowText(PrintMatter);
                                }
                            }
                        }
                        else
                        {

                            if (!string.IsNullOrEmpty((TotalMatter.Trim())))
                            {
                                Yp1PDF = Yp1PDF + 20;

                                DummyMatter = TotalMatter;
                                SplitMatter = TotalMatter.Split(Environment.NewLine.ToCharArray());//Strings.Split(TotalMatter, Constants.vbCrLf);//chk by jayant



                                for (int EntCnt = 0; EntCnt <= Cnt; EntCnt++)
                                {
                                    TotalMatter = SplitMatter[EntCnt];
                                Again1PDF:
                                    Yp1PDF = Yp1PDF - 20;
                                    PrintMatter = "";
                                    for (int ChrCnt = 1; ChrCnt <= (TotalMatter.Length); ChrCnt++)
                                    {
                                        if (string.IsNullOrEmpty((TotalMatter.Trim())))
                                        {
                                            PrintMatter = "";
                                        }
                                        else
                                        {
                                            PrintMatter = TotalMatter.Substring(0, ChrCnt); //Strings.Left(TotalMatter, ChrCnt);
                                        }


                                        if (cb.GetEffectiveStringWidth(PrintMatter, true) >= (Col1Left - Col0Left - 10))
                                        {
                                            //Search for last blank space

                                            //ChrCnt = PrintMatter.IndexOf(',');//Strings.InStrRev(PrintMatter, ",");
                                            //PrintMatter = TotalMatter.Substring(0, ChrCnt);//Strings.Left(TotalMatter, ChrCnt);


                                            ChrCnt = PrintMatter.LastIndexOf('_');//Strings.InStrRev(PrintMatter, ",");
                                            PrintMatter = TotalMatter.Substring(0, ChrCnt);

                                            cb.SetTextMatrix(Col1Left, Yp1PDF);
                                            cb.SetFontAndSize(bf, 8);
                                            cb.ShowText(PrintMatter);

                                            TotalMatter = TotalMatter.Substring(PrintMatter.Length, TotalMatter.Length - ChrCnt);

                                            //TotalMatter = TotalMatter.Substring(0,TotalMatter.Length - ChrCnt);//Strings.Right(TotalMatter, Strings.Len(TotalMatter) - ChrCnt);





                                            goto Again1PDF;
                                        }
                                    }

                                    cb.SetTextMatrix(Col1Left, Yp1PDF);
                                    cb.SetFontAndSize(bf, 8);
                                    cb.ShowText(PrintMatter);
                                }
                            }
                        }
                        cb.SetTextMatrix(Col2Left, YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText(lblDLStudentName.Text);

                        cb.SetTextMatrix(Col3Left, YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText(lblDLCentre.Text);

                        cb.SetTextMatrix((Col4Left + ((Col5Left - Col4Left) / 2) - (cb.GetEffectiveStringWidth(lblDLScore.Text, false) / 2)), YPos);
                        cb.SetFontAndSize(bf, 8);
                        cb.ShowText(lblDLScore.Text);

                        //cb.MoveTo(20, YPos - 5);
                        //cb.LineTo(570, YPos - 5);
                        //cb.Stroke();
                        YPos = Yp1PDF;

                        cb.MoveTo(20, YPos - 5);
                        cb.LineTo(570, YPos - 5);

                        cb.Stroke();
                        //cb.EndText();
                    }


                    cb.MoveTo(20, TableStartYPos);
                    cb.LineTo(20, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col1Left - 5, TableStartYPos);
                    cb.LineTo(Col1Left - 5, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col2Left - 5, TableStartYPos);
                    cb.LineTo(Col2Left - 5, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col3Left - 5, TableStartYPos);
                    cb.LineTo(Col3Left - 5, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col4Left, TableStartYPos);
                    cb.LineTo(Col4Left, YPos - 5);
                    cb.Stroke();

                    cb.MoveTo(Col5Left, TableStartYPos);
                    cb.LineTo(Col5Left, YPos - 5);
                    cb.Stroke();

                }

                YPos = YPos - 25;
                cb.EndText();

                document.NewPage();
                //Create New Page for new student

            }
        }

        document.Close();

        string CurTimeFrame = null;
        CurTimeFrame = System.DateTime.Now.ToString("ddMMyyyyhhmmss");

        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=StudentMarkSheet{0}.pdf", CurTimeFrame));
        Response.BinaryWrite(output.ToArray());

        Show_Error_Success_Box("S", "PDF File generated successfully.");

        btnStudSelect_Close_Click(sender, e);
    }


    protected void ddlTestName_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnStudentName_Click(sender, e);
    }
}
