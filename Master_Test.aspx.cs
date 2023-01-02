using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;

partial class Master_Test : System.Web.UI.Page
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

        if (ddlTestMode.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0011");
            ddlTestMode.Focus();
            return;
        }

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
            StandardCode = Common.RemoveComma(StandardCode);
            //if (Strings.Right(StandardCode, 1) == ",")
            //    StandardCode = Strings.Left(StandardCode, Strings.Len(StandardCode) - 1);
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
            StandardCode = Common.RemoveComma(StandardCode);
            //if (Strings.Right(StandardCode, 1) == ",")
            //    StandardCode = Strings.Left(StandardCode, Strings.Len(StandardCode) - 1);
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
            //if (Strings.Right(TestType_ID, 1) == ",")
            //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);
        }

        ControlVisibility("Result");

        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string TestName = null;
        if (string.IsNullOrEmpty(txtTestName.Text.Trim()))
        {
            TestName = "%";
        }
        else
        {
            TestName = "%" + txtTestName.Text.Trim();
        }

        DataSet dsGrid = ProductController.GetTestMasterBy_Division_Year_Standard(DivisionCode, YearName, StandardCode, ddlTestMode.SelectedValue, ddlTestCategory.SelectedValue, TestType_ID, TestName, 1);
        dlGridDisplay.DataSource = dsGrid;
        dlGridDisplay.DataBind();

        dlGridExport.DataSource = dsGrid;
        dlGridExport.DataBind();

        lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            FillDDL_TestModes();
            FillDDL_TestCategories();
            FillDDL_TestTypes();
            FillDDL_Duration();
            FillDDL_Division();
            FillDDL_AcadYear();
        }
    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivEditPanelAs.Visible = false;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = true;
            DivUploadPannel.Visible = false;
        }
        else if (Mode == "Result")
        {
            DivEditPanelAs.Visible = false;
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = true;
            DivUploadPannel.Visible = false;
        }
        else if (Mode == "Add")
        {
            DivEditPanelAs.Visible = false;
            DivAddPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
            DivUploadPannel.Visible = false;
        }

        else if (Mode == "Edit")
        {
            DivEditPanelAs.Visible = true;
            DivAddPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = true;
            DivUploadPannel.Visible = false;
        }

        else if (Mode == "Upload")
        {
            DivEditPanelAs.Visible = false;
            DivAddPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = true;
            DivUploadPannel.Visible = true;
           
        }
        Clear_Error_Success_Box();
    }

    protected void BtnAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Add");
        lblDivision_Add.Visible = false;
        lblAcadYear_Add.Visible = false;
        lblStandard_Add.Visible = false;
        ddlDivision_Add.Visible = true;
        ddlAcadYear_Add.Visible = true;
        ddlStandard_Add.Visible = true;
        lblHeader_Add.Text = "Create New Test";
        lblTestPKey_Hidden.Text = "";
        Clear_AddPanel();
    }

    protected void BtnCloseAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
        Clear_AddPanel();
    }


    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {
                Clear_Error_Success_Box();
                lbldelCode.Text = e.CommandArgument.ToString();
                txtDeleteItemName.Text = (((Label)e.Item.FindControl("lblModeName")).Text);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
            }
            else if (e.CommandName == "Authorise")
            {
                lblPKey_Authorise.Text = e.CommandArgument.ToString();
                lblAuthoriseTestName.Text = (((Label)e.Item.FindControl("lblModeName")).Text);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalAuthorise();", true);
            }
            else if (e.CommandName == "Edit")
            {
                ControlVisibility("Add");
                lblDivision_Add.Visible = true;
                lblAcadYear_Add.Visible = true;
                lblStandard_Add.Visible = true;
                ddlDivision_Add.Visible = false;
                ddlAcadYear_Add.Visible = false;
                ddlStandard_Add.Visible = false;
                BtnSave.Visible = true;

                lblPKey_Edit.Text = e.CommandArgument.ToString();
                lblHeader_Add.Text = "Edit Test";
                lblTestPKey_Hidden.Text = "";
                txtSyllabusDesc.Text = "";
                FillTestMasterDetails(lblPKey_Edit.Text, e.CommandName);
            }

            else if (e.CommandName == "EditAfterAs")
            {
                ControlVisibility("Edit");
                lblDivision_edit.Visible = true;
                lblAcadYear_edit.Visible = true;
                lblStandard_edit.Visible = true;
                ddlDivision_Edit.Visible = false;
                ddlAcadYear_Edit.Visible = false;
                ddlcourse_edit.Visible = false;
                btn_save_edit.Visible = true;

                lblpkeyeditas.Text = e.CommandArgument.ToString();
                lblHeader_edit.Text = "Edit Test After Authorisation";
                lblTestPKey_Hidden_edit.Text = "";
                FillTestMasterDetails_Edit(lblpkeyeditas.Text, e.CommandName);
            }

            else if (e.CommandName == "UploadOMR")
            {
                ControlVisibility("Upload");
                txtuploadfoldername.Text = "";



                lblpkeyuploadomr.Text = e.CommandArgument.ToString();
                FillTestMasterDetails_Upload(lblpkeyuploadomr.Text, e.CommandName);
                //lblHeader_edit.Text = "Edit Test After Authorisation";
                //lblTestPKey_Hidden_edit.Text = "";

            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }

    }

    private void FillTestMasterDetails_Upload(string PKey, string CommandName)
    {
        DataSet dsTest = ProductController.GetTestMasterBY_PKey(PKey, 1);

        if (dsTest.Tables[0].Rows.Count > 0)
        {
            lblTestPKey_Hidden_edit.Text = PKey;

            string Div_Code = null;
            Div_Code = Convert.ToString(dsTest.Tables[0].Rows[0]["Division_Code"]);

            string YearName = null;
            YearName = Convert.ToString(dsTest.Tables[0].Rows[0]["Acad_Year"]);

            string StandardCode = null;
            StandardCode = Convert.ToString(dsTest.Tables[0].Rows[0]["Standard_Code"]);
            string Subjects_Code = null;
            Subjects_Code = Convert.ToString(dsTest.Tables[0].Rows[0]["Subjects_Code"]);
            string Chapters_Code = null;
            Chapters_Code = Convert.ToString(dsTest.Tables[0].Rows[0]["Chapters_Code"]);
            string Centres_Code = null;
            Centres_Code = Convert.ToString(dsTest.Tables[0].Rows[0]["Centre_Code"]);

            int check_already_uploaded = Convert.ToInt32(dsTest.Tables[0].Rows[0]["Is_OMR_Upload"]);


            if (check_already_uploaded == 1)
            {
                txtuploadfoldername.Enabled = false;
                txtuploadfoldername.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["OMR_Sheets_Path"]);
            }
            else
            {
                txtuploadfoldername.Enabled = true;
                txtuploadfoldername.Text = "";
            }




            lbluploaddiviosion.Text = ddlDivision.SelectedItem.ToString();
            lbluploadacadyear.Text = ddlAcadYear.SelectedItem.ToString();
            lbluploadcourse.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Standard_Name"]);
            ddluploadtestcategory.SelectedValue = Convert.ToString(dsTest.Tables[0].Rows[0]["TestCategory_Id"]);
            ddluploadtesttype.SelectedValue = Convert.ToString(dsTest.Tables[0].Rows[0]["TestType_Id"]);
            ddluploadtestmode.SelectedValue = Convert.ToString(dsTest.Tables[0].Rows[0]["TestMode_Id"]);
            txtuploadtestdesc.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Test_Description"]);
            txtuploadtestname.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Test_Name"]);
        }
    }



    private void FillTestMasterDetails_Edit(string PKey, string CommandName)
    {
        DataSet dsTest = ProductController.GetTestMasterBY_PKey(PKey, 1);

        if (dsTest.Tables[0].Rows.Count > 0)
        {
            lblTestPKey_Hidden_edit.Text = PKey;

            string Div_Code = null;
            Div_Code = Convert.ToString(dsTest.Tables[0].Rows[0]["Division_Code"]);

            string YearName = null;
            YearName = Convert.ToString(dsTest.Tables[0].Rows[0]["Acad_Year"]);

            string StandardCode = null;
            StandardCode = Convert.ToString(dsTest.Tables[0].Rows[0]["Standard_Code"]);
            string Subjects_Code = null;
            Subjects_Code = Convert.ToString(dsTest.Tables[0].Rows[0]["Subjects_Code"]);
            string Chapters_Code = null;
            Chapters_Code = Convert.ToString(dsTest.Tables[0].Rows[0]["Chapters_Code"]);
            string Centres_Code = null;
            Centres_Code = Convert.ToString(dsTest.Tables[0].Rows[0]["Centre_Code"]);

            FillDDL_Slab_edit(Div_Code);

            if (Convert.ToString(dsTest.Tables[0].Rows[0]["Slab_Code"]) != "0" && Convert.ToString(dsTest.Tables[0].Rows[0]["Slab_Code"]) != "")
            {
                ddlPCSlab_edit.SelectedValue = Convert.ToString(dsTest.Tables[0].Rows[0]["Slab_Code"]);
            }


            lblDivision_edit.Text = ddlDivision.SelectedItem.ToString();
            lblAcadYear_edit.Text = ddlAcadYear.SelectedItem.ToString();
            lblStandard_edit.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Standard_Name"]);
            ddlTestCategory_Edit.SelectedValue = Convert.ToString(dsTest.Tables[0].Rows[0]["TestCategory_Id"]);
            ddlTestType_Edit.SelectedValue = Convert.ToString(dsTest.Tables[0].Rows[0]["TestType_Id"]);
            ddlTestMode_edit.SelectedValue = Convert.ToString(dsTest.Tables[0].Rows[0]["TestMode_Id"]);
            txtTestDesc_edit.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Test_Description"]);
            txtTestName_edit.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Test_Name"]);
            txtRemarks_Edit.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Remarks"]);
            txtMaxMarks_edit.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["MaxMarks"]);
            txtqpsetcntedit.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["QPSetCnt"]);
            txtQueCnt_edit1.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["QuestionCount"]);
            txtSyllabusDesc_AfterAuth.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Syllabus_Description"]);            
            //lblSubject_Add.Text = dsTest.Tables(0).Rows(0)("Subjects")
            //FillQPSetNo(dsTest.Tables(0).Rows(0)("QPSetCnt"))
            if (Convert.ToInt32(dsTest.Tables[0].Rows[0]["NegativeMarkingFlag"]) == 1)
            {
                chkNegativeMarkingFlagedit.Checked = true;
            }
            else
            {
                chkNegativeMarkingFlagedit.Checked = false;
            }

            if (Convert.ToInt32(dsTest.Tables[0].Rows[0]["IsChapterHide"]) == 1)
            {
                ChkChapterHide_edit.Checked = true;
            }
            else
            {
                ChkChapterHide_edit.Checked = false;
            }


            if (ddlTestCategory_Edit.SelectedValue == "002")
            {
                Row_MCQTestOptionsEdit.Visible = true;
                //MCQ
            }
            else
            {
                Row_MCQTestOptionsEdit.Visible = false;
                //Normal Test
            }

            int TotTime = 0;
            int HrCnt = 0;
            int MinCnt = 0;
            TotTime = Convert.ToInt32(dsTest.Tables[0].Rows[0]["TestDuration"].ToString());
            MinCnt = TotTime % 60;
            HrCnt = (TotTime - MinCnt) / 60;

            for (int cnt = 0; cnt <= ddlHour_edit.Items.Count - 1; cnt++)
            {
                if (ddlHour_edit.Items[cnt].Text == HrCnt.ToString())
                {
                    ddlHour_edit.SelectedIndex = cnt;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }

            for (int cnt = 0; cnt <= ddlMin_edit.Items.Count - 1; cnt++)
            {
                if (ddlMin_edit.Items[cnt].Text == MinCnt.ToString())
                {
                    ddlMin_edit.SelectedIndex = cnt;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }

            //DataSet dsSubject = ProductController.GetAllSubjectsBy_Division_Year_Standard(Div_Code, YearName, StandardCode);
            DataSet dsSubject = ProductController.GetAllSubjectsByStandard(StandardCode);
            BindListBox(ddlSubject_edit, dsSubject, "Subject_Name", "Subject_Code");



            for (int cnt = 0; cnt <= ddlSubject_edit.Items.Count - 1; cnt++)
            {
                string p = Subjects_Code;
                if (Subjects_Code.IndexOf(ddlSubject_edit.Items[cnt].Value.ToString()) > -1)//need to be chk
                // if (Strings.InStr(1, Subjects_Code, ddlSubject_Add.Items[cnt].Value.ToString(), 1) > 0)
                {
                    ddlSubject_edit.Items[cnt].Selected = true;
                    //ddlMin_Add.SelectedIndex = cnt;

                }
            }
            FillDDL_Centre_Edit(Div_Code);

            ddlSubject_edit_Hidden.DataSource = dsTest.Tables[1];
            ddlSubject_edit_Hidden.DataTextField = "Subject_Name";
            ddlSubject_edit_Hidden.DataValueField = "Subject_Code";
            ddlSubject_edit_Hidden.DataBind();

            string SubjectCode = "";
            int SubCnt = 0;
            int SubSelCnt = 0;
            for (SubCnt = 0; SubCnt <= ddlSubject_edit.Items.Count - 1; SubCnt++)
            {
                if (ddlSubject_edit.Items[SubCnt].Selected == true)
                {
                    SubSelCnt = SubSelCnt + 1;
                    SubjectCode = SubjectCode + ddlSubject_edit.Items[SubCnt].Value + ",";
                }
            }
            SubjectCode = Common.RemoveComma(SubjectCode);
            //if (Strings.Right(SubjectCode, 1) == ",")
            //    SubjectCode = Strings.Left(SubjectCode, Strings.Len(SubjectCode) - 1);

            DataSet dsChapter = ProductController.GetAllChaptersBy_Division_Year_Standard_Subject(Div_Code, YearName, StandardCode, SubjectCode);
            dlChapter_Add_edit.DataSource = dsChapter;
            dlChapter_Add_edit.DataBind();




            List<string> chapters = Chapters_Code.Split(',').ToList();

            for (int cnt = 0; cnt <= chapters.Count - 1; cnt++)
            {
                foreach (DataListItem dtlItem in dlChapter_Add_edit.Items)
                {
                    CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkChapteredit");
                    Label lblChapterCode = (Label)dtlItem.FindControl("lblChapterCode_edit");
                    Label lblSubjectCode = (Label)dtlItem.FindControl("lblSubjectCode_edit");
                    if (chapters[cnt].ToString().Trim() == (StandardCode + lblSubjectCode.Text + lblChapterCode.Text.Trim()).Trim())
                    {
                        chkitemck.Checked = true;
                        break;
                    }

                }
            }





            foreach (DataListItem dtlItem in dlCentre_edit.Items)
            {
                CheckBox chkCentre = (CheckBox)dtlItem.FindControl("chkCentre_edit");
                Label lblCenterCode = (Label)dtlItem.FindControl("lblCenterCode_edit");
                Label lblCenterName = (Label)dtlItem.FindControl("lblCenterName_edit");

                if (Centres_Code.IndexOf(lblCenterCode.Text.Trim()) > 0)
                {
                    chkCentre.Checked = true;
                }
            }


            if (Convert.ToInt32(dsTest.Tables[4].Rows[0]["Count"]) >= 1)
            {
                Show_Error_Success_Box("E", "Test Attendance Authorized Hence You Cannot Update Test Details");
                btn_save_edit.Visible = false;
            }
            else
            {
                btn_save_edit.Visible = true;
            }
        }
    }

    private void FillTestMasterDetails(string PKey, string CommandName)
    {
        DataSet dsTest = ProductController.GetTestMasterBY_PKey(PKey, 1);

        if (dsTest.Tables[0].Rows.Count > 0)
        {
            lblTestPKey_Hidden.Text = PKey;

            string Div_Code = null;
            Div_Code = Convert.ToString(dsTest.Tables[0].Rows[0]["Division_Code"]);

            string YearName = null;
            YearName = Convert.ToString(dsTest.Tables[0].Rows[0]["Acad_Year"]);

            string StandardCode = null;
            StandardCode = Convert.ToString(dsTest.Tables[0].Rows[0]["Standard_Code"]);
            string Subjects_Code = null;
            Subjects_Code = Convert.ToString(dsTest.Tables[0].Rows[0]["Subjects_Code"]);
            string Chapters_Code = null;
            Chapters_Code = Convert.ToString(dsTest.Tables[0].Rows[0]["Chapters_Code"]);
            string Centres_Code = null;
            Centres_Code = Convert.ToString(dsTest.Tables[0].Rows[0]["Centre_Code"]);

            FillDDL_Slab(Div_Code);
            if (Convert.ToString(dsTest.Tables[0].Rows[0]["Slab_Code"]) != "0" && Convert.ToString(dsTest.Tables[0].Rows[0]["Slab_Code"]) != "")
            {
                ddlPCSlab.SelectedValue = Convert.ToString(dsTest.Tables[0].Rows[0]["Slab_Code"]);
            }


            lblDivision_Add.Text = ddlDivision.SelectedItem.ToString();
            lblAcadYear_Add.Text = ddlAcadYear.SelectedItem.ToString();
            lblStandard_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Standard_Name"]);
            ddlTestCategory_Add.SelectedValue = Convert.ToString(dsTest.Tables[0].Rows[0]["TestCategory_Id"]);
            ddlTestType_Add.SelectedValue = Convert.ToString(dsTest.Tables[0].Rows[0]["TestType_Id"]);
            ddlTestMode_Add.SelectedValue = Convert.ToString(dsTest.Tables[0].Rows[0]["TestMode_Id"]);
            txtTestDesc_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Test_Description"]);
            txtTestName_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Test_Name"]);
            txtRemarks_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Remarks"]);
            txtMaxMarks_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["MaxMarks"]);
            txtQPSetCount_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["QPSetCnt"]);
            txtQueCnt_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["QuestionCount"]);
            txtSyllabusDesc.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Syllabus_Description"]);            
            //lblSubject_Add.Text = dsTest.Tables(0).Rows(0)("Subjects")
            //FillQPSetNo(dsTest.Tables(0).Rows(0)("QPSetCnt"))
            if (Convert.ToInt32(dsTest.Tables[0].Rows[0]["NegativeMarkingFlag"]) == 1)
            {
                chkNegativeMarkingFlag.Checked = true;
            }
            else
            {
                chkNegativeMarkingFlag.Checked = false;
            }

            if (Convert.ToInt32(dsTest.Tables[0].Rows[0]["IsChapterHide"]) == 1)
            {
                ChkChapterHide.Checked = true;
            }
            else
            {
                ChkChapterHide.Checked = false;
            }


            if (ddlTestCategory_Add.SelectedValue == "002")
            {
                Row_MCQTestOptions.Visible = true;
                //MCQ
            }
            else
            {
                Row_MCQTestOptions.Visible = false;
                //Normal Test
            }

            int TotTime = 0;
            int HrCnt = 0;
            int MinCnt = 0;
            TotTime = Convert.ToInt32(dsTest.Tables[0].Rows[0]["TestDuration"].ToString());
            MinCnt = TotTime % 60;
            HrCnt = (TotTime - MinCnt) / 60;

            for (int cnt = 0; cnt <= ddlHour_Add.Items.Count - 1; cnt++)
            {
                if (ddlHour_Add.Items[cnt].Text == HrCnt.ToString())
                {
                    ddlHour_Add.SelectedIndex = cnt;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }

            for (int cnt = 0; cnt <= ddlMin_Add.Items.Count - 1; cnt++)
            {
                if (ddlMin_Add.Items[cnt].Text == MinCnt.ToString())
                {
                    ddlMin_Add.SelectedIndex = cnt;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }

            //DataSet dsSubject = ProductController.GetAllSubjectsBy_Division_Year_Standard(Div_Code, YearName, StandardCode);
            DataSet dsSubject = ProductController.GetAllSubjectsByStandard(StandardCode);
            BindListBox(ddlSubject_Add, dsSubject, "Subject_Name", "Subject_Code");



            for (int cnt = 0; cnt <= ddlSubject_Add.Items.Count - 1; cnt++)
            {
                string p = Subjects_Code;
                if (Subjects_Code.IndexOf(ddlSubject_Add.Items[cnt].Value.ToString()) > -1)//need to be chk
                // if (Strings.InStr(1, Subjects_Code, ddlSubject_Add.Items[cnt].Value.ToString(), 1) > 0)
                {
                    ddlSubject_Add.Items[cnt].Selected = true;
                    //ddlMin_Add.SelectedIndex = cnt;

                }
            }
            FillDDL_Centre(Div_Code);

            ddlSubject_Add_Hidden.DataSource = dsTest.Tables[1];
            ddlSubject_Add_Hidden.DataTextField = "Subject_Name";
            ddlSubject_Add_Hidden.DataValueField = "Subject_Code";
            ddlSubject_Add_Hidden.DataBind();

            string SubjectCode = "";
            int SubCnt = 0;
            int SubSelCnt = 0;
            for (SubCnt = 0; SubCnt <= ddlSubject_Add.Items.Count - 1; SubCnt++)
            {
                if (ddlSubject_Add.Items[SubCnt].Selected == true)
                {
                    SubSelCnt = SubSelCnt + 1;
                    SubjectCode = SubjectCode + ddlSubject_Add.Items[SubCnt].Value + ",";
                }
            }
            SubjectCode = Common.RemoveComma(SubjectCode);
            //if (Strings.Right(SubjectCode, 1) == ",")
            //    SubjectCode = Strings.Left(SubjectCode, Strings.Len(SubjectCode) - 1);

            DataSet dsChapter = ProductController.GetAllChaptersBy_Division_Year_Standard_Subject(Div_Code, YearName, StandardCode, SubjectCode);
            dlChapter_Add.DataSource = dsChapter;
            dlChapter_Add.DataBind();




            List<string> chapters = Chapters_Code.Split(',').ToList();

            for (int cnt = 0; cnt <= chapters.Count - 1; cnt++)
            {
                foreach (DataListItem dtlItem in dlChapter_Add.Items)
                {
                    CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkChapter");
                    Label lblChapterCode = (Label)dtlItem.FindControl("lblChapterCode");
                    Label lblSubjectCode = (Label)dtlItem.FindControl("lblSubjectCode");
                    if (chapters[cnt].ToString().Trim() == (StandardCode + lblSubjectCode.Text + lblChapterCode.Text.Trim()).Trim())
                    {
                        chkitemck.Checked = true;
                        break;
                    }

                }
            }





            foreach (DataListItem dtlItem in dlCentre_Add.Items)
            {
                CheckBox chkCentre = (CheckBox)dtlItem.FindControl("chkCentre");
                Label lblCenterCode = (Label)dtlItem.FindControl("lblCenterCode");
                Label lblCenterName = (Label)dtlItem.FindControl("lblCenterName");

                if (Centres_Code.IndexOf(lblCenterCode.Text.Trim()) > 0)
                {
                    chkCentre.Checked = true;
                }
            }
        }
    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    private void FillDDL_TestModes()
    {
        DataSet dsTestModes = ProductController.GetAllActiveTestMode();
        BindDDL(ddlTestMode, dsTestModes, "TestMode_Name", "TestMode_Id");
        ddlTestMode.Items.Insert(0, "Select");
        ddlTestMode.SelectedIndex = 0;

        BindDDL(ddlTestMode_Add, dsTestModes, "TestMode_Name", "TestMode_Id");
        ddlTestMode_Add.Items.Insert(0, "Select");
        ddlTestMode_Add.SelectedIndex = 0;

        BindDDL(ddlTestMode_edit, dsTestModes, "TestMode_Name", "TestMode_Id");
        ddlTestMode_edit.Items.Insert(0, "Select");
        ddlTestMode_edit.SelectedIndex = 0;

        BindDDL(ddluploadtestmode, dsTestModes, "TestMode_Name", "TestMode_Id");
        ddluploadtestmode.Items.Insert(0, "Select");
        ddluploadtestmode.SelectedIndex = 0;

    }

    private void FillDDL_TestCategories()
    {
        DataSet dsTestCategory = ProductController.GetAllActiveTestCategory();
        BindDDL(ddlTestCategory, dsTestCategory, "TestCategory_Name", "TestCategory_Id");
        ddlTestCategory.Items.Insert(0, "Select");
        ddlTestCategory.SelectedIndex = 0;

        BindDDL(ddlTestCategory_Add, dsTestCategory, "TestCategory_Name", "TestCategory_Id");
        ddlTestCategory_Add.Items.Insert(0, "Select");
        ddlTestCategory_Add.SelectedIndex = 0;

        BindDDL(ddlTestCategory_Edit, dsTestCategory, "TestCategory_Name", "TestCategory_Id");
        ddlTestCategory_Edit.Items.Insert(0, "Select");
        ddlTestCategory_Edit.SelectedIndex = 0;

        BindDDL(ddluploadtestcategory, dsTestCategory, "TestCategory_Name", "TestCategory_Id");
        ddluploadtestcategory.Items.Insert(0, "Select");
        ddluploadtestcategory.SelectedIndex = 0;
    }

    private void FillDDL_TestTypes()
    {
        DataSet dsTestType = ProductController.GetAllActiveTestType();
        BindListBox(ddlTestType, dsTestType, "TestType_Name", "TestType_Id");

        BindDDL(ddlTestType_Add, dsTestType, "TestType_Name", "TestType_Id");
        ddlTestType_Add.Items.Insert(0, "Select");
        ddlTestType_Add.SelectedIndex = 0;

        BindDDL(ddluploadtesttype, dsTestType, "TestType_Name", "TestType_Id");
        ddluploadtesttype.Items.Insert(0, "Select");
        ddluploadtesttype.SelectedIndex = 0;


        BindDDL(ddlTestType_Edit, dsTestType, "TestType_Name", "TestType_Id");
        ddlTestType_Edit.Items.Insert(0, "Select");
        ddlTestType_Edit.SelectedIndex = 0;
    }

    private void FillDDL_Duration()
    {
        ddlHour_Add.Items.Clear();
        for (int cnt = 0; cnt <= 12; cnt++)
        {
            ddlHour_Add.Items.Add(Convert.ToString(cnt));
            ddlHour_edit.Items.Add(Convert.ToString(cnt));
        }

        ddlMin_Add.Items.Clear();
        for (int cnt = 0; cnt <= 59; cnt++)
        {
            ddlMin_Add.Items.Add(Convert.ToString(cnt));
            ddlMin_edit.Items.Add(Convert.ToString(cnt));
        }
        ddlHour_Add.SelectedIndex = 0;
        ddlHour_edit.SelectedIndex = 0;
        ddlMin_Add.SelectedIndex = 0;
        ddlMin_edit.SelectedIndex = 0;
    }

    protected void ddlTestCategory_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlTestCategory_Add.SelectedValue == "002")
        {
            Row_MCQTestOptions.Visible = true;
            //MCQ
        }
        else
        {
            Row_MCQTestOptions.Visible = false;
            //Normal Test
        }
        Clear_Error_Success_Box();
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

        BindDDL(ddlDivision_Edit, dsDivision, "Division_Name", "Division_Code");
        ddlDivision_Edit.Items.Insert(0, "Select");
        ddlDivision_Edit.SelectedIndex = 0;

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

        BindDDL(ddlAcadYear_Edit, dsAcadYear, "Description", "Id");
        ddlAcadYear_Add.Items.Insert(0, "Select");
        ddlAcadYear_Add.SelectedIndex = 0;


    }

    private void FillDDL_Centre(string Div_Code)
    {
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");



        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);
        dlCentre_Add.DataSource = dsCentre;
        dlCentre_Add.DataBind();
    }


    private void FillDDL_Centre_Edit(string Div_Code)
    {
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");



        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);
        dlCentre_edit.DataSource = dsCentre;
        dlCentre_edit.DataBind();
    }

    private void FillDDL_Slab(string Div_Code)
    {
        DataSet dsSlab = ProductController.Get_SlabBy_Division(Div_Code);
        if (dsSlab != null)
        {
            if (dsSlab.Tables.Count != 0)
            {
                if (dsSlab.Tables[0].Rows.Count != 0)
                {
                    BindDDL(ddlPCSlab, dsSlab, "Slab_Name", "Slab_Code");

                }

            }

        }

        ddlPCSlab.Items.Insert(0, "Select");
        ddlPCSlab.SelectedIndex = 0;
    }


    private void FillDDL_Slab_edit(string Div_Code)
    {
        DataSet dsSlab = ProductController.Get_SlabBy_Division(Div_Code);
        if (dsSlab != null)
        {
            if (dsSlab.Tables.Count != 0)
            {
                if (dsSlab.Tables[0].Rows.Count != 0)
                {
                    BindDDL(ddlPCSlab_edit, dsSlab, "Slab_Name", "Slab_Code");

                }

            }

        }

        ddlPCSlab_edit.Items.Insert(0, "Select");
        ddlPCSlab_edit.SelectedIndex = 0;
    }



    protected void ddlDivision_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        string Div_Code = null;
        Div_Code = ddlDivision_Add.SelectedValue;

        FillDDL_Centre(Div_Code);
        FillDDL_Standard_Add();
        FillDDL_Slab(Div_Code);
        Clear_Error_Success_Box();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
        Clear_Error_Success_Box();
    }

    private void FillDDL_Standard()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        DataSet dsStandard = ProductController.GetAllActive_Standard_ForYear(Div_Code, YearName);
        BindListBox(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");
        //ddlStandard.Items.Insert(0, "Select")
        //ddlStandard.SelectedIndex = 0
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

        ddlSubject_Add.Items.Clear();
        dlChapter_Add.DataSource = null;
        dlChapter_Add.DataBind();
    }

    protected void ddlAcadYear_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard_Add();
        Clear_Error_Success_Box();
    }

    protected void ddlStandard_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Subject_Add();
        Clear_Error_Success_Box();
    }

    private void FillDDL_Subject_Add()
    {
        //string Div_Code = null;
        //Div_Code = ddlDivision_Add.SelectedValue;

        //string YearName = null;
        //YearName = ddlAcadYear_Add.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard_Add.SelectedValue;

        //DataSet dsStandard = ProductController.GetAllSubjectsBy_Division_Year_Standard(Div_Code, YearName, StandardCode);
        DataSet dsStandard = ProductController.GetAllSubjectsByStandard(StandardCode);
        BindListBox(ddlSubject_Add, dsStandard, "Subject_Name", "Subject_Code");
        //ddlSubject_Add.Items.Insert(0, "Select")
        //ddlSubject_Add.SelectedIndex = 0
    }

    protected void ddlSubject_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Chapter_Add();
        Clear_Error_Success_Box();
    }

    private void FillDDL_Chapter_Add()
    {
        string Div_Code = null;
        Div_Code = ddlDivision_Add.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear_Add.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard_Add.SelectedValue;

        if (lblTestPKey_Hidden.Text.Trim() != "")
        {
            string PKeyp = lblTestPKey_Hidden.Text;
            string[] parts = PKeyp.Split('%');

            Div_Code = parts[0];


            YearName = parts[1];


            StandardCode = parts[2];

        }

        string SubjectCode = "";
        int SubCnt = 0;
        int SubSelCnt = 0;
        for (SubCnt = 0; SubCnt <= ddlSubject_Add.Items.Count - 1; SubCnt++)
        {
            if (ddlSubject_Add.Items[SubCnt].Selected == true)
            {
                SubSelCnt = SubSelCnt + 1;
                SubjectCode = SubjectCode + ddlSubject_Add.Items[SubCnt].Value + ",";
            }
        }
        //if (Strings.Right(SubjectCode, 1) == ",")
        //    SubjectCode = Strings.Left(SubjectCode, Strings.Len(SubjectCode) - 1);
        SubjectCode = Common.RemoveComma(SubjectCode);
        DataSet dsChapter = ProductController.GetAllChaptersBy_Division_Year_Standard_Subject(Div_Code, YearName, StandardCode, SubjectCode);
        dlChapter_Add.DataSource = dsChapter;
        dlChapter_Add.DataBind();
    }

    public void All_Chapter_ChkBox_Selected(object sender, System.EventArgs e)
    {

        CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlChapter_Add.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkChapter");

            chkitemck.Checked = s.Checked;
        }

    }

    public void All_Chapter_ChkBox_Selected_Edit(object sender, System.EventArgs e)
    {

        CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlChapter_Add_edit.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkChapteredit");

            chkitemck.Checked = s.Checked;
        }

    }

    public void All_Centre_ChkBox_Selected(object sender, System.EventArgs e)
    {
        //Change checked status of a hidden check box
        //chkCentreAllHidden_Sel.Checked = !(chkCentreAllHidden_Sel.Checked);

        //Set checked status of hidden check box to items in grid
        CheckBox s = sender as CheckBox;
        foreach (DataListItem dtlItem in dlCentre_Add.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCentre");

            chkitemck.Checked = s.Checked;
        }

    }

    protected void BtnSave_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();

        //Validation
        //Validate if all information is entered correctly
        if (ddlDivision_Add.SelectedIndex == 0 & string.IsNullOrEmpty(lblTestPKey_Hidden.Text))
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivision_Add.Focus();
            return;
        }

        if (ddlAcadYear_Add.SelectedIndex == 0 & string.IsNullOrEmpty(lblTestPKey_Hidden.Text))
        {
            Show_Error_Success_Box("E", "0002");
            ddlAcadYear_Add.Focus();
            return;
        }

        if (ddlStandard_Add.SelectedIndex == 0 & string.IsNullOrEmpty(lblTestPKey_Hidden.Text))
        {
            Show_Error_Success_Box("E", "0003");
            ddlStandard_Add.Focus();
            return;
        }

        if (ddlTestMode_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0011");
            ddlTestMode_Add.Focus();
            return;
        }

        if (ddlTestCategory_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0012");
            ddlTestCategory_Add.Focus();
            return;
        }

        if (ddlTestType_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0013");
            ddlTestType_Add.Focus();
            return;
        }

        //if (ddlPCSlab.SelectedIndex == 0)
        //{
        //    Show_Error_Success_Box("E", "Select Paper Checker Slab");
        //    ddlPCSlab.Focus();
        //    return;
        //}



        int SelCnt = 0;
        string SubjectCode = "";
        SelCnt = 0;
        for (int cnt = 0; cnt <= ddlSubject_Add.Items.Count - 1; cnt++)
        {
            if (ddlSubject_Add.Items[cnt].Selected == true)
            {
                SelCnt = SelCnt + 1;
                SubjectCode = SubjectCode + ddlSubject_Add.Items[cnt].Value + ",";
            }
        }
        if (SelCnt == 0)
        {
            Show_Error_Success_Box("E", "0005");
            ddlSubject_Add.Focus();
            return;
        }
        //if (Strings.Right(SubjectCode, 1) == ",")
        //    SubjectCode = Strings.Left(SubjectCode, Strings.Len(SubjectCode) - 1);
        SubjectCode = Common.RemoveComma(SubjectCode);


        if (ddlHour_Add.SelectedIndex == 0 & ddlMin_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0014");
            ddlHour_Add.Focus();
            return;
        }

        SelCnt = 0;
        string ChapterCode = "";
        foreach (DataListItem dtlItem in dlChapter_Add.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkChapter");
            Label lblChapterCode = (Label)dtlItem.FindControl("lblChapterCode");
            Label lblSubjectCode = (Label)dtlItem.FindControl("lblSubjectCode");
            if (chkitemck.Checked == true)
            {
                SelCnt = SelCnt + 1;
                ChapterCode = ChapterCode + lblSubjectCode.Text + '%' + lblChapterCode.Text + ",";
            }
        }
        //if (Strings.Right(ChapterCode, 1) == ",")
        //    ChapterCode = Strings.Left(ChapterCode, Strings.Len(ChapterCode) - 1);
        ChapterCode = Common.RemoveComma(ChapterCode);
        if (SelCnt == 0)
        {
            Show_Error_Success_Box("E", "0010");
            dlChapter_Add.Focus();
            return;
        }

        SelCnt = 0;
        string CentreCode = "";
        foreach (DataListItem dtlItem in dlCentre_Add.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCentre");
            Label lblCenterCode = (Label)dtlItem.FindControl("lblCenterCode");
            if (chkitemck.Checked == true)
            {
                SelCnt = SelCnt + 1;
                CentreCode = CentreCode + lblCenterCode.Text + ",";
            }
        }
        CentreCode = Common.RemoveComma(CentreCode);
        //if (Strings.Right(CentreCode, 1) == ",")
        //    CentreCode = Strings.Left(CentreCode, Strings.Len(CentreCode) - 1);

        if (SelCnt == 0)
        {
            Show_Error_Success_Box("E", "0006");
            dlCentre_Add.Focus();
            return;
        }

        //Validate if Test Name is duplicate or not


        //Save
        int ResultId = 0;

        int MaxMarks = 0;
        if (txtMaxMarks_Add.Text.Trim() != "")
        {
            MaxMarks = Convert.ToInt32(txtMaxMarks_Add.Text);
        }


        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        string TestModeCode = null;
        TestModeCode = ddlTestMode_Add.SelectedValue;

        string TestCategoryCode = null;
        TestCategoryCode = ddlTestCategory_Add.SelectedValue;

        string TestTypeCode = null;
        TestTypeCode = ddlTestType_Add.SelectedValue;

        int TestDuration = 0;
        TestDuration = (Convert.ToInt32(ddlHour_Add.SelectedValue) * 60) + Convert.ToInt32(ddlMin_Add.SelectedValue);

        int QPSetCount = 0;
        if (txtQPSetCount_Add.Text.Trim() != "")
        {
            QPSetCount = Convert.ToInt32(txtQPSetCount_Add.Text);
        }


        int QuestionCount = 0;
        if (txtQueCnt_Add.Text.Trim() != "")
        {
            QuestionCount = Convert.ToInt32(txtQueCnt_Add.Text);
        }


        int NegativeMarkingFlag = 0;
        if (chkNegativeMarkingFlag.Checked == true)
        {
            NegativeMarkingFlag = 1;
        }
        else
        {
            NegativeMarkingFlag = 0;
        }


        int HideChapterFlag = 0;
        if (ChkChapterHide.Checked == true)
        {
            HideChapterFlag = 1;
        }
        else
        {
            HideChapterFlag = 0;
        }
        string Slab_Code = "0";
        if (ddlPCSlab.SelectedIndex == 0)
        {
            Slab_Code = "0";
        }
        else
        {
            Slab_Code = ddlPCSlab.SelectedValue;
        }



        if (string.IsNullOrEmpty(lblTestPKey_Hidden.Text))
        {
            string DivisionCode = null;
            DivisionCode = ddlDivision_Add.SelectedValue;

            string YearName = null;
            YearName = ddlAcadYear_Add.SelectedItem.ToString();

            string StandardCode = null;
            StandardCode = ddlStandard_Add.SelectedValue;
            ResultId = ProductController.Insert_Test(DivisionCode, YearName, StandardCode, TestModeCode, TestCategoryCode, TestTypeCode, SubjectCode, CentreCode, ChapterCode, MaxMarks,
            TestDuration, QPSetCount, QuestionCount, txtTestName_Add.Text, txtTestDesc_Add.Text, txtRemarks_Add.Text, NegativeMarkingFlag, CreatedBy, HideChapterFlag, Slab_Code, txtSyllabusDesc.Text.Trim());
        }
        else
        {
            string PKeyp = lblTestPKey_Hidden.Text;
            string[] parts = PKeyp.Split('%');//PKeyp.Split( "%" );
            string DivisionCode = null;
            DivisionCode = parts[0];

            string YearName = null;
            YearName = parts[1];

            string StandardCode = null;
            StandardCode = parts[2];

            string TestCode = null;
            TestCode = parts[3];
            ResultId = ProductController.Update_Test(TestCode, DivisionCode, YearName, StandardCode, TestModeCode, TestCategoryCode, TestTypeCode, SubjectCode, CentreCode, ChapterCode,
            MaxMarks, TestDuration, QPSetCount, QuestionCount, txtTestName_Add.Text, txtTestDesc_Add.Text, txtRemarks_Add.Text, NegativeMarkingFlag, CreatedBy, HideChapterFlag, Slab_Code, txtSyllabusDesc.Text.Trim());
            //ResultId = 1
        }
        //Close the Add Panel and go to Search Grid
        if (ResultId == 1)
        {
            ControlVisibility("Result");
            BtnSearch_Click(sender, e);
            Show_Error_Success_Box("S", "0000");
            Clear_AddPanel();
        }
        else if (ResultId == -1)
        {
            Show_Error_Success_Box("E", "0029");
            txtTestName_Add.Focus();
            return;
        }
        else if (ResultId == -2)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Record not saved";
            UpdatePanelMsgBox.Update();
            return;
        }
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
        ddlStandard_Add.Items.Clear();
        ddlSubject_Add.Items.Clear();
        ddlTestMode_Add.SelectedIndex = 0;
        ddlTestType_Add.SelectedIndex = 0;
        ddlTestCategory_Add.SelectedIndex = 0;
        ddlHour_Add.SelectedIndex = 0;
        ddlMin_Add.SelectedIndex = 0;
        txtTestName_Add.Text = "";
        txtTestDesc_Add.Text = "";
        txtRemarks_Add.Text = "";
        txtMaxMarks_Add.Text = "";
        txtQPSetCount_Add.Text = "";
        txtQueCnt_Add.Text = "";
        dlChapter_Add.DataSource = null;
        dlChapter_Add.DataBind();
        dlCentre_Add.DataSource = null;
        dlCentre_Add.DataBind();
        ddlPCSlab.Items.Clear();
        txtSyllabusDesc.Text = "";
    }

    protected void ddlTestMode_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
    }

    protected void ddlTestType_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
    }

    protected void btnAuthorise_Yes_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
        //Authorise the selected test
        string PKey = null;
        PKey = lblPKey_Authorise.Text;

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string AlteredBy = null;
        AlteredBy = lblHeader_User_Code.Text;

        int ResultId = 0;
        ResultId = ProductController.UpdateTest_Authorise_Block(PKey, 1, AlteredBy);

        //Close the Add Panel and go to Search Grid
        if (ResultId == 1)
        {
            ControlVisibility("Result");
            BtnSearch_Click(sender, e);
            Show_Error_Success_Box("S", "0000");
            Clear_AddPanel();
        }

        //Refresh the grid

    }

    protected void btnDelete_Yes_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
        //Authorise the selected test
        string PKey = null;
        PKey = lbldelCode.Text;

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string AlteredBy = null;
        AlteredBy = lblHeader_User_Code.Text;

        int ResultId = 0;
        ResultId = ProductController.UpdateTest_Delete(PKey, AlteredBy);

        //Close the Add Panel and go to Search Grid
        if (ResultId == 1)
        {
            ControlVisibility("Result");
            BtnSearch_Click(sender, e);
            Show_Error_Success_Box("S", "0067");
            Clear_AddPanel();
        }
    }


    public Master_Test()
    {
        Load += Page_Load;
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlAcadYear.SelectedIndex = 0;
        ddlTestMode.SelectedIndex = 0;
        ddlTestCategory.SelectedIndex = 0;
        ddlStandard.Items.Clear();
        ddlTestType.Items.Clear();
        Msg_Error.Visible = false;
        lblerror.Text = "";
        Msg_Success.Visible = false;
        lblSuccess.Text = "";

    }
    protected void HLExport_Click(object sender, EventArgs e)
    {

        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=TestMaster.xls");


        Response.Charset = "";


        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

        dlGridExport.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the specified ASP.NET
        //     server control at run time. 

    }
    protected void btn_save_edit_Click(object sender, EventArgs e)
    {
        try
        {
            int SelCnt = 0;
            string ChapterCode = "";
            foreach (DataListItem dtlItem in dlChapter_Add_edit.Items)
            {
                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkChapteredit");
                Label lblChapterCode = (Label)dtlItem.FindControl("lblChapterCode_edit");
                Label lblSubjectCode = (Label)dtlItem.FindControl("lblSubjectCode_edit");
                if (chkitemck.Checked == true)
                {
                    SelCnt = SelCnt + 1;
                    ChapterCode = ChapterCode + lblSubjectCode.Text + '%' + lblChapterCode.Text + ",";
                }
            }
            //if (Strings.Right(ChapterCode, 1) == ",")
            //    ChapterCode = Strings.Left(ChapterCode, Strings.Len(ChapterCode) - 1);
            ChapterCode = Common.RemoveComma(ChapterCode);
            if (SelCnt == 0)
            {
                Show_Error_Success_Box("E", "0010");
                //dlChapter_Add_edit.Focus();
                return;
            }

            string PKeyp = lblTestPKey_Hidden_edit.Text;
            string[] parts = PKeyp.Split('%');//PKeyp.Split( "%" );
            string DivisionCode = null;
            DivisionCode = parts[0];

            string YearName = null;
            YearName = parts[1];

            string StandardCode = null;
            StandardCode = parts[2];

            string TestCode = null;
            TestCode = parts[3];

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string ModifiedBy = cookie.Values["UserID"];


            int ResultId = 0;
            ResultId = ProductController.Update_Test_After_Authorisation(TestCode, DivisionCode, YearName, StandardCode, ChapterCode, ModifiedBy);

            if (ResultId == 1)
            {
                ControlVisibility("Result");
                BtnSearch_Click(sender, e);
                Show_Error_Success_Box("S", "0000");
                Clear_AddPanel();
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void btn_close_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
        Clear_AddPanel();
    }
    protected void BtnSaveUpload_Click(object sender, EventArgs e)
    {

        divwaitsave.Visible = true;
        if (txtuploadfoldername.Text.Trim() == "")
        {
            divwaitsave.Visible = false;
            Show_Error_Success_Box("E", "Enter Folder Name");
            txtuploadfoldername.Focus();
            return;
        }



        string path = Server.MapPath("~/Omr_Sheets/" + txtuploadfoldername.Text + "/");


        if (txtuploadfoldername.Enabled == true)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            else
            {
                divwaitsave.Visible = false;
                Show_Error_Success_Box("E", "Folder Name Already Exists");
                txtuploadfoldername.Focus();
                return;
            }
        }

        
        HttpFileCollection files = Request.Files;
        
        if (files.Count > 0)
        {
            
            int sucesscount = 0;
            int errorcount = 0;

            for (int i = 0; i < files.Count; i++)
            {

                HttpPostedFile file = files[i];


                string fileName = file.FileName;
                string extension = Path.GetExtension(fileName);

                if (File.Exists(path + fileName))
                {

                }
                else
                {
                    if (extension == ".TIF" || extension == ".tif")
                    {
                        sucesscount = sucesscount + 1;
                        file.SaveAs(path + fileName);
                    }

                    else
                    {
                        errorcount = errorcount + 1;
                    }

                }





            }

            string PKeyp = lblpkeyuploadomr.Text;
            string[] parts = PKeyp.Split('%');//PKeyp.Split( "%" );
            string DivisionCode = null;
            DivisionCode = parts[0];

            string YearName = null;
            YearName = parts[1];

            string StandardCode = null;
            StandardCode = parts[2];

            string TestCode = null;
            TestCode = parts[3];

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string ModifiedBy = cookie.Values["UserID"];


            int ResultId = 0;
            if (sucesscount > 0)
            {
                divwaitsave.Visible = false;

                ResultId = 0;
                ResultId = ProductController.Update_OMR_Sheets_Path(TestCode, DivisionCode, YearName, StandardCode, "Omr_Sheets/" + txtuploadfoldername.Text + "/", ModifiedBy, 1);
            }


            if (errorcount > 0)
            {
                divwaitsave.Visible = false;
                ResultId = ProductController.Update_OMR_Sheets_Path(TestCode, DivisionCode, YearName, StandardCode, "Omr_Sheets/" + txtuploadfoldername.Text + "/", ModifiedBy, 1);
                BtnSearch_Click(sender, e);
                Show_Error_Success_Box("E", "No of files saved  sucessfully " + sucesscount + " files eliminated " + errorcount);          
            }
            else
            {
                divwaitsave.Visible = false;
                BtnSearch_Click(sender, e);
                Show_Error_Success_Box("S", "All files saved sucessfully");
                
                
            }

        }
        else
        {
            divwaitsave.Visible = false;
            Show_Error_Success_Box("E", "Select at least one file");
        
        }
    }

    
    
    protected void BtnCloseUpload_Click1(object sender, EventArgs e)
    {
        ControlVisibility("Result");
    }

    
}
