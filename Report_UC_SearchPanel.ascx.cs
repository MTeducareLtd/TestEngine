
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI;

partial class Report_UC_SearchPanel : System.Web.UI.UserControl
{

    public string DivisionCode
    {
        get { return ddlDivision.SelectedValue; }
        set
        {
            try
            {
                ddlDivision.SelectedValue = value;

            }
            catch (Exception ex)
            {
            }

        }
    }

    public string DivisionName
    {
        get { return ddlDivision.SelectedItem.ToString(); }
        //ddlDivision.SelectedItem.Text = value
        set { }
    }

    public string AcadYearName
    {
        get { return ddlAcadYear.SelectedItem.ToString(); }
        //ddlAcadYear.SelectedItem.Text = value
        set { }
    }

    public string StandardName
    {
        get { return ddlStandard.SelectedItem.ToString(); }
        //ddlStandard.SelectedItem.Text = value
        set { }
    }

    public string TestCategoryName
    {
        get { return ddlTestCategory.SelectedItem.ToString(); }
        //ddlTestCategory.SelectedItem.Text = value
        set { }
    }

    public string ReportTypeName
    {
        get { return ddlReportType.SelectedItem.ToString(); }
        //ddlReportType.SelectedItem.Text = value
        set { }
    }


    public string ReportTypeCode
    {
        get { return Convert.ToString(ddlReportType.SelectedIndex + 1); }
        set { ddlReportType.SelectedValue = value; }
    }

    public string UserCode
    {
        get { return lblHeader_User_Code.Text; }
        set { lblHeader_User_Code.Text = value; }
    }

    public string CompanyCode
    {
        get { return lblHeader_Company_Code.Text; }
        set { lblHeader_Company_Code.Text = value; }
    }

    public string Test_Id
    {
        get { return lblFinal_Test_Id.Text; }
        set { lblFinal_Test_Id.Text = value; }
    }

    public string Test_ID_Report_Type_8
    {
        get { return ddltestname_1.SelectedValue; }
        set { lblFinal_Test_Id.Text = value; }
    }

    public string Centre_Code
    {
        get { return lblFinal_Centre_Code.Text; }
        set { lblFinal_Centre_Code.Text = value; }
    }



    public string Batch_Code
    {
        get { return lblFinal_Batch_Code.Text; }
        set { lblFinal_Batch_Code.Text = value; }
    }

    public string DBName
    {
        get { return lblHeader_DBName.Text; }
        set { lblHeader_DBName.Text = value; }
    }

    public string ReportPeriod
    {
        get { return id_date_range_picker_1.Value; }
        set { id_date_range_picker_1.Value = value; }
    }

    public string Subject_Code
    {
        //get { return ddlSubject.SelectedValue.ToString(); }

        //set { }
        get { return lblFinal_Subject_Code.Text; }
        set { lblFinal_Subject_Code.Text = value; }
    }

    public string Subject_Name
    {

        //get
        //{
        //    // ERROR: Not supported in C#: OnErrorStatement

        //    return ddlSubject.SelectedItem.Text;
        //}

        //set { }
        get { return lblFinal_Subject_Code.Text; }
        set { lblFinal_Subject_Code.Text = value; }
    }


    protected void Page_Load(object sender, System.EventArgs e)
    {

        if (!IsPostBack)
        {
            FillDDL_Division();
            FillDDL_AcadYear();
            FillDDL_TestCategories();
            FillDDL_TestTypes();
        }
    }

    public void FillDDL_ReportType(int ReportType)
    {
        if (ReportType == 0)
        {
            trreporttype.Visible = true;
            ddlReportType.Items.Clear();
            ddlReportType.Items.Add("Test Performance-Summary");
            ddlReportType.Items.Add("Test Performance-Detailed");
            ddlReportType.Items.Add("Concise Studentwise");
            ddlReportType.Items.Add("Re-Test Report");
        }
        else if (ReportType == 1)
        {
            trreporttype.Visible = true;
            ddlReportType.Items.Clear();
            ddlReportType.Items.Add("Test Ranking for - Test Conducted");
            ddlReportType.Items.Add("Test Ranking for - Test Attended");
            ddlReportType.Items.Add("Test Ranking for - Board + Objective Conducted");
            ddlReportType.Items.Add("Test Ranking for- Board + Objective Attended");
        }
        else if (ReportType == 2)
        {
            trreporttype.Visible = true;
            ddlReportType.Items.Clear();
            ddlReportType.Items.Add("Attendance Authorisation-Concise");
            ddlReportType.Items.Add("Marks Authorisation-Concise");
            ddlReportType.Items.Add("Test Conduction");
            ddlReportType.Items.Add("Test Cancellation - Summary");
            ddlReportType.Items.Add("Test Cancellation - Detailed");
        }
        if (ReportType == 4)
        {
            trreporttype.Visible = true;
            ddlReportType.Items.Clear();
            ddlReportType.Items.Add("Test Attendance-Summary");
            ddlReportType.Items.Add("Test Attendance- Detailed");
            ddlReportType.Items.Add("Concise Studentwise");
        }
        if (ReportType == 5)
        {
            trreporttype.Visible = true;
            ddlReportType.Items.Clear();
            ddlReportType.Items.Add("Faculty Test Performance- Summary");
            ddlReportType.Items.Add("Faculty Test Performance- Detailed");


        }

        // for marks entry and test scheduled report
        if (ReportType == 6)
        {
            trreporttype.Visible = false;
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

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    public void FillDDL_Division()
    {
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

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
        FillDDL_Search_Centre();
        Clear_Error_Success_Box();
    }

    private void FillDDL_Search_Centre()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);

        BindListBox(ddlCentre, dsCentre, "Center_Name", "Center_Code");
        ddlCentre.Items.Insert(0, "");
        ddlCentre.Items.Insert(1, "All");
        ddlCentre.SelectedIndex = 0;

    }


    private void FillDDL_Standard()
    {
        ddlStandard.Items.Clear();
        ddlBatch.Items.Clear();


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

    private void Clear_Error_Success_Box()
    {
        Panel Msg_Error = this.Parent.FindControl("Msg_Error") as Panel;
        Panel Msg_Success = this.Parent.FindControl("Msg_Success") as Panel;
        Label lblSuccess = this.Parent.FindControl("lblSuccess") as Label;
        Label lblerror = this.Parent.FindControl("lblerror") as Label;
        UpdatePanel UpdatePanelMsgBox = this.Parent.FindControl("UpdatePanelMsgBox") as UpdatePanel;
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }

    private void Show_Error_Success_Box(string BoxType, string Error_Code)
    {
        Panel Msg_Error = this.Parent.FindControl("Msg_Error") as Panel;
        Panel Msg_Success = this.Parent.FindControl("Msg_Success") as Panel;
        Label lblSuccess = this.Parent.FindControl("lblSuccess") as Label;
        Label lblerror = this.Parent.FindControl("lblerror") as Label;
        UpdatePanel UpdatePanelMsgBox = this.Parent.FindControl("UpdatePanelMsgBox") as UpdatePanel;
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

    protected void ddlCentre_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Batch();
        FillDDL_TestName();
        Clear_Error_Success_Box();
    }

    private void FillDDL_Batch()
    {
        string CentreCode = "";
        int CentreCnt = 0;
        int CentreSelCnt = 0;
        for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
        {
            if (ddlCentre.Items[CentreCnt].Selected == true)
            {
                CentreSelCnt = CentreSelCnt + 1;
            }
        }

        if (CentreSelCnt == 0)
        {
            //When all is selected
            for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
            {
                CentreCode = CentreCode + ddlCentre.Items[CentreCnt].Value + ",";
            }
            CentreCode = Common.RemoveComma(CentreCode);
            //if (Strings.Right(CentreCode, 1) == ",")
            //    CentreCode = Strings.Left(CentreCode, Strings.Len(CentreCode) - 1);
        }
        else
        {
            for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
            {
                if (ddlCentre.Items[CentreCnt].Selected == true)
                {
                    CentreCode = CentreCode + ddlCentre.Items[CentreCnt].Value + ",";
                }
                CentreCode = Common.RemoveComma(CentreCode);
            }
            //CentreCode = Common.RemoveComma(CentreCode);
            //if (Strings.Right(CentreCode, 1) == ",")
            //    CentreCode = Strings.Left(CentreCode, Strings.Len(CentreCode) - 1);
        }

        ddlBatch.Items.Clear();

        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        DataSet dsBatch = ProductController.GetAllActive_Batch_ForStandard(Div_Code, YearName, StandardCode, CentreCode, "3");
        BindListBox(ddlBatch, dsBatch, "Batch_Name", "Batch_Code");


    }

    private void FillDDL_TestName()
    {
        ddlTestName.Items.Clear();
        ddltestname_1.Items.Clear();

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
        string CenterCode = "";

        for (int CenterCnt = 0; CenterCnt <= ddlCentre.Items.Count - 1; CenterCnt++)
        {
            if (ddlCentre.Items[CenterCnt].Selected == true)
            {
                CenterCode = CenterCode + ddlCentre.Items[CenterCnt].Value + ",";
            }
        }

        //if (CenterCode == "")
        //{
        //    return;
        //}
        //If ddlCentre.SelectedIndex = 0 Then
        //    'Show_Error_Success_Box("E", "0006")
        //    'ddlCentre.Focus()
        //    Exit Sub
        //End If

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
            //if (Strings.Right(BatchCode, 1) == ",")
            //    BatchCode = Strings.Left(BatchCode, Strings.Len(BatchCode) - 1);
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
            //if (Strings.Right(TestType_ID, 1) == ",")
            //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);//
        }

        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        string TestName = null;
        TestName = "%";

        string DateRange = "";
        DateRange = id_date_range_picker_1.Value;

        string FromDate = "";
        string ToDate = "";
        if (DateRange != "")
        {
            FromDate = DateRange.Substring(0, 10);//Strings.Left(DateRange, 10);            
            ToDate = DateRange.Substring(DateRange.Length - 10);
        }

        DataSet dsTestName = ProductController.GetTestScheduleBy_Division_Year_Standard(DivisionCode, YearName, StandardCode, BatchCode, "01", ddlTestCategory.SelectedValue, TestType_ID, TestName, FromDate, ToDate,
        0, 0, 4, CenterCode);

        BindListBox(ddlTestName, dsTestName, "Test_Name", "PKey");

        BindDDL(ddltestname_1, dsTestName, "Test_Name", "PKey");
        ddltestname_1.Items.Insert(0, "Select");
        ddltestname_1.SelectedIndex = 0;
    }

    protected void ddlStandard_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Batch();
        FillDDL_TestName();
        FillDDL_Subject_Add();
        Clear_Error_Success_Box();
    }

    private void FillDDL_Subject_Add()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        DataSet dsStandard = ProductController.GetAllSubjectsByStandard(StandardCode);
        //BindDDL(ddlSubject, dsStandard, "Subject_Name", "Subject_Code");
        //BindDDL(ddlSubject, dsStandard, "Subject_ShortName", "Subject_Code");
        //ddlSubject.Items.Insert(0, "All");
        //ddlSubject.SelectedIndex = 0;
        BindListBox(ddlSubject, dsStandard, "Subject_Name", "Subject_Code");
        ddlSubject.Items.Insert(0, "");
        ddlSubject.Items.Insert(1, "All");

    }

    protected void ddlTestCategory_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_TestName();
        Clear_Error_Success_Box();

        if (trreporttype.Visible == true)
        {
            if (ddlTestCategory.SelectedItem.Text == "Objective" && ddlReportType.SelectedItem.Text == "Concise Studentwise")
            {
                Table_Row_5.Visible = true;

            }
            else
            {
                Table_Row_5.Visible = false;
                Subject_Code = "All";
            }
        }
    }

    protected void ddlTestType_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_TestName();
        Clear_Error_Success_Box();
    }

    public void Validate_Search_Marks_Entry()
    {
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

        //If ddlCentre.SelectedIndex = 0 Then
        //    Show_Error_Success_Box("E", "0006")
        //    ddlCentre.Focus()
        //    Exit Sub
        //End If

        if (ddlStandard.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0003");
            ddlStandard.Focus();
            return;
        }

        if (ddlTestCategory.Enabled == true)
        {
            if (ddlTestCategory.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0012");
                ddlTestCategory.Focus();
                return;
            }
        }

        if (Table_Row_5.Visible == true)
        {
            string Subject_Code = "";
            int SubjectCnt = 0;
            int SubjectSelCnt = 0;
            for (SubjectCnt = 0; SubjectCnt <= ddlSubject.Items.Count - 1; SubjectCnt++)
            {
                if (ddlSubject.Items[SubjectCnt].Selected == true)
                {
                    SubjectSelCnt = SubjectSelCnt + 1;
                }
            }

            if (SubjectSelCnt == 0)
            {
                //When all is selected
                for (SubjectCnt = 0; SubjectCnt <= ddlSubject.Items.Count - 1; SubjectCnt++)
                {
                    Subject_Code = Subject_Code + ddlSubject.Items[SubjectCnt].Value + ",";
                }
                //if (Strings.Right(Centre_Code, 1) == ",")
                //    Centre_Code = Strings.Left(Centre_Code, Strings.Len(Centre_Code) - 1);

                Subject_Code = Common.RemoveComma(Subject_Code);
            }
            else
            {
                for (SubjectCnt = 0; SubjectCnt <= ddlSubject.Items.Count - 1; SubjectCnt++)
                {
                    if (ddlSubject.Items[SubjectCnt].Selected == true)
                    {
                        Subject_Code = Subject_Code + ddlSubject.Items[SubjectCnt].Value + ",";
                    }
                }
                //if (Strings.Right(Centre_Code, 1) == ",")
                //    Centre_Code = Strings.Left(Centre_Code, Strings.Len(Centre_Code) - 1);
                Subject_Code = Common.RemoveComma(Subject_Code);
            }
            lblFinal_Subject_Code.Text = Subject_Code;


        }



        string Test_ID = "";
        string Test_Name = "";
        string Test_ID_Report_Type_8 = "";

        Test_ID_Report_Type_8 = ddltestname_1.SelectedValue;
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
            for (int TypeCnt = 0; TypeCnt <= ddlTestName.Items.Count - 1; TypeCnt++)
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
            for (int TypeCnt = 0; TypeCnt <= ddlTestName.Items.Count - 1; TypeCnt++)
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
        lblFinal_Test_Id.Text = Test_ID;

        lblFinal_Centre_Code.Text = "";

        string Centre_Code = "";
        string Centre_Name = "";
        int CentreCnt = 0;
        int CentreSelCnt = 0;
        for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
        {
            if (ddlCentre.Items[CentreCnt].Selected == true)
            {
                CentreSelCnt = CentreSelCnt + 1;
            }
        }

        if (CentreSelCnt == 0)
        {
            //When all is selected
            for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
            {
                Centre_Code = Centre_Code + ddlCentre.Items[CentreCnt].Value + ",";
                Centre_Name = Centre_Name + ddlCentre.Items[CentreCnt].Text + ",";
            }
            Centre_Code = Common.RemoveComma(Centre_Code);
            Centre_Name = Common.RemoveComma(Centre_Name);
            //if (Strings.Right(Centre_Code, 1) == ",")
            //    Centre_Code = Strings.Left(Centre_Code, Strings.Len(Centre_Code) - 1);


        }
        else
        {
            for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
            {
                if (ddlCentre.Items[CentreCnt].Selected == true)
                {
                    Centre_Code = Centre_Code + ddlCentre.Items[CentreCnt].Value + ",";
                    Centre_Name = Centre_Name + ddlCentre.Items[CentreCnt].Text + ",";
                }

            }
            //if (Strings.Right(Centre_Code, 1) == ",")
            //    Centre_Code = Strings.Left(Centre_Code, Strings.Len(Centre_Code) - 1);
            Centre_Code = Common.RemoveComma(Centre_Code);
            Centre_Name = Common.RemoveComma(Centre_Name);

        }
        lblFinal_Centre_Code.Text = Centre_Code;


        string Batch_Code = "";
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
                Batch_Code = Batch_Code + ddlBatch.Items[BatchCnt].Value + ",";
            }
            //if (Strings.Right(Centre_Code, 1) == ",")
            //    Centre_Code = Strings.Left(Centre_Code, Strings.Len(Centre_Code) - 1);

            Batch_Code = Common.RemoveComma(Batch_Code);
        }
        else
        {
            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                if (ddlBatch.Items[BatchCnt].Selected == true)
                {
                    Batch_Code = Batch_Code + ddlBatch.Items[BatchCnt].Value + ",";
                }
            }
            //if (Strings.Right(Centre_Code, 1) == ",")
            //    Centre_Code = Strings.Left(Centre_Code, Strings.Len(Centre_Code) - 1);
            Batch_Code = Common.RemoveComma(Batch_Code);
        }

        lblFinal_Batch_Code.Text = Batch_Code;
    }

    public void Validate_Search()
    {
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

        //If ddlCentre.SelectedIndex = 0 Then
        //    Show_Error_Success_Box("E", "0006")
        //    ddlCentre.Focus()
        //    Exit Sub
        //End If

        if (ddlStandard.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0003");
            ddlStandard.Focus();
            return;
        }

        if (ddlTestCategory.Enabled == true)
        {
            if (ddlTestCategory.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0012");
                ddlTestCategory.Focus();
                return;
            }
        }

        if (Table_Row_5.Visible == true)
        {
            string Subject_Code = "";
            int SubjectCnt = 0;
            int SubjectSelCnt = 0;
            for (SubjectCnt = 0; SubjectCnt <= ddlSubject.Items.Count - 1; SubjectCnt++)
            {
                if (ddlSubject.Items[SubjectCnt].Selected == true)
                {
                    SubjectSelCnt = SubjectSelCnt + 1;
                }
            }

            if (SubjectSelCnt == 0)
            {
                //When all is selected
                for (SubjectCnt = 0; SubjectCnt <= ddlSubject.Items.Count - 1; SubjectCnt++)
                {
                    Subject_Code = Subject_Code + ddlSubject.Items[SubjectCnt].Value + ",";
                }
                //if (Strings.Right(Centre_Code, 1) == ",")
                //    Centre_Code = Strings.Left(Centre_Code, Strings.Len(Centre_Code) - 1);

                Subject_Code = Common.RemoveComma(Subject_Code);
            }
            else
            {
                for (SubjectCnt = 0; SubjectCnt <= ddlSubject.Items.Count - 1; SubjectCnt++)
                {
                    if (ddlSubject.Items[SubjectCnt].Selected == true)
                    {
                        Subject_Code = Subject_Code + ddlSubject.Items[SubjectCnt].Value + ",";
                    }
                }
                //if (Strings.Right(Centre_Code, 1) == ",")
                //    Centre_Code = Strings.Left(Centre_Code, Strings.Len(Centre_Code) - 1);
                Subject_Code = Common.RemoveComma(Subject_Code);
            }
            lblFinal_Subject_Code.Text = Subject_Code;


        }



        string Test_ID = "";
        string Test_Name = "";
        string Test_ID_Report_Type_8 = "";

        Test_ID_Report_Type_8 = ddltestname_1.SelectedValue;
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
            for (int TypeCnt = 0; TypeCnt <= ddlTestName.Items.Count - 1; TypeCnt++)
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
            for (int TypeCnt = 0; TypeCnt <= ddlTestName.Items.Count - 1; TypeCnt++)
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
        lblFinal_Test_Id.Text = Test_ID;

        lblFinal_Centre_Code.Text = "";

        string Centre_Code = "";
        string Centre_Name = "";
        int CentreCnt = 0;
        int CentreSelCnt = 0;
        for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
        {
            if (ddlCentre.Items[CentreCnt].Selected == true)
            {
                CentreSelCnt = CentreSelCnt + 1;
            }
        }

        if (CentreSelCnt == 0)
        {
            //When all is selected
            for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
            {
                Centre_Code = Centre_Code + ddlCentre.Items[CentreCnt].Value + ",";
                Centre_Name = Centre_Name + ddlCentre.Items[CentreCnt].Text + ",";
            }
            Centre_Code = Common.RemoveComma(Centre_Code);
            Centre_Name = Common.RemoveComma(Centre_Name);
            //if (Strings.Right(Centre_Code, 1) == ",")
            //    Centre_Code = Strings.Left(Centre_Code, Strings.Len(Centre_Code) - 1);


        }
        else
        {
            for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
            {
                if (ddlCentre.Items[CentreCnt].Selected == true)
                {
                    Centre_Code = Centre_Code + ddlCentre.Items[CentreCnt].Value + ",";
                    Centre_Name = Centre_Name + ddlCentre.Items[CentreCnt].Text + ",";
                }

            }
            //if (Strings.Right(Centre_Code, 1) == ",")
            //    Centre_Code = Strings.Left(Centre_Code, Strings.Len(Centre_Code) - 1);
            Centre_Code = Common.RemoveComma(Centre_Code);
            Centre_Name = Common.RemoveComma(Centre_Name);

        }
        lblFinal_Centre_Code.Text = Centre_Code;


        string Batch_Code = "";
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
                Batch_Code = Batch_Code + ddlBatch.Items[BatchCnt].Value + ",";
            }
            //if (Strings.Right(Centre_Code, 1) == ",")
            //    Centre_Code = Strings.Left(Centre_Code, Strings.Len(Centre_Code) - 1);

            Batch_Code = Common.RemoveComma(Batch_Code);
        }
        else
        {
            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                if (ddlBatch.Items[BatchCnt].Selected == true)
                {
                    Batch_Code = Batch_Code + ddlBatch.Items[BatchCnt].Value + ",";
                }
            }
            //if (Strings.Right(Centre_Code, 1) == ",")
            //    Centre_Code = Strings.Left(Centre_Code, Strings.Len(Centre_Code) - 1);
            Batch_Code = Common.RemoveComma(Batch_Code);
        }

        if (BatchSelCnt == 0)
        {
            if ((ddlReportType.SelectedItem.ToString() == "Test Ranking for - Test Conducted") || (ddlReportType.SelectedItem.ToString() == "Test Ranking for - Test Attended") ||
                (ddlReportType.SelectedItem.ToString() == "Test Ranking for - Board + Objective Conducted") || (ddlReportType.SelectedItem.ToString() == "Test Ranking for- Board + Objective Attended"))
            {
                Batch_Code = "";
            }

        }
        lblFinal_Batch_Code.Text = Batch_Code;

        if (string.IsNullOrEmpty(lblFinal_Test_Id.Text))
        {
            Show_Error_Success_Box("E", "0038");
            ddlTestName.Focus();
            return;
        }
    }




    protected void btnTestName_Click(object sender, System.EventArgs e)
    {
        FillDDL_TestName();
    }

    protected void ddlReportType_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlReportType.SelectedItem.Text == "Test Ranking for - Board + Objective Conducted" | ddlReportType.SelectedItem.Text == "Test Ranking for- Board + Objective Attended")
        {
            ddlTestCategory.Enabled = false;
            ddlTestType.Enabled = false;
            ddlTestName.Enabled = false;

            for (int SubjectCnt = 0; SubjectCnt <= ddlSubject.Items.Count - 1; SubjectCnt++)
            {
                if (ddlSubject.Items[SubjectCnt].Selected == true)
                {
                    ddlSubject.Items[SubjectCnt].Selected = false;
                }
            }

        }



        else
        {
            ddlTestCategory.Enabled = true;
            ddlTestType.Enabled = true;
            ddlTestName.Enabled = true;
            for (int SubjectCnt = 0; SubjectCnt <= ddlSubject.Items.Count - 1; SubjectCnt++)
            {
                if (ddlSubject.Items[SubjectCnt].Selected == true)
                {
                    ddlSubject.Items[SubjectCnt].Selected = false;
                }
            }

        }

        if (ddlReportType.SelectedItem.Text == "Re-Test Report")
        {
            ddltestname_1.Visible = true;
            ddlTestName.Visible = false;
            lbltestname_1.Visible = true;
            lbltestname.Visible = false;
            btnTestName.Visible = false;
        }
        else
        {
            ddlTestName.Visible = true;
            ddltestname_1.Visible = false;
            lbltestname_1.Visible = false;
            lbltestname.Visible = true;
            btnTestName.Visible = true;
        }

        //if (ddlReportType.SelectedItem.Text == "Concise Studentwise")
        //{
        //    Table_Row_5.Visible = true;

        //}
        //else
        //{
        //    Table_Row_5.Visible = false;
        //}
    }

    public Report_UC_SearchPanel()
    {
        Load += Page_Load;
    }

    public void ClearControl()
    {
        Clear_Error_Success_Box();
        ddlDivision.SelectedIndex = 0;
        ddlAcadYear.SelectedIndex = 0;
        ddlTestCategory.SelectedIndex = 0;
        ddlStandard.Items.Clear();
        ddlTestType.Items.Clear();
        ddlCentre.Items.Clear();
        ddlBatch.Items.Clear();
        ddlTestName.Items.Clear();
        ddltestname_1.Items.Clear();
        id_date_range_picker_1.Value = "";
        ddlReportType.SelectedIndex = 0;
        ddlSubject.Items.Clear();


    }

    public void ClearControl_MarksEntry()
    {
        Clear_Error_Success_Box();
        ddlDivision.SelectedIndex = 0;
        ddlAcadYear.SelectedIndex = 0;
        ddlTestCategory.SelectedIndex = 0;
        ddlStandard.Items.Clear();
        ddlTestType.Items.Clear();
        ddlCentre.Items.Clear();
        ddlBatch.Items.Clear();
        ddlTestName.Items.Clear();
        ddltestname_1.Items.Clear();
        id_date_range_picker_1.Value = "";
       


    }











}
