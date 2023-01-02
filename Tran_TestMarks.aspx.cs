using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text;

//using System.Runtime.InteropServices.Marshal;
using System.Data.OleDb;
using System.Web.UI;
using System.IO;
using System.Web;
using System.Net.Mail;
using System.Net;
using ShoppingCart.BL;
using System.Web.UI.WebControls;

partial class Tran_TestMarks : System.Web.UI.Page
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
      
        Clear_AddPanel();
        BindSearchGrid();
        
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

        //dlGridStudent.DataSource = Nothing
        //dlGridStudent.DataBind()
    }

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        BindSearchGrid();
    }

    
    private void BindSearchGrid()
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

        // ToDate = Strings.Right(DateRange, 10);
        if (DateRange != "")
        {
            ToDate = DateRange.Substring(DateRange.Length - 10);//DateRange.Substring(DateRange.Length, 10);
        }
        if (string.IsNullOrEmpty(ToDate))
            ToDate = System.DateTime.Now.ToString("dd MMM yyyy");

        string Centre_Code = null;
        Centre_Code = ddlCentre.SelectedItem.Value;

        DataSet dsGrid = ProductController.GetTestScheduleBy_Division_Year_Standard_Centre(DivisionCode, YearName, StandardCode, BatchCode, "01,02", ddlTestCategory.SelectedValue, TestType_ID, TestName, FromDate, ToDate,
        1, -1, Centre_Code, 1);
        dlGridDisplay.DataSource = dsGrid;
        dlGridDisplay.DataBind();

        dlGridExport.DataSource = dsGrid;
        dlGridExport.DataBind();

        lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
        lblDivision_Result.Text = Convert.ToString(ddlDivision.SelectedItem.ToString());
        lblAcadYear_Result.Text = Convert.ToString(ddlAcadYear.SelectedItem.ToString());
        lblStandard_Result.Text = Convert.ToString(ddlStandard.SelectedItem.ToString());
        lblTestCategory_Result.Text = Convert.ToString(ddlTestCategory.SelectedItem.ToString());
        lblCentre_Result.Text = Convert.ToString(ddlCentre.SelectedItem.ToString());


        


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

            //Print Div

            
            lblDivisionPrint.Text = lblDivision_Edit.Text.Trim();
            lblPrintCource.Text = lblStandard_Edit.Text.Trim();
            lblPrintBatch.Text = lblBatch_Edit.Text.Trim();
            lblPrintTestName.Text = lblTestName_Edit.Text.Trim();
            lblPrintCoductNo.Text = lblConductNo_Edit.Text.Trim();
            lblPrintTestDate.Text = lblTestDate_Edit.Text.Trim();
            lblPrintMaxMarks.Text = lblMaxMarks_Edit.Text.Trim();

            //

            lblTestPKey_Edit.Text = e.CommandArgument.ToString();

            FillGridStudent();
        }

    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
        Clear_AddPanel();
    }


    private void FillGridStudent()
    {
        string TestPKey = null;
        TestPKey = lblTestPKey_Edit.Text;

        int ActionFlag = 0;

        ActionFlag = 3;
        //Show only present students


        DataSet dsStudent = ProductController.GetAllStudentDetails_ByTestPKey(TestPKey, ActionFlag);
        dlGridDisplay_StudAttendance.DataSource = dsStudent;
        dlGridDisplay_StudAttendance.DataBind();

        DataList1.DataSource = dsStudent;
        DataList1.DataBind();

        if (dsStudent.Tables[1].Rows.Count > 0)
        {
            long ActualBatchStrength = 0;
            ActualBatchStrength =Convert.ToInt64(Convert.ToDecimal(dsStudent.Tables[1].Rows[0]["BatchStrength"]) - Convert.ToDecimal(dsStudent.Tables[1].Rows[0]["ExemptCount"]));

            float PresentPercent = 0;
            if (ActualBatchStrength != 0)
            {
                
                PresentPercent =float.Parse(Convert.ToString(Math.Round(100 *Convert.ToDouble(dsStudent.Tables[1].Rows[0]["PresentCount"]) / ActualBatchStrength, 1)));
            }
            else
            {
                PresentPercent = 0;
            }

            float AbsentPercent = 0;
            if (ActualBatchStrength != 0)
            {
                AbsentPercent =float.Parse(Convert.ToString(Math.Round(100 *Convert.ToDouble(dsStudent.Tables[1].Rows[0]["AbsentCount"]) / ActualBatchStrength, 1)));
            }
            else
            {
                AbsentPercent = 0;
            }

            lblSummary_BatchStrength.Text =Convert.ToString(dsStudent.Tables[1].Rows[0]["BatchStrength"]);
            lblSummary_ExemptCount.Text = Convert.ToString(dsStudent.Tables[1].Rows[0]["ExemptCount"]);
            lblSummary_PresentCount.Text = Convert.ToString(dsStudent.Tables[1].Rows[0]["PresentCount"]);
            lblSummary_PresentPercent.Text = Convert.ToString("[ " + PresentPercent.ToString() + " %]");
            lblSummary_AbsentCount.Text = Convert.ToString(dsStudent.Tables[1].Rows[0]["AbsentCount"]);
            lblSummary_AbsentPercent.Text = Convert.ToString("[ " + AbsentPercent.ToString() + " %]");
            lblSummary_NMCount.Text = Convert.ToString(dsStudent.Tables[1].Rows[0]["NotMarkedCount"]);

            //Attendance closusre done
            if (dsStudent.Tables[1].Rows[0]["MarksClosureStatus_Flag"].ToString() == "1")
            {
                btnLock_UnAuthorise.Visible = true;
                btnSmsSend.Visible = true;
                btnLock_Authorise.Visible = false;
                Flag_Authorise.Visible = true;
                BtnSaveAdd.Enabled = false;
                BtnSaveAdd.Visible = false;
                CHk_btnVisiblity();

            }
            else
            {
                btnLock_UnAuthorise.Visible = false;
                btnSmsSend.Visible = false;
                btnLock_Authorise.Visible = true;
                Flag_Authorise.Visible = false;
                BtnSaveAdd.Enabled = true;
                BtnSaveAdd.Visible = true;

                //
                Label110.Visible = false;
                lblMessage_Template_SMS.Visible = false;
                Btn_TestMessage.Visible = false;
                btnSmsSend.Visible = false;
                FlagAutoSMS.Visible = false;
               
            }
            
        }

        UpdatePanel_Add.Update();
    }


    protected void ddlCentre_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Batch();
        Clear_Error_Success_Box();
    }

    protected void btnLock_Authorise_ServerClick(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();

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
        ResultId = ProductController.InsertStudentTestMarks_Authorisation(TestPKey, ActionFlag, CreatedBy);

        if (ResultId == 1)
        {
            FillGridStudent();
            Show_Error_Success_Box2("S", "0034");
            
        }
        else if (ResultId == -1)
        {
            //Authorisation cant be done as some records are pending for entry
            Show_Error_Success_Box2("E", "0036");
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
        ResultId = ProductController.InsertStudentTestMarks_Authorisation(TestPKey, ActionFlag, CreatedBy);

        if (ResultId == 1)
        {
            FillGridStudent();
            Show_Error_Success_Box2("S", "0035");
            
        }
    }

    protected void BtnSaveAdd_Click(object sender, System.EventArgs e)
    {
        string TestPKey = null;
        TestPKey = lblTestPKey_Edit.Text;
        

        string SBEntryCode = "";
        string NotSel_SBEntryCode = "";

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        string MaxMarks = null;
        MaxMarks = lblMaxMarks_Edit.Text;
        int iCoun = 0;

        foreach (DataListItem dtlItem in dlGridDisplay_StudAttendance.Items)
        {
            TextBox txtDLMarks = (TextBox)dtlItem.FindControl("txtDLMarks");
            Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");

            //Save Marks
            if (!string.IsNullOrEmpty(txtDLMarks.Text))
            {
                //if (Convert.ToBoolean(Convert.ToInt32(txtDLMarks.Text)) == true)//
                //{
                    if (Convert.ToDouble(txtDLMarks.Text) <=Convert.ToDouble(MaxMarks))
                    {
                        int ResultId = 0;
                        ResultId = ProductController.Insert_StudentTestMarks(TestPKey, lblSBEntryCode.Text, txtDLMarks.Text, MaxMarks, CreatedBy);

                        iCoun = iCoun + 1;
                    }
                //}
            }
        }

        if (iCoun > 0)
        {
            CHk_btnVisiblity();
            Show_Error_Success_Box2("S", "Record Saved successfully");
        }
        
        

    }

    protected void Linkbtnstdnm_Click(object sender, System.EventArgs e)
    {
        txtsmsstd.Text = txtsmsstd.Text + "< NAME >";
    }

    protected void Linkbtnstdfnm_Click(object sender, System.EventArgs e)
    {
        txtsmsstd.Text = txtsmsstd.Text + "< FIRSTNAME >";
    }



    protected void Linkbtnmxmark_Click(object sender, System.EventArgs e)
    {
        txtsmsstd.Text = txtsmsstd.Text + "< MXMARK >";
    }

    protected void Linkbtnobtnmark_Click(object sender, System.EventArgs e)
    {
        txtsmsstd.Text = txtsmsstd.Text + "< MARK >";
    }


    protected void Linkbtntestdt_Click(object sender, System.EventArgs e)
    {
        txtsmsstd.Text = txtsmsstd.Text + "< TESTDATE >";
    }

    protected void Linkbtntestnm_Click(object sender, System.EventArgs e)
    {
        txtsmsstd.Text = txtsmsstd.Text + "< TESTNAME >";
    }

    protected void Linkbtnperctg_Click(object sender, System.EventArgs e)
    {
        txtsmsstd.Text = txtsmsstd.Text + "< PERCENTAGE >";
    }


    protected void btnSendsmsstd_Click(object sender, System.EventArgs e)
    {

        int resultid = 0;
        foreach (DataListItem row in dlGridDisplay_StudAttendance.Items)
        {
            string msg = txtsmsstd.Text;
            if ((row.FindControl("lblDLAttendStatus") as Label).Text == "Present" & !string.IsNullOrEmpty((row.FindControl("txtDLMarks") as TextBox).Text))
            {
                string num1 = (row.FindControl("lblMobileNo") as Label).Text;

                string num2 = (row.FindControl("lblPMobileNo") as Label).Text;
                string fname = (row.FindControl("lblFName") as Label).Text;
                string upmsg = msg.Replace("< NAME >", (row.FindControl("lblDLStudentName") as Label).Text);

                string perct = null;
                TextBox txtDLMarks = (TextBox)row.FindControl("txtDLMarks");
                perct =Convert.ToString((Convert.ToDouble(txtDLMarks.Text) /Convert.ToDouble(lblMaxMarks_Edit.Text)) * 100);
                msg = upmsg;
                upmsg = msg.Replace("< FIRSTNAME >", fname);
                msg = upmsg;
                upmsg = msg.Replace("< MXMARK >", lblMaxMarks_Edit.Text);
                msg = upmsg;
                upmsg = msg.Replace("< MARK >", (row.FindControl("txtDLMarks") as TextBox).Text);
                msg = upmsg;
                upmsg = msg.Replace("< TESTDATE >", lblTestDate_Edit.Text);
                msg = upmsg;
                upmsg = msg.Replace("< TESTNAME >", lblTestName_Edit.Text);
                msg = upmsg;
                upmsg = msg.Replace("< PERCENTAGE >", perct);
                msg = upmsg;

                
                if (inputstd.Checked == true & !string.IsNullOrEmpty(num1))
                {
                    string[] nums1 = num1.Split(new char[] { ',' });
                    num1 = nums1[0].ToString();
                    //SMSSend(num1, msg);
                    //resultid = ProductController.Insert_SMSLog(ddlCentre.SelectedValue.ToString(), "008", num1, upmsg, "0", UserID, "Promotional", 7);
                }
                else if (inputpar.Checked == true & !string.IsNullOrEmpty(num2))
                {
                    string[] nums2 = num2.Split(new char[] { ',' });
                    num2 = nums2[0].ToString();
                    SMSSend(num2, msg);
                }
                //else if (!string.IsNullOrEmpty(num2))
                //{
                //    string[] nums2 = num2.Split(new char[] { ',' });
                //    num2 = nums2[0].ToString();
                //    SMSSend(num2, msg);
                //}
                else if (inputboth.Checked == true)
                {
                    if (!string.IsNullOrEmpty(num1))
                    {
                        string[] nums1 = num1.Split(new char[] { ',' });
                        num1 = nums1[0].ToString();
                        SMSSend(num1, msg);
                    }
                    if (!string.IsNullOrEmpty(num2))
                    {
                        string[] nums2 = num2.Split(new char[] { ',' });
                        num2 = nums2[0].ToString();
                        SMSSend(num2, msg);
                    }            
                }
            }

        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CloseModalSms();", true);

    }

    
    public void SMSSend(string MobNo, string Msg)
    {
        if (MobNo.Length == 10)
        {
            //MobNo = MobNo; need to chk by jayant

            WebClient client = new WebClient();

            string baseurl = "http://api.smscountry.com/SMSCwebservice_bulk.aspx?User=Sciencetr&passwd=25892846&mobilenumber=" + MobNo + "&message=" + Msg + "&sid=MTEDU&mtype=N&DR=Y";

            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();

        }
    }

    public Tran_TestMarks()
    {
        Load += Page_Load;
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlAcadYear.SelectedIndex = 0;        
        ddlTestCategory.SelectedIndex = 0;
        ddlStandard.Items.Clear();
        ddlTestType.SelectedIndex = -1;
        
        ddlBatch.Items.Clear();
        ddlCentre.Items.Clear();
        txtTestName.Text = "";
        Msg_Error.Visible = false;
        lblerror.Text = "";
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        id_date_range_picker_1.Value = "";
    }


    //// Code on 4 Aug 2015

    private void CHk_btnVisiblity()
    {
        string count = "", Notification = "";

        DataSet DSChk = ProductController.Check_MesageTemplate("011", ddlDivision.SelectedValue.ToString().Trim(), 1);
        if (DSChk != null)
        {
            if (DSChk.Tables[0].Rows.Count > 0)
            {
                count = DSChk.Tables[0].Rows[0]["count1"].ToString();
                if (count == "0")
                {
                    //Disable
                }
                else
                {
                    Notification = DSChk.Tables[0].Rows[0]["Send_Type"].ToString();
                    if (Notification == "Manual")
                    {
                        Label110.Visible = true;
                        lblMessage_Template_SMS.Visible = true;
                        Btn_TestMessage.Visible = true;

                        //Show Button
                        btnSmsSend.Visible = true;
                        FlagAutoSMS.Visible = false;

                    }
                    else
                    {
                        Label110.Visible = true;
                        lblMessage_Template_SMS.Visible = true;
                        Btn_TestMessage.Visible = true;

                        //Hide Button
                        btnSmsSend.Visible = false;
                        FlagAutoSMS.Visible = true;
                    }
                }


            }
            else
            {
                //Hide Button
                btnSmsSend.Visible = false;
                FlagAutoSMS.Visible = false;
            }
        }


        ///MSG Temp
        ///

        string Template = "", Message_cd = "", MsgMode = "", newTemplate="";

        DataSet DSgridChk = ProductController.Check_MesageTemplate("011", ddlDivision.SelectedValue.ToString().Trim(), 1);
        if (DSgridChk != null)
        {
            if (DSgridChk.Tables[0].Rows.Count > 0)
            {
                count = DSgridChk.Tables[0].Rows[0]["count1"].ToString();
                if (count == "0")
                {
                    //Disable
                }
                else
                {
                    Template = DSgridChk.Tables[0].Rows[0]["Message_Template"].ToString();
                    Message_cd = DSgridChk.Tables[0].Rows[0]["Message_Code"].ToString();
                    MsgMode = DSgridChk.Tables[0].Rows[0]["Send_Type"].ToString();
                }
            }
        }

        newTemplate = Template.Replace("%2526", "&").Replace("%252D", "+").Replace("%25", "%").Replace("%23", "#").Replace("%3D", "=").Replace("%5E", "^").Replace("%7E", "~");

        lblMessage_Template_SMS.Text = newTemplate.ToString().Trim();
        lblMessage_Code_Fin.Text = Message_cd.ToString().Trim();
        lblMessage_Mode_Fin.Text = MsgMode.ToString().Trim();
        

        if(Flag_Authorise.Visible == true)
        {
            btnAuthPrint.Visible = true;
        }
        else
        {
            btnAuthPrint.Visible = false ;
        }
   

    }


    protected void Btn_TestMessage_Click(object sender, System.EventArgs e)
    {
        string template = lblMessage_Template_SMS.Text.ToString().Trim();
        string newTemplate = "";

        foreach (DataListItem row in dlGridDisplay_StudAttendance.Items)
        {
            string msg = txtsmsstd.Text;
            if ((row.FindControl("lblDLAttendStatus") as Label).Text == "Present" & !string.IsNullOrEmpty((row.FindControl("txtDLMarks") as TextBox).Text))
            {
                string num1 = (row.FindControl("lblMobileNo") as Label).Text;

                string num2 = (row.FindControl("lblPMobileNo") as Label).Text;
                string fname = (row.FindControl("lblFName") as Label).Text;
                string upmsg = msg.Replace("< NAME >", (row.FindControl("lblDLStudentName") as Label).Text);

                string perct = null;
                TextBox txtDLMarks = (TextBox)row.FindControl("txtDLMarks");
                perct = Convert.ToString((Convert.ToDouble(txtDLMarks.Text) / Convert.ToDouble(lblMaxMarks_Edit.Text)) * 100);
                msg = upmsg;
                upmsg = msg.Replace("< FIRSTNAME >", fname);
                msg = upmsg;
                upmsg = msg.Replace("< MXMARK >", lblMaxMarks_Edit.Text);
                msg = upmsg;
                upmsg = msg.Replace("< MARK >", (row.FindControl("txtDLMarks") as TextBox).Text);
                msg = upmsg;
                upmsg = msg.Replace("< TESTDATE >", lblTestDate_Edit.Text);
                msg = upmsg;
                upmsg = msg.Replace("< TESTNAME >", lblTestName_Edit.Text);
                msg = upmsg;
                upmsg = msg.Replace("< PERCENTAGE >", perct);
                msg = upmsg;

                newTemplate = template.Replace("[FullName]", (row.FindControl("lblDLStudentName") as Label).Text).Replace("[FirstName]", fname).Replace("[TestName]", lblTestName_Edit.Text.Trim()).Replace("[TestDate]", lblTestDate_Edit.Text.Trim()).Replace("[MaxMarks]", lblMaxMarks_Edit.Text.Trim()).Replace("[ObtainMark]", (row.FindControl("txtDLMarks") as TextBox).Text.Trim()).Replace("[Percentage]", perct);
                break;
               
            }

        }

            
        

        Clear_Error_Success_Box();
        Label46.Text = newTemplate.ToString();
        //System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTestSMS();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTestSMS();", true);
        UpdatePanel1.Update();

    }

    protected void btnSmsSend_ServerClick(object sender, System.EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSms();", true);
        //txtsmsstd.Text = "";
        //inputstd.Checked = true;
        //UpdatePaneSMSsend.Update();

        try
        {
            if (Flag_Authorise.Visible = true)
            {
                int resultid = 0,icountsms=0;

                string template = lblMessage_Template_SMS.Text.ToString().Trim();
                string newTemplate = "";

                string MsgMode = lblMessage_Mode_Fin.Text.ToString().Trim();
                string Message_cd = lblMessage_Code_Fin.Text.ToString().Trim();


                Label lblHeader_User_Code = default(Label);
                lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                string CreatedBy = null;
                CreatedBy = lblHeader_User_Code.Text;


                foreach (DataListItem row in dlGridDisplay_StudAttendance.Items)
                {
                    string msg = txtsmsstd.Text;
                    if ((row.FindControl("lblDLAttendStatus") as Label).Text == "Present" & !string.IsNullOrEmpty((row.FindControl("txtDLMarks") as TextBox).Text))
                    {
                        string num1 = (row.FindControl("lblMobileNo") as Label).Text;

                        string num2 = (row.FindControl("lblPMobileNo") as Label).Text;
                        string fname = (row.FindControl("lblFName") as Label).Text;
                        string upmsg = msg.Replace("< NAME >", (row.FindControl("lblDLStudentName") as Label).Text);

                        string perct = null;
                        TextBox txtDLMarks = (TextBox)row.FindControl("txtDLMarks");
                        perct = Convert.ToString((Convert.ToDouble(txtDLMarks.Text) / Convert.ToDouble(lblMaxMarks_Edit.Text)) * 100);
                        msg = upmsg;
                        upmsg = msg.Replace("< FIRSTNAME >", fname);
                        msg = upmsg;
                        upmsg = msg.Replace("< MXMARK >", lblMaxMarks_Edit.Text);
                        msg = upmsg;
                        upmsg = msg.Replace("< MARK >", (row.FindControl("txtDLMarks") as TextBox).Text);
                        msg = upmsg;
                        upmsg = msg.Replace("< TESTDATE >", lblTestDate_Edit.Text);
                        msg = upmsg;
                        upmsg = msg.Replace("< TESTNAME >", lblTestName_Edit.Text);
                        msg = upmsg;
                        upmsg = msg.Replace("< PERCENTAGE >", perct);
                        msg = upmsg;

                        newTemplate = template.Replace("[FullName]", (row.FindControl("lblDLStudentName") as Label).Text).Replace("[FirstName]", fname).Replace("[TestName]", lblTestName_Edit.Text.Trim()).Replace("[TestDate]", lblTestDate_Edit.Text.Trim()).Replace("[MaxMarks]", lblMaxMarks_Edit.Text.Trim()).Replace("[ObtainMark]", (row.FindControl("txtDLMarks") as TextBox).Text.Trim()).Replace("[Percentage]", perct).Replace("&", "%26").Replace("+", "%2D").Replace("%", "%25").Replace("#", "%23").Replace("=", "%3D").Replace("^", "%5E").Replace("~", "%7E");

                        if (MsgMode == "Auto")
                        {
                            resultid = ProductController.Insert_SMSLog(ddlCentre.SelectedValue.ToString(), Message_cd, num1, newTemplate, "0", "Auto", "Transactional");
                            icountsms = icountsms + 1;
                        }
                        else if (MsgMode == "Manual")
                        {
                            resultid = ProductController.Insert_SMSLog(ddlCentre.SelectedValue.ToString(), Message_cd, num1, newTemplate, "0", CreatedBy, "Transactional");
                            icountsms = icountsms + 1;
                        }

                    }

                }

                if (icountsms > 0)
                {
                    //Success SMS
                    Msg_Error2.Visible = false;
                    Msg_Success2.Visible = true;
                    lblSuccess2.Text = "SMS Sent Successfully!!";
                    UpdatePanelMsgBox2.Update();
                }
            }
            else
            {
                //Error MSG
                Msg_Error2.Visible = true;
                Msg_Success2.Visible = false;
                lblerror2.Text = "Marks Authorisation can't be done as marks of few students are not entered";
                UpdatePanelMsgBox2.Update();
            }

        }
        catch (Exception ex)
        {
        }
    }

    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);


        StringWriter swp = new StringWriter();
        HtmlTextWriter hwp = new HtmlTextWriter(swp);

        DataList1.Visible = true;
        DataList1.RenderControl(hw);
        divPrint.Visible = true;
        divPrint.RenderControl(hwp);

        string htmlprint = swp.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
        string html = htmlprint + sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload = new function(){");
        sb.Append("var printWin = window.open('', '', 'left=0");
        sb.Append(",top=0,width=1000,height=600,status=0');");
        sb.Append("printWin.document.write(\"");
        sb.Append(html);
        sb.Append("\");");
        sb.Append("printWin.document.close();");
        sb.Append("printWin.focus();");
        sb.Append("printWin.print();");
        sb.Append("printWin.close();};");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
        divPrint.Visible = false;
        DataList1.Visible = false;
    }

    protected void HLExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;
        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=Test_Marks.xls");


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

}
