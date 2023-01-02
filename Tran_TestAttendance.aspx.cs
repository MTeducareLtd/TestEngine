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



public partial class Tran_TestAttendance : System.Web.UI.Page
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
            //FillAttendanceType();
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
        FillGrid();
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

        btnAllStudAttend_Save.Visible = false;
        btnLock_Authorise.Visible = true;


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

                btnLock_UnAuthorise.Visible = true;
                btnLock_Authorise.Visible = false;
                Flag_Authorise.Visible = true;
            }
            else
            {

                btnLock_UnAuthorise.Visible = false;
                btnLock_Authorise.Visible = true;
                Flag_Authorise.Visible = false;
            }

            lblTestPKey_Edit.Text = e.CommandArgument.ToString();
            //StudentAttendenceRecord();
            int smscount = 0;

            DataSet DSSentStatus = ProductController.Check_SMSSendStatus(lblTestPKey_Edit.Text, 8);
            if (DSSentStatus != null)
            {
                if (DSSentStatus.Tables[0].Rows.Count > 0)
                {
                    smscount = Convert.ToInt32(DSSentStatus.Tables[0].Rows[0]["SMSSentFlag"].ToString());
                    if (smscount > 0)
                    {
                        Flag_Authorise0.Visible = true;
                    }
                    else
                    {
                        Flag_Authorise0.Visible = false;
                    }
                }
            }


            ////// Show Button & Flags for SMS

            string count = "", Notification = "";

            DataSet DSChk = ProductController.Check_MesageTemplate("014", ddlDivision.SelectedValue.ToString().Trim(), 1);
            if (DSChk != null)
            {
                if (DSChk.Tables[0].Rows.Count > 0)
                {
                    count = DSChk.Tables[0].Rows[0]["count1"].ToString();
                    if (count == "0")
                    {
                        //Disable
                        Label110.Visible = false;
                        lblMessage_Template_SMS.Visible = false;
                        Btn_TestMessage.Visible = false;
                    }
                    else
                    {
                        Label110.Visible = true;
                        lblMessage_Template_SMS.Visible = true;
                        Btn_TestMessage.Visible = true;

                        Notification = DSChk.Tables[0].Rows[0]["Send_Type"].ToString();
                        if (Notification == "Manual")
                        {
                            //Show Button
                            BtnAttendanceMessage.Visible = true;
                            FlagAutoSMS.Visible = false;

                            ///For Preview
                            

                        }
                        else if (Notification == "Auto")
                        {
                            //Show Notification
                            BtnAttendanceMessage.Visible = false;
                            FlagAutoSMS.Visible = true;

                        }
                    }


                }
            }


            ///// For Preview SMS

            string Template = "", Message_cd = "", MsgMode = "", newTemplate = "";

            DataSet DSgridChk = ProductController.Check_MesageTemplate("014", ddlDivision.SelectedValue.ToString().Trim(), 1);
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

            ddlEntityType.SelectedValue = "001";//Student
            btnSearchAttendance_Click(source, e);

        }
    }

    protected void Btn_TestMessage_Click(object sender, System.EventArgs e)
    {
        string template = lblMessage_Template_SMS.Text.ToString().Trim();
        string newTemplate = "", firstname = "", date = "", StudentName = "", Centre_code = "", RollNo = "", MobileNo="";

        string Pkey = lblTestPKey_Edit.Text;
        DataSet dsStudent = ProductController.GetAllStudentDetails_ByTestPKey(Pkey, 0);

        for (int i = 0; i <= dsStudent.Tables[0].Rows.Count - 1; i++)
        {
            firstname = dsStudent.Tables[0].Rows[i]["FirstName"].ToString();
            date = dsStudent.Tables[0].Rows[i]["Test_Date"].ToString();
            StudentName = dsStudent.Tables[0].Rows[i]["StudentName"].ToString();
            Centre_code = dsStudent.Tables[0].Rows[i]["Centre_Code"].ToString();
            RollNo = dsStudent.Tables[0].Rows[i]["RollNo"].ToString();

            MobileNo = dsStudent.Tables[0].Rows[i]["mobileno"].ToString();

            newTemplate = template.Replace("[ParentName]", "").Replace("[StudentFullName]", StudentName).Replace("[RollNo]", RollNo).Replace("[FirstName]", firstname).Replace("[SessionDate]", date).Replace("[SubjectName]", lblSubject_Edit.Text.Trim()).Replace("[ChapterName]", "").Replace("%2526", "%26");

            break;

        }

        //foreach (DataListItem row in dlGridDisplay_StudAttendance.Items)
        //{
        //    string msg = txtsmsstd.Text;
        //    if ((row.FindControl("lblDLAttendStatus") as Label).Text == "Present" & !string.IsNullOrEmpty((row.FindControl("txtDLMarks") as TextBox).Text))
        //    {
        //        string num1 = (row.FindControl("lblMobileNo") as Label).Text;

        //        string num2 = (row.FindControl("lblPMobileNo") as Label).Text;
        //        string fname = (row.FindControl("lblFName") as Label).Text;
        //        string upmsg = msg.Replace("< NAME >", (row.FindControl("lblDLStudentName") as Label).Text);

        //        string perct = null;
        //        TextBox txtDLMarks = (TextBox)row.FindControl("txtDLMarks");
        //        perct = Convert.ToString((Convert.ToDouble(txtDLMarks.Text) / Convert.ToDouble(lblMaxMarks_Edit.Text)) * 100);
        //        msg = upmsg;
        //        upmsg = msg.Replace("< FIRSTNAME >", fname);
        //        msg = upmsg;
        //        upmsg = msg.Replace("< MXMARK >", lblMaxMarks_Edit.Text);
        //        msg = upmsg;
        //        upmsg = msg.Replace("< MARK >", (row.FindControl("txtDLMarks") as TextBox).Text);
        //        msg = upmsg;
        //        upmsg = msg.Replace("< TESTDATE >", lblTestDate_Edit.Text);
        //        msg = upmsg;
        //        upmsg = msg.Replace("< TESTNAME >", lblTestName_Edit.Text);
        //        msg = upmsg;
        //        upmsg = msg.Replace("< PERCENTAGE >", perct);
        //        msg = upmsg;

        //        newTemplate = template.Replace("[FullName]", (row.FindControl("lblDLStudentName") as Label).Text).Replace("[FirstName]", fname).Replace("[TestName]", lblTestName_Edit.Text.Trim()).Replace("[TestDate]", lblTestDate_Edit.Text.Trim()).Replace("[MaxMarks]", lblMaxMarks_Edit.Text.Trim()).Replace("[ObtainMark]", (row.FindControl("txtDLMarks") as TextBox).Text.Trim()).Replace("[Percentage]", perct);
        //        break;

        //    }

        //}




        Clear_Error_Success_Box();
        Label46.Text = newTemplate.ToString();
        //System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTestSMS();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalTestSMS();", true);
        
        UpdatePanel1.Update();

    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
        Clear_AddPanel();
    }


    private void FillGrid()
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




        lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
        lblDivision_Result.Text = Convert.ToString(ddlDivision.SelectedItem.ToString());
        lblAcadYear_Result.Text = Convert.ToString(ddlAcadYear.SelectedItem.ToString());
        lblStandard_Result.Text = Convert.ToString(ddlStandard.SelectedItem.ToString());
        lblTestCategory_Result.Text = Convert.ToString(ddlTestCategory.SelectedItem.ToString());
        lblCentre_Result.Text = Convert.ToString(ddlCentre.SelectedItem.ToString());
    }

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        FillGrid();
    }

    private void FillEntityType()
    {
        string Action_Id = "003";
        //Action_Id = ddlAttendanceType.SelectedValue;

        int Flag = 1;
        //if (ddlAttendanceType.SelectedIndex == 0)
        //{
        //    Flag = 2;
        //}
        //else
        //{
        //    Flag = 1;
        //}

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


            DataSet dsStudent = ProductController.GetAllStudentDetails_ByTestPKey(TestPKey, ActionFlag);
            if (dsStudent != null)
            {
                if (dsStudent.Tables[1].Rows.Count > 0)
                {

                    dlGridDisplay_StudAttendance.DataSource = dsStudent;
                    dlGridDisplay_StudAttendance.DataBind();



                    long ActualBatchStrength = 0;
                    btnAllStudAttend_Save.Visible = true;
                    btnLock_Authorise.Visible = false;
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
                    }
                    else
                    {
                        AbsentPercent = 0;
                    }
                    if (AbsentPercent == 0 && PresentPercent == 0)
                    {

                        foreach (DataListItem dtlItem in dlGridDisplay_StudAttendance.Items)
                        {
                            CheckBox chkReset = (CheckBox)dtlItem.FindControl("chkReset");
                            CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
                            System.Web.UI.HtmlControls.HtmlInputText ReTestDate = (System.Web.UI.HtmlControls.HtmlInputText)dtlItem.FindControl("ReTestDate");
                            HtmlGenericControl spanFromTime = (HtmlGenericControl)dtlItem.FindControl("spanFromTime");
                            chkReset.Visible = false;
                            ReTestDate.Visible = false;
                            spanFromTime.Visible = false;
                            // added new
                            DropDownList ddlabsentreason = (DropDownList)dtlItem.FindControl("ddlabsentreason");
                            ddlabsentreason.SelectedIndex = 0;
                                DataSet ds1 = ProductController.GetAllAbsentreasons();

                                BindDDL(ddlabsentreason, ds1, "AbsentReason_Name", "Test_Cancel_Reason_ID");
                                ddlabsentreason.Items.Insert(0, "Select");
                                ddlabsentreason.SelectedIndex = 0;
                                Label lblDLAbsentReasonID = (Label)dtlItem.FindControl("lblDLAbsentReasonID");
                                Label lblDLAbsentReason = (Label)dtlItem.FindControl("lblDLAbsentReason");
                                // lblDLAbsentReason.Text = ddlabsentreason.SelectedIndex.ToString();
                                ddlabsentreason.SelectedValue = lblDLAbsentReason.Text;

                                ddlabsentreason.SelectedValue = lblDLAbsentReasonID.Text;


                                if (chkStudent.Checked == true)
                                {
                                    ddlabsentreason.Enabled = false;
                                    ddlabsentreason.SelectedIndex = 0;
                                }
                                // till here
                            
                        }
                    }
                    else
                    {
                        foreach (DataListItem dtlItem in dlGridDisplay_StudAttendance.Items)
                        {
                            CheckBox chkReset = (CheckBox)dtlItem.FindControl("chkReset");
                            CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
                            System.Web.UI.HtmlControls.HtmlInputText ReTestDate = (System.Web.UI.HtmlControls.HtmlInputText)dtlItem.FindControl("ReTestDate");
                            HtmlGenericControl spanFromTime = (HtmlGenericControl)dtlItem.FindControl("spanFromTime");

                            //added new
                             DropDownList ddlabsentreason = (DropDownList)dtlItem.FindControl("ddlabsentreason");
                            ddlabsentreason.SelectedIndex = 0;
                            DataSet ds1 = ProductController.GetAllAbsentreasons();

                            BindDDL(ddlabsentreason, ds1, "AbsentReason_Name", "Test_Cancel_Reason_ID");
                            ddlabsentreason.Items.Insert(0, "Select");
                            ddlabsentreason.SelectedIndex = 0;
                            Label lblDLAbsentReasonID = (Label)dtlItem.FindControl("lblDLAbsentReasonID");
                            Label lblDLAbsentReason = (Label)dtlItem.FindControl("lblDLAbsentReason");
                            // lblDLAbsentReason.Text = ddlabsentreason.SelectedIndex.ToString();
                            ddlabsentreason.SelectedValue = lblDLAbsentReason.Text;

                            ddlabsentreason.SelectedValue = lblDLAbsentReasonID.Text;
                            //till here
                            if (chkStudent.Checked == true)
                            {
                                ddlabsentreason.Enabled = false;
                                ddlabsentreason.SelectedIndex = 0;
                            }
                            if (chkStudent.Checked == true && chkReset.Checked == false)
                            {
                                chkReset.Enabled = false;
                                ReTestDate.Disabled = true;
                                spanFromTime.Visible = false;

                            }
                            else if (chkStudent.Checked == true && chkReset.Checked == true)
                            {
                                chkReset.Enabled = true;
                                ReTestDate.Disabled = false;
                                spanFromTime.Visible = true;

                            }
                            else if (chkStudent.Checked == false && chkReset.Checked == false)
                            {
                                chkReset.Enabled = true;
                                ReTestDate.Disabled = false;
                                spanFromTime.Visible = true;
                            }


                        }
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
                        btnLock_UnAuthorise.Visible = true;
                        btnLock_Authorise.Visible = false;
                        Flag_Authorise.Visible = true;
                        btnAllStudAttend_Save.Visible = false;
                    }
                    else
                    {

                        btnLock_UnAuthorise.Visible = false;
                        //btnLock_Authorise.Visible = true;
                        Flag_Authorise.Visible = false;
                        btnAllStudAttend_Save.Visible = true;
                    }

                    dlGridDisplay_StudAttendance.Visible = true;

                }
                else
                {
                    dlGridDisplay_StudAttendance.Visible = false;
                    btnAllStudAttend_Save.Visible = false;
                    btnLock_Authorise.Visible = true;
                }
            }
            else
            {
                dlGridDisplay_StudAttendance.Visible = false;
                btnAllStudAttend_Save.Visible = false;
                btnLock_Authorise.Visible = true;
            }
        }
        //UpdatePanel_Add.Update();
        //UpdatePanel_StudAttendance_Result.Update();
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
        //if (Convert.ToInt32(lblSummary_BatchStrength.Text) > 0 & Convert.ToInt32(lblSummary_NMCount.Text) != 0)
        //{
        //    Show_Error_Success_Box2("E", "0031");
        //    dlGridStudent.Focus();
        //    return;
        //}

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
        string count;

        DataSet DSChk = ProductController.Check_MesageTemplate("014", ddlDivision.SelectedValue.ToString().Trim(), 1);
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
                    string MsgMode = DSChk.Tables[0].Rows[0]["Send_Type"].ToString();

                    if (MsgMode == "Auto")
                    {
                        Message_Template();
                    }
                }
            }
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

            //DataSet dsReason = ProductController.GetAllActiveTestAbsentReason();
            //BindDDL(ddlDLAbsentReason, dsReason, "AbsentReason_Name", "AbsentReason_ID");
            //ddlDLAbsentReason.Items.Insert(0, "[ Select ]");
            //ddlDLAbsentReason.SelectedIndex = 0;

            //try
            //{
            //    string ReasonID = null;
            //    ReasonID = lblDLAbsentReasonID.Text;
            //    ddlDLAbsentReason.SelectedValue = ReasonID;
            //}
            //catch (Exception ex)
            //{
            //    if (ddlDLAbsentReason.Items.Count > 0)
            //        ddlDLAbsentReason.SelectedIndex = 0;
            //}


            DropDownList ddlabsentreason = (DropDownList)e.Item.FindControl("AbsentReason_ID");
            DataSet ds1 = ProductController.GetAllAbsentreasons();

            BindDDL(ddlabsentreason, ds1, "AbsentReason_ID", "AbsentReason_ID");
            ddlabsentreason.Items.Insert(0, "Select");
            ddlabsentreason.SelectedIndex = 0;


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

    public Tran_TestAttendance()
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

    protected void chkAttendanceAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlGridDisplay_StudAttendance.Items)
        {
            DropDownList ddlabsentreason = (DropDownList)dtlItem.FindControl("ddlabsentreason");
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkStudent");
            chkitemck.Checked = s.Checked;

            if (s.Checked == true)
            {
                ddlabsentreason.Enabled = false;
                ddlabsentreason.SelectedIndex = 0;

            }
            else

            {

                ddlabsentreason.Enabled = true; 
            }
        }

    }

    protected void chkAttendance_CheckedChanged(object sender, EventArgs e)

    {
        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlGridDisplay_StudAttendance.Items)
        {
            DropDownList ddlabsentreason = (DropDownList)dtlItem.FindControl("ddlabsentreason");
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkStudent");


            if (chkitemck.Checked == true)
            {
                ddlabsentreason.Enabled = false;
                ddlabsentreason.SelectedIndex = 0;

            }
            else
            {

                ddlabsentreason.Enabled = true;
            }
        }

    }

    protected void btnAllStudAttend_Save_Click(object sender, EventArgs e)
    {
        try
        {

            string TestPKey = null;
            TestPKey = lblTestPKey_Edit.Text;
            string SBEntryCode = "";


            Clear_Error_Success_Box();

            bool flag = false;
            bool flag1 = false;
            //foreach (DataListItem dtlItem in dlGridDisplay_StudAttendance.Items)
            //{
            //    CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
            //    //TextBox lblDLAbsentReason = (TextBox)dtlItem.FindControl("lblDLAbsentReason");


            //    DropDownList ddlabsentreason = (DropDownList)dtlItem.FindControl("ddlabsentreason");
            //    Label lblDLAbsentReasonID = (Label)dtlItem.FindControl("lblDLAbsentReasonID");
            //    Label lblDLAbsentReason = (Label)dtlItem.FindControl("lblDLAbsentReason");
                


            //    if (ddlabsentreason.Enabled == true && ddlabsentreason.SelectedIndex == 0)
            //    {
            //        Show_Error_Success_Box("E", "Absent Reason Not Selected For Absent Students");
            //        return;
            //    }

            //    if (chkStudent.Checked == true && ddlabsentreason.SelectedIndex == 0)
            //    {
            //        flag = false;
            //    }

            //    if (chkStudent != null && chkStudent.Checked == false)
            //    {
            //        flag = true;
            //    }
            //    //if (flag == true && l)
            //    //{
            //    //    flag = false;
            //    //}
            //    if (ddlabsentreason.SelectedIndex != 0)
            //    {
            //        flag = true;
            //    }
            //    if (chkStudent.Checked == true)
            //    {
            //        ddlabsentreason.SelectedIndex = 0;
            //    }
            //    if (chkStudent.Checked == false && ddlabsentreason.SelectedIndex == 0)
            //    {
            //        //lbl_DLError.Title = "Please Select Reason";
            //        //icon_Error.Visible = true;
            //        return;
            //    }

            //    if (flag == false && chkStudent.Checked == false && ddlabsentreason.SelectedIndex == 0)
            //    {

            //        flag1 = true;
            //        //lbl_DLError.Title = "Please Select Reason";
            //        //icon_Error.Visible = true;
            //        UpdatePanelMsgBox.Update();
            //        lblDLAbsentReason.Focus();
            //        lblDLAbsentReasonID.Focus();
            //        return;

            //    }
            //}

            if (flag1 == false)
            {


                Label lblHeader_User_Code = default(Label);
                lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                string CreatedBy = null;
                CreatedBy = lblHeader_User_Code.Text;
                string ActionFlag = "";


                int ResultId = 0;

                foreach (DataListItem dtlItem in dlGridDisplay_StudAttendance.Items)
                {
                    CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
                    CheckBox chkReset = (CheckBox)dtlItem.FindControl("chkReset");
                    Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
                    //TextBox lblDLAbsentReason = (TextBox)dtlItem.FindControl("lblDLAbsentReason");
                    DropDownList ddlabsentreason = (DropDownList)dtlItem.FindControl("ddlabsentreason");

                    System.Web.UI.HtmlControls.HtmlInputText ReTestDate = (System.Web.UI.HtmlControls.HtmlInputText)dtlItem.FindControl("ReTestDate");
                    int isRetest = 0;
                    if (chkReset.Checked)
                    {
                        isRetest = 1;
                    }
                    else
                    {
                        isRetest = 0;
                    }


                    if (chkReset.Checked == true && ReTestDate.Value == "")
                    {
                        Msg_Error.Visible = true;
                        Msg_Success.Visible = false;
                        lblerror.Text = "Please Enter Re-TestDate";
                        UpdatePanelMsgBox.Update();
                        ReTestDate.Focus();
                        return;
                    }
                    if (chkReset.Checked == false && ReTestDate.Value != "")
                    {
                        Msg_Error.Visible = true;
                        Msg_Success.Visible = false;
                        lblerror.Text = "Please Check Re-Test";
                        UpdatePanelMsgBox.Update();
                        chkReset.Focus();
                        return;
                    }

                    Nullable<DateTime> retestdate = null;
                    if (ReTestDate.Value != "")
                    {
                        DateTime tdt = DateTime.ParseExact(ReTestDate.Value, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        retestdate = tdt;
                    }




                    SBEntryCode = lblSBEntryCode.Text.Trim();
                    string AbsentReason = ddlabsentreason.Text.Trim();
                    if (ddlabsentreason.SelectedIndex == 0)
                    {
                        AbsentReason="";
                    }

                    if (chkStudent.Checked)
                    {
                        ActionFlag = "P";
                        AbsentReason = "";
                    }
                    else
                    {
                        ActionFlag = "A";
                    }

                    //Mark exemption/absent/present for those students who are selected
                    ResultId = ProductController.Insert_UpdateStudentTestAttendace(TestPKey, ActionFlag, SBEntryCode, CreatedBy, AbsentReason, isRetest, retestdate);
                }


                //Close the Add Panel and go to Search Grid
                if (ResultId == 1)
                {
                    btnSearchAttendance_Click(sender, e);
                    Show_Error_Success_Box("S", "0000");
                    btnLock_Authorise.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }


    }

    protected void btnMesage_ManualSending_ServerClick(object sender, System.EventArgs e)
    {
        //// Sending Code
        if (Flag_Authorise.Visible == true)
        {
            Message_Template();
        }
        else
        {
            Show_Error_Success_Box("E", "Attendance Authorisation can't be done as attendance of few Students is not marked");
        }

    }

    private void Message_Template()
    {
        try
        {
            string count = "", Notification = "", newTemplate = "", firstname = "", date = "", StudentName = "", Centre_code = "", MobileNo = "", RollNo = "";
            int resultid = 0;
            int smscount = 0;

            DataSet DSSentStatus = ProductController.Check_SMSSendStatus(lblTestPKey_Edit.Text, 8);
            if (DSSentStatus != null)
            {
                if (DSSentStatus.Tables[0].Rows.Count > 0)
                {
                    smscount = Convert.ToInt32(DSSentStatus.Tables[0].Rows[0]["SMSSentFlag"].ToString());
                    if (smscount == 0)
                    {

                        Label lblHeader_User_Code = default(Label);
                        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                        string CreatedBy = null;
                        CreatedBy = lblHeader_User_Code.Text;

                        DataSet DSChk = ProductController.Check_MesageTemplate("014", ddlDivision.SelectedValue.ToString().Trim(), 1);
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
                                    string Template = DSChk.Tables[0].Rows[0]["Message_Template"].ToString();
                                    string Message_cd = DSChk.Tables[0].Rows[0]["Message_Code"].ToString();
                                    string MsgMode = DSChk.Tables[0].Rows[0]["Send_Type"].ToString();

                                    string Pkey = lblTestPKey_Edit.Text;
                                    DataSet dsStudent = ProductController.GetAllStudentDetails_ByTestPKey(Pkey, 0);

                                    string strQuery = "AttendStatusStr = 'Absent'";
                                    DataRow[] drFilterRows = dsStudent.Tables["Table"].Select(strQuery);

                                    int iMSg = 0;
                                    DataSet dsNew1 = dsStudent.Clone();

                                    foreach (DataRow dr in drFilterRows)
                                        dsNew1.Tables["Table"].ImportRow(dr);

                                    for (int i = 0; i <= dsNew1.Tables[0].Rows.Count - 1; i++)
                                    {
                                        firstname = dsNew1.Tables[0].Rows[i]["FirstName"].ToString();
                                        date = dsNew1.Tables[0].Rows[i]["Test_Date"].ToString();
                                        StudentName = dsNew1.Tables[0].Rows[i]["StudentName"].ToString();
                                        Centre_code = dsNew1.Tables[0].Rows[i]["Centre_Code"].ToString();
                                        RollNo = dsNew1.Tables[0].Rows[i]["RollNo"].ToString();

                                        MobileNo = dsNew1.Tables[0].Rows[i]["mobileno"].ToString();

                                        newTemplate = Template.Replace("[ParentName]", "").Replace("[StudentFullName]", StudentName).Replace("[RollNo]", RollNo).Replace("[FirstName]", firstname).Replace("[SessionDate]", date).Replace("[SubjectName]", lblSubject_Edit.Text.Trim()).Replace("[ChapterName]", "").Replace("%2526", "%26");

                                        if (MsgMode == "Auto")
                                        {
                                            resultid = ProductController.Insert_SMSLog(Centre_code, Message_cd, MobileNo, newTemplate, "0", "Auto", "Transactional");
                                            iMSg = iMSg + 1;
                                        }
                                        else if (MsgMode == "Manual")
                                        {
                                            resultid = ProductController.Insert_SMSLog(Centre_code, Message_cd, MobileNo, newTemplate, "0", CreatedBy, "Transactional");
                                            iMSg = iMSg + 1;
                                        }

                                    }

                                    int rowcount = dsNew1.Tables[0].Rows.Count;
                                    if (rowcount > 0)
                                    {
                                        resultid = ProductController.Update_SMSSendStatus_T601(lblTestPKey_Edit.Text, 1, MsgMode, 9);
                                    }

                                    if (iMSg > 0)
                                    {
                                        Show_Error_Success_Box2("S", "SMS Sent Successfully");
                                    }

                                }


                            }
                        }

                    }
                    else
                    {
                        //Message is already sent
                    }
                }
            }

            //////////




        }
        catch (Exception ex)
        {
        }

    }



    protected void HLExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;
        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=Test_Attendance.xls");
        

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

    private void FillDDL_AbsentReason(DataListCommandEventArgs e)
    {
        try
        {


            if (e.CommandName == "EditReason")
            {
                DropDownList ddlabsentreason = (DropDownList)e.Item.FindControl("AbsentReason_ID");
                DataSet ds1 = ProductController.GetAllAbsentreasons();

                BindDDL(ddlabsentreason, ds1, "Division_Name", "Division_Code");
                ddlabsentreason.Items.Insert(0, "Select");
                ddlabsentreason.SelectedIndex = 0;
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
    private void StudentAttendenceRecord()
    {
        Clear_Error_Success_Box();

        foreach (DataListItem dtlItem in dlGridDisplay_StudAttendance.Items)
        {


            DropDownList ddlabsentreason = (DropDownList)dtlItem.FindControl("ddlabsentreason");
            ddlabsentreason.SelectedIndex = 0;
            DataSet ds1 = ProductController.GetAllAbsentreasons();

            BindDDL(ddlabsentreason, ds1, "AbsentReason_Name", "AbsentReason");
            ddlabsentreason.Items.Insert(0, "Select");
            ddlabsentreason.SelectedIndex = 0;
            Label lblDLAbsentReasonID = (Label)dtlItem.FindControl("lblDLAbsentReasonID");
            Label lblDLAbsentReason = (Label)dtlItem.FindControl("lblDLAbsentReason");
            // lblDLAbsentReason.Text = ddlabsentreason.SelectedIndex.ToString();
            ddlabsentreason.SelectedValue = lblDLAbsentReason.Text;

            ddlabsentreason.SelectedValue = lblDLAbsentReasonID.Text;



        }
        long ActualBatchStrength = 0;
        btnAllStudAttend_Save.Visible = true;
        btnLock_Authorise.Visible = false;



        dlGridDisplay_StudAttendance.Visible = true;

    }
             
         
         


}