using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.IO;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

partial class Tran_ProcessStudentAnswer : System.Web.UI.Page
{

    protected void BtnSearch_Click(object sender, System.EventArgs e)
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

            if (ddlAcadyear.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0002");
                ddlAcadyear.Focus();
                return;
            }

            //If ddlTestMode.SelectedIndex = 0 Then
            //    Show_Error_Success_Box("E", "0011")
            //    ddlTestMode.Focus()
            //    Exit Sub
            //End If

            if (ddlTestCategory.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "0012");
                ddlTestCategory.Focus();
                return;
            }

            string StandardCode = "";
            int StdCnt = 0;
            int StdSelCnt = 0;
            for (StdCnt = 0; StdCnt <= ddlStandard.Items.Count - 1; StdCnt++)
            {
                if (ddlStandard.Items[StdCnt].Selected == true)
                {
                    StdSelCnt = StdSelCnt + 1;
                }
            }

            if (StdSelCnt == 0)
            {
                //When all is selected
                for (StdCnt = 0; StdCnt <= ddlStandard.Items.Count - 1; StdCnt++)
                {
                    StandardCode = StandardCode + ddlStandard.Items[StdCnt].Value + ",";
                }
                //if (Strings.Right(StandardCode, 1) == ",")
                //    StandardCode = Strings.Left(StandardCode, Strings.Len(StandardCode) - 1);
                StandardCode = Common.RemoveComma(StandardCode);
            }
            else
            {
                for (StdCnt = 0; StdCnt <= ddlStandard.Items.Count - 1; StdCnt++)
                {
                    if (ddlStandard.Items[StdCnt].Selected == true)
                    {
                        StandardCode = StandardCode + ddlStandard.Items[StdCnt].Value + ",";
                    }
                }
                //if (Strings.Right(StandardCode, 1) == ",")
                //    StandardCode = Strings.Left(StandardCode, Strings.Len(StandardCode) - 1);
                StandardCode = Common.RemoveComma(StandardCode);
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
                //if (Strings.Right(TestType_ID, 1) == ",")
                //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);
                TestType_ID = Common.RemoveComma(TestType_ID);
            }

            ControlVisibility("Result");

            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string YearName = null;
            YearName = ddlAcadyear.SelectedItem.ToString();

            string TestName = null;
            if (string.IsNullOrEmpty(txtTestName.Text.Trim()))
            {
                TestName = "%";
            }
            else
            {
                TestName = "%" + txtTestName.Text.Trim();
            }

            string FromDate = null;
            string ToDate = null;
            string DateRange = dtrange.Value;

            if (DateRange != "")
            {
                FromDate = DateRange.Substring(0, 10);
            }
            //FromDate = Strings.Left(DateRange, 10);
            if (string.IsNullOrEmpty(FromDate))
                FromDate = System.DateTime.Now.ToString("dd MMM yyyy");
            if (DateRange != "")
            {
                ToDate = DateRange.Substring(DateRange.Length - 10);
            }
            //ToDate = Strings.Right(DateRange, 10);
            if (string.IsNullOrEmpty(ToDate))
                ToDate = System.DateTime.Now.ToString("dd MMM yyyy");


            DataSet dsGrid = ProductController.GetTest_AnswerUploadHistory(DivisionCode, YearName, StandardCode, "01", ddlTestCategory.SelectedValue, TestType_ID, TestName, FromDate, ToDate, 1);
            dlGridDisplay.DataSource = dsGrid;
            dlGridDisplay.DataBind();

            dlGridExport.DataSource = dsGrid;
            dlGridExport.DataBind();

            lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }


    

    protected void Page_Load(object sender, System.EventArgs e)
    {

        FileUpload1.Attributes["onchange"] = "UploadFile(this)";
        if (Path.GetFileName(FileUpload1.FileName) != "")
        {
            txtUploadedFileName.Text = Path.GetFileName(FileUpload1.FileName);
        }

        if (!IsPostBack)
        {
            ControlVisibility("Search");
            txtImportDate_Add.Text = System.DateTime.Now.ToString("dd MMM yyyy");
            try
            {
                FillDDL_TestTypes();
                FillDDL_Division();
                FillDDL_AcadYear();
                FillDDL_TestCategories();
            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.ToString());
                return;
            }
        }
    }



    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
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

        BindDDL(ddlTestType_Add, dsTestType, "TestType_Name", "TestType_Id");
        ddlTestType_Add.Items.Insert(0, "Select");
        ddlTestType_Add.SelectedIndex = 0;

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

        BindDDL(ddlDivision_Add, dsDivision, "Division_Name", "Division_Code");
        ddlDivision_Add.Items.Insert(0, "Select");
        ddlDivision_Add.SelectedIndex = 0;

    }

    private void FillDDL_AcadYear()
    {
        DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcadyear, dsAcadYear, "Description", "Id");
        ddlAcadyear.Items.Insert(0, "Select");
        ddlAcadyear.SelectedIndex = 0;

        BindDDL(ddlAcadYear_Add, dsAcadYear, "Description", "Id");
        ddlAcadYear_Add.Items.Insert(0, "Select");
        ddlAcadYear_Add.SelectedIndex = 0;

    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = true;
            DivEditPanel.Visible = false;
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = true;
            DivEditPanel.Visible = false;
        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
            DivEditPanel.Visible = false;
        }
        else if (Mode == "Edit")
        {
            DivAddPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            DivEditPanel.Visible = true;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
        }
        Clear_Error_Success_Box();
    }

    protected void BtnCloseAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Result");
        Clear_AddPanel();
    }


    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        try
        {


            if (e.CommandName == "ViewFile")
            {
                //DivAddPanel.Visible = True
                //DivResultPanel.Visible = False
                //DivSearchPanel.Visible = False
                //BtnShowSearchPanel.Visible = True
                string CurFileName = null;
                CurFileName = e.CommandArgument.ToString().Replace("%", "_");//Strings.Replace(e.CommandArgument, "%", "_");
                if (!string.IsNullOrEmpty(CurFileName))
                {
                    string Path1 = Server.MapPath(("~/UserUploads/CSV_ResultFiles/Processed_Files/" + CurFileName + ".csv"));
                    string Path2 = "UserUploads/CSV_ResultFiles/Processed_Files/" + CurFileName + ".csv";
                    if (System.IO.File.Exists(Path1) == true)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "open('" + Path2 + "');", true);
                    }
                    else
                    {
                        Show_Error_Success_Box("E", "0037");
                        ddlAcadyear.Focus();
                        return;
                    }

                }
                else
                {
                    Show_Error_Success_Box("E", "0037");
                    ddlAcadyear.Focus();
                    return;
                }
            }
            else if (e.CommandName == "ViewResult")
            {
                ControlVisibility("Edit");

                string PKey = null;
                PKey = e.CommandArgument.ToString();

                FillResult_View(PKey);

            }
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }

    private void FillResult_View(string PKey)
    {
        try
        {
            //Find Upload Result
            DataSet dsGrid = ProductController.GetTest_AnswerUploadHistory_ByPKey(PKey, 1);
            lblResult_RunPKey.Text = PKey;
            lblResult_PKey.Text = Convert.ToString(dsGrid.Tables[0].Rows[0]["TestPKey"]);
            lblResult_Division.Text = Convert.ToString(ddlDivision.SelectedItem);
            lblResult_AcadYear.Text = Convert.ToString(ddlAcadyear.SelectedItem);
            lblResult_Standard.Text = Convert.ToString(dsGrid.Tables[0].Rows[0]["Standard_Name"]);
            lblResult_FileName.Text = Convert.ToString(dsGrid.Tables[0].Rows[0]["Import_FileName"]);
            lblResult_TestType.Text = Convert.ToString(dsGrid.Tables[0].Rows[0]["TestType_Name"]);
            lblResult_TestName.Text = Convert.ToString(dsGrid.Tables[0].Rows[0]["Test_Name"]);
            lblResult_ConductNo.Text = Convert.ToString(dsGrid.Tables[0].Rows[0]["Conduct_No"]);
            lblResult_QueNo.Text = Convert.ToString(dsGrid.Tables[0].Rows[0]["QuestionCount"]);
            lblResult_QPSetCount.Text = Convert.ToString(dsGrid.Tables[0].Rows[0]["QPSetCnt"]);
            lblResult_ImportDate.Text = Convert.ToString(dsGrid.Tables[0].Rows[0]["Import_Run_Date"]);
            lblResult_IDColumn.Text = Convert.ToString(dsGrid.Tables[0].Rows[0]["Student_ID_Column_Name"]);


            DLResult_Correct.DataSource = dsGrid.Tables[1];
            DLResult_Correct.DataBind();

            lblResult_Success.Text = Convert.ToString(DLResult_Correct.Items.Count);
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void BtnAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Add");
        ResultRow.Visible = false;
    }


    protected void ddlDivision_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard_Add();
        Clear_Error_Success_Box();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
    }

    private void FillDDL_Standard()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadyear.SelectedItem.ToString();

        DataSet dsStandard = ProductController.GetAllActive_Standard_ForYear(Div_Code, YearName);
        BindListBox(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");

    }

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    protected void ddlAcadYear_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
    }

    private void FillDDL_Standard_Add()
    {
        string Div_Code = null;
        Div_Code = ddlDivision_Add.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear_Add.SelectedItem.ToString();

        DataSet dsStandard = ProductController.GetAllActive_Standard_ForYear(Div_Code, YearName);
        BindDDL(ddlStandard_Add, dsStandard, "Standard_Name", "Standard_Code");
        ddlStandard_Add.Items.Insert(0, "Select");
        ddlStandard_Add.SelectedIndex = 0;

        ddlTestName_Add.Items.Clear();
        txtQPSetCount_Add.Text = "";
        txtQueCount_Add.Text = "";
        icon_NegativeMarking_Add.Visible = false;
    }

    private void FillDDL_TestName_Add()
    {
        if (ddlDivision_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivision_Add.Focus();
            return;
        }

        if (ddlAcadYear_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0002");
            ddlAcadYear_Add.Focus();
            return;
        }

        if (ddlStandard_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0003");
            ddlStandard_Add.Focus();
            return;
        }

        if (ddlTestType_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0013");
            ddlTestType_Add.Focus();
            return;
        }

        string DivisionCode = null;
        DivisionCode = ddlDivision_Add.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear_Add.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard_Add.SelectedValue;

        string TestTypeCode = null;
        TestTypeCode = ddlTestType_Add.SelectedValue;

        string TestMode = null;
        TestMode = "01";
        //Offline

        string TestCategory = null;
        TestCategory = "002";
        //MCQ

        DataSet dsTestName = ProductController.GetTestMasterBy_Division_Year_Standard(DivisionCode, YearName, StandardCode, TestMode, TestCategory, TestTypeCode, "%", 2);
        BindDDL(ddlTestName_Add, dsTestName, "Test_Name", "PKey");
        ddlTestName_Add.Items.Insert(0, "Select");
        ddlTestName_Add.SelectedIndex = 0;
    }

    protected void ddlAcadYear_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard_Add();
        Clear_Error_Success_Box();
    }

    protected void ddlStandard_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_TestName_Add();
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

    private void Clear_AddPanel()
    {
        ddlDivision_Add.SelectedIndex = 0;
        ddlAcadYear_Add.SelectedIndex = 0;
        ddlTestType_Add.SelectedIndex = 0;

        ddlStandard_Add.Items.Clear();
        ddlTestName_Add.Items.Clear();
        ddlConductNo_Add.Items.Clear();

        dlErrorResult.DataSource = null;
        dlErrorResult.DataBind();

        dlCorrectResult.DataSource = null;
        dlCorrectResult.DataBind();

        dlWarningResult.DataSource = null;
        dlWarningResult.DataBind();

        BtnSave.Visible = false;

        lblSuccessRecCnt.Text = "0";
        lblWarnRecCnt.Text = "0";
        lblErrorRecCnt.Text = "0";

        lstPresentSBEntryCode_Add.Items.Clear();
        lstPresentSBEntryCodeCentre_Add.Items.Clear();

        txtQPSetCount_Add.Text = "";
        txtQueCount_Add.Text = "";
        icon_NegativeMarking_Add.Visible = false;
        txtStudentIDColumn_Add.Text = "";

    }

    protected void BtnUpload_Click(object sender, System.EventArgs e)
    {
        //Validation

        lblSuccessRecCnt.Text = "0";
        lblErrorRecCnt.Text = "0";
        lblWarnRecCnt.Text = "0";
        BtnSave.Visible = false;

        if (ddlDivision_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivision_Add.Focus();
            return;
        }

        if (ddlAcadYear_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0002");
            ddlAcadYear_Add.Focus();
            return;
        }

        if (ddlStandard_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0003");
            ddlStandard_Add.Focus();
            return;
        }

        if (ddlTestType_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0013");
            ddlTestType_Add.Focus();
            return;
        }

        if (ddlTestName_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0016");
            ddlTestName_Add.Focus();
            return;
        }

        if (ddlConductNo_Add.Items.Count == 0)
        {
            Show_Error_Success_Box("E", "0028");
            ddlConductNo_Add.Focus();
            return;
        }

        //Check if correct column number is specified or not
        string UserColNo = null;
        UserColNo = txtStudentIDColumn_Add.Text;

        if (string.IsNullOrEmpty(UserColNo))
        {
            Show_Error_Success_Box("E", "0025");
            txtStudentIDColumn_Add.Focus();
            return;
        }

        int ColNo = 0;

        ColNo = NumberFromExcelColumn(UserColNo.Trim());


        //if (Convert.ToInt32(UserColNo.Length) == 2)
        //{
        //    ColNo = Common.ExcelColumnNameToNumber(UserColNo);
            
        //    //ColNo = 34;// (Convert.ToInt32(Convert.ToInt32(UserColNo.Substring(0, 1)) - 64) * 26) + ((Convert.ToInt32(UserColNo.Substring(UserColNo.Length - 1)) - 64) * 1);
        //    // ((Strings.Asc(Strings.Left(UserColNo, 1)) - 64) * 26) + ((Strings.Asc(Strings.Right(UserColNo, 1)) - 64) * 1);
        //}
        //else if (Convert.ToInt32(UserColNo.Length) == 1)
        //{
        //    ColNo = Convert.ToInt32(Convert.ToInt32(UserColNo.Substring(0, 1) )- 64);
        //        //((Strings.Asc(Strings.Left(UserColNo, 1)) - 64));  Need to chk by jayant
        //}
        
        ColNo = ColNo - 1;

        if (ColNo == 0)
        {
            Show_Error_Success_Box("E", "0026");
            lblSuccessRecCnt.Text = "0";
            lblErrorRecCnt.Text = "0";
            txtStudentIDColumn_Add.Focus();
            return;
        }

        int QueCount = 0;
        QueCount = Convert.ToInt32(txtQueCount_Add.Text);
        //Question count defined for the test

        int SetCount = 0;
        SetCount = Convert.ToInt32(txtQPSetCount_Add.Text);
        //Number of qp sets defined for the test

        string FullName = null;
        string AllSbEntryCode="";

        Clear_Error_Success_Box();

        if (!string.IsNullOrEmpty(FileUpload1.FileName))
        {
            txtFileName_Add.Text = Path.GetFileName(FileUpload1.FileName);
            FullName = Server.MapPath("~/UserUploads/CSV_ResultFiles") + "\\" + Path.GetFileName(FileUpload1.FileName);

            //Check if file is already uploaded or not
            //FileNameEx = ClX.FindImportExcalFile(LitUploadfile.Text).Rows(0)
            bool DupliFileName = false;
            //If FileNameEx.Item(0) <> 0 Then
            //    DupliFileName = True
            //Else
            //    DupliFileName = False
            //End If

            string strFileType = Path.GetExtension(FileUpload1.FileName).ToLower();
            if (strFileType != ".csv")
            {
                Show_Error_Success_Box("E", "0024");
                lblSuccessRecCnt.Text = "0";
                lblErrorRecCnt.Text = "0";
                return;
            }

            if (DupliFileName == true)
            {
                Show_Error_Success_Box("E", "0023");
                lblSuccessRecCnt.Text = "0";
                lblErrorRecCnt.Text = "0";

            }
            else
            {
                FileUpload1.SaveAs(FullName);

                DataTable dtRaw = new DataTable();
                DataTable dtCorrectEntry = new DataTable();
                DataTable dtErrorEntry = new DataTable();
                DataTable dtWarningEntry = new DataTable();

                //create object for CSVReader and pass the stream
                CSVReader reader = new CSVReader(FileUpload1.PostedFile.InputStream);
                //get the header
                string[] headers = reader.GetCSVLine();

                //add headers
                foreach (string strHeader in headers)
                {
                    dtRaw.Columns.Add(strHeader);
                }
                DataRow NewRow = null;
                int CurRowNo = 0;

                //Create datatable to hold result temporarily
                var _with1 = dtCorrectEntry;
                _with1.Columns.Add("ExcelRowNo");
                _with1.Columns.Add("RollNo");
                _with1.Columns.Add("SBEntryCode");
                _with1.Columns.Add("CentreName");
                _with1.Columns.Add("CentreCode");
                _with1.Columns.Add("Marks");
                _with1.Columns.Add("TestNumber");
                _with1.Columns.Add("Status");
                _with1.Columns.Add("Remarks");
                _with1.Columns.Add("TestCode");
                _with1.Columns.Add("Set_Number");
                _with1.Columns.Add("Answer_Key");

                var _with2 = dtErrorEntry;
                _with2.Columns.Add("ExcelRowNo");
                _with2.Columns.Add("RollNo");
                _with2.Columns.Add("SBEntryCode");
                _with2.Columns.Add("CentreName");
                _with2.Columns.Add("CentreCode");
                _with2.Columns.Add("Marks");
                _with2.Columns.Add("TestNumber");
                _with2.Columns.Add("Status");
                _with2.Columns.Add("Remarks");
                _with2.Columns.Add("TestCode");
                _with2.Columns.Add("Set_Number");
                _with2.Columns.Add("Answer_Key");

                var _with3 = dtWarningEntry;
                _with3.Columns.Add("ExcelRowNo");
                _with3.Columns.Add("RollNo");
                _with3.Columns.Add("SBEntryCode");
                _with3.Columns.Add("CentreName");
                _with3.Columns.Add("CentreCode");
                _with3.Columns.Add("Marks");
                _with3.Columns.Add("TestNumber");
                _with3.Columns.Add("Status");
                _with3.Columns.Add("Remarks");
                _with3.Columns.Add("TestCode");
                _with3.Columns.Add("Set_Number");
                _with3.Columns.Add("Answer_Key");

                string[] data = null;
                string CurRollNo = "";
                string CurTestName = "";
                int CurSetNo = 0;
                int StdCnt = 0;
                bool StudRollNoFoundFlag = false;
                string CurSBEntryCode = null;
                string CurCentreName = null;
                string CurCentreCode = null;
                string CurAnswerKey = null;
                int CQNo = 0;
                string CurQueAns = null;

                data = reader.GetCSVLine();
                //Read first line
                CurRowNo = 1;
                while (data != null)
                {
                    dtRaw.Rows.Add(data);
                    CurRollNo = "";
                    CurTestName = "";
                    CurSetNo = 0;
                    StudRollNoFoundFlag = false;
                    CurSBEntryCode = "";
                    CurCentreCode = "";
                    CurCentreName = "";
                    CurAnswerKey = "";

                    CurRollNo = dtRaw.Rows[dtRaw.Rows.Count - 1][ColNo].ToString();
                    CurTestName = dtRaw.Rows[dtRaw.Rows.Count - 1][ColNo + 2].ToString();
                    CurSetNo = Convert.ToInt32("" + dtRaw.Rows[dtRaw.Rows.Count - 1][ColNo + 3].ToString());
                    CurCentreCode = dtRaw.Rows[dtRaw.Rows.Count - 1][ColNo + 1].ToString();

                    for (CQNo = 6; CQNo <= QueCount+5; CQNo++)
                    {
                        CurQueAns = dtRaw.Rows[dtRaw.Rows.Count - 1][CQNo].ToString();
                        CurQueAns = CurQueAns.Trim();
                        CurQueAns = CurQueAns.Replace(" ", "N");//Strings.Replace(CurQueAns, " ", "N");                       
                        if (CurQueAns == "")
                        {
                            CurQueAns =  "N";
                        }
                       
                        //Replace not attempted with N
                        CurQueAns = CurQueAns.Replace("*", "M");//Strings.Replace(CurQueAns, "*", "M");
                        //Replace * with M indicating Multiple attempt
                        CurAnswerKey = CurAnswerKey + CurQueAns + ",";
                        CurQueAns = "";
                    }

                    CurAnswerKey = Common.RemoveComma(CurAnswerKey);

                    //Check for duplicate rollno in correct records
                    DataRow[] DupliRollNoRow = null;
                    DupliRollNoRow = dtCorrectEntry.Select("RollNo ='" + CurRollNo + "'");
                    if (DupliRollNoRow.Length > 0)
                    {
                        //Add entry in Error Datatable
                        NewRow = dtWarningEntry.NewRow();
                        NewRow["ExcelRowNo"] = CurRowNo;
                        NewRow["RollNo"] = CurRollNo;
                        NewRow["SBEntryCode"] = "";
                        NewRow["CentreName"] = "";
                        NewRow["CentreCode"] = "";
                        NewRow["Marks"] = "";
                        NewRow["TestNumber"] = CurTestName;
                        NewRow["Status"] = "WARNING";
                        NewRow["Remarks"] = "Duplicate Roll No";
                        NewRow["Set_Number"] = CurSetNo;
                        NewRow["TestCode"] = CurSetNo;
                        NewRow["Answer_Key"] = CurAnswerKey;

                        dtWarningEntry.Rows.Add(NewRow);

                        goto NextCSVLine;
                    }

                    //Check for duplicate rollno in error records
                    DupliRollNoRow = dtErrorEntry.Select("RollNo ='" + CurRollNo + "'");
                    if (DupliRollNoRow.Length > 0)
                    {
                        //Add entry in Error Datatable
                        NewRow = dtWarningEntry.NewRow();
                        NewRow["ExcelRowNo"] = CurRowNo;
                        NewRow["RollNo"] = CurRollNo;
                        NewRow["SBEntryCode"] = "";
                        NewRow["CentreName"] = "";
                        NewRow["CentreCode"] = "";
                        NewRow["Marks"] = "";
                        NewRow["TestNumber"] = CurTestName;
                        NewRow["Status"] = "WARNING";
                        NewRow["Remarks"] = "Duplicate Roll No";
                        NewRow["Set_Number"] = CurSetNo;
                        NewRow["TestCode"] = CurSetNo;
                        NewRow["Answer_Key"] = CurAnswerKey;
                        dtWarningEntry.Rows.Add(NewRow);

                        goto NextCSVLine;
                    }

                    //Check for duplicate rollno in warning records
                    DupliRollNoRow = dtWarningEntry.Select("RollNo ='" + CurRollNo + "'");
                    if (DupliRollNoRow.Length > 0)
                    {
                        //Add entry in Error Datatable
                        NewRow = dtWarningEntry.NewRow();
                        NewRow["ExcelRowNo"] = CurRowNo;
                        NewRow["RollNo"] = CurRollNo;
                        NewRow["SBEntryCode"] = "";
                        NewRow["CentreName"] = "";
                        NewRow["CentreCode"] = "";
                        NewRow["Marks"] = "";
                        NewRow["TestNumber"] = CurTestName;
                        NewRow["Status"] = "WARNING";
                        NewRow["Remarks"] = "Duplicate Roll No";
                        NewRow["Set_Number"] = CurSetNo;
                        NewRow["TestCode"] = CurSetNo;
                        NewRow["Answer_Key"] = CurAnswerKey;
                        dtWarningEntry.Rows.Add(NewRow);

                        goto NextCSVLine;
                    }
                    ///''''''''''

                    //Check if Roll No is in Correct format
                    if(CurRollNo.IndexOf("*") != -1)
                    {

                      //Add entry in Error Datatable
                        NewRow = dtWarningEntry.NewRow();
                        NewRow["ExcelRowNo"] = CurRowNo;
                        NewRow["RollNo"] = CurRollNo;
                        NewRow["SBEntryCode"] = "";
                        NewRow["CentreName"] = "";
                        NewRow["CentreCode"] = "";
                        NewRow["Marks"] = "";
                        NewRow["TestNumber"] = CurTestName;
                        NewRow["Status"] = "WARNING";
                        NewRow["Remarks"] = "* in Record";
                        NewRow["Set_Number"] = CurSetNo;
                        NewRow["TestCode"] = CurSetNo;
                        NewRow["Answer_Key"] = CurAnswerKey;
                        dtWarningEntry.Rows.Add(NewRow);

                        goto NextCSVLine;
                    }
                    ///''''''''''

                    //Check for valid Test Name
                    if (CurTestName != ddlTestName_Add.SelectedItem.ToString())
                    {
                        //Add entry in Error Datatable
                        NewRow = dtErrorEntry.NewRow();
                        NewRow["ExcelRowNo"] = CurRowNo;
                        NewRow["RollNo"] = CurRollNo;
                        NewRow["SBEntryCode"] = "";
                        NewRow["CentreName"] = "";
                        NewRow["CentreCode"] = "";
                        NewRow["Marks"] = "";
                        NewRow["TestNumber"] = CurTestName;
                        NewRow["Status"] = "ERROR";
                        NewRow["Remarks"] = "Invalid Test Name";
                        NewRow["Set_Number"] = CurSetNo;
                        NewRow["TestCode"] = CurSetNo;
                        NewRow["Answer_Key"] = CurAnswerKey;
                        dtErrorEntry.Rows.Add(NewRow);

                        goto NextCSVLine;
                    }

                    ///''''''''''

                    //Check if Set Number is present
                    if (CurSetNo > 0 & CurSetNo <= SetCount)
                    {
                        //Valid entry
                    }
                    else
                    {
                        //Add entry in Error Datatable
                        NewRow = dtErrorEntry.NewRow();
                        NewRow["ExcelRowNo"] = CurRowNo;
                        NewRow["RollNo"] = CurRollNo;
                        NewRow["SBEntryCode"] = "";
                        NewRow["CentreName"] = "";
                        NewRow["CentreCode"] = "";
                        NewRow["Marks"] = "";
                        NewRow["TestNumber"] = CurTestName;
                        NewRow["Status"] = "ERROR";
                        NewRow["Remarks"] = "Invalid Set Number";
                        NewRow["Set_Number"] = CurSetNo;
                        NewRow["TestCode"] = CurSetNo;
                        NewRow["Answer_Key"] = CurAnswerKey;
                        dtErrorEntry.Rows.Add(NewRow);

                        goto NextCSVLine;
                    }

                    ///''''''''''
                    //Check if rollnumber is a valid roll number based on attendance marked by the centre for the test
                    StudRollNoFoundFlag = false;
                    for (StdCnt = 0; StdCnt <= lstPresentSBEntryCode_Add.Items.Count - 1; StdCnt++)
                    {
                        if (lstPresentSBEntryCode_Add.Items[StdCnt].ToString().Trim() == CurRollNo & lstPresentSBEntryCodeCentre_Add.Items[StdCnt].Value.Trim() == CurCentreCode)
                        {
                            StudRollNoFoundFlag = true;
                            CurSBEntryCode = lstPresentSBEntryCode_Add.Items[StdCnt].Value.ToString().Trim();
                            CurCentreCode = lstPresentSBEntryCodeCentre_Add.Items[StdCnt].Value.ToString().Trim();
                            CurCentreName = lstPresentSBEntryCodeCentre_Add.Items[StdCnt].ToString().Trim();
                            AllSbEntryCode=AllSbEntryCode + CurSBEntryCode + ",";
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }



                    if (StudRollNoFoundFlag == false)//if the roll no not found for this lstPresentSBEntryCode_Add Array then check the roll no for this center and if it is exist then insert attendance to this RollNo
                    {
                        Label lblHeader_User_Code = default(Label);
                        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");
                        //if the roll no not found for this lstPresentSBEntryCode_Add Array then check the roll no for this center and if it is exist then insert attendance to this RollNo
                        DataSet dsTest1 = ProductController.GetTestStudentDetail_ByPKey(CurRollNo, CurCentreCode, ddlTestName_Add.SelectedValue, Convert.ToInt32(ddlConductNo_Add.SelectedValue), lblHeader_User_Code.Text, 1);
                        if (dsTest1 != null)
                        {
                            if (dsTest1.Tables[0].Rows.Count > 0)
                            {
                                StudRollNoFoundFlag = true;
                                CurSBEntryCode = dsTest1.Tables[0].Rows[0]["SBEntryCode"].ToString();
                                CurCentreCode = dsTest1.Tables[0].Rows[0]["Centre_Code"].ToString(); 
                                CurCentreName = dsTest1.Tables[0].Rows[0]["Centre_Name"].ToString(); 
                                AllSbEntryCode=AllSbEntryCode + CurSBEntryCode + ",";
                            }
                            else
                            {
                                //Student not found hence add entry in warning table
                                //Add entry in Error Datatable
                                NewRow = dtWarningEntry.NewRow();
                                NewRow["ExcelRowNo"] = CurRowNo;
                                NewRow["RollNo"] = CurRollNo;
                                NewRow["SBEntryCode"] = "";
                                NewRow["CentreName"] = "";
                                NewRow["CentreCode"] = "";
                                NewRow["Marks"] = "";
                                NewRow["TestNumber"] = CurTestName;
                                NewRow["Status"] = "WARNING";
                                NewRow["Remarks"] = "Roll No not found";//Roll no not found for this div,Acadyear,course,center,batch which test scheduled for this test
                                NewRow["Set_Number"] = CurSetNo;
                                NewRow["TestCode"] = CurSetNo;
                                NewRow["Answer_Key"] = CurAnswerKey;
                                dtWarningEntry.Rows.Add(NewRow);

                                goto NextCSVLine;
                            }
                        }
                        else
                        {
                            //Student not found hence add entry in warning table
                            //Add entry in Warning Datatable
                            NewRow = dtWarningEntry.NewRow();
                            NewRow["ExcelRowNo"] = CurRowNo;
                            NewRow["RollNo"] = CurRollNo;
                            NewRow["SBEntryCode"] = "";
                            NewRow["CentreName"] = "";
                            NewRow["CentreCode"] = "";
                            NewRow["Marks"] = "";
                            NewRow["TestNumber"] = CurTestName;
                            NewRow["Status"] = "WARNING";
                            NewRow["Remarks"] = "Attendance not found";
                            NewRow["Set_Number"] = CurSetNo;
                            NewRow["TestCode"] = CurSetNo;
                            NewRow["Answer_Key"] = CurAnswerKey;
                            dtWarningEntry.Rows.Add(NewRow);

                            goto NextCSVLine;
                        }
                    }


                    ///''''''''''


                    //Everything is OK hence add row in correct Datatable
                    NewRow = dtCorrectEntry.NewRow();
                    NewRow["ExcelRowNo"] = CurRowNo;
                    NewRow["RollNo"] = CurRollNo;
                    NewRow["SBEntryCode"] = CurSBEntryCode;
                    NewRow["CentreName"] = CurCentreName;
                    NewRow["CentreCode"] = CurCentreCode;
                    NewRow["Marks"] = "";
                    NewRow["TestNumber"] = CurTestName;
                    NewRow["Status"] = "OK";
                    NewRow["Remarks"] = "";
                    NewRow["Set_Number"] = CurSetNo;
                    NewRow["TestCode"] = "";
                    NewRow["Answer_Key"] = CurAnswerKey;
                    dtCorrectEntry.Rows.Add(NewRow);
                NextCSVLine:


                    data = reader.GetCSVLine();
                    //Read next line
                    CurRowNo = CurRowNo + 1;
                }

                //bind gridview
                //gv.DataSource = dtRaw
                //gv.DataBind()

                dlCorrectResult.DataSource = dtCorrectEntry;
                dlCorrectResult.DataBind();

                dlErrorResult.DataSource = dtErrorEntry;
                dlErrorResult.DataBind();

                dlWarningResult.DataSource = dtWarningEntry;
                dlWarningResult.DataBind();

                lblSuccessRecCnt.Text =Convert.ToString(dtCorrectEntry.Rows.Count);
                lblErrorRecCnt.Text =Convert.ToString(dtErrorEntry.Rows.Count);
                lblWarnRecCnt.Text =Convert.ToString(dtWarningEntry.Rows.Count);

                //AllSbEntryCode=AllSbEntryCode + CurSBEntryCode + ",";
                Label lblHeader_User_Code1 = default(Label);
                lblHeader_User_Code1 = (Label)Master.FindControl("lblHeader_User_Code");
                //Sbentrycode not comes in CSV upload and this sbentrycode attendance is not marked then mark the Absent attendance
                DataSet dsTest2 = ProductController.Update_AutoTestStudentAttendance_ByPKey(AllSbEntryCode,ddlTestName_Add.SelectedValue, Convert.ToInt32(ddlConductNo_Add.SelectedValue), lblHeader_User_Code1.Text, 1);
                
            }


            ResultRow.Visible = true;
            if (Convert.ToInt32(lblErrorRecCnt.Text) == 0)
            {
                BtnSave.Visible = true;
            }
            else
            {
                BtnSave.Visible = false;
            }
        }
        else
        {
            Show_Error_Success_Box("E", "0022");
        }



    }

    protected void ddlTestType_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_TestName_Add();
        Clear_Error_Success_Box();
    }

    protected void ddlTestName_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillTestMasterDetails(ddlTestName_Add.SelectedValue);
    }

    protected void ddlConductNo_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillSbEntrycode(ddlTestName_Add.SelectedValue);
    }

    private void FillTestMasterDetails(string PKey)
    {
        try
        {
            ddlConductNo_Add.Items.Clear();
            lstPresentSBEntryCode_Add.Items.Clear();
            lstPresentSBEntryCodeCentre_Add.Items.Clear();
            icon_NegativeMarking_Add.Visible = false;
            txtQPSetCount_Add.Text = "";
            txtQueCount_Add.Text = "";

            DataSet dsTest = ProductController.GetTestMasterBY_PKey(PKey, 1);

            if (dsTest.Tables[0].Rows.Count > 0)
            {
                //lblSubject_Add.Text = dsTest.Tables(0).Rows(0)("Subjects")
                txtQPSetCount_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["QPSetCnt"]);
                txtQueCount_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["QuestionCount"]);

                int ConductCount = 0;
                ConductCount = Convert.ToInt32(dsTest.Tables[3].Rows[0]["Conduct_No"]);

                if (Convert.ToInt32(dsTest.Tables[0].Rows[0]["NegativeMarkingFlag"]) == 1)
                {
                    icon_NegativeMarking_Add.Visible = true;
                }
                else
                {
                    icon_NegativeMarking_Add.Visible = false;
                }


                for (int cnt = 1; cnt <= ConductCount; cnt++)
                {
                    ddlConductNo_Add.Items.Add(Convert.ToString(cnt));
                }


                int Conduct_No = 0;
                Conduct_No = Convert.ToInt32(ddlConductNo_Add.SelectedItem.ToString());
                DataSet dsTest1 = ProductController.GetTestPresentStudent_ByPKey(PKey, Conduct_No, 1);

                lstPresentSBEntryCode_Add.DataSource = dsTest1;
                lstPresentSBEntryCode_Add.DataTextField = "RollNo";
                lstPresentSBEntryCode_Add.DataValueField = "SBEntryCode";
                lstPresentSBEntryCode_Add.DataBind();

                lstPresentSBEntryCodeCentre_Add.DataSource = dsTest1;
                lstPresentSBEntryCodeCentre_Add.DataTextField = "Centre_Name";
                lstPresentSBEntryCodeCentre_Add.DataValueField = "Centre_Code";
                lstPresentSBEntryCodeCentre_Add.DataBind();
            }
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }

    private void FillSbEntrycode(string PKey)
    {
        try
        {
           
            lstPresentSBEntryCode_Add.Items.Clear();
            lstPresentSBEntryCodeCentre_Add.Items.Clear();
            icon_NegativeMarking_Add.Visible = false;
          

            DataSet dsTest = ProductController.GetTestMasterBY_PKey(PKey, 1);

            if (dsTest.Tables[0].Rows.Count > 0)
            {
                //lblSubject_Add.Text = dsTest.Tables(0).Rows(0)("Subjects")                 

                int Conduct_No = 0;
                Conduct_No = Convert.ToInt32(ddlConductNo_Add.SelectedItem.ToString());
                DataSet dsTest1 = ProductController.GetTestPresentStudent_ByPKey(PKey, Conduct_No, 1);

                lstPresentSBEntryCode_Add.DataSource = dsTest1;
                lstPresentSBEntryCode_Add.DataTextField = "RollNo";
                lstPresentSBEntryCode_Add.DataValueField = "SBEntryCode";
                lstPresentSBEntryCode_Add.DataBind();

                lstPresentSBEntryCodeCentre_Add.DataSource = dsTest1;
                lstPresentSBEntryCodeCentre_Add.DataTextField = "Centre_Name";
                lstPresentSBEntryCodeCentre_Add.DataValueField = "Centre_Code";
                lstPresentSBEntryCodeCentre_Add.DataBind();
            }
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }


    protected void BtnSave_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
        try
        {

            //Validation
            if (Convert.ToInt32(lblSuccessRecCnt.Text) + Convert.ToInt32(lblWarnRecCnt.Text) == 0)
            {
                Show_Error_Success_Box("E", "0027");
                btnUpload.Focus();
                return;
            }




            //Save
            string ResultId = null;
            int ResultId1 = 0;
            int ResultId2 = 0;

            string TestPKey = null;
            int Conduct_Number = 0;
            string Import_FileName = null;
            string Student_ID_Column_Name = null;
            int Correct_Record_Cnt = 0;
            int Warning_Record_Cnt = 0;

            TestPKey = ddlTestName_Add.SelectedValue.ToString();
            Conduct_Number = Convert.ToInt32(ddlConductNo_Add.SelectedItem.ToString());
            Import_FileName = txtFileName_Add.Text;
            Student_ID_Column_Name = txtStudentIDColumn_Add.Text;
            Correct_Record_Cnt = Convert.ToInt32(lblSuccessRecCnt.Text);
            Warning_Record_Cnt = Convert.ToInt32(lblWarnRecCnt.Text);

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;

            //Check if file with same name is already uploaded or not


            ResultId = ProductController.InsertStudent_Answer_Import(TestPKey, Conduct_Number, Import_FileName, Student_ID_Column_Name, Correct_Record_Cnt, Warning_Record_Cnt, CreatedBy);

            if (ResultId == "Dupli")
            {
                Show_Error_Success_Box("E", "0023");
                btnUpload.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(ResultId))
            {
                //Save Correct records
                foreach (DataListItem dtlItem in dlCorrectResult.Items)
                {
                    Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
                    Label lblAnswerKey = (Label)dtlItem.FindControl("lblAnswerKey");
                    Label lblCentreCode = (Label)dtlItem.FindControl("lblCentreCode");
                    Label lblSetNumber = (Label)dtlItem.FindControl("lblSetNumber");

                    ResultId1 = ProductController.InsertStudent_Answer_Import_StudentAnswerKey(TestPKey, lblCentreCode.Text, Conduct_Number, lblSBEntryCode.Text, Convert.ToInt32(lblSetNumber.Text), ref ResultId, lblAnswerKey.Text, CreatedBy);
                }

                //Save Warning records
                foreach (DataListItem dtlItem in dlWarningResult.Items)
                {
                    Label lblRollNo = (Label)dtlItem.FindControl("lblRollNo");
                    Label lblRemarks = (Label)dtlItem.FindControl("lblRemarks");
                    Label lblRowNo = (Label)dtlItem.FindControl("lblRowNo");
                    Label lblSetNumber = (Label)dtlItem.FindControl("lblSetNumber");
                    string sRollNo = lblRollNo.Text;
                    ResultId2 = ProductController.InsertStudent_Answer_Import_Error_Item(TestPKey, Conduct_Number, ResultId, Convert.ToInt32(lblRowNo.Text), ref sRollNo, lblSetNumber.Text, lblRemarks.Text);

                }



                //Move file to Processed File location and rename it with Test PKey + conduct NO + ResultId
                string NewFileName = null;
                string CurFileName = null;

                CurFileName = Server.MapPath("~/UserUploads/CSV_ResultFiles/" + Import_FileName);

                string NewCSVName = null;
                NewCSVName = (TestPKey + "%" + Conduct_Number.ToString() + "%" + ResultId + ".csv").Replace("%", "_"); //Strings.Replace(TestPKey + "%" + Conduct_Number.ToString() + "%" + ResultId + ".csv", "%", "_");

                NewFileName = Server.MapPath("~/UserUploads/CSV_ResultFiles/Processed_Files/" + NewCSVName);

                //Move file from Unprocessed to Processed
                File.Copy(CurFileName, NewFileName, true);

                //Run the background process
                ResultId = Convert.ToString(ProductController.InsertStudent_Answer_Import_Background_Process(TestPKey, CreatedBy));

                string ResultId123 = Convert.ToString(ProductController.AutoAttendance_Closure(TestPKey,Conduct_Number, CreatedBy));
                

                ControlVisibility("Result");
                BtnSearch_Click(sender, e);
                Show_Error_Success_Box("S", "0000");
                Clear_AddPanel();
            }
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }

    protected void btnResult_Close_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Result");
    }

    protected void btnResult_Reprocess1_Click(object sender, System.EventArgs e)
    {
        //Run the background process
        string TestPKey = null;
        TestPKey = lblResult_PKey.Text;

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        int ResultId = 0;

        try
        {
            ResultId = ProductController.InsertStudent_Answer_Import_Background_Process(TestPKey, CreatedBy);

            FillResult_View(lblResult_RunPKey.Text);

            Show_Error_Success_Box("S", "0039");
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.Message);
        }
    }

    public Tran_ProcessStudentAnswer()
    {
        Load += Page_Load;
    }
    
    public  int NumberFromExcelColumn(string column)
    {
        int retVal = 0;
        string col = column.ToUpper();
        for (int iChar = col.Length - 1; iChar >= 0; iChar--)
        {
            char colPiece = col[iChar];
            int colNum = colPiece - 64;
            retVal = retVal + colNum * (int)Math.Pow(26, col.Length - (iChar + 1));
        }
        return retVal;
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlAcadyear.SelectedIndex = 0;
        ddlTestCategory.SelectedIndex = 0;
        ddlStandard.Items.Clear();
        ddlTestType.Items.Clear();
        Msg_Error.Visible = false;
        lblerror.Text = "";
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        
        txtTestName.Text = "";
        dtrange.Value = "";
    }


    protected void HLExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;
        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=Test_ProcessStudentAnswer..xls");


        Response.Charset = "";


        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

        dlGridExport.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();
        dlGridExport.Visible = false;
    }
    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
        // Confirms that an HtmlForm control is rendered for the specified ASP.NET
        //     server control at run time. 

    }
    protected void ddlStandard_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtTestName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlTestCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
