using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using System.Text;

partial class Tran_TestSchedule : System.Web.UI.Page
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            // txtTestDate_Add.Value = System.DateTime.Now.ToString("dd MMM yyyy");
            FillDDL_Division();
            FillDDL_AcadYear();
            FillDDL_ReTest_Flag();
            FillDDL_TestCategories();
            FillDDL_TestTypes();
            divPrint.Visible = false;
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

    private void FillDDL_ReTest_Flag()
    {
        ddlReTest_Add.Items.Clear();
        ddlReTest_Add.Items.Add("Yes");
        ddlReTest_Add.Items.Add("No");
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


        BindDDL(ddlDivision_Add, dsDivision, "Division_Name", "Division_Code");
        ddlDivision_Add.Items.Insert(0, "Select");
        ddlDivision_Add.SelectedIndex = 0;


        BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
        ddlDivision.Items.Insert(0, "Select");
        ddlDivision.SelectedIndex = 0;

        //DataSet dsDivision = null;
        //OrderDataService.OrderDataServiceSoapClient client = new OrderDataService.OrderDataServiceSoapClient();
        //dsDivision = client.GetCompany_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, "", "", "2", lblHeader_DBName.Text);
        //if (dsDivision != null)
        //{
        //    if (dsDivision.Tables.Count != 0)
        //    {
        //        BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
        //        BindDDL(ddlDivision_Add, dsDivision, "Division_Name", "Division_Code");
        //        ddlDivision.Items.Insert(0, "Select");
        //        ddlDivision.SelectedIndex = 0;

        //        ddlDivision_Add.Items.Insert(0, "Select");
        //        ddlDivision_Add.SelectedIndex = 0;

        //    }
        //    else
        //    {

        //        ddlDivision.Items.Insert(0, "Select");
        //        ddlDivision.SelectedIndex = 0;
        //        ddlDivision_Add.Items.Insert(0, "Select");
        //        ddlDivision_Add.SelectedIndex = 0;
        //    }

        //}
        //else
        //{

        //    ddlDivision.Items.Insert(0, "Select");
        //    ddlDivision.SelectedIndex = 0;
        //    ddlDivision_Add.Items.Insert(0, "Select");
        //    ddlDivision_Add.SelectedIndex = 0;
        //}

    }

    private void FillDDL_AcadYear()
    {
        DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcadYear, dsAcadYear, "Description", "Id");
        ddlAcadYear.Items.Insert(0, "Select");
        ddlAcadYear.SelectedIndex = 0;

        BindDDL(ddlAcadYear_Add, dsAcadYear, "Description", "Id");
        ddlAcadYear_Add.Items.Insert(0, "Select");
        ddlAcadYear_Add.SelectedIndex = 0;

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
            btnAddPaperChecker.Visible = false;
        }
        else if (Mode == "Edit")
        {
            DivEditPanel.Visible = true;
            DivAddPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
            btnAddPaperChecker.Visible = false;
        }
        Clear_Error_Success_Box();
    }

    protected void BtnAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Add");
    }

    protected void btnAddPaperChecker_Click(object sender, System.EventArgs e)
    {
        //ControlVisibility("Add");
        string BatchCode = "";
        //BatchCode = ddlBatch_Add.SelectedValue;
        for (int cnt = 0; cnt <= lstboxbatch.Items.Count - 1; cnt++)
        {
            if (lstboxbatch.Items[cnt].Selected == true)
            {
                BatchCode = BatchCode + lstboxbatch.Items[cnt].Value + ",";
            }
        }

        if (BatchCode != "")
        {
            BatchCode = BatchCode.Substring(0, BatchCode.Length - 1);
        }

        Response.Redirect("Tran_TestAnswerPapers.aspx?Div=" + ddlDivision_Add.SelectedValue + "&AcadYear=" + ddlAcadYear_Add.SelectedValue + "&Center=" + ddlCentre_Add.SelectedValue + "&Course=" + ddlStandard_Add.SelectedValue + "&BatchCode=" + BatchCode + "&TestCategoryId=" + lblTestCategoryId.Text + "&TestTypeId=" + lblTestTypeId.Text + "&TestId=" + ddlTestName_Add.SelectedValue);
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
            if (e.CommandName == "Delete")
            {
                FillDDL_Test_Cancel_Reason();
                lbldelCode.Text = e.CommandArgument.ToString();
                txtDeleteItemName.Text = (((Label)e.Item.FindControl("lblModeName")).Text);

                if (ProductController.GetCountRemoveTestRequest(lbldelCode.Text.Trim()) == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalWarningDelete();", true);
                }

            }
            else if (e.CommandName == "Edit")
            {
                ControlVisibility("Edit");
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
                txtMaxMarks_Edit.Text = (((Label)e.Item.FindControl("lblDLMaxMarks")).Text);
                txtTestDate_Edit.Value = (((Label)e.Item.FindControl("lblDLTestDate")).Text);
                txtFromTime_Edit.Text = (((Label)e.Item.FindControl("lblDLFromTime")).Text);
                txtToTime_Edit.Text = (((Label)e.Item.FindControl("lblDLToTime")).Text);
                lblTestPKey_Edit.Text = e.CommandArgument.ToString();


                string Test_ID = "", Subject_Code = "", division_cd = "", Acad_yr = "", Standard_Cd = "", PTCode = "";
                DataSet ds = ProductController.GettestCode_ForSubCode(lblTestPKey_Edit.Text.Trim(), 1);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Test_ID = Convert.ToString(ds.Tables[0].Rows[0]["Test_Id"]);
                    division_cd = Convert.ToString(ds.Tables[0].Rows[0]["division_code"]);
                    Acad_yr = Convert.ToString(ds.Tables[0].Rows[0]["Acad_Year"]);
                    Standard_Cd = Convert.ToString(ds.Tables[0].Rows[0]["Standard_Code"]);
                    PTCode = Convert.ToString(ds.Tables[0].Rows[0]["Partner_Code"]);

                    string Pkey23 = division_cd + "%" + Acad_yr + "%" + Standard_Cd + "%" + Test_ID;

                    DataSet dsTest = ProductController.GetTestMasterBY_PKey(Pkey23, 1);

                    if (dsTest.Tables[0].Rows.Count > 0)
                    {

                        Subject_Code = Convert.ToString(dsTest.Tables[0].Rows[0]["subjects_code"]);

                        if (Subject_Code.Length == 0)
                        {
                            lsteditteacher.Items.Clear();
                            //ddlTeacherName_Edit.Items.Clear();
                            //ddlTeacherName_Edit.Items.Insert(0, "Select");
                            //ddlTeacherName_Edit.SelectedIndex = 0;
                        }
                        else
                        {
                            DataSet dsPartnerCD = ProductController.GetPartnerCode_BySubCode(division_cd, Acad_yr, Standard_Cd, Subject_Code);
                            if (dsPartnerCD.Tables[0].Rows.Count > 0)
                            {
                                BindListBox(lsteditteacher, dsPartnerCD, "Partner_Name", "Partner_Code");
                                //BindDDL(ddlTeacherName_Edit, dsPartnerCD, "Partner_Name", "Partner_Code");
                                //ddlTeacherName_Edit.Items.Insert(0, "Select");
                                //ddlTeacherName_Edit.SelectedIndex = 0;
                            }
                            else
                            {
                                lsteditteacher.Items.Clear();
                                //ddlTeacherName_Edit.Items.Clear();
                                //ddlTeacherName_Edit.Items.Insert(0, "Select");
                                //ddlTeacherName_Edit.SelectedIndex = 0;
                            }

                        }


                    }

                    if (PTCode.Length == 0)
                    {
                        //ddlTeacherName_Edit.SelectedIndex = 0;
                    }
                    else
                    {
                        int RCnt1 = 0;
                        string[] TeacherList = PTCode.Split(',');

                        if (TeacherList.Length != 0)
                        {
                            foreach (string item in TeacherList)
                            {
                                for (RCnt1 = 0; RCnt1 <= lsteditteacher.Items.Count - 1; RCnt1++)
                                {
                                    if (item == lsteditteacher.Items[RCnt1].Value)
                                    {
                                        lsteditteacher.Items[RCnt1].Selected = true;
                                       // lblteachers.Text = lblteachers.Text + ddlTeacher.Items[RCnt1].ToString() + ',';

                                        break; // TODO: might not be correct. Was : Exit For
                                    }
                                }

                            }
                        }
                       // ddlTeacherName_Edit.SelectedValue = PTCode;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
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

    protected void ddlAcadYear_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard_Add();
        FillDDL_Centre_Add();
        Clear_Error_Success_Box();
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

        //ddlBatch_Add.Items.Clear();
        lstboxbatch.Items.Clear();
        ddlTestName_Add.Items.Clear();
        Clear_TestDetails();
    }

    protected void ddlDivision_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard_Add();
        FillDDL_Centre_Add();
        Clear_Error_Success_Box();
    }

    private void FillDDL_Centre_Add()
    {
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        string Div_Code = null;
        Div_Code = ddlDivision_Add.SelectedValue;

        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);

        BindDDL(ddlCentre_Add, dsCentre, "Center_Name", "Center_Code");
        ddlCentre_Add.Items.Insert(0, "Select");
        ddlCentre_Add.SelectedIndex = 0;
    }

    private void FillDDL_Batch_Add()
    {
        string Div_Code = null;
        Div_Code = ddlDivision_Add.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear_Add.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard_Add.SelectedValue;

        string CentreCode = null;
        CentreCode = ddlCentre_Add.SelectedValue;

        DataSet dsBatch = ProductController.GetAllActive_Batch_ForStandard(Div_Code, YearName, StandardCode, CentreCode);


        BindListBox(lstboxbatch, dsBatch, "Batch_Name", "Batch_Code");

        ddlTestName_Add.Items.Clear();
        Clear_TestDetails();
    }

    protected void ddlCentre_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Batch_Add();
        Clear_Error_Success_Box();
    }

    protected void ddlStandard_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Batch_Add();
        Clear_Error_Success_Box();
    }

    private void FillDDL_Test_Add()
    {
        try
        {
            string Div_Code = null;
            Div_Code = ddlDivision_Add.SelectedValue;

            string YearName = null;
            YearName = ddlAcadYear_Add.SelectedItem.ToString();

            string StandardCode = null;
            StandardCode = ddlStandard_Add.SelectedValue;

            string CentreCode = null;
            CentreCode = ddlCentre_Add.SelectedValue;

            int ReTestFlag = 0;
            if (ddlReTest_Add.SelectedIndex == 0)
            {
                ReTestFlag = 1;
            }
            else
            {
                ReTestFlag = 0;
            }


            string BatchCode = null;
            //BatchCode = ddlBatch_Add.SelectedValue;
            for (int cnt = 0; cnt <= lstboxbatch.Items.Count - 1; cnt++)
            {
                if (lstboxbatch.Items[cnt].Selected == true)
                {
                    BatchCode = BatchCode + lstboxbatch.Items[cnt].Value + ",";
                }
            }

            BatchCode = BatchCode.Substring(0, BatchCode.Length - 1);
            DataSet dsTest = ProductController.GetTestFor_Batch_Centre(Div_Code, YearName, CentreCode, StandardCode, BatchCode, ReTestFlag);
            BindDDL(ddlTestName_Add, dsTest, "Test_Name", "Test_Code");
            ddlTestName_Add.Items.Insert(0, "Select");
            ddlTestName_Add.SelectedIndex = 0;

            Clear_TestDetails();
        }
        catch (Exception ex)
        {


        }
    }

    protected void ddlBatch_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Test_Add();
        Clear_Error_Success_Box();
    }

    protected void ddlTestName_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        string PKey = null;
        PKey = ddlTestName_Add.SelectedValue;
        Clear_TestDetails();
        FillTestMasterDetails(PKey);
    }

    private void FillTestMasterDetails(string PKey)
    {

        string Subject_Code = "";

        DataSet dsTest = ProductController.GetTestMasterBY_PKey(PKey, 1);

        if (dsTest.Tables[0].Rows.Count > 0)
        {
            txtMaxMarks_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["MaxMarks"]);
            txtTestCategory_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["TestCategory_Name"]);
            lblTestCategoryId.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["testcategory_id"]);
            txtTestType_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["TestType_Name"]);
            lblTestTypeId.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["testtype_id"]);
            txtSubject_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Subjects"]);
            txtRemarks_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Remarks"]);
            txtChapters_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Chapters"]);
            Subject_Code = Convert.ToString(dsTest.Tables[0].Rows[0]["subjects_code"]);

            if (dsTest.Tables[0].Rows[0]["NegativeMarkingFlag"].ToString() == "1")
            {
                txtNegativeMark_Add.Text = "Applicable";
            }
            else
            {
                txtNegativeMark_Add.Text = "Not Applicable";
            }

            if (dsTest.Tables[0].Rows[0]["IsChapterHide"].ToString() == "1")
            {
                tbHideChapter.Visible = false;
            }
            else
            {
                tbHideChapter.Visible = true;
            }


            if (Subject_Code.Length == 0)
            {
                lstaddteacher.Items.Clear();
                //ddlTeacherName_Add.Items.Clear();
                //ddlTeacherName_Add.Items.Insert(0, "Select");
                //ddlTeacherName_Add.SelectedIndex = 0;
            }
            else
            {
                DataSet dsPartnerCD = ProductController.GetPartnerCode_BySubCode(ddlDivision_Add.SelectedValue.ToString().Trim(), ddlAcadYear_Add.SelectedValue.ToString().Trim(), ddlStandard_Add.SelectedValue.ToString().Trim(), Subject_Code);
                //BindDDL(ddlTeacherName_Add, dsPartnerCD, "Partner_Name", "Partner_Code");
                //ddlTeacherName_Add.Items.Insert(0, "Select");
                //ddlTeacherName_Add.SelectedIndex = 0;
                BindListBox(lstaddteacher, dsPartnerCD, "Partner_Name", "Partner_Code");

            }


        }
    }

    protected void BtnSave_Click(object sender, System.EventArgs e)
    {
        //Validation
        Clear_Error_Success_Box();

        //Validate if all information is entered correctly
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

        if (ddlCentre_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0006");
            ddlCentre_Add.Focus();
            return;
        }

        if (ddlStandard_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0003");
            ddlStandard_Add.Focus();
            return;
        }

        string BatchCode = "";
        //BatchCode = ddlBatch_Add.SelectedValue;
        for (int cnt = 0; cnt <= lstboxbatch.Items.Count - 1; cnt++)
        {
            if (lstboxbatch.Items[cnt].Selected == true)
            {
                BatchCode = BatchCode + lstboxbatch.Items[cnt].Value + ",";
            }
        }

        if (BatchCode == "")
        {
            Show_Error_Success_Box("E", "Select At Least One Batch");
        }
        else
        {

            BatchCode = BatchCode.Substring(0, BatchCode.Length - 1);
        }
        //if (ddlBatch_Add.SelectedIndex == 0)
        //{
        //    Show_Error_Success_Box("E", "0015");
        //    ddlBatch_Add.Focus();
        //    return;
        //}


        if (ddlTestName_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0016");
            ddlTestName_Add.Focus();
            return;
        }

        if (Convert.ToInt32(txtMaxMarks_Add.Text) == 0)
        {
            Show_Error_Success_Box("E", "0017");
            txtMaxMarks_Add.Focus();
            return;
        }

        int TestStartTime = 0;
        int TestEndTime = 0;

        TestStartTime = Convert.ToInt32(Convert.ToInt32(txtfromtime.Text.Substring(0, 2)) * 60) + Convert.ToInt32(txtfromtime.Text.Substring(txtfromtime.Text.Length - 2));//Strings.Left(txtfromtime.Text, 2)) * 60 + Conversion.Val(Strings.Right(txtfromtime.Text, 2));
        TestEndTime = Convert.ToInt32(Convert.ToInt32(txttotime.Text.Substring(0, 2)) * 60) + Convert.ToInt32(txttotime.Text.Substring(txttotime.Text.Length - 2));//Strings.Left(txttotime.Text, 2)) * 60 + Conversion.Val(Strings.Right(txttotime.Text, 2));

        if (TestStartTime <= 0 | TestStartTime >= 1440)
        {
            Show_Error_Success_Box("E", "0018");
            txtfromtime.Focus();
            return;
        }

        if (TestEndTime <= 0 | TestEndTime >= 1440)
        {
            Show_Error_Success_Box("E", "0019");
            txttotime.Focus();
            return;
        }

        if (TestStartTime >= TestEndTime)
        {
            Show_Error_Success_Box("E", "0020");
            txttotime.Focus();
            return;
        }

        //Save Test Schedule
        string ResultId = "";

        string CentreCode = ddlCentre_Add.SelectedValue;

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        string TestPKey = null;
        TestPKey = ddlTestName_Add.SelectedValue;


        string Test_Date = null;
        Test_Date = id_date_range_picker.Value;

        //string DateRange = null;
        //DateRange = id_date_range_picker_1.Value;

        string partner_code = "";
        //partner_code = ddlTeacherName_Add.SelectedValue.ToString();


        for (int cnt = 0; cnt <= lstaddteacher.Items.Count - 1; cnt++)
        {
            if (lstaddteacher.Items[cnt].Selected == true)
            {
                partner_code = partner_code + lstaddteacher.Items[cnt].Value + ",";
            }
        }

        if (partner_code == "")
        {
           // Show_Error_Success_Box("E", "Select At Least One Batch");
        }
        else
        {

            partner_code = partner_code.Substring(0, partner_code.Length - 1);
        }


        if (partner_code == "Select")
        {
            partner_code = "";
            //Show_Error_Success_Box("E", "Select teacher");
            //return;
        }

        try
        {
            ResultId = ProductController.Insert_TestSchedule(TestPKey, CentreCode, BatchCode, Test_Date, TestStartTime, TestEndTime, txtfromtime.Text, txttotime.Text, Convert.ToInt32(txtMaxMarks_Add.Text), txtRemarks_Add.Text,
            CreatedBy, partner_code);

            //Close the Add Panel and go to Search Grid
            if (!(ResultId == ""))
            {
                //ControlVisibility("Result");
                //BtnSearch_Click(sender, e);

                lblTestPKey_Edit.Text = ResultId;
                Show_Error_Success_Box("S", "0000");
                //Clear_AddPanel();
                CHk_btnVisiblity();

                if (lblTestCategoryId.Text == "001")
                {
                    btnAddPaperChecker.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.Message);

        }
    }

    private void Clear_AddPanel()
    {
        ddlDivision_Add.SelectedIndex = 0;
        ddlAcadYear_Add.SelectedIndex = 0;

        ddlStandard_Add.Items.Clear();
        ddlCentre_Add.Items.Clear();
        //ddlBatch_Add.Items.Clear();
        lstboxbatch.Items.Clear();
        ddlTestName_Add.Items.Clear();

        Clear_TestDetails();
    }

    private void Clear_TestDetails()
    {
        txtfromtime.Text = "";
        txttotime.Text = "";
        // txtTestDate_Add.Value = System.DateTime.Now.ToString("dd MMM yyyy");

        txtChapters_Add.Text = "";
        txtMaxMarks_Add.Text = "";
        txtNegativeMark_Add.Text = "";
        txtRemarks_Add.Text = "";
        txtSubject_Add.Text = "";
        txtTestCategory_Add.Text = "";
        txtTestType_Add.Text = "";
    }

    protected void ddlReTest_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Test_Add();
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

    protected void btnClose_Edit_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Result");
        Clear_AddPanel();
    }

    protected void btnSave_Edit_Click(object sender, System.EventArgs e)
    {
        //Validation
        Clear_Error_Success_Box();

        //Validate if all information is entered correctly

        if (Convert.ToInt32(txtMaxMarks_Edit.Text) == 0)
        {
            Show_Error_Success_Box("E", "0017");
            txtMaxMarks_Edit.Focus();
            return;
        }

        int TestStartTime = 0;
        int TestEndTime = 0;

        //TestStartTime = Conversion.Val(Strings.Left(txtFromTime_Edit.Text, 2)) * 60 + Conversion.Val(Strings.Right(txtFromTime_Edit.Text, 2));
        //TestEndTime = Conversion.Val(Strings.Left(txtToTime_Edit.Text, 2)) * 60 + Conversion.Val(Strings.Right(txtToTime_Edit.Text, 2));
        TestStartTime = Convert.ToInt32(Convert.ToInt32(txtFromTime_Edit.Text.Substring(0, 2)) * 60) + Convert.ToInt32(txtFromTime_Edit.Text.Substring(txtFromTime_Edit.Text.Length - 2));//Strings.Left(txtfromtime.Text, 2)) * 60 + Conversion.Val(Strings.Right(txtfromtime.Text, 2));
        TestEndTime = Convert.ToInt32(Convert.ToInt32(txtToTime_Edit.Text.Substring(0, 2)) * 60) + Convert.ToInt32(txtToTime_Edit.Text.Substring(txtToTime_Edit.Text.Length - 2));//Strings.Left(txttotime.Text, 2)) * 60 + Conversion.Val(Strings.Right(txttotime.Text, 2));

        if (TestStartTime <= 0 | TestStartTime >= 1440)
        {
            Show_Error_Success_Box("E", "0018");
            txtFromTime_Edit.Focus();
            return;
        }

        if (TestEndTime <= 0 | TestEndTime >= 1440)
        {
            Show_Error_Success_Box("E", "0019");
            txtToTime_Edit.Focus();
            return;
        }

        if (TestStartTime >= TestEndTime)
        {
            Show_Error_Success_Box("E", "0020");
            txtToTime_Edit.Focus();
            return;
        }

        //Save Test Schedule
        int ResultId = 0;

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        string TestPKey = null;
        TestPKey = lblTestPKey_Edit.Text;

        string Test_Date = null;
        Test_Date = txtTestDate_Edit.Value;

        string partner_code = "";
        //partner_code = ddlTeacherName_Add.SelectedValue.ToString();


        for (int cnt = 0; cnt <= lsteditteacher.Items.Count - 1; cnt++)
        {
            if (lsteditteacher.Items[cnt].Selected == true)
            {
                partner_code = partner_code + lsteditteacher.Items[cnt].Value + ",";
            }
        }

        if (partner_code == "")
        {
            // Show_Error_Success_Box("E", "Select At Least One Batch");
        }
        else
        {

            partner_code = partner_code.Substring(0, partner_code.Length - 1);
        }



        //if (partner_code == "Select")
        //{
        //    partner_code = "";
        //    //Show_Error_Success_Box("E", "Select teacher");
        //    //return;
        //}

        ResultId = ProductController.Update_TestSchedule(TestPKey, Test_Date, TestStartTime, TestEndTime, txtFromTime_Edit.Text, txtToTime_Edit.Text, Convert.ToInt32(txtMaxMarks_Edit.Text), lblRemarks_Edit.Text, 1, CreatedBy,
        1, partner_code);

        //Close the Add Panel and go to Search Grid
        if (ResultId == 1)
        {
            //ControlVisibility("Result");
            //BtnSearch_Click(sender, e);
            Show_Error_Success_Box("S", "0000");
            //Clear_AddPanel();
            CHk_btnVisiblity_edit();
        }
    }

    protected void btnExport_Click(object sender, System.EventArgs e)
    {
        dlGridExport.Visible = true;
        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=Test_Schedule.xls");

        Response.Charset = "";


        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        dlGridExport.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();
        dlGridExport.Visible = false;
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
        if (DateRange != "")
        //ToDate = Strings.Right(DateRange, 10);
        {
            ToDate = DateRange.Substring(DateRange.Length - 10);
        }
        if (string.IsNullOrEmpty(ToDate))
            ToDate = System.DateTime.Now.ToString("dd MMM yyyy");

        string Centre_Code = null;
        Centre_Code = ddlCentre.SelectedItem.Value;

        DataSet dsGrid = ProductController.GetTestScheduleBy_Division_Year_Standard_Centre(DivisionCode, YearName, StandardCode, BatchCode, "01,02", ddlTestCategory.SelectedValue, TestType_ID, TestName, FromDate, ToDate,
        -1, -1, Centre_Code, 1);
        dlGridDisplay.DataSource = dsGrid.Tables[0];
        dlGridDisplay.DataBind();


        if (dsGrid != null)
        {
            DataTable table1 = new DataTable();
            table1.Columns.Add("Test_Id");
            table1.Columns.Add("Test_Name");
            table1.Columns.Add("Test_date");
            table1.Columns.Add("Subject_Code");
            table1.Columns.Add("Subject_Display_name");
            table1.Columns.Add("Chapters");
            table1.Columns.Add("MaxMarks");
            table1.Columns.Add("Duration");
            table1.Columns.Add("Pkey");
            table1.Columns.Add("Conduct_No");

            table1 = dsGrid.Tables[1];


            for (int i = table1.Rows.Count - 1; i > 0; i--)
            {
                if (table1.Rows[i]["Test_Name"].ToString() == table1.Rows[i - 1]["Test_Name"].ToString() && table1.Rows[i]["Conduct_No"].ToString() == table1.Rows[i - 1]["Conduct_No"].ToString() && table1.Rows[i]["Test_date"].ToString() == table1.Rows[i - 1]["Test_date"].ToString() && table1.Rows[i]["MaxMarks"].ToString() == table1.Rows[i - 1]["MaxMarks"].ToString() && table1.Rows[i]["Duration"].ToString() == table1.Rows[i - 1]["Duration"].ToString())
                {
                    table1.Rows[i]["Test_Name"] = "";
                    table1.Rows[i]["Test_date"] = "";
                    table1.Rows[i]["Duration"] = "";
                    table1.Rows[i]["MaxMarks"] = "";
                }
            }

            dlGridExport.DataSource = table1;
            dlGridExport.DataBind();
        }

        dlGridExport.DataSource = dsGrid.Tables[1];
        dlGridExport.DataBind();

        lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
        lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
        lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
        lblStandard_Result.Text = ddlStandard.SelectedItem.ToString();
        lblTestCategory_Result.Text = ddlTestCategory.SelectedItem.ToString();
        lblCentre_Result.Text = ddlCentre.SelectedItem.ToString();


        lblPrintTestDivision.Text = ddlDivision.SelectedItem.ToString();
        lblPrintTestAcademicYear.Text = ddlAcadYear.SelectedItem.ToString();
        lblPrintTestCourse.Text = ddlStandard.SelectedItem.ToString();
        lblPrintTestCategory.Text = ddlTestCategory.SelectedItem.ToString();
        lblPrintTestCentre.Text = ddlCentre.SelectedItem.ToString();
    }

    protected void btnDelete_Click(object sender, System.EventArgs e)
    {
        string TestPKey = null;
        TestPKey = lbldelCode.Text;

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        string Reason = null;
        Reason = ddlLect_Test_Reason.SelectedItem.ToString();

        int ResultId = 0;
        ResultId = ProductController.Insert_RemoveTestRequest(TestPKey, Reason, CreatedBy);
    }

    public Tran_TestSchedule()
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

    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);


        StringWriter swp = new StringWriter();
        HtmlTextWriter hwp = new HtmlTextWriter(swp);

        dlGridExport.Visible = true;
        dlGridExport.RenderControl(hw);
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
        dlGridExport.Visible = false;
    }
    protected void btnMesage_ManualSending_ServerClick(object sender, System.EventArgs e)
    {
        //// Sending Code
        Message_Template();

    }
    protected void btnMesage_ManualSending_Edit_Click(object sender, System.EventArgs e)
    {
        //// Sending Code
        Message_Template_edit();


    }

    private void CHk_btnVisiblity()
    {
        string count = "", Notification = "";

        DataSet DSChk = ProductController.Check_MesageTemplate("007", ddlDivision_Add.SelectedValue.ToString().Trim(), 1);
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
                        //Show Button
                        BtnAttendanceMessage.Visible = true;
                        btnMesage_ManualSending_Edit.Visible = true;

                    }
                    else
                    {
                        //Hide Button
                        BtnAttendanceMessage.Visible = false;
                        btnMesage_ManualSending_Edit.Visible = false;
                    }
                }


            }
            else
            {
                //Hide Button
                BtnAttendanceMessage.Visible = false;
                btnMesage_ManualSending_Edit.Visible = false;
            }
        }
    }

    private void CHk_btnVisiblity_edit()
    {
        string count = "", Notification = "";

        DataSet DSChk = ProductController.Check_MesageTemplate("007", ddlDivision.SelectedValue.ToString().Trim(), 1);
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
                        //Show Button
                        BtnAttendanceMessage.Visible = true;
                        btnMesage_ManualSending_Edit.Visible = true;

                    }
                    else
                    {
                        //Hide Button
                        BtnAttendanceMessage.Visible = false;
                        btnMesage_ManualSending_Edit.Visible = false;
                    }
                }


            }
            else
            {
                //Hide Button
                BtnAttendanceMessage.Visible = false;
                btnMesage_ManualSending_Edit.Visible = false;
            }
        }
    }

    private void Message_Template()
    {
        try
        {
            string count = "", Notification = "", newTemplate = "", firstname = "", date = "", StudentName = "", Centre_code = "", MobileNo = "", Pkey = "", MaxMarks = "", fromtime = "", totime = "", TestNo = "";
            int resultid = 0;
            int smscount = 0;

            DataSet DSSentStatus = ProductController.Check_SMSSendStatus(lblTestPKey_Edit.Text, 10);
            if (DSSentStatus != null)
            {
                if (DSSentStatus.Tables[0].Rows.Count > 0)
                {
                    smscount = Convert.ToInt32(DSSentStatus.Tables[0].Rows[0]["TestSMSFlag"].ToString());
                    if (smscount == 0)
                    {

                        Label lblHeader_User_Code = default(Label);
                        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                        string CreatedBy = null;
                        CreatedBy = lblHeader_User_Code.Text;

                        DataSet DSChk = ProductController.Check_MesageTemplate("007", ddlDivision_Add.SelectedValue.ToString().Trim(), 1);
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



                                    DataSet dsStudent = ProductController.GetAllStudentDetails_ByTestPKeyForTestSchedule(lblTestPKey_Edit.Text, 0);

                                    for (int i = 0; i <= dsStudent.Tables[0].Rows.Count - 1; i++)
                                    {
                                        firstname = dsStudent.Tables[0].Rows[i]["FirstName"].ToString();
                                        date = dsStudent.Tables[0].Rows[i]["NTest_Date"].ToString();
                                        StudentName = dsStudent.Tables[0].Rows[i]["StudentName"].ToString();
                                        Centre_code = dsStudent.Tables[0].Rows[i]["Centre_Code"].ToString();
                                        MaxMarks = dsStudent.Tables[0].Rows[i]["MaxMarks"].ToString();
                                        fromtime = dsStudent.Tables[0].Rows[i]["NfromtimeStr"].ToString();
                                        totime = dsStudent.Tables[0].Rows[i]["NtotimeStr"].ToString();
                                        TestNo = dsStudent.Tables[0].Rows[i]["Conduct_No"].ToString();

                                        MobileNo = dsStudent.Tables[0].Rows[i]["mobileno"].ToString();

                                        newTemplate = Template.Replace("[StudentFullName]", StudentName).Replace("[FirstName]", firstname).Replace("[TestDate]", date).Replace("[TestFromTime]", fromtime).Replace("[TestToTime]", totime).Replace("[SubjectName]", txtSubject_Add.Text.Trim()).Replace("[MaxMarks]", MaxMarks).Replace("[TestNo]", TestNo).Replace("%2526", "%26");

                                        if (MsgMode == "Auto")
                                        {
                                            resultid = ProductController.Insert_SMSLog(Centre_code, Message_cd, MobileNo, newTemplate, "0", "Auto", "Transactional");
                                        }
                                        else if (MsgMode == "Manual")
                                        {
                                            resultid = ProductController.Insert_SMSLog(Centre_code, Message_cd, MobileNo, newTemplate, "0", CreatedBy, "Transactional");
                                        }

                                    }

                                    int rowcount = dsStudent.Tables[0].Rows.Count;
                                    if (rowcount > 0)
                                    {
                                        resultid = ProductController.Update_TestScheduleSMSSendStatus_T601(lblTestPKey_Edit.Text, 1, MsgMode, 11);
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


    private void Message_Template_edit()
    {
        try
        {
            string count = "", Notification = "", newTemplate = "", firstname = "", date = "", StudentName = "", Centre_code = "", MobileNo = "", Pkey = "", MaxMarks = "", fromtime = "", totime = "", TestNo = "";
            int resultid = 0;
            int smscount = 0;

            DataSet DSSentStatus = ProductController.Check_SMSSendStatus(lblTestPKey_Edit.Text, 10);
            if (DSSentStatus != null)
            {
                if (DSSentStatus.Tables[0].Rows.Count > 0)
                {
                    smscount = Convert.ToInt32(DSSentStatus.Tables[0].Rows[0]["TestSMSFlag"].ToString());
                    if (smscount == 0)
                    {

                        Label lblHeader_User_Code = default(Label);
                        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                        string CreatedBy = null;
                        CreatedBy = lblHeader_User_Code.Text;

                        DataSet DSChk = ProductController.Check_MesageTemplate("007", ddlDivision.SelectedValue.ToString().Trim(), 1);
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



                                    DataSet dsStudent = ProductController.GetAllStudentDetails_ByTestPKeyForTestSchedule(lblTestPKey_Edit.Text, 0);

                                    for (int i = 0; i <= dsStudent.Tables[0].Rows.Count - 1; i++)
                                    {
                                        firstname = dsStudent.Tables[0].Rows[i]["FirstName"].ToString();
                                        date = dsStudent.Tables[0].Rows[i]["NTest_Date"].ToString();
                                        StudentName = dsStudent.Tables[0].Rows[i]["StudentName"].ToString();
                                        Centre_code = dsStudent.Tables[0].Rows[i]["Centre_Code"].ToString();
                                        MaxMarks = dsStudent.Tables[0].Rows[i]["MaxMarks"].ToString();
                                        fromtime = dsStudent.Tables[0].Rows[i]["NfromtimeStr"].ToString();
                                        totime = dsStudent.Tables[0].Rows[i]["NtotimeStr"].ToString();
                                        TestNo = dsStudent.Tables[0].Rows[i]["Conduct_No"].ToString();

                                        MobileNo = dsStudent.Tables[0].Rows[i]["mobileno"].ToString();

                                        newTemplate = Template.Replace("[StudentFullName]", StudentName).Replace("[FirstName]", firstname).Replace("[TestDate]", date).Replace("[TestFromTime]", fromtime).Replace("[TestToTime]", totime).Replace("[SubjectName]", lblSubject_Edit.Text.Trim()).Replace("[MaxMarks]", MaxMarks).Replace("[TestNo]", TestNo).Replace("%2526", "%26");

                                        if (MsgMode == "Auto")
                                        {
                                            resultid = ProductController.Insert_SMSLog(Centre_code, Message_cd, MobileNo, newTemplate, "0", "Auto", "Transactional");
                                        }
                                        else if (MsgMode == "Manual")
                                        {
                                            resultid = ProductController.Insert_SMSLog(Centre_code, Message_cd, MobileNo, newTemplate, "0", CreatedBy, "Transactional");
                                        }

                                    }

                                    int rowcount = dsStudent.Tables[0].Rows.Count;
                                    if (rowcount > 0)
                                    {
                                        resultid = ProductController.Update_TestScheduleSMSSendStatus_T601(lblTestPKey_Edit.Text, 1, MsgMode, 11);
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

    private void FillDDL_Test_Cancel_Reason()
    {
        try
        {
            ddlLect_Test_Reason.Items.Clear();
            DataSet dscancelReason = ProductController.GetAll_LectureTestReason();
            BindDDL(ddlLect_Test_Reason, dscancelReason, "Test_Cancel_Reason_Name", "Test_Cancel_Reason_ID");
            ddlLect_Test_Reason.Items.Insert(0, "Select");
            ddlLect_Test_Reason.SelectedIndex = 0;
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
    protected void lstboxbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Count = 0;
        //BatchCode = ddlBatch_Add.SelectedValue;
        for (int cnt = 0; cnt <= lstboxbatch.Items.Count - 1; cnt++)
        {
            if (lstboxbatch.Items[cnt].Selected == true)
            {
                Count = Count + 1;
            }
        }

        if (Count > 1)
        {
            Show_Error_Success_Box("W", "Re Test Cannot Be Done For Multiple Batches");
            ddlReTest_Add.Enabled = false;
            ddlReTest_Add.SelectedIndex = 1;

        }
        else
        {
            ddlReTest_Add.Enabled = true;
            Clear_Error_Success_Box();
            ddlReTest_Add.SelectedIndex = 0;

        }


        FillDDL_Test_Add();
    }
    protected void btnuploadviexcel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Tran_Test_Schedule_Upload.aspx");
    }
}
