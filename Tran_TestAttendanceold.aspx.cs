using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;

partial class Tran_TestAttendanceold : System.Web.UI.Page
{

   

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();
            FillDDL_TestCategories();
            FillDDL_TestTypes();
            FillAttendanceType();
            FillEntityType();
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
            DivAddPanel.Visible = false;

        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = false;

        }
        else if (Mode == "Manage")
        {
            DivAddPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
        }
        Clear_Error_Success_Box();
    }

    protected void BtnCloseAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Result");
        Clear_AddPanel();
    }

    private void Clear_AddPanel()
    {
        lblTestPKey_Edit.Text = "";
        dlGridDisplay_StudAttendance.DataSource = null;
        dlGridDisplay_StudAttendance.DataBind();

        lblSummary_BatchStrength.Text = "";
        lblSummary_ExemptCount.Text = "";
        lblSummary_PresentCount.Text = "";
        lblSummary_PresentPercent.Text = "";
        lblSummary_AbsentCount.Text = "";
        lblSummary_AbsentPercent.Text = "";
        lblSummary_NMCount.Text = "";

        dlGridStudent.DataSource = null;
        dlGridStudent.DataBind();
    }

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

    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();

        Msg_Error2.Visible = false;
        Msg_Success2.Visible = false;
        lblSuccess2.Text = "";
        lblerror2.Text = "";
        UpdatePanelMsgBox2.Update();
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
        Clear_Error_Success_Box();
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


    }

    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        if (e.CommandName == "Manage")
        {
            ControlVisibility("Manage");
            lblAcadYear_Edit.Text = lblAcadYear_Result.Text;
            lblDivision_Edit.Text = lblDivision_Result.Text;
            lblStandard_Edit.Text = lblStandard_Result.Text;
            lblCentre_Edit.Text = lblCentre_Result.Text;
            lblTestCategory_Edit.Text = lblTestCategory_Result.Text;
            lblBatch_Edit.Text = (((Label)e.Item.FindControl("lblDLBatchName")).Text);
            lblConductNo_Edit.Text = (((Label)e.Item.FindControl("lblDLConductNo")).Text);
            lblTestName_Edit.Text = (((Label)e.Item.FindControl("lblModeName")).Text);
            lblTestType_Edit.Text = (((Label)e.Item.FindControl("lblDLTestType")).Text);
            lblSubject_Edit.Text = (((Label)e.Item.FindControl("lblDLSubjects")).Text);
            lblRemarks_Edit.Text = (((Label)e.Item.FindControl("lblDLRemarks")).Text);
            lblMaxMarks_Edit.Text = (((Label)e.Item.FindControl("lblDLMaxMarks")).Text);
            lblTestDate_Edit.Text = (((Label)e.Item.FindControl("lblDLTestDate")).Text);
            lblTestTime_Edit.Text = (((Label)e.Item.FindControl("lblDLTestTime")).Text);

            string DLAuthoriseFlag = null;
            DLAuthoriseFlag = (((Label)e.Item.FindControl("lblDLAuthoriseFlag")).Text);

            //Attendance closusre done
            if (DLAuthoriseFlag == "1")
            {
                btnAddAttendance.Enabled = false;
                btnLock_UnAuthorise.Visible = true;
                btnLock_Authorise.Visible = false;
                Flag_Authorise.Visible = true;
            }
            else
            {
                btnAddAttendance.Enabled = true;
                btnLock_UnAuthorise.Visible = false;
                btnLock_Authorise.Visible = true;
                Flag_Authorise.Visible = false;
            }

            lblTestPKey_Edit.Text = e.CommandArgument.ToString();


        }
    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
        Clear_AddPanel();
    }

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
            //if (Strings.Right(BatchCode, 1) == ",")
            //    BatchCode = Strings.Left(BatchCode, Strings.Len(BatchCode) - 1);
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
            //if (Strings.Right(TestType_ID, 1) == ",")
            //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);
            TestType_ID = Common.RemoveComma(TestType_ID);
        }

        ControlVisibility("Result");

        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        string TestName = null;
        if (string.IsNullOrEmpty(txtTestName.Text.Trim()))
        {
            TestName = "%";
        }
        else
        {
            TestName = "%" + txtTestName.Text.Trim();
        }

        string DateRange = null;
        DateRange = id_date_range_picker_1.Value;

        string FromDate = null;
        string ToDate = null;
        //FromDate = Strings.Left(DateRange, 10);
        if (DateRange != "")
        {
            FromDate = DateRange.Substring(0, 10);
        }
        if (string.IsNullOrEmpty(FromDate))
            FromDate = System.DateTime.Now.ToString("dd MMM yyyy");

        //ToDate = Strings.Right(DateRange, 10);
        if (DateRange != "")
        {
            ToDate = DateRange.Substring(DateRange.Length - 10);
        }
        if (string.IsNullOrEmpty(ToDate))
            ToDate = System.DateTime.Now.ToString("dd MMM yyyy");

        string Centre_Code = null;
        Centre_Code = ddlCentre.SelectedItem.Value;

        DataSet dsGrid = ProductController.GetTestScheduleBy_Division_Year_Standard_Centre(DivisionCode, YearName, StandardCode, BatchCode, "01,02", ddlTestCategory.SelectedValue, TestType_ID, TestName, FromDate, ToDate,
        -1, -1, Centre_Code, 1);

        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {
                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();

                dlGridExport.DataSource = dsGrid;
                dlGridExport.DataBind();
            }
            else
            {
                dlGridDisplay.DataSource = null;
                dlGridDisplay.DataBind();

                dlGridExport.DataSource = null;
                dlGridExport.DataBind();
            }
        }
        else
        {
            dlGridDisplay.DataSource = null;
            dlGridDisplay.DataBind();

            dlGridExport.DataSource = null;
            dlGridExport.DataBind();
        }
        
        
       

        lbltotalcount.Text =Convert.ToString(dsGrid.Tables[0].Rows.Count);
        lblDivision_Result.Text = Convert.ToString(ddlDivision.SelectedItem.ToString());
        lblAcadYear_Result.Text = Convert.ToString(ddlAcadYear.SelectedItem.ToString());
        lblStandard_Result.Text = Convert.ToString(ddlStandard.SelectedItem.ToString());
        lblTestCategory_Result.Text = Convert.ToString(ddlTestCategory.SelectedItem.ToString());
        lblCentre_Result.Text = Convert.ToString(ddlCentre.SelectedItem.ToString());
    }

    private void FillAttendanceType()
    {
        DataSet dsAttendanceType = ProductController.GetAllTestAttendanceActionType();
        BindDDL(ddlAttendanceType, dsAttendanceType, "Action_Name", "Action_Id");
        ddlAttendanceType.Items.Insert(0, "[ All ]");
        ddlAttendanceType.SelectedIndex = 0;
    }

    private void FillEntityType()
    {
        string Action_Id = null;
        Action_Id = ddlAttendanceType.SelectedValue;

        int Flag = 0;
        if (ddlAttendanceType.SelectedIndex == 0)
        {
            Flag = 2;
        }
        else
        {
            Flag = 1;
        }

        DataSet dsEntityType = ProductController.GetAllTestAttendanceEntityType(Action_Id, Flag);
        BindDDL(ddlEntityType, dsEntityType, "Entity_Name", "Entity_Id");
        ddlEntityType.Items.Insert(0, "Select");

        if (ddlEntityType.Items.Count == 2)
        {
            ddlEntityType.SelectedIndex = 1;
        }
        else
        {
            ddlEntityType.SelectedIndex = 0;
        }

    }

    protected void btnSearchAttendance_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();

        //Validation
        if (ddlEntityType.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0021");
            ddlEntityType.Focus();
            return;
        }

        DivResultPanelLevel2.Visible = true;

        string TestPKey = null;
        TestPKey = lblTestPKey_Edit.Text;

        int ActionFlag = 0;


        if (ddlEntityType.SelectedValue == "001")
        {
            if (ddlAttendanceType.SelectedValue == "001")
            {
                ActionFlag = 1;
            }
            else if (ddlAttendanceType.SelectedValue == "002")
            {
                ActionFlag = 2;
            }
            else if (ddlAttendanceType.SelectedValue == "003")
            {
                ActionFlag = 3;
            }
            else
            {
                ActionFlag = 0;
            }

            DataSet dsStudent = ProductController.GetStudent_ForTest_ByTestPKey(TestPKey, ActionFlag);
            if (dsStudent != null)
            {
                if (dsStudent.Tables[1].Rows.Count > 0)
                {
                    dlGridDisplay_StudAttendance.DataSource = dsStudent;
                    dlGridDisplay_StudAttendance.DataBind();
                    long ActualBatchStrength = 0;
                    ActualBatchStrength = Convert.ToInt64(Convert.ToInt64(dsStudent.Tables[1].Rows[0]["BatchStrength"]) - Convert.ToInt64(dsStudent.Tables[1].Rows[0]["ExemptCount"]));

                    float PresentPercent = 0;
                    if (ActualBatchStrength != 0)
                    {
                        PresentPercent = float.Parse(Convert.ToString((Math.Round(Convert.ToDouble(100 * Convert.ToInt32(dsStudent.Tables[1].Rows[0]["PresentCount"]) / ActualBatchStrength), 1))));
                    }
                    else
                    {
                        PresentPercent = 0;
                    }

                    float AbsentPercent = 0;
                    if (ActualBatchStrength != 0)
                    {
                        AbsentPercent = float.Parse(Convert.ToString((Math.Round(Convert.ToDouble(100 * Convert.ToInt32(dsStudent.Tables[1].Rows[0]["AbsentCount"]) / ActualBatchStrength), 1))));
                        //AbsentPercent = Math.Round(100 * dsStudent.Tables[1].Rows[0]["AbsentCount"] / ActualBatchStrength, 1);
                    }
                    else
                    {
                        AbsentPercent = 0;
                    }

                    lblSummary_BatchStrength.Text = Convert.ToString(dsStudent.Tables[1].Rows[0]["BatchStrength"]);
                    lblSummary_ExemptCount.Text = Convert.ToString(dsStudent.Tables[1].Rows[0]["ExemptCount"]);
                    lblSummary_PresentCount.Text = Convert.ToString(dsStudent.Tables[1].Rows[0]["PresentCount"]);
                    lblSummary_PresentPercent.Text = Convert.ToString("[ " + PresentPercent.ToString() + " %]");
                    lblSummary_AbsentCount.Text = Convert.ToString(dsStudent.Tables[1].Rows[0]["AbsentCount"]);
                    lblSummary_AbsentPercent.Text = Convert.ToString("[ " + AbsentPercent.ToString() + " %]");
                    lblSummary_NMCount.Text = Convert.ToString(dsStudent.Tables[1].Rows[0]["NotMarkedCount"]);

                    //Attendance closusre done
                    if (dsStudent.Tables[1].Rows[0]["AttendClosureStatus_Flag"].ToString() == "1")
                    {
                        btnAddAttendance.Enabled = false;
                        btnLock_UnAuthorise.Visible = true;
                        btnLock_Authorise.Visible = false;
                        Flag_Authorise.Visible = true;
                    }
                    else
                    {
                        btnAddAttendance.Enabled = true;
                        btnLock_UnAuthorise.Visible = false;
                        btnLock_Authorise.Visible = true;
                        Flag_Authorise.Visible = false;
                    }

                    dlGridDisplay_StudAttendance.Visible = true;
                }
                else
                {
                    dlGridDisplay_StudAttendance.Visible = false;
                }
            }
            else
            {
                dlGridDisplay_StudAttendance.Visible = false;
            }
        }
        UpdatePanel_Add.Update();
        UpdatePanel_StudAttendance_Result.Update();
    }

    protected void ddlAttendanceType_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlAttendanceType.SelectedIndex == 0 | ddlEntityType.SelectedIndex == 0)
        {
            btnAddAttendance.Visible = false;
        }
        else
        {
            btnAddAttendance.Visible = true;
        }
        FillEntityType();
    }

    protected void ddlEntityType_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlAttendanceType.SelectedIndex == 0 | ddlEntityType.SelectedIndex == 0)
        {
            btnAddAttendance.Visible = false;
        }
        else
        {
            btnAddAttendance.Visible = true;
        }
    }

    protected void btnAddAttendance_Click(object sender, System.EventArgs e)
    {
        FillGridStudent();
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalStudAttend();", true);
    }

    private void FillGridStudent()
    {
        string TestPKey = null;
        TestPKey = lblTestPKey_Edit.Text;

        int ActionFlag = 0;

        if (ddlAttendanceType.SelectedValue == "001")
        {
            lblStudAttend_Header.Text = "Mark Student Exemption";
            ActionFlag = 1;
            lblStudAttend_Action.Text = "E";
        }
        else if (ddlAttendanceType.SelectedValue == "002")
        {
            lblStudAttend_Header.Text = "Mark Student Absent";
            ActionFlag = 2;
            lblStudAttend_Action.Text = "A";
        }
        else if (ddlAttendanceType.SelectedValue == "003")
        {
            lblStudAttend_Header.Text = "Mark Student Present";
            ActionFlag = 3;
            lblStudAttend_Action.Text = "P";
        }

        DataSet dsStudent = ProductController.GetStudent_ForTest_ByTestPKey(TestPKey, ActionFlag);

        if (dsStudent != null)
        {
            if (dsStudent.Tables.Count != 0)
            {
                dlGridStudent.DataSource = dsStudent;
                dlGridStudent.DataBind();
            }
            else
            {
                dlGridStudent.DataSource = null;
                dlGridStudent.DataBind();
            }
        }
        else
        {
            dlGridStudent.DataSource = null;
            dlGridStudent.DataBind();
        }
       
        UpdatePanel_StudAttendance.Update();

    }

    protected void btnStudAttend_Save_Click(object sender, System.EventArgs e)
    {
        string TestPKey = null;
        TestPKey = lblTestPKey_Edit.Text;

        int StudCnt = 0;
        string SBEntryCode = "";
        string NotSel_SBEntryCode = "";

        foreach (DataListItem dtlItem in dlGridStudent.Items)
        {
            CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
            Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
            if (chkStudent.Checked == true)
            {
                StudCnt = StudCnt + 1;
                SBEntryCode = SBEntryCode + lblSBEntryCode.Text + ",";
            }
            else
            {
                NotSel_SBEntryCode = NotSel_SBEntryCode + lblSBEntryCode.Text + ",";
            }
        }
        //if (Strings.Right(SBEntryCode, 1) == ",")
        //    SBEntryCode = Strings.Left(SBEntryCode, Strings.Len(SBEntryCode) - 1);
        //if (Strings.Right(NotSel_SBEntryCode, 1) == ",")
        //    NotSel_SBEntryCode = Strings.Left(NotSel_SBEntryCode, Strings.Len(NotSel_SBEntryCode) - 1);

        SBEntryCode = Common.RemoveComma(SBEntryCode);
        NotSel_SBEntryCode = Common.RemoveComma(NotSel_SBEntryCode);

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        string ActionFlag = null;
        ActionFlag = lblStudAttend_Action.Text;

        int ResultId = 0;
        //Mark exemption/absent/present for those students who are selected
        ResultId = ProductController.Insert_StudentTestAttendace(TestPKey, ActionFlag, SBEntryCode, CreatedBy);

        //Mark NA for those students who are not selected
        ResultId = ProductController.Insert_StudentTestAttendace(TestPKey, "N", NotSel_SBEntryCode, CreatedBy);

        //Close the Add Panel and go to Search Grid
        if (ResultId == 1)
        {
            btnSearchAttendance_Click(sender, e);
            btnStudAttend_Close_Click(sender, e);
        }
    }

    protected void btnStudAttend_Close_Click(object sender, System.EventArgs e)
    {
        //Close the modal box
    }

    protected void ddlCentre_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Batch();
        Clear_Error_Success_Box();
    }

    protected void btnLock_Authorise_ServerClick(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();

        //Check if attendance of students is marked or not
        if (Convert.ToInt32(lblSummary_BatchStrength.Text) > 0 & Convert.ToInt32(lblSummary_NMCount.Text) != 0)
        {
            Show_Error_Success_Box2("E", "0031");
            dlGridStudent.Focus();
            return;
        }

        //Check if reason for all absent students is entered or not


        //Change AttendanceClosureFlag for the test
        string TestPKey = null;
        TestPKey = lblTestPKey_Edit.Text;

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        string ActionFlag = null;
        ActionFlag = "1";
        //Authorise

        int ResultId = 0;
        ResultId = ProductController.InsertStudentTestAttendace_Authorisation(TestPKey, ActionFlag, CreatedBy);

        if (ResultId == 1)
        {
            btnSearchAttendance_Click(sender, e);
            Show_Error_Success_Box2("S", "0032");
        }

    }

    private void Show_Error_Success_Box2(string BoxType, string Error_Code)
    {
        if (BoxType == "E")
        {
            Msg_Error2.Visible = true;
            Msg_Success2.Visible = false;
            lblerror2.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox2.Update();
        }
        else
        {
            Msg_Success2.Visible = true;
            Msg_Error2.Visible = false;
            lblSuccess2.Text = ProductController.Raise_Error(Error_Code);
            UpdatePanelMsgBox2.Update();
        }
    }

    protected void btnLock_UnAuthorise_ServerClick(object sender, System.EventArgs e)
    {
        string TestPKey = null;
        TestPKey = lblTestPKey_Edit.Text;

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        string ActionFlag = null;
        ActionFlag = "0";
        //UnAuthorise

        int ResultId = 0;
        ResultId = ProductController.InsertStudentTestAttendace_Authorisation(TestPKey, ActionFlag, CreatedBy);

        if (ResultId == 1)
        {
            btnSearchAttendance_Click(sender, e);
            Show_Error_Success_Box2("S", "0033");
        }
    }

    protected void dlGridDisplay_StudAttendance_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        if (e.CommandName == "EditReason")
        {
            Label lblDLAbsentReason = (Label)e.Item.FindControl("lblDLAbsentReason");
            DropDownList ddlDLAbsentReason = (DropDownList)e.Item.FindControl("ddlDLAbsentReason");
            LinkButton btnDL_AbsentReason = (LinkButton)e.Item.FindControl("btnDL_AbsentReason");
            LinkButton lnkDLEditReason = (LinkButton)e.Item.FindControl("lnkDLEditReason");
            Label lblDLAbsentReasonID = (Label)e.Item.FindControl("lblDLAbsentReasonID");

            lblDLAbsentReason.Visible = false;
            ddlDLAbsentReason.Visible = true;
            btnDL_AbsentReason.Visible = true;
            lnkDLEditReason.Visible = false;

            DataSet dsReason = ProductController.GetAllActiveTestAbsentReason();
            BindDDL(ddlDLAbsentReason, dsReason, "AbsentReason_Name", "AbsentReason_ID");
            ddlDLAbsentReason.Items.Insert(0, "[ Select ]");
            ddlDLAbsentReason.SelectedIndex = 0;

            try
            {
                string ReasonID = null;
                ReasonID = lblDLAbsentReasonID.Text;
                ddlDLAbsentReason.SelectedValue = ReasonID;
            }
            catch (Exception ex)
            {
                if (ddlDLAbsentReason.Items.Count > 0)
                    ddlDLAbsentReason.SelectedIndex = 0;
            }


        }
        else if (e.CommandName == "SaveReason")
        {
            Label lblDLAbsentReason = (Label)e.Item.FindControl("lblDLAbsentReason");
            DropDownList ddlDLAbsentReason = (DropDownList)e.Item.FindControl("ddlDLAbsentReason");
            LinkButton btnDL_AbsentReason = (LinkButton)e.Item.FindControl("btnDL_AbsentReason");
            LinkButton lnkDLEditReason = (LinkButton)e.Item.FindControl("lnkDLEditReason");

            lblDLAbsentReason.Visible = true;
            ddlDLAbsentReason.Visible = false;
            btnDL_AbsentReason.Visible = false;
            lnkDLEditReason.Visible = true;

            string TestPKey = "";
            TestPKey = lblTestPKey_Edit.Text;

            string SBEntryCode = "";
            SBEntryCode = e.CommandArgument.ToString();

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;

            string AbsentReasonId = null;
            AbsentReasonId = ddlDLAbsentReason.SelectedValue;

            int ResultId = 0;
            //Mark exemption/absent/present for those students who are selected
            ResultId = ProductController.InsertStudentTestAbsentReason(TestPKey, AbsentReasonId, SBEntryCode, CreatedBy);

            if (ResultId == 1)
            {
                lblDLAbsentReason.Text = ddlDLAbsentReason.SelectedItem.ToString();
            }
        }
    }
    public Tran_TestAttendanceold()
    {
        Load += Page_Load;
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlAcadYear.SelectedIndex = 0;
        ddlTestCategory.SelectedIndex = 0;
        ddlStandard.Items.Clear();
        ddlTestType.Items.Clear();
        Msg_Error.Visible = false;
        lblerror.Text = "";
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        ddlBatch.Items.Clear();
        txtTestName.Text = "";
        id_date_range_picker_1.Value = "";
    }
}
