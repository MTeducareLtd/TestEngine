using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;

partial class Tran_TestAnswerPapers : System.Web.UI.Page
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();
            FillIssuerType();
            FillReceiverType();
            FillDDL_TestCategories();
            FillDDL_TestTypes();
            //Response.Redirect("Tran_TestAnswerPapers.aspx?Div=" + ddlDivision_Add.SelectedValue + "&AcadYear=" + ddlAcadYear_Add.SelectedValue + "&Center=" + ddlCentre_Add.SelectedValue + "&Course=" + ddlStandard_Add.SelectedValue + "&BatchCode=" + BatchCode + "&TestCategoryId=" + lblTestCategoryId.Text + "&TestTypeId=" + lblTestTypeId.Text + "&TestId=" + ddlTestName_Add.SelectedValue);
            if ((Request.QueryString["Div"] != null) && (Request.QueryString["AcadYear"] != null) && (Request.QueryString["Center"] != null) && (Request.QueryString["Course"] != null) && (Request.QueryString["BatchCode"] != null) && (Request.QueryString["TestCategoryId"] != null) && (Request.QueryString["TestTypeId"] != null) && (Request.QueryString["TestId"] != null))
            {
                ControlVisibility("Add");
                ddlDivision_Add.SelectedValue = Request.QueryString["Div"].ToString();
                ddlDivision_Add_SelectedIndexChanged(sender, e);
                ddlAcadYear_Add.SelectedValue = Request.QueryString["AcadYear"].ToString();
                ddlAcadYear_Add_SelectedIndexChanged(sender, e);
                ddlCentre_Add.SelectedValue = Request.QueryString["Center"].ToString();
                ddlCentre_Add_SelectedIndexChanged(sender, e);
                ddlStandard_Add.SelectedValue = Request.QueryString["Course"].ToString();
                ddlStandard_Add_SelectedIndexChanged(sender, e);
                string[] parts = Request.QueryString["BatchCode"].Split(',');
                ddlBatch_Add.SelectedValue = parts[0];
                ddlBatch_Add_SelectedIndexChanged(sender, e);
                ddlTestCategory_Add.SelectedValue = Request.QueryString["TestCategoryId"].ToString();
                //ddlTestCategory_Add_SelectedIndexChanged(sender, e);

                for (int cnt = 0; cnt <= ddlTestType_Add.Items.Count - 1; cnt++)
                {
                    if (ddlTestType_Add.Items[cnt].Value.ToString() == Request.QueryString["TestTypeId"].ToString())
                    {
                        ddlTestType_Add.Items[cnt].Selected = true;
                        break;
                    }
                }

                ddlTestType_Add_SelectedIndexChanged(sender, e);
                string TestId = "";
                //string[] parts2 = Request.QueryString["TestId"].Split('%');
                //TestId = Request.QueryString["Div"].ToString() + "%" + Request.QueryString["AcadYear"].ToString() + "%" + parts2[1] + "%" + parts2[2];
                //ddlTestName_Add.SelectedValue = TestId;
                ddlTestName_Add_SelectedIndexChanged(sender, e);
                ddlIssuerType.SelectedValue = "Center";
                ddlIssuerType_SelectedIndexChanged(sender, e);
                trStudentList.Visible = false;
                tblIssueDate.Visible = false;
                trExpectedReturnDate.Visible = false;
                //ddlTestType_Add.SelectedValue
                //string[] parts = TestSubPKey.Split('%');
                //string StandardCode = parts[2];
                //ddlDLRefCouse.SelectedValue = StandardCode;
                //ddlBatch_Add.Items.Remove(DropDownList3.Items[x]);
                //ddlBatch_Add.SelectedValue = Request.QueryString["Course"].ToString();
            }
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

        BindDDL(ddlDivision_Add, dsDivision, "Division_Name", "Division_Code");
        ddlDivision_Add.Items.Insert(0, "Select");
        ddlDivision_Add.SelectedIndex = 0;

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
            DivReturn.Visible = false;
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = true;
            DivEditPanel.Visible = false;
            DivReturn.Visible = false;
        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
            DivEditPanel.Visible = false;
            DivReturn.Visible = false;
        }
        else if (Mode == "Edit")
        {
            DivAddPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            DivEditPanel.Visible = true;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
            DivReturn.Visible = false;
        }
        else if (Mode == "Return")
        {
            DivAddPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            DivEditPanel.Visible = false;
            DivReturn.Visible = true;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
        }

        Clear_Error_Success_Box();
    }

    protected void BtnAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Add");
    }

    protected void BtnCloseAdd_Click(object sender, System.EventArgs e)
    {
        if ((Request.QueryString["Div"] != null) && (Request.QueryString["AcadYear"] != null) && (Request.QueryString["Center"] != null) && (Request.QueryString["Course"] != null) && (Request.QueryString["BatchCode"] != null) && (Request.QueryString["TestCategoryId"] != null) && (Request.QueryString["TestTypeId"] != null) && (Request.QueryString["TestId"] != null))
        {
            Response.Redirect("Tran_TestSchedule.aspx"); 
        }
        else
        {
            ControlVisibility("Result");
        }
    }

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        Fill_Grid();

    }

    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            lbldelCode.Text = e.CommandArgument.ToString();
            txtDeleteItemName.Text = (((Label)e.Item.FindControl("lblModeName")).Text);
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }
        else if (e.CommandName == "Manage")
        {
            ControlVisibility("Edit");

            lblPKey_Edit.Text = e.CommandArgument.ToString();
            FillAnswersheetIssueDetails(lblPKey_Edit.Text);
        }

        else if (e.CommandName == "Return")
        {
            ControlVisibility("Return");

            lblPKey_Edit.Text = e.CommandArgument.ToString();
            FillReturnAnswersheetIssueDetails(lblPKey_Edit.Text);
        }
    }

    private void FillAnswersheetIssueDetails(string PKey)
    {
        try
        {
            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            FillPaperCheckerList_Edit();

            DataSet dsIssue = ProductController.GetAnswerSheetIssueDetails_ByPKey(PKey, lblHeader_User_Code.Text, 1);

            if (dsIssue.Tables[0].Rows.Count > 0)
            {
                txtEdit_Division.Text = ddlDivision.SelectedItem.Text;
                txtEdit_Year.Text = ddlAcadYear.SelectedItem.Text;
                txtEditStandard.Text = ddlStandard.SelectedItem.Text;
                txtEditCentre.Text = ddlCentre.SelectedItem.Text;

                txtEditBatch.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["BatchName"]);
                txtEdit_Category.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["TestCategory_Name"]);
                txtEdit_TestType.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["TestType_Name"]);
                txtEdit_TestName.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Test_Name"]);
                txtEdit_ConductNo.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Conduct_No"]);
                txtEdit_MaxMarks.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["MaxMarks"]);
                txtEdit_TestDate.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["TestDate"]);
                txtEdit_IssueDate.Value = Convert.ToString(dsIssue.Tables[0].Rows[0]["Issue_Date"]);
                txtexpectedreturndate_edit.Value = Convert.ToString(dsIssue.Tables[0].Rows[0]["Expected_Return_Date"]);
                txtEdit_ReceiverType.Text = "Paper Checker";
                lblEdit_ReceiverType.Text = "Paper Checker";
                ddlEdit_IssuedTo.SelectedValue = Convert.ToString(dsIssue.Tables[0].Rows[0]["Partner_Code"]);
                lblPaperCoamrrectorName.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Partner_Name"]);


                FillDDL_Slab(Convert.ToString(dsIssue.Tables[0].Rows[0]["Division_Code"]));
                if (Convert.ToString(dsIssue.Tables[0].Rows[0]["PCSlab_Id"]) != "")
                {
                    ddlEditPCSlab.SelectedValue = Convert.ToString(dsIssue.Tables[0].Rows[0]["PCSlab_Id"]);
                }

                lblPrintDivision.Text = ddlDivision.SelectedItem.Text;
                lblPrintAcademic.Text = ddlAcadYear.SelectedItem.Text;
                lblPrintCourse.Text = ddlStandard.SelectedItem.Text;
                lblPrintCenter.Text = ddlCentre.SelectedItem.Text;
                lblPrintBatch.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["BatchName"]);
                lblPrintTestCategory.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["TestCategory_Name"]);


                lblTestType.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["TestType_Name"]);
                lblPrintTestName.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Test_Name"]);
                lblMaximumMarks.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["MaxMarks"]);
                lblTestDate.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["TestDate"]);
                lblPrintIssueDate.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Issue_Date"]);
                lblPrintSubjects.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Subjects"]);
                lblreturndate.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Return_Date"]);
                lblPaperCount.Text = Convert.ToString(dsIssue.Tables[1].Rows[0]["PresentCount"]);
                lblprintabsent.Text = Convert.ToString(dsIssue.Tables[1].Rows[0]["AbsentCount"]);
                lblprinttotalstudents.Text = Convert.ToString(dsIssue.Tables[1].Rows[0]["BatchStrength"]);


                string TestPKey = null;
                TestPKey = lblPKey_Edit.Text;

                //Find last occurance of % sign
                int LastPercentPos = 0;
                //LastPercentPos = Strings.InStrRev(TestPKey, "%");
                LastPercentPos = TestPKey.LastIndexOf("%");
                //TestPKey = Strings.Left(TestPKey, LastPercentPos - 1);
                TestPKey = TestPKey.Substring(0, LastPercentPos);
                int ActionFlag = 0;
                ActionFlag = 2;
                DataSet dsStudent = ProductController.GetStudent_ForAnswerSheetIssue_ByTestPKey(TestPKey, ActionFlag);

                DLEdit_StudentList.DataSource = dsStudent;
                DLEdit_StudentList.DataBind();


                dsPrint.DataSource = dsStudent;
                dsPrint.DataBind();
                if (dsStudent != null)
                {
                    if (dsStudent.Tables.Count != 0)
                    {
                        if (dsStudent.Tables[0].Rows.Count != 0)
                        {
                            btnPrint.Visible = true;
                        }
                        else
                        {
                            btnPrint.Visible = false;
                        }
                    }
                    else
                    {
                        btnPrint.Visible = false;
                    }
                }
                else
                {
                    btnPrint.Visible = false;
                }


                lblFooterDate.Text = DateTime.Now.ToString("dd MMM yyyy");

                // lblPaperCount.Text = dsStudent.Tables[0].Rows.Count.ToString();

                foreach (DataListItem dtlItem in DLEdit_StudentList.Items)
                {
                    CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
                    Label lblBagDispatch_Id = (Label)dtlItem.FindControl("lblBagDispatch_Id");
                    if (chkStudent.Checked == true && chkStudent.Visible == true && lblBagDispatch_Id.Text != Convert.ToString(dsIssue.Tables[0].Rows[0]["BagDispatch_Id"]))
                    {
                        chkStudent.Enabled = false;
                        //Exit For
                    }
                }

            }
            else
            {
                DLEdit_StudentList.DataSource = null;
                DLEdit_StudentList.DataBind();


                dsPrint.DataSource = null;
                dsPrint.DataBind();
                btnPrint.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }

    }

    private void FillReturnAnswersheetIssueDetails(string PKey)
    {
        try
        {
            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            FillPaperCheckerList_Edit();

            DataSet dsIssue = ProductController.GetAnswerSheetIssueDetails_ByPKey(PKey, lblHeader_User_Code.Text, 1);

            if (dsIssue.Tables[0].Rows.Count > 0)
            {
                txtReturnDivision.Text = ddlDivision.SelectedItem.Text;
                txtReturnAcademic.Text = ddlAcadYear.SelectedItem.Text;
                txtReturnCourse.Text = ddlStandard.SelectedItem.Text;
                txtReturnCenter.Text = ddlCentre.SelectedItem.Text;

                txtReturnBatch.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["BatchName"]);
                txtReturnTestCategory.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["TestCategory_Name"]);
                txtReturnTestType.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["TestType_Name"]);
                txtReturnTestName.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Test_Name"]);
                txtReturnConductNo.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Conduct_No"]);
                txtReturnMaximumMarks.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["MaxMarks"]);
                txtreturnTestDate.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["TestDate"]);
                txtreturnReceiverType.Text = "Center";
                txtReturnPartner.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Partner_Name"]);
                txtreturnIssueQuantity.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Issue_Quantity"]);
                txtreturnQuantity.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Returned_Quantity"]);
                lblpaper_corrector.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Partner_Name"]);
                lblCorrectorPrint.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Partner_Name"]);

                lblDivisionP.Text = ddlDivision.SelectedItem.Text;
                lblyearP.Text = ddlAcadYear.SelectedItem.Text;
                lblcoursep.Text = ddlStandard.SelectedItem.Text;
                lblcenterp.Text = ddlCentre.SelectedItem.Text;
                lblbatchp.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["BatchName"]);
                lblcategoryP.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["TestCategory_Name"]);

                lbldivisionprint.Text = ddlDivision.SelectedItem.Text;
                lblacadyearprint.Text = ddlAcadYear.SelectedItem.Text;
                lblCoursePrint.Text = ddlStandard.SelectedItem.Text;
                lblCenterPrint.Text = ddlCentre.SelectedItem.Text;
                lblBatchPrint.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["BatchName"]);
                lblTestCategoryPrint.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["TestCategory_Name"]);



                lbltesttyp_Print.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["TestType_Name"]);
                lbltestNM_Print.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Test_Name"]);
                lblmarksPrint.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["MaxMarks"]);
                lblDate.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["TestDate"]);
                lblissueDatePrint.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Issue_Date"]);
                lblsubjecrprint.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Subjects"]);
                submissionDateprint.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Return_Date"]);
                lblpresentprint.Text = Convert.ToString(dsIssue.Tables[1].Rows[0]["PresentCount"]);
                lblpaperscorrectedPrint.Text = Convert.ToString(dsIssue.Tables[1].Rows[0]["PresentCount"]);
                lblabsentPrint.Text = Convert.ToString(dsIssue.Tables[1].Rows[0]["AbsentCount"]);
                TotalstdntPrint.Text = Convert.ToString(dsIssue.Tables[1].Rows[0]["BatchStrength"]);


                testP.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["TestType_Name"]);
                TestNameP.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Test_Name"]);
                DateP.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["TestDate"]);
                MaxMarkP.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["MaxMarks"]);
                DateIssueP.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Issue_Date"]);
                lblsubjectP.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Subjects"]);
                lblreturndateP.Text = Convert.ToString(dsIssue.Tables[0].Rows[0]["Return_Date"]);
                lblprintpresentP.Text = Convert.ToString(dsIssue.Tables[1].Rows[0]["PresentCount"]);
                lblprintabsentP.Text = Convert.ToString(dsIssue.Tables[1].Rows[0]["AbsentCount"]);
                total_studentP.Text = Convert.ToString(dsIssue.Tables[1].Rows[0]["BatchStrength"]);
                lblpapercorrectedP.Text = Convert.ToString(dsIssue.Tables[1].Rows[0]["PresentCount"]);


                if (Convert.ToString(dsIssue.Tables[0].Rows[0]["Return_Date"]) != "")
                {
                    txtReturnDate.Value = Convert.ToString(dsIssue.Tables[0].Rows[0]["Return_Date"]);
                }
                else
                {
                    txtReturnDate.Value = "";
                }



                string TestPKey = null;
                TestPKey = lblPKey_Edit.Text;

                //Find last occurance of % sign
                int LastPercentPos = 0;
                //LastPercentPos = Strings.InStrRev(TestPKey, "%");
                LastPercentPos = TestPKey.LastIndexOf("%");
                //TestPKey = Strings.Left(TestPKey, LastPercentPos - 1);
                TestPKey = TestPKey.Substring(0, LastPercentPos);
                int ActionFlag = 0;
                ActionFlag = 2;
                DataSet dsStudent = ProductController.GetStudent_ForAnswerSheetIssue_ByTestPKey(TestPKey, ActionFlag);

                ddlReturn.DataSource = dsStudent;
                ddlReturn.DataBind();

                if (Convert.ToString(dsIssue.Tables[0].Rows[0]["Return_Flag"]) == "1")
                {
                    btnReturnSave.Visible = false;
                }
                else
                {
                    btnReturnSave.Visible = true;
                }
            }
            else
            {
                ddlReturn.DataSource = null;
                ddlReturn.DataBind();

            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }

    }

    private void FillPaperCheckerList_Edit()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        DataSet dsPaperChecker = ProductController.GetPartnerMasterBy_Division(Div_Code, "1");

        BindDDL(ddlEdit_IssuedTo, dsPaperChecker, "Partner_Name", "Partner_Code");
        ddlEdit_IssuedTo.Items.Insert(0, "Select");
        ddlEdit_IssuedTo.SelectedIndex = 0;
    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    private void FillIssuerType()
    {
        ddlIssuerType.Items.Clear();
        ddlIssuerType.Items.Add("[ Select ]");
        ddlIssuerType.Items.Add("Center");
        ddlIssuerType.Items.Add("Paper Checker");

        ddlIssuedFrom_Search.Items.Clear();
        ddlIssuedFrom_Search.Items.Add("[ Select ]");
        ddlIssuedFrom_Search.Items.Add("Center");
        ddlIssuedFrom_Search.Items.Add("Paper Checker");
    }

    private void FillReceiverType()
    {
        ddlReceiverType.Items.Clear();
        ddlReceiverType.Items.Add("[ Select ]");
        ddlReceiverType.Items.Add("Center");
        ddlReceiverType.Items.Add("Paper Checker");

        ddlIssuedTo_Search.Items.Clear();
        ddlIssuedTo_Search.Items.Add("[ Select ]");
        ddlIssuedTo_Search.Items.Add("Center");
        ddlIssuedTo_Search.Items.Add("Paper Checker");
    }

    protected void ddlIssuerType_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlIssuerType.SelectedItem.ToString() == "Center")
        {
            lblIssuerType.Text = "Center";
            if (ddlCentre_Add.SelectedIndex > 0)
            {
                lblIssuedToCentreName.Text = ddlCentre_Add.SelectedItem.Text;
            }
            else
            {
                lblIssuedToCentreName.Text = "";
            }

            lblIssuedToCentreName.Visible = true;
            ddlIssuedToPaperChecker.Visible = false;

            FillStudentList_ForIssue();

            ddlReceiverType.SelectedIndex = 2;
        }
        else if (ddlIssuerType.SelectedItem.ToString() == "Paper Checker")
        {
            lblIssuerType.Text = "Paper Checker";
            lblIssuedToCentreName.Visible = false;
            ddlIssuedToPaperChecker.Visible = true;

            FillPaperCheckerList();
            if (ddlCentre_Add.SelectedIndex > 0)
            {
                lblRcvdToCentreName.Text = ddlCentre_Add.SelectedItem.Text;
            }
            else
            {
                lblRcvdToCentreName.Text = "";
            }



            ddlReceiverType.SelectedIndex = 1;
        }
        FillReceiverInfo();
    }

    private void FillReceiverInfo()
    {
        if (ddlReceiverType.SelectedItem.ToString() == "Center")
        {
            lblReceiverType.Text = "Center";
            if (ddlCentre_Add.SelectedIndex > 0)
            {
                lblIssuedToCentreName.Text = ddlCentre_Add.SelectedItem.Text;
            }
            else
            {
                lblIssuedToCentreName.Text = "";
            }


            lblRcvdToCentreName.Visible = true;
            ddlRcvdFromPaperChecker.Visible = false;
        }
        else if (ddlReceiverType.SelectedItem.ToString() == "Paper Checker")
        {
            lblReceiverType.Text = "Paper Checker";
            lblRcvdToCentreName.Visible = false;
            ddlRcvdFromPaperChecker.Visible = true;


            FillPaperCheckerList();
        }
    }

    protected void ddlReceiverType_SelectedIndexChanged(object sender, System.EventArgs e)
    {


    }

    private void FillStudentList_ForIssue()
    {
        try
        {
            Clear_Error_Success_Box();
            //Fill Names of all present students for the selected test whose Answer Papers are not issued for correction
            string TestPKey = null;
            //TestPKey = ddlDivision_Add.SelectedValue + "%" + ddlAcadYear_Add.SelectedItem.Text + "%" + ddlStandard_Add.SelectedValue + "%" + ddlCentre_Add.SelectedValue + "%" + ddlBatch_Add.SelectedValue + "%" + Strings.Right(ddlTestName_Add.SelectedValue, Strings.Len(ddlTestName_Add.SelectedValue) - 16) + "%" + ddlConductNo_Add.SelectedItem.Text;

            //TestPKey = ddlDivision_Add.SelectedValue + "%" + ddlAcadYear_Add.SelectedItem.Text + "%" + ddlStandard_Add.SelectedValue + "%" + ddlCentre_Add.SelectedValue + "%" + ddlBatch_Add.SelectedValue + "%" + ddlTestName_Add.SelectedValue.Substring(ddlTestName_Add.SelectedValue.Length - (ddlTestName_Add.SelectedValue.Length) - 16) + "%" + ddlConductNo_Add.SelectedItem.Text;
            if (ddlTestName_Add.SelectedValue != "Select" && ddlTestName_Add.SelectedValue != "" && ddlTestName_Add.SelectedValue != "0")
            {
                string[] words = ddlTestName_Add.SelectedValue.ToString().Split('%');

                TestPKey = ddlDivision_Add.SelectedValue + "%" + ddlAcadYear_Add.SelectedItem.Text + "%" + ddlStandard_Add.SelectedValue + "%" + ddlCentre_Add.SelectedValue + "%" + ddlBatch_Add.SelectedValue + "%" + words[3].ToString() + "%" + ddlConductNo_Add.SelectedItem.Text;

                int ActionFlag = 0;
                ActionFlag = 1;
                DataSet dsStudent = ProductController.GetStudent_ForAnswerSheetIssue_ByTestPKey(TestPKey, ActionFlag);
                dlStudentList.DataSource = dsStudent;
                dlStudentList.DataBind();

                txtTestDate_Add.Text = Convert.ToDateTime(dsStudent.Tables[1].Rows[0]["TestDate"]).ToString("dd MMM yyyy");


            }
            else
            {
                txtTestDate_Add.Text = "";

            }

        }
        catch (Exception ex)
        {
            dlStudentList.DataSource = null;
            dlStudentList.DataBind();
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }

    }

    private void FillPaperCheckerList()
    {
        string Div_Code = null;
        Div_Code = ddlDivision_Add.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear_Add.SelectedItem.ToString();

        DataSet dsPaperChecker = ProductController.GetPartnerMasterBy_Division(Div_Code, "1");
        BindDDL(ddlIssuedToPaperChecker, dsPaperChecker, "Partner_Name", "Partner_Code");
        ddlIssuedToPaperChecker.Items.Insert(0, "Select");
        ddlIssuedToPaperChecker.SelectedIndex = 0;

        BindDDL(ddlRcvdFromPaperChecker, dsPaperChecker, "Partner_Name", "Partner_Code");
        ddlRcvdFromPaperChecker.Items.Insert(0, "Select");
        ddlRcvdFromPaperChecker.SelectedIndex = 0;
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

        ddlBatch_Add.Items.Clear();
        ddlTestName_Add.Items.Clear();
        Clear_TestDetails();
    }

    protected void ddlDivision_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard_Add();
        FillDDL_Centre_Add();
        FillDDL_Slab(ddlDivision_Add.SelectedValue);
        Clear_Error_Success_Box();
    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
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
        
        BindDDL(ddlBatch_Add, dsBatch, "Batch_Name", "Batch_Code");
        ddlBatch_Add.Items.Insert(0, "Select");
        ddlBatch_Add.SelectedIndex = 0;

        if (Request.QueryString["BatchCode"] != null)
        {
            string[] parts = Request.QueryString["BatchCode"].Split(',');
            int c = parts.Length;
            int flag = 0;
            ArrayList list = new ArrayList();
            for (int i = 1; i < ddlBatch_Add.Items.Count; i++)
            {
                for (int j = 0; j < parts.Length; j++)
                {
                    if (ddlBatch_Add.Items[i].Value.ToString() == parts[j].ToString())
                    {
                        flag = 1;
                        break;
                    }
                    flag = 0;
                }
                if (flag == 0)
                {
                    list.Add(i);
                }
            }

            int k = 0;
            foreach (int i in list)
            {
                
                try
                {
                    ddlBatch_Add.Items.Remove(ddlBatch_Add.Items[i - k]);
                    k++;
                }
                catch
                {
                }
            }
        }

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
        ddlTestName_Add.Items.Clear();
        ddlConductNo_Add.Items.Clear();

        //Validate if all information is entered correctly
        if (ddlDivision_Add.SelectedIndex == 0)
        {
            //Show_Error_Success_Box("E", "0001")
            //ddlDivision.Focus()
            return;
        }

        if (ddlAcadYear_Add.SelectedIndex == 0)
        {
            //Show_Error_Success_Box("E", "0002")
            //ddlAcadYear.Focus()
            return;
        }

        if (ddlCentre_Add.SelectedIndex == 0)
        {
            //Show_Error_Success_Box("E", "0006")
            //ddlCentre.Focus()
            return;
        }

        if (ddlStandard_Add.SelectedIndex == 0)
        {
            //Show_Error_Success_Box("E", "0003")
            //ddlStandard.Focus()
            return;
        }

        if (ddlTestCategory_Add.SelectedIndex == 0)
        {
            //Show_Error_Success_Box("E", "0012")
            //ddlTestCategory.Focus()
            return;
        }

        if (ddlBatch_Add.SelectedIndex == 0)
        {
            return;
        }

        string BatchCode = "";
        BatchCode = ddlBatch_Add.SelectedValue.ToString();

        //Dim BatchCnt As Integer
        //Dim BatchSelCnt As Integer = 0
        //For BatchCnt = 0 To ddlBatch_Add.Items.Count - 1
        //    If ddlBatch_Add.Items(BatchCnt).Selected = True Then
        //        BatchSelCnt = BatchSelCnt + 1
        //    End If
        //Next

        //If BatchSelCnt = 0 Then
        //    'When all is selected
        //    For BatchCnt = 0 To ddlBatch_Add.Items.Count - 1
        //        BatchCode = BatchCode & ddlBatch_Add.Items(BatchCnt).Value & ","
        //    Next
        //    If Right(BatchCode, 1) = "," Then BatchCode = Left(BatchCode, Len(BatchCode) - 1)
        //Else
        //    For BatchCnt = 0 To ddlBatch_Add.Items.Count - 1
        //        If ddlBatch_Add.Items(BatchCnt).Selected = True Then
        //            BatchCode = BatchCode & ddlBatch_Add.Items(BatchCnt).Value & ","
        //        End If
        //    Next
        //    If Right(BatchCode, 1) = "," Then BatchCode = Left(BatchCode, Len(BatchCode) - 1)
        //End If

        string TestType_ID = "";
        int TypeCnt = 0;
        int TypeSelCnt = 0;
        for (TypeCnt = 0; TypeCnt <= ddlTestType_Add.Items.Count - 1; TypeCnt++)
        {
            if (ddlTestType_Add.Items[TypeCnt].Selected == true)
            {
                TypeSelCnt = TypeSelCnt + 1;
            }
        }

        if (TypeSelCnt == 0)
        {
            //When all is selected
            for (TypeCnt = 0; TypeCnt <= ddlTestType_Add.Items.Count - 1; TypeCnt++)
            {
                TestType_ID = TestType_ID + ddlTestType_Add.Items[TypeCnt].Value + ",";
            }
            //if (Strings.Right(TestType_ID, 1) == ",")
            //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);
            TestType_ID = Common.RemoveComma(TestType_ID);
        }
        else
        {
            for (TypeCnt = 0; TypeCnt <= ddlTestType_Add.Items.Count - 1; TypeCnt++)
            {
                if (ddlTestType_Add.Items[TypeCnt].Selected == true)
                {
                    TestType_ID = TestType_ID + ddlTestType_Add.Items[TypeCnt].Value + ",";
                }
            }
            //if (Strings.Right(TestType_ID, 1) == ",")
            //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);
            TestType_ID = Common.RemoveComma(TestType_ID);
        }

        string DivisionCode = null;
        DivisionCode = ddlDivision_Add.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear_Add.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard_Add.SelectedValue;

        string TestName = null;
        TestName = "%";

        string CentreCode = null;
        CentreCode = ddlCentre_Add.SelectedValue;

        string FromDate = null;
        string ToDate = null;
        FromDate = System.DateTime.Now.ToString("01 Jan 2010");
        ToDate = System.DateTime.Now.ToString("31 Dec 2050");

        DataSet dsTestName = null;
        if ((Request.QueryString["Div"] != null) && (Request.QueryString["AcadYear"] != null) && (Request.QueryString["Center"] != null) && (Request.QueryString["Course"] != null) && (Request.QueryString["BatchCode"] != null) && (Request.QueryString["TestCategoryId"] != null) && (Request.QueryString["TestTypeId"] != null) && (Request.QueryString["TestId"] != null))
        {
            dsTestName=ProductController.GetTestScheduleBy_Division_Year_Standard_Centre(DivisionCode, YearName, StandardCode, BatchCode, "01,02", ddlTestCategory_Add.SelectedValue, TestType_ID, TestName, FromDate, ToDate,
                                0, 0, CentreCode, 3);
            BindDDL(ddlTestName_Add, dsTestName, "Test_Name", "PKey");
            ddlTestName_Add.Items.Insert(0, "Select");
            ddlTestName_Add.SelectedIndex = 0;
            //
            try
            {
                string TestId = "";
                string[] parts2 = Request.QueryString["TestId"].Split('%');
                TestId = Request.QueryString["Div"].ToString() + "%" + Request.QueryString["AcadYear"].ToString() + "%" + parts2[1] + "%" + parts2[2];
                ddlTestName_Add.SelectedValue = TestId;

                string PKey = null;
                PKey = ddlTestName_Add.SelectedValue;
                Clear_TestDetails();
                FillTestMasterDetails(PKey);
                FillStudentList_ForIssue();
                
            }
            catch
            {

            }
        }
        else
        {
            dsTestName=ProductController.GetTestScheduleBy_Division_Year_Standard_Centre(DivisionCode, YearName, StandardCode, BatchCode, "01,02", ddlTestCategory_Add.SelectedValue, TestType_ID, TestName, FromDate, ToDate,
                                1, -1, CentreCode, 3);
            BindDDL(ddlTestName_Add, dsTestName, "Test_Name", "PKey");
            ddlTestName_Add.Items.Insert(0, "Select");
            ddlTestName_Add.SelectedIndex = 0;
        }
        
        Clear_TestDetails();
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
        FillStudentList_ForIssue();
    }

    private void FillTestMasterDetails(string PKey)
    {
        DataSet dsTest = ProductController.GetTestMasterBY_PKey(PKey, 1);
        ddlConductNo_Add.Items.Clear();

        if (dsTest.Tables[0].Rows.Count > 0)
        {
            txtMaxMarks_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["MaxMarks"]);
            //txtTestCategory_Add.Text = dsTest.Tables(0).Rows(0)("TestCategory_Name")
            //txtTestType_Add.Text = dsTest.Tables(0).Rows(0)("TestType_Name")
            txtSubject_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Subjects"]);
            txtRemarks_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Remarks"]);
            txtfromtime.Text = "";
            txttotime.Text = "";


            int ConductCount = 0;
            ConductCount = Convert.ToInt32(dsTest.Tables[3].Rows[0]["Conduct_No"]);

            for (int cnt = 1; cnt <= ConductCount; cnt++)
            {
                ListItem li = new ListItem(cnt.ToString(), cnt.ToString());
                ddlConductNo_Add.Items.Add(li);
            }
        }
    }

    private void Clear_TestDetails()
    {
        txtfromtime.Text = "";
        txttotime.Text = "";
        txtTestDate_Add.Text = System.DateTime.Now.ToString("dd MMM yyyy");

        txtMaxMarks_Add.Text = "";
        txtRemarks_Add.Text = "";
        txtSubject_Add.Text = "";
        //txtTestCategory_Add.Text = ""
        //txtTestType_Add.Text = ""

        dlStudentList.DataSource = null;
        dlStudentList.DataBind();
    }


    private void FillDDL_TestCategories()
    {
        DataSet dsTestCategory = ProductController.GetAllActiveTestCategory();
        BindDDL(ddlTestCategory_Add, dsTestCategory, "TestCategory_Name", "TestCategory_Id");
        ddlTestCategory_Add.Items.Insert(0, "Select");
        ddlTestCategory_Add.SelectedIndex = 0;

    }

    private void FillDDL_TestTypes()
    {
        DataSet dsTestType = ProductController.GetAllActiveTestType();
        BindListBox(ddlTestType_Add, dsTestType, "TestType_Name", "TestType_Id");

    }

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    protected void ddlTestCategory_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Test_Add();
        Clear_Error_Success_Box();
    }

    protected void ddlTestType_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Test_Add();
        Clear_Error_Success_Box();
    }

    protected void lnkRefreshStudentList_Click(object sender, System.EventArgs e)
    {
        FillStudentList_ForIssue();
    }

    protected void BtnSaveAdd_Click(object sender, System.EventArgs e)
    {
        //Validate if all information is entered correctly
        Clear_Error_Success_Box();

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

        if (ddlBatch_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0015");
            ddlBatch_Add.Focus();
            return;
        }

        if (ddlTestName_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0016");
            ddlTestName_Add.Focus();
            return;
        }

        if (ddlIssuerType.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0068");
            ddlIssuerType.Focus();
            return;
        }

        if (ddlReceiverType.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0069");
            ddlReceiverType.Focus();
            return;
        }


        if (ddlIssuerType.SelectedIndex == 2)
        {
            if (ddlIssuedToPaperChecker.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select " + lblIssuerType.Text);
                ddlIssuedToPaperChecker.Focus();
                return;
            }
        }



        if (ddlReceiverType.SelectedIndex == 2)
        {
            if (ddlRcvdFromPaperChecker.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select " + lblReceiverType.Text);
                ddlRcvdFromPaperChecker.Focus();
                return;
            }
        }
        if (tblIssueDate.Visible == true)
        {
            if (txtIssueDate.Value == "")
            {
                Show_Error_Success_Box("E", "Select Issue Date");
                //txtIssueDate.Focus();
                return;
            }
        }

        if (ddlPCSlab.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Paper Checker Slab");
            ddlPCSlab.Focus();
            return;
        }

        if (trExpectedReturnDate.Visible == true)
        {
            if (txtexpectdreturndate_Add.Value == "")
            {
                Show_Error_Success_Box("E", "Select Expected Return Date");
                //txtIssueDate.txtexpectdreturndate_Add();
                return;
            }
        }

        if (tblIssueDate.Visible == true && trExpectedReturnDate.Visible == true)
        {
            if (Convert.ToDateTime(txtexpectdreturndate_Add.Value) < Convert.ToDateTime(txtIssueDate.Value))
            {
                Show_Error_Success_Box("E", "Expected Return Date Cannot Be Less Then Issue Date");
                //txtIssueDate.txtexpectdreturndate_Add();
                return;
            }
        }
        string partnercode = "";



        string TestPKey = null;
        //TestPKey = ddlDivision_Add.SelectedValue + "%" + ddlAcadYear_Add.SelectedItem.Text + "%" + ddlStandard_Add.SelectedValue + "%" + ddlCentre_Add.SelectedValue + "%" + ddlBatch_Add.SelectedValue + "%" + Strings.Right(ddlTestName_Add.SelectedValue, Strings.Len(ddlTestName_Add.SelectedValue) - 16) + "%" + ddlConductNo_Add.SelectedItem.Text;
        if (ddlTestName_Add.SelectedValue != "select")
        {


            string[] words = ddlTestName_Add.SelectedValue.ToString().Split('%');
            TestPKey = ddlDivision_Add.SelectedValue + "%" + ddlAcadYear_Add.SelectedItem.Text + "%" + ddlStandard_Add.SelectedValue + "%" + ddlCentre_Add.SelectedValue + "%" + ddlBatch_Add.SelectedValue + "%" + words[3] + "%" + ddlConductNo_Add.SelectedItem.Text;
        }



        int StudCnt = 0;
        string SBEntryCode = "";
        string NotSel_SBEntryCode = "";

        foreach (DataListItem dtlItem in dlStudentList.Items)
        {
            CheckBox chkStudent = (CheckBox)dtlItem.FindControl("chkStudent");
            Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
            if (chkStudent.Checked == true && chkStudent.Visible==true)
            {
                StudCnt = StudCnt + 1;
                SBEntryCode = SBEntryCode + lblSBEntryCode.Text + ",";
            }
            else
            {
                NotSel_SBEntryCode = NotSel_SBEntryCode + lblSBEntryCode.Text + ",";
            }
        }

        //if (StudCnt == 0)
        //{
        //    Show_Error_Success_Box("E", "0030");
        //    dlStudentList.Focus();
        //    return;
        //}


        SBEntryCode = Common.RemoveComma(SBEntryCode);

        NotSel_SBEntryCode = Common.RemoveComma(NotSel_SBEntryCode);
        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        string Issue_Date = null;
        Issue_Date = txtIssueDate.Value;

        string Expected_Return_Date = null;
        Expected_Return_Date = txtexpectdreturndate_Add.Value;


        int ResultId = 0;

        if (ddlReceiverType.SelectedIndex == 2)
        {
            partnercode = ddlRcvdFromPaperChecker.SelectedValue;
        }
        else if (ddlIssuerType.SelectedIndex == 2)
        {
            partnercode = ddlIssuedToPaperChecker.SelectedValue;
        }


        string PCSlabId = "";
        if (ddlPCSlab.SelectedIndex != 0)
        {
            PCSlabId = ddlPCSlab.SelectedValue;
        }

        //ResultId = ProductController.InsertAnswerSheet_Issue(TestPKey, partnercode, Issue_Date, StudCnt, SBEntryCode, CreatedBy);
        ResultId = ProductController.InsertAnswerSheet_Issue(TestPKey, partnercode, Issue_Date, StudCnt, SBEntryCode, PCSlabId, CreatedBy,Expected_Return_Date);

        //Close the Add Panel and go to Search Grid
        if (ResultId == 1)
        {
            Fill_Grid();
            Show_Error_Success_Box("S", "0000");
            if ((Request.QueryString["Div"] != null) && (Request.QueryString["AcadYear"] != null) && (Request.QueryString["Center"] != null) && (Request.QueryString["Course"] != null) && (Request.QueryString["BatchCode"] != null) && (Request.QueryString["TestCategoryId"] != null) && (Request.QueryString["TestTypeId"] != null) && (Request.QueryString["TestId"] != null))
            {
            }
            else
            {
                Clear_AddPanel();
            }
        }
        else if (ResultId == -1)
        {
            Show_Error_Success_Box("E", "Paper Checker already assigned");
            return;
        }
    }

    private void Clear_AddPanel()
    {
        ddlDivision_Add.SelectedIndex = 0;
        ddlAcadYear_Add.SelectedIndex = 0;

        ddlStandard_Add.Items.Clear();
        ddlCentre_Add.Items.Clear();
        ddlBatch_Add.Items.Clear();
        ddlTestName_Add.Items.Clear();

        Clear_TestDetails();
    }

    protected void ddlConductNo_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillStudentList_ForIssue();
    }

    protected void ddlCentre_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Batch();
        Clear_Error_Success_Box();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
        FillDDL_Centre();
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

        ddlBatch.Items.Clear();
    }

    private void FillDDL_Centre()
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


    private void PrintPaperCheckerDetails()
    {




    }

    protected void ddlAcadYear_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
        Clear_Error_Success_Box();
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

        Clear_TestDetails();
    }

    protected void BtnCloseEdit_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Result");
    }

    protected void BtnSaveEdit_Click(object sender, System.EventArgs e)
    {
        //Validate if all information is entered correctly
        try
        {

            if (ddlEdit_IssuedTo.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Paper Checker");
                ddlEdit_IssuedTo.Focus();
                return;
            }
            if (txtEdit_IssueDate.Value == "")
            {
                Show_Error_Success_Box("E", "Enter Paper Issue Date");
                txtEdit_IssueDate.Focus();
                return;
            }

            if (ddlEditPCSlab.SelectedIndex == 0)
            {
                Show_Error_Success_Box("E", "Select Paper Checker Slab");
                ddlEditPCSlab.Focus();
                return;
            }

            if (txtexpectedreturndate_edit.Value == "")
            {
                Show_Error_Success_Box("E", "Select Expected Return Date");
                //txtexpectedreturndate_edit.focus();
                return;

            }


            if (Convert.ToDateTime(txtexpectedreturndate_edit.Value) < Convert.ToDateTime(txtEdit_IssueDate.Value))
            {
                Show_Error_Success_Box("E", "Expected Return Date Cannot Be Less Then Issue Date");
                //txtIssueDate.txtexpectdreturndate_Add();
                return;

            }
            string BagPKey = null;
            BagPKey = lblPKey_Edit.Text;

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;

            string Issue_Date = null;
            Issue_Date = txtEdit_IssueDate.Value;


            string Expected_Return_Date = null;
            Expected_Return_Date = txtexpectedreturndate_edit.Value;
            string PCSlabId = "";
            if (ddlEditPCSlab.SelectedIndex != 0)
            {
                PCSlabId = ddlEditPCSlab.SelectedValue;
            }

            int ResultId = ProductController.UpdateAnswerSheet_Issue_Edit(BagPKey, Convert.ToDateTime(Issue_Date), ddlEdit_IssuedTo.SelectedValue, PCSlabId, CreatedBy, Convert.ToDateTime(Expected_Return_Date));
            if (ResultId == 0)
            {
                Show_Error_Success_Box("E", "Record not updated");
                return;
            }
            else if (ResultId == 1)
            {
                Fill_Grid();
                Show_Error_Success_Box("S", "Record updated successfully");
                return;
            }
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
            return;
        }

    }

    public Tran_TestAnswerPapers()
    {
        Load += Page_Load;
    }

    protected void btnReturnSave_Click(object sender, EventArgs e)
    {
        if (txtReturnDate.Value == "")
        {
            Show_Error_Success_Box("E", "Select Paper Return Date");
            txtReturnDate.Focus();
            return;
        }

        string TestPKey = null;
        TestPKey = lblPKey_Edit.Text;

        int LastPercentPos = 0;
        LastPercentPos = TestPKey.LastIndexOf("%");
        TestPKey = TestPKey.Substring(0, LastPercentPos);
        int StudCnt = 0;

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        string ReturnDate = null;
        ReturnDate = txtReturnDate.Value;

        string MaxMarks = txtReturnMaximumMarks.Text;
        foreach (DataListItem dtlItem in ddlReturn.Items)
        {
            Label lblSBEntryCode = (Label)dtlItem.FindControl("lblSBEntryCode");
            TextBox txtDLMarks = (TextBox)dtlItem.FindControl("txtDLMarks");


            if (!string.IsNullOrEmpty(txtDLMarks.Text) && txtDLMarks.Enabled == true)
            {
                if (Convert.ToDouble(txtDLMarks.Text) <= Convert.ToDouble(MaxMarks))
                {
                    int ResultId = 0;
                    StudCnt++;
                    ResultId = ProductController.Insert_StudentTestMarks(TestPKey, lblSBEntryCode.Text, txtDLMarks.Text, MaxMarks, CreatedBy);
                }

            }
        }
        if (StudCnt == 0)
        {
            Show_Error_Success_Box("E", "Enter Marks");
            ddlReturn.Focus();
            return;
        }


        int ResultUpdateId = 0;
        ResultUpdateId = ProductController.UpdateAnswerSheet_Issue(lblPKey_Edit.Text, Convert.ToDateTime(ReturnDate), StudCnt, CreatedBy);

        if (ResultUpdateId == 0)
        {
            Show_Error_Success_Box("E", "Record not saved");
            return;
        }
        else if (ResultUpdateId == 1)
        {
            Fill_Grid();
            Show_Error_Success_Box("S", "Record saved successfully");
            return;
        }

    }

    protected void btnReturnClose_Click(object sender, EventArgs e)
    {
        ControlVisibility("Result");
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {

        ddlDivision.SelectedIndex = 0;
        ddlAcadYear.SelectedIndex = 0;
        ddlCentre.Items.Clear();
        ddlBatch.Items.Clear();
        ddlStandard.Items.Clear();
        id_date_range_picker_1.Value = "";
        txtTestName.Text = "";
    }

    private void Fill_Grid()
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

        string DateRange = null;
        DateRange = id_date_range_picker_1.Value;

        //if (string.IsNullOrEmpty(DateRange))
        //{
        //    Show_Error_Success_Box("E", "0070");
        //    id_date_range_picker_1.Focus();
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



        string FromDate = "";
        string ToDate = "";
        //FromDate = Strings.Left(DateRange, 10);
        if (DateRange != "")
        {
            FromDate = DateRange.Substring(0, 10);
        }
        //if (string.IsNullOrEmpty(FromDate))
        //    FromDate = System.DateTime.Now.ToString("dd MMM yyyy");

        //ToDate = Strings.Right(DateRange, 10);
        if (DateRange != "")
        {
            ToDate = DateRange.Substring(DateRange.Length - 10);
        }
        //if (string.IsNullOrEmpty(ToDate))
        //    ToDate = System.DateTime.Now.ToString("dd MMM yyyy");

        string Centre_Code = null;
        Centre_Code = ddlCentre.SelectedItem.Value;

        DataSet dsGrid = ProductController.GetAnswerSheet_IssueBy_Division_Year_Standard_Centre(DivisionCode, YearName, StandardCode, BatchCode, "01", TestName, FromDate, ToDate, Centre_Code, 1);


        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {
                if (dsGrid.Tables[0].Rows.Count != 0)
                {
                    lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
                    dlGridDisplay.DataSource = dsGrid;
                    dlGridDisplay.DataBind();
                }
                else
                {
                    lbltotalcount.Text = "0";
                    dlGridDisplay.DataSource = dsGrid;
                    dlGridDisplay.DataBind();
                    dlGridExport.DataSource = dsGrid;
                    dlGridExport.DataBind();
                }
            }
            else
            {
                lbltotalcount.Text = "0";
                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();
                dlGridExport.DataSource = dsGrid;
                dlGridExport.DataBind();
            }
        }
        else
        {
            lbltotalcount.Text = "0";
            dlGridDisplay.DataSource = dsGrid;
            dlGridDisplay.DataBind();
            dlGridExport.DataSource = dsGrid;
            dlGridExport.DataBind();
        }




    }

    protected void HLExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;
        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=Test_AnswerPapers.xls");


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

    protected void chkStudentAllAdd_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlStudentList.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkStudent");

            chkitemck.Checked = s.Checked;
        }
    }

    protected void chkStudentAllEdit_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in DLEdit_StudentList.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkStudent");

            chkitemck.Checked = s.Checked;
        }
    }

    private void FillDDL_Slab(string Div_Code)
    {
        ddlPCSlab.Items.Clear();
        ddlEditPCSlab.Items.Clear();

        DataSet dsSlab = ProductController.Get_SlabBy_Division(Div_Code);
        if (dsSlab != null)
        {
            if (dsSlab.Tables.Count != 0)
            {
                if (dsSlab.Tables[0].Rows.Count != 0)
                {
                    BindDDL(ddlPCSlab, dsSlab, "Slab_Name", "Slab_Code");
                    BindDDL(ddlEditPCSlab, dsSlab, "Slab_Name", "Slab_Code");
                }
            }
        }

        ddlPCSlab.Items.Insert(0, "Select");
        ddlPCSlab.SelectedIndex = 0;

        ddlEditPCSlab.Items.Insert(0, "Select");
        ddlEditPCSlab.SelectedIndex = 0;
    }

}
