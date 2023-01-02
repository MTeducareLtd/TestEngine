using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;

using System.Web.UI;
using System.Drawing;


partial class Master_QPSet : System.Web.UI.Page
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
            TestType_ID = Common.RemoveComma(TestType_ID);
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
            TestType_ID = Common.RemoveComma(TestType_ID);
            //if (Strings.Right(TestType_ID, 1) == ",")
            //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);
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



        DataSet dsGrid = ProductController.GetTestMasterBy_Division_Year_Standard(DivisionCode, YearName, StandardCode, "01", ddlTestCategory.SelectedValue, TestType_ID, TestName, 2);
        dlGridDisplay.DataSource = dsGrid;
        dlGridDisplay.DataBind();

        dlGridExport.DataSource = dsGrid;
        dlGridExport.DataBind();

        lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
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

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            QuestionGrid.Visible = false;

            FillDDL_TestCategories();
            FillDDL_TestTypes();
            FillDDL_Division();
            FillDDL_AcadYear();
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
        BindDDL(ddlAcadyear, dsAcadYear, "Description", "Id");
        ddlAcadyear.Items.Insert(0, "Select");
        ddlAcadyear.SelectedIndex = 0;

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
            //BtnAdd.Visible = True
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = false;
            //BtnAdd.Visible = True
        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            //BtnAdd.Visible = False
        }
        Clear_Error_Success_Box();
    }

    //Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
    //    ControlVisibility("Add")
    //End Sub

    protected void BtnCloseAdd_Click(object sender, System.EventArgs e)
    {
        Clear_AddPanel();
        ControlVisibility("Result");
    }



    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        if (e.CommandName == "Manage")
        {
            Clear_AddPanel();
            ControlVisibility("Add");
            lblPKey_Edit.Text = e.CommandArgument.ToString();
            FillTestMasterDetails(lblPKey_Edit.Text, e.CommandName);
            tdonline.Visible = false;
            tblUploadFile.Visible = false;
            txtassesmenttestcode.Text = "";
            txtassesmenttestcode.Enabled = true;
        }
    }

    private void FillTestMasterDetails(string PKey, string CommandName)
    {

        DataSet dsTest = ProductController.GetTestMasterBY_PKey(PKey, 1);

        lbltestneagtivemarkingflag.Text = "U";
        if (dsTest.Tables[0].Rows.Count > 0)
        {
            lblDivision_Add.Text = ddlDivision.SelectedItem.ToString();
            lblAcadYear_Add.Text = ddlAcadyear.SelectedItem.ToString();
            lblStandard_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Standard_Name"]);
            lblTestCategory_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["TestCategory_Name"]);
            lblTestType_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["TestType_Name"]);
            lblTestName_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Test_Name"]);
            lblDivCode.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["division_code"]);
            lblStandardCode.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["standard_code"]);

            lblSubject_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Subjects"]);

            lbltestneagtivemarkingflag.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["negativemarkingflag"]);
            FillQPSetNo(Convert.ToInt32(dsTest.Tables[0].Rows[0]["QPSetCnt"]));

            ddlSubject_Hidden.DataSource = dsTest.Tables[1];
            ddlSubject_Hidden.DataTextField = "Subject_Name";
            ddlSubject_Hidden.DataValueField = "Subject_Code";
            ddlSubject_Hidden.DataBind();

            if (lblTestCategory_Add.Text.Trim() == "Objective")
            {
                Label2.Visible = true;
                FFLExcel.Visible = true;
                BtnUploadExcel.Visible = true;
                BtnDownloadExcel.Visible = true;
                SMSHelpFlag.Visible = true;
            }
            else if (lblTestCategory_Add.Text.Trim() == "Subjective")
            {
                Label2.Visible = false;
                FFLExcel.Visible = false;
                BtnUploadExcel.Visible = false;
                BtnDownloadExcel.Visible = false;
                SMSHelpFlag.Visible = false;
            }
        }
    }


    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    private void FillQPSetNo(int QPSetCnt)
    {
        ddlQPSetNo.Items.Clear();
        ddlQPSetNo.Items.Insert(0, "Select");
        for (int cnt = 1; cnt <= QPSetCnt; cnt++)
        {

            ddlQPSetNo.Items.Add(cnt.ToString());
        }
        if (QPSetCnt > 0)
        {
            ddlQPSetNo.SelectedIndex = 0;
            ddlQPSetNo.Enabled = true;
        }
        else
        {
            //ddlQPSetNo.Enabled = false;

        }

    }

    protected void btnShowQPSetDetails_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
        if (ddlQPSetNo.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select QP Set No");
            return;
        }
        tblUploadFile.Visible = true;
        FillQPSetDetails();
        QuestionGrid.Visible = true;
    }

    private void FillQPSetDetails()
    {
        string TestPKey = null;
        string SetNo = null;

        TestPKey = lblPKey_Edit.Text;
        SetNo = ddlQPSetNo.SelectedValue;

        DataSet dsGrid = ProductController.GetTestQPSet_ByPKey(TestPKey, SetNo, 1);
        dlQuestion.DataSource = dsGrid;
        dlQuestion.DataBind();
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

    protected void ddlAcadYear_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
    }


    protected void dlQuestion_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        TextBox txtDLAnswerKey = (TextBox)e.Item.FindControl("txtDLAnswerKey");
        DropDownList ddlDLDiffLevel = (DropDownList)e.Item.FindControl("ddlDLDiffLevel");
        TextBox txtDLCorrectMarks = (TextBox)e.Item.FindControl("txtDLCorrectMarks");
        TextBox txtDLWrongMarks = (TextBox)e.Item.FindControl("txtDLWrongMarks");
        DropDownList ddlDLSubject = (DropDownList)e.Item.FindControl("ddlDLSubject");
        DropDownList ddlDLRefCouse = (DropDownList)e.Item.FindControl("ddlDLRefCouse");
        DropDownList ddlDLRefSubject = (DropDownList)e.Item.FindControl("ddlDLRefSubject");
        DropDownList ddlDLChapter = (DropDownList)e.Item.FindControl("ddlDLChapter");
        DropDownList ddlDLTopic = (DropDownList)e.Item.FindControl("ddlDLTopic");
        DropDownList ddlDLSubTopic = (DropDownList)e.Item.FindControl("ddlDLSubTopic");
        DropDownList ddlDLModul = (DropDownList)e.Item.FindControl("ddlDLModul");
        DropDownList ddlDLQueType = (DropDownList)e.Item.FindControl("ddlDLQueType");
        DropDownList ddlquestionrule = (DropDownList)e.Item.FindControl("ddlquestionrule");


        Label lblDLAnswerKey_Name = (Label)e.Item.FindControl("lblDLAnswerKey_Name");
        Label lblDLDiffLevel_Name = (Label)e.Item.FindControl("lblDLDiffLevel_Name");
        Label lblDLDiffLevel = (Label)e.Item.FindControl("lblDLDiffLevel");
        Label lblDLCorrectMarks_Name = (Label)e.Item.FindControl("lblDLCorrectMarks_Name");
        Label lblDLWrongMarks = (Label)e.Item.FindControl("lblDLWrongMarks");
        Label lblDLSubject_Name = (Label)e.Item.FindControl("lblDLSubject_Name");
        Label lblDLSubject_Code = (Label)e.Item.FindControl("lblDLSubject_Code");
        Label lblDLRefCourse_Name = (Label)e.Item.FindControl("lblDLRefCourse_Name");
        Label lblDLRefCourse_Code = (Label)e.Item.FindControl("lblDLRefCourse_Code");
        Label lblDLRefSubject_Name = (Label)e.Item.FindControl("lblDLRefSubject_Name");
        Label lblDLRefSubject_Code = (Label)e.Item.FindControl("lblDLRefSubject_Code");
        Label lblDLChapter_Name = (Label)e.Item.FindControl("lblDLChapter_Name");
        Label lblDLChapter_Code = (Label)e.Item.FindControl("lblDLChapter_Code");
        Label lblDLTopic_Name = (Label)e.Item.FindControl("lblDLTopic_Name");
        Label lblDLTopic_Code = (Label)e.Item.FindControl("lblDLTopic_Code");
        Label lblDLSubTopic_Name = (Label)e.Item.FindControl("lblDLSubTopic_Name");
        Label lblDLSubTopic_Code = (Label)e.Item.FindControl("lblDLSubTopic_Code");
        Label lblDLModule_Name = (Label)e.Item.FindControl("lblDLModule_Name");
        Label lblDLModule_Code = (Label)e.Item.FindControl("lblDLModule_Code");
        Label lblquetypeid = (Label)e.Item.FindControl("lblquetypeid");
        Label lblquetypename = (Label)e.Item.FindControl("lblquetypename");

        Label lblruleid = (Label)e.Item.FindControl("lblruleid");
        Label lblrulename = (Label)e.Item.FindControl("lblrulename");

        LinkButton lnkDLEdit = (LinkButton)e.Item.FindControl("lnkDLEdit");
        LinkButton lnkDLSave = (LinkButton)e.Item.FindControl("lnkDLSave");

        if (e.CommandName == "Edit")
        {
            lblDLAnswerKey_Name.Visible = false;
            lblDLDiffLevel_Name.Visible = false;
            lblDLCorrectMarks_Name.Visible = false;
            lblDLWrongMarks.Visible = false;
            lblDLSubject_Name.Visible = false;
            lblDLRefSubject_Name.Visible = false;
            lblDLRefCourse_Name.Visible = false;
            lblDLChapter_Name.Visible = false;
            lblDLTopic_Name.Visible = false;
            lblDLSubTopic_Name.Visible = false;
            lblDLModule_Name.Visible = false;
            lnkDLEdit.Visible = false;
            lnkDLSave.Visible = true;
            lblquetypename.Visible = false;
            lblrulename.Visible = false;

            txtDLAnswerKey.Visible = true;
            ddlDLDiffLevel.Visible = true;
            txtDLCorrectMarks.Visible = true;
            txtDLWrongMarks.Visible = true;
            ddlDLSubject.Visible = true;
            ddlDLRefCouse.Visible = true;
            ddlDLRefSubject.Visible = true;
            ddlDLChapter.Visible = true;
            ddlDLTopic.Visible = true;
            ddlDLSubTopic.Visible = true;
            ddlDLModul.Visible = true;
            ddlDLQueType.Visible = true;
            ddlquestionrule.Visible = true;

            if (lbltestneagtivemarkingflag.Text != "1")
            {
                txtDLWrongMarks.Text = "0";
                txtDLWrongMarks.Enabled = false;
                lblDLWrongMarks.Text = "0";
            }

            DataSet dsDiffLevel = ProductController.GetAllActiveDiffLevel();
            BindDDL(ddlDLDiffLevel, dsDiffLevel, "DiffLevel_Name", "DiffLevel_Id");

            string DiffLevel = null;
            DiffLevel = lblDLDiffLevel.Text;
            ddlDLDiffLevel.SelectedValue = DiffLevel;

            string TestSubPKey = null;
            TestSubPKey = lblPKey_Edit.Text;

            DataSet dsSubject = ProductController.GetTestMasterBY_PKey(TestSubPKey, 4);
            BindDDL(ddlDLSubject, dsSubject, "Subject_Name", "Subject_Code");
            ddlDLSubject.SelectedValue = lblDLSubject_Code.Text;

            string Div_Code = null;
            Div_Code = ddlDivision.SelectedValue;

            string YearName = null;
            YearName = ddlAcadyear.SelectedItem.ToString();

            DataSet dsStandard = ProductController.GetAllActive_Standard_ForYear(Div_Code, YearName);
            BindDDL(ddlDLRefCouse, dsStandard, "Standard_Name", "Standard_Code");
            ddlDLRefCouse.Items.Insert(0, "[Select]");
            ddlDLRefCouse.SelectedIndex = 0;

            DataSet dsquetype = ProductController.GetQuestionTypeIdByName("", 2);
            BindDDL(ddlDLQueType, dsquetype, "Question_Type_Name", "Question_Type_Id");


            DataSet dsquestionrule = ProductController.GetQuestionRuleName("", 2);
            BindDDL(ddlquestionrule, dsquestionrule, "Rule_Name", "Rule_Id");

            string QuestionType = null;
            QuestionType = lblquetypeid.Text;
            ddlDLQueType.SelectedValue = QuestionType;

            string QuestionRule = null;
            QuestionRule = lblruleid.Text;
            ddlquestionrule.SelectedValue = QuestionRule;

            //Check if single subject is selected or not
            if (string.IsNullOrEmpty(lblDLSubject_Code.Text) & ddlDLSubject.Items.Count == 2)
            {
                ddlDLSubject.SelectedIndex = 1;
            }

            try
            {
                ddlDLRefCouse.SelectedValue = lblDLRefCourse_Code.Text;
            }
            catch
            {
                ddlDLRefCouse.SelectedIndex = 0;
            }

            if (ddlDLRefCouse.SelectedIndex == 0)
            {
                try
                {
                    string[] parts = TestSubPKey.Split('%');
                    string StandardCode = parts[2];
                    ddlDLRefCouse.SelectedValue = StandardCode;
                }
                catch
                {
                    ddlDLRefCouse.SelectedIndex = 0;
                }
            }

            ddlDLRefCouse_SelectedIndexChanged(ddlDLRefCouse, e);

            txtDLAnswerKey.Focus();
        }
        else if (e.CommandName == "Save")
        {
            lblDLAnswerKey_Name.Visible = true;
            lblDLDiffLevel_Name.Visible = true;
            lblDLCorrectMarks_Name.Visible = true;
            lblDLWrongMarks.Visible = true;
            lblDLSubject_Name.Visible = true;
            lblDLRefCourse_Name.Visible = true;
            lblDLRefSubject_Name.Visible = true;
            lblDLChapter_Name.Visible = true;
            lblDLTopic_Name.Visible = true;
            lblDLSubTopic_Name.Visible = true;
            lblDLModule_Name.Visible = true;
            lnkDLEdit.Visible = true;
            lnkDLSave.Visible = false;
            lblquetypename.Visible = true;
            lblrulename.Visible = true;

            txtDLAnswerKey.Visible = false;
            ddlDLDiffLevel.Visible = false;
            txtDLCorrectMarks.Visible = false;
            txtDLWrongMarks.Visible = false;
            ddlDLSubject.Visible = false;
            ddlDLRefCouse.Visible = false;
            ddlDLRefSubject.Visible = false;
            ddlDLChapter.Visible = false;
            ddlDLTopic.Visible = false;
            ddlDLSubTopic.Visible = false;
            ddlDLModul.Visible = false;
            ddlDLQueType.Visible = false;
            ddlquestionrule.Visible = false;

            try
            {
                lblDLAnswerKey_Name.Text = txtDLAnswerKey.Text;
                lblDLDiffLevel_Name.Text = ddlDLDiffLevel.SelectedItem.Text;
                lblDLDiffLevel.Text = ddlDLDiffLevel.SelectedItem.Value;
                lblDLCorrectMarks_Name.Text = txtDLCorrectMarks.Text;
                lblDLWrongMarks.Text = txtDLWrongMarks.Text;

                lblDLSubject_Name.Text = ddlDLSubject.SelectedItem.Text;
                lblDLSubject_Code.Text = ddlDLSubject.SelectedItem.Value;

                lblDLRefCourse_Name.Text = ddlDLRefCouse.SelectedItem.Text;
                lblDLRefCourse_Code.Text = ddlDLRefCouse.SelectedItem.Value;

                lblDLRefSubject_Name.Text = ddlDLRefSubject.SelectedItem.Text;
                lblDLRefSubject_Code.Text = ddlDLRefSubject.SelectedItem.Value;

                lblDLChapter_Name.Text = ddlDLChapter.SelectedItem.Text;
                lblDLChapter_Code.Text = ddlDLChapter.SelectedItem.Value;

                lblDLTopic_Name.Text = ddlDLTopic.SelectedItem.Text;
                lblDLTopic_Code.Text = ddlDLTopic.SelectedItem.Value;

                lblDLSubTopic_Name.Text = ddlDLSubTopic.SelectedItem.Text;
                lblDLSubTopic_Code.Text = ddlDLSubTopic.SelectedItem.Value;

                lblDLModule_Name.Text = ddlDLModul.SelectedItem.Text;
                lblDLModule_Code.Text = ddlDLModul.SelectedItem.Value;

                lblquetypename.Text = ddlDLQueType.SelectedItem.Text;
                lblquetypeid.Text = ddlDLQueType.SelectedItem.Value;

                lblrulename.Text = ddlquestionrule.SelectedItem.Text;
                lblruleid.Text = ddlquestionrule.SelectedItem.Value;

            }
            catch (Exception ex)
            {
            }


            lnkDLEdit.Focus();
        }


    }

    protected void dlQuestion_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        //If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
        //    Dim ddlDiffLevel = TryCast(e.Item.FindControl("ddlDLDiffLevel"), DropDownList)
        //    Dim lblDLDiffLevel = TryCast(e.Item.FindControl("lblDLDiffLevel"), Label)

        //    Dim dsDiffLevel As DataSet = ProductController.GetAllActiveDiffLevel()
        //    BindDDL(ddlDiffLevel, dsDiffLevel, "DiffLevel_Name", "DiffLevel_Id")

        //    Dim DiffLevel As String
        //    DiffLevel = lblDLDiffLevel.Text
        //    ddlDiffLevel.SelectedValue = DiffLevel

        //    Dim ddlSubject = TryCast(e.Item.FindControl("ddlDLSubject"), DropDownList)
        //    Dim lblDLSubject_Code = TryCast(e.Item.FindControl("lblDLSubject_Code"), Label)

        //    Dim TestSubPKey As String
        //    TestSubPKey = lblPKey_Edit.Text
        //    Try
        //        Dim dsSubject As DataSet = ProductController.GetTestMasterBY_PKey(TestSubPKey, 4)
        //        BindDDL(ddlSubject, dsSubject, "Subject_Name", "Subject_Code")
        //        ddlSubject.SelectedValue = lblDLSubject_Code.Text

        //        'Check if single subject is selected or not
        //        If lblDLSubject_Code.Text = "" And ddlSubject.Items.Count = 2 Then
        //            ddlSubject.SelectedIndex = 1
        //        End If
        //    Catch ex As Exception
        //        If ddlSubject.Items.Count > 0 Then ddlSubject.SelectedIndex = 0
        //    End Try


        //    ddlSubject_SelectedIndexChanged(ddlSubject, e)


        //End If

    }


    protected void ddlDLRefCouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlList = (DropDownList)sender;
        DataListItem row = (DataListItem)ddlList.NamingContainer;

        DropDownList ddlDLRefSubject = (DropDownList)row.FindControl("ddlDLRefSubject");
        DropDownList ddlDLRefCouse = (DropDownList)row.FindControl("ddlDLRefCouse");
        Label lblDLRefSubject_Code = (Label)row.FindControl("lblDLRefSubject_Code");
        //Bind chapter combo based on subject
        string TestSubPKey = null;

        if (!string.IsNullOrEmpty(ddlDLRefCouse.SelectedValue.ToString()))
        {
            TestSubPKey = lblPKey_Edit.Text + "%" + ddlDLRefCouse.SelectedValue.ToString();
            string StandardCode = ddlDLRefCouse.SelectedValue;
            DataSet dsSubject = ProductController.GetAllSubjectsByStandard(StandardCode);

            try
            {
                ddlDLRefSubject.DataSource = dsSubject;
                ddlDLRefSubject.DataTextField = "Subject_Name";
                ddlDLRefSubject.DataValueField = "Subject_Code";
                ddlDLRefSubject.DataBind();
                ddlDLRefSubject.Items.Insert(0, "[Select]");
                ddlDLRefSubject.SelectedIndex = 0;

                if (ddlDLRefSubject.Items.Count > 0)
                    ddlDLRefSubject.SelectedValue = lblDLRefSubject_Code.Text;
            }
            catch
            {
                if (ddlDLRefSubject.Items.Count > 0)
                    ddlDLRefSubject.SelectedIndex = 0;
            }

            ddlRefSubject_SelectedIndexChanged(ddlDLRefSubject, e);

            //if (string.IsNullOrEmpty(lblDLChapter.Text) && ddlChapter.Items.Count == 2)
            //{
            //    ddlChapter.SelectedIndex = 1;
            //}

            //ddlChapter_SelectedIndexChanged(ddlChapter, e);
        }

    }

    protected void ddlRefSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlList = (DropDownList)sender;
        DataListItem row = (DataListItem)ddlList.NamingContainer;

        DropDownList ddlChapter = (DropDownList)row.FindControl("ddlDLChapter");
        DropDownList ddlDLRefCouse = (DropDownList)row.FindControl("ddlDLRefCouse");
        DropDownList ddlDLRefSubject = (DropDownList)row.FindControl("ddlDLRefSubject");
        Label lblDLChapter = (Label)row.FindControl("lblDLChapter_Code");
        //Bind chapter combo based on subject
        string TestSubPKey = "", Div_Code = "", YearName = "", StandardCode = "";
        if (!string.IsNullOrEmpty(ddlDLRefSubject.SelectedValue.ToString()))
        {
            TestSubPKey = lblPKey_Edit.Text;
            string[] parts = TestSubPKey.Split('%');
            Div_Code = parts[0];
            YearName = parts[1];
            StandardCode = ddlDLRefCouse.SelectedValue;
            //DataSet dsChapter = ProductController.GetTestMasterBY_PKey(TestSubPKey, 2);
            DataSet dsChapter = ProductController.GetAllChaptersBy_Division_Year_Standard_Subject(Div_Code, YearName, StandardCode, ddlDLRefSubject.SelectedValue.ToString());

            try
            {
                ddlChapter.DataSource = dsChapter;
                ddlChapter.DataTextField = "Chapter_Name";
                ddlChapter.DataValueField = "Chapter_Code";
                ddlChapter.DataBind();
                ddlChapter.Items.Insert(0, "[Select]");
                ddlChapter.SelectedIndex = 0;

                if (ddlChapter.Items.Count > 0)
                    ddlChapter.SelectedValue = lblDLChapter.Text;
            }
            catch
            {
                if (ddlChapter.Items.Count > 0)
                    ddlChapter.SelectedIndex = 0;
            }


            if (string.IsNullOrEmpty(lblDLChapter.Text) && ddlChapter.Items.Count == 2)
            {
                ddlChapter.SelectedIndex = 1;
            }

            ddlChapter_SelectedIndexChanged(ddlChapter, e);
        }


    }

    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlList = (DropDownList)sender;
        DataListItem row = (DataListItem)ddlList.NamingContainer;

        DropDownList ddlChapter = (DropDownList)row.FindControl("ddlDLChapter");
        DropDownList ddlSubject = (DropDownList)row.FindControl("ddlDLSubject");
        Label lblDLChapter = (Label)row.FindControl("lblDLChapter_Code");
        //Bind chapter combo based on subject
        string TestSubPKey = null;

        if (!string.IsNullOrEmpty(ddlSubject.SelectedValue.ToString()))
        {
            TestSubPKey = lblPKey_Edit.Text + "%" + ddlSubject.SelectedValue.ToString();
            DataSet dsChapter = ProductController.GetTestMasterBY_PKey(TestSubPKey, 2);

            try
            {
                ddlChapter.DataSource = dsChapter;
                ddlChapter.DataTextField = "Chapter_Name";
                ddlChapter.DataValueField = "Chapter_Code";
                ddlChapter.DataBind();


                if (ddlChapter.Items.Count > 0)
                    ddlChapter.SelectedValue = lblDLChapter.Text;
            }
            catch
            {
                if (ddlChapter.Items.Count > 0)
                    ddlChapter.SelectedIndex = 0;
            }


            if (string.IsNullOrEmpty(lblDLChapter.Text) && ddlChapter.Items.Count == 2)
            {
                ddlChapter.SelectedIndex = 1;
            }

            ddlChapter_SelectedIndexChanged(ddlChapter, e);
        }


    }

    protected void ddlChapter_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlList = (DropDownList)sender;
        DataListItem row = (DataListItem)ddlList.NamingContainer;

        DropDownList ddlChapter = (DropDownList)row.FindControl("ddlDLChapter");
        DropDownList ddlDLRefCouse = (DropDownList)row.FindControl("ddlDLRefCouse");
        DropDownList ddlDLRefSubject = (DropDownList)row.FindControl("ddlDLRefSubject");
        DropDownList ddlTopic = (DropDownList)row.FindControl("ddlDLTopic");
        Label lblDLTopic = (Label)row.FindControl("lblDLTopic_Code") as Label;

        //Bind chapter combo based on subject
        string TestSubChpPKey = null;
        string Div_Code = "", YearName = "", StandardCode = "";
        if (!string.IsNullOrEmpty(ddlDLRefSubject.SelectedValue))
        {
            // need to be test by jayant
            //TestSubChpPKey = Strings.Left(lblPKey_Edit.Text, Strings.Len(lblPKey_Edit.Text) - 7) + "%" + ddlSubject.SelectedValue.ToString + "%" + ddlChapter.SelectedValue.ToString;
            TestSubChpPKey = lblPKey_Edit.Text;
            string[] parts = TestSubChpPKey.Split('%');
            Div_Code = parts[0];
            YearName = parts[1];
            StandardCode = parts[2];
            TestSubChpPKey = Div_Code + "%" + YearName + "%" + ddlDLRefCouse.SelectedValue + "%" + ddlDLRefSubject.SelectedValue + "%" + ddlChapter.SelectedValue;
            // TestSubChpPKey = lblPKey_Edit.Text.Substring(0, Convert.ToInt32((lblPKey_Edit.Text.Length - 7))) + "%" + ddlSubject.SelectedValue.ToString() + "%" + ddlChapter.SelectedValue.ToString();
            DataSet dsTopic = ProductController.GetTestMasterBY_PKey(TestSubChpPKey, 3);
            if (dsTopic != null)
            {
                if (dsTopic.Tables.Count != 0)
                {

                    ddlTopic.DataSource = dsTopic;
                    ddlTopic.DataTextField = "Topic_Name";
                    ddlTopic.DataValueField = "Topic_Code";
                    ddlTopic.DataBind();

                    if (lblDLTopic.Text != "")
                    {
                        try
                        {
                            ddlTopic.SelectedValue = lblDLTopic.Text;
                        }
                        catch { }
                    }

                    ddlDLTopic_SelectedIndexChanged(sender, e);

                    //if (ddlTopic.Items.Count > 0)
                    //    ddlTopic.SelectedValue = lblDLTopic.Text;

                    //if (string.IsNullOrEmpty(lblDLTopic.Text) & ddlTopic.Items.Count == 2)
                    //{
                    //    ddlTopic.SelectedIndex = 1;
                    //}
                }
            }
        }


    }

    protected void ddlDLTopic_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlList = (DropDownList)sender;
        DataListItem row = (DataListItem)ddlList.NamingContainer;

        DropDownList ddlChapter = (DropDownList)row.FindControl("ddlDLChapter");
        DropDownList ddlDLRefCouse = (DropDownList)row.FindControl("ddlDLRefCouse");
        DropDownList ddlDLRefSubject = (DropDownList)row.FindControl("ddlDLRefSubject");
        DropDownList ddlTopic = (DropDownList)row.FindControl("ddlDLTopic");
        DropDownList ddlSubTopic = (DropDownList)row.FindControl("ddlDLSubTopic");
        Label lblSubTopic = (Label)row.FindControl("lblDLSubTopic_Code") as Label;

        //Bind chapter combo based on subject
        string TestSubChpPKey = null;
        string Div_Code = "", YearName = "", StandardCode = "";
        if (!string.IsNullOrEmpty(ddlDLRefSubject.SelectedValue))
        {
            // need to be test by jayant
            //TestSubChpPKey = Strings.Left(lblPKey_Edit.Text, Strings.Len(lblPKey_Edit.Text) - 7) + "%" + ddlSubject.SelectedValue.ToString + "%" + ddlChapter.SelectedValue.ToString;
            //T0%2016-2017%CO1676%SB16280%C161001305%T161000002034
            //TestSubChpPKey = lblPKey_Edit.Text.Substring(0, Convert.ToInt32((lblPKey_Edit.Text.Length - 7))) + "%" + ddlSubject.SelectedValue.ToString() + "%" + ddlChapter.SelectedValue.ToString() + "%" + ddlTopic.SelectedValue.ToString();
            TestSubChpPKey = lblPKey_Edit.Text;
            string[] parts = TestSubChpPKey.Split('%');
            Div_Code = parts[0];
            YearName = parts[1];
            StandardCode = parts[2];
            TestSubChpPKey = Div_Code + "%" + YearName + "%" + ddlDLRefCouse.SelectedValue + "%" + ddlDLRefSubject.SelectedValue + "%" + ddlChapter.SelectedValue.ToString() + "%" + ddlTopic.SelectedValue.ToString();

            DataSet dsSubTopic = ProductController.GetTestMasterBY_PKey(TestSubChpPKey, 7);
            if (dsSubTopic != null)
            {
                if (dsSubTopic.Tables.Count != 0)
                {

                    ddlSubTopic.DataSource = dsSubTopic;
                    ddlSubTopic.DataTextField = "SubTopic_Name";
                    ddlSubTopic.DataValueField = "SubTopic_Code";
                    ddlSubTopic.DataBind();

                    if (lblSubTopic.Text != "")
                    {
                        try
                        {
                            ddlSubTopic.SelectedValue = lblSubTopic.Text;

                        }
                        catch { }
                    }
                    ddlDLSubTopic_SelectedIndexChanged(sender, e);
                }
            }
        }


    }


    protected void ddlDLSubTopic_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlList = (DropDownList)sender;
        DataListItem row = (DataListItem)ddlList.NamingContainer;

        DropDownList ddlChapter = (DropDownList)row.FindControl("ddlDLChapter");
        DropDownList ddlDLRefCouse = (DropDownList)row.FindControl("ddlDLRefCouse");
        DropDownList ddlDLRefSubject = (DropDownList)row.FindControl("ddlDLRefSubject");
        DropDownList ddlTopic = (DropDownList)row.FindControl("ddlDLTopic");
        DropDownList ddlSubTopic = (DropDownList)row.FindControl("ddlDLSubTopic");
        DropDownList ddlModul = (DropDownList)row.FindControl("ddlDLModul");
        Label lblModule = (Label)row.FindControl("lblDLModule_Code") as Label;

        //Bind chapter combo based on subject
        string TestSubChpPKey = null;
        string Div_Code = "", YearName = "", StandardCode = "";

        if (!string.IsNullOrEmpty(ddlDLRefSubject.SelectedValue))
        {
            string TopicCode = "", SubTopicCode = "";
            TopicCode = ddlTopic.SelectedValue.ToString();
            SubTopicCode = ddlSubTopic.SelectedValue.ToString();
            //T0%2016-2017%CO1676%SB16280%C161001305%T161000002034%ST166079
            // TestSubChpPKey = lblPKey_Edit.Text.Substring(0, Convert.ToInt32((lblPKey_Edit.Text.Length - 7))) + "%" + ddlSubject.SelectedValue.ToString() + "%" + ddlChapter.SelectedValue.ToString() + "%" + TopicCode + "%" + SubTopicCode;
            TestSubChpPKey = lblPKey_Edit.Text;
            string[] parts = TestSubChpPKey.Split('%');
            Div_Code = parts[0];
            YearName = parts[1];
            StandardCode = parts[2];

            TestSubChpPKey = Div_Code + "%" + YearName + "%" + ddlDLRefCouse.SelectedValue + "%" + ddlDLRefSubject.SelectedValue + "%" + ddlChapter.SelectedValue.ToString() + "%" + TopicCode + "%" + SubTopicCode;

            DataSet dsModule = ProductController.GetTestMasterBY_PKey(TestSubChpPKey, 8);

            if (dsModule != null)
            {
                if (dsModule.Tables.Count != 0)
                {

                    ddlModul.DataSource = dsModule;
                    ddlModul.DataTextField = "Module_Name";
                    ddlModul.DataValueField = "Module_Code";
                    ddlModul.DataBind();

                    if (lblModule.Text != "")
                    {
                        try
                        {
                            ddlModul.SelectedValue = lblModule.Text;
                        }
                        catch { }
                    }
                }
            }
        }


    }

    protected void BtnSave_Click(object sender, System.EventArgs e)
    {
        //Validation


        //Start saving data in loop
        string PKey = null;
        PKey = lblPKey_Edit.Text;

        int ResultId = 0;

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        if (ddlQPSetNo.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select QP Set No");
            return;
        }

        int SetNumber = 0, cnttrue = 0;
        SetNumber = Convert.ToInt32(ddlQPSetNo.Text);

        ResultId = ProductController.Insert_Test_Set(PKey, SetNumber.ToString(), 0, "", "", "", "", 0, 0, "", CreatedBy, 1, txtassesmenttestcode.Text.Trim(), "", "", "", "", "", "");        //Save Header

        //Close the Add Panel and go to Search Grid

        if (ResultId == 1)
        {
            if (ResultId == 1 && lblTestCategory_Add.Text == "Objective")
            {
                //Save Item details in a loop
                foreach (DataListItem item in dlQuestion.Items)
                {
                    if (item.ItemType == ListItemType.Item | item.ItemType == ListItemType.AlternatingItem)
                    {
                        Label lblQueNo = (Label)item.FindControl("lblQueNo");
                        TextBox txtDLAnswerKey = (TextBox)item.FindControl("txtDLAnswerKey");
                        //Dim ddlDLDiffLevel = TryCast(item.FindControl("ddlDLDiffLevel"), DropDownList)
                        TextBox txtDLCorrectMarks = (TextBox)item.FindControl("txtDLCorrectMarks");
                        TextBox txtDLWrongMarks = (TextBox)item.FindControl("txtDLWrongMarks");
                        Label lblDLDiffLevel = (Label)item.FindControl("lblDLDiffLevel");
                        //Dim ddlDLSubject = TryCast(item.FindControl("ddlDLSubject"), DropDownList)
                        //Dim ddlDLChapter = TryCast(item.FindControl("ddlDLChapter"), DropDownList)
                        //Dim ddlDLTopic = TryCast(item.FindControl("ddlDLTopic"), DropDownList)

                        Label lblDLSubject_Code = (Label)item.FindControl("lblDLSubject_Code");
                        Label lblDLRefCourse_Code = (Label)item.FindControl("lblDLRefCourse_Code");
                        Label lblDLRefSubject_Code = (Label)item.FindControl("lblDLRefSubject_Code");
                        Label lblDLChapter_Code = (Label)item.FindControl("lblDLChapter_Code");
                        Label lblDLTopic_Code = (Label)item.FindControl("lblDLTopic_Code");
                        Label lblDLSubTopic_Code = (Label)item.FindControl("lblDLSubTopic_Code");
                        Label lblDLModule_Code = (Label)item.FindControl("lblDLModule_Code");
                        Label lblResult = (Label)item.FindControl("lblResult");
                        Label lblquetypeid = (Label)item.FindControl("lblquetypeid");
                        Label lblruleid = (Label)item.FindControl("lblruleid");

                        //Check if AnswerKey, Difficulty Level, Correct Marks, Wrong Marks and Subject is entered

                        if (!string.IsNullOrEmpty(txtDLAnswerKey.Text) & !string.IsNullOrEmpty(lblDLDiffLevel.Text) & !string.IsNullOrEmpty(txtDLCorrectMarks.Text) & !string.IsNullOrEmpty(txtDLWrongMarks.Text) & !string.IsNullOrEmpty(lblDLSubject_Code.Text))
                        {
                            ResultId = ProductController.Insert_Test_Set(PKey, Convert.ToString(SetNumber), Convert.ToInt32(lblQueNo.Text), lblDLSubject_Code.Text, lblDLChapter_Code.Text, lblDLTopic_Code.Text, txtDLAnswerKey.Text, float.Parse(txtDLCorrectMarks.Text), float.Parse(txtDLWrongMarks.Text), lblDLDiffLevel.Text,
                            CreatedBy, 2, txtassesmenttestcode.Text.Trim(), lblquetypeid.Text, lblDLSubTopic_Code.Text, lblDLModule_Code.Text, lblDLRefCourse_Code.Text, lblDLRefSubject_Code.Text, lblruleid.Text);

                            lblResult.ForeColor = System.Drawing.Color.Green;
                            lblResult.Text = "Success";
                        }
                        else
                        {
                            lblResult.ForeColor = System.Drawing.Color.Red;
                            lblResult.Text = "Error : Invalid Entry";
                            cnttrue = cnttrue + 1;
                        }
                    }
                }

                if (cnttrue > 0)
                {
                }
                else if (cnttrue == 0)
                {
                    //ControlVisibility("Result");
                    //BtnSearch_Click(sender, e);
                    Show_Error_Success_Box("S", "0000");
                    //Clear_AddPanel();
                }


            }

        }

        else if (ResultId == -1)
        {
            Show_Error_Success_Box("E", "Assesment Test Code already exists");
        }


    }

    private void Clear_AddPanel()
    {
        txtassesmenttestcode.Text = "";
        dlQuestion.DataSource = null;
        dlQuestion.DataBind();
    }

    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }
    public Master_QPSet()
    {
        Load += Page_Load;
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
    }

    protected void BtnDownloadExcel_ServerClick(object sender, System.EventArgs e)
    {
        try
        {
            DataList1.Visible = true;
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            string filenamexls1 = "QP_Set.xls";
            Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:14.0pt; font-family:Calibri; text-align:center;'><TR><TD>Que_No</TD><TD>Que_Type</TD><TD>Answer_Key</TD><TD>Difficulti_Level</TD><TD>Correct_Marks</TD><TD>Wrong_Marks(-ve)</TD><TD>Subject</TD><TD>Ref_Course</TD><TD>Ref_Subject</TD><TD>Chapter</TD><TD>Topic</TD><TD>SubTopic</TD><TD>Module</TD><TD>Question_Rule</TD></TR>");
            Response.Charset = "";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
            //this.ClearControls(dladmissioncount)
            DataList1.RenderControl(oHtmlTextWriter1);
            Response.Write(oStringWriter1.ToString());
            Response.Flush();
            Response.End();
            DataList1.Visible = false;
        }
        catch (Exception ex)
        {
        }

    }


    protected void BtnUploadExcel_ServerClick(object sender, System.EventArgs e)
    {
        try
        {
            Clear_Error_Success_Box();
            if (dlQuestion.Visible == true)
            {

                if (!string.IsNullOrEmpty(FFLExcel.FileName))
                {
                    //txtFileName_Add.Text = Path.GetFileName(FileUpload1.FileName);
                    string FullName = Server.MapPath("~/UserUploads/CSV_ResultFiles") + "\\" + Path.GetFileName(FFLExcel.FileName);
                    lblfilepath.Text = FullName;
                    string strFileType = Path.GetExtension(FFLExcel.FileName).ToLower();
                    if (strFileType != ".csv")
                    {
                        Show_Error_Success_Box("E", "0024");
                        return;
                    }
                    else
                    {
                        FFLExcel.SaveAs(FullName);

                        DataTable dtRaw = new DataTable();
                        DataTable dtCorrectEntry = new DataTable();
                        DataTable dtErrorEntry = new DataTable();
                        DataTable dtWarningEntry = new DataTable();
                        DataTable dtUnQue = new DataTable();

                        ////////

                        foreach (DataListItem dtlItem in dlQuestion.Items)
                        {
                            Label lblQueNo = (Label)dtlItem.FindControl("lblQueNo");
                        }

                        ///////


                        //create object for CSVReader and pass the stream
                        ////CSVReader reader = new CSVReader(FFLExcel.PostedFile.InputStream);
                        FileStream fileStream = new FileStream(FullName, FileMode.Open);
                        CSVReader reader = new CSVReader(fileStream);
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
                        _with1.Columns.Add("Question_No");
                        _with1.Columns.Add("Question_Type");
                        _with1.Columns.Add("Rule_Name");
                        _with1.Columns.Add("Correct_Ans_Key");
                        _with1.Columns.Add("DiffLevel_Name");
                        _with1.Columns.Add("DiffLevel_Id");
                        _with1.Columns.Add("Correct_Marks");
                        _with1.Columns.Add("Wrong_Marks");
                        _with1.Columns.Add("Subject_Name");
                        _with1.Columns.Add("Subject_Code");
                        _with1.Columns.Add("RefCourse_Name");
                        _with1.Columns.Add("RefCourse_Code");
                        _with1.Columns.Add("RefSubject_Name");
                        _with1.Columns.Add("RefSubject_Code");
                        _with1.Columns.Add("Chapter_Name");
                        _with1.Columns.Add("Chapter_Code");
                        _with1.Columns.Add("Topic_Name");
                        _with1.Columns.Add("Topic_Code");
                        _with1.Columns.Add("SubTopic_Name");
                        _with1.Columns.Add("SubTopic_Code");
                        _with1.Columns.Add("Module_Name");
                        _with1.Columns.Add("Module_Code");
                        _with1.Columns.Add("Question_Type_Id");
                        _with1.Columns.Add("Rule_Id");
                        _with1.Columns.Add("ErrrorMessage");


                        var _with4 = dtUnQue;
                        _with4.Columns.Add("Question_No");
                        _with4.Columns.Add("Question_Type");
                        _with4.Columns.Add("Rule_Name");
                        _with4.Columns.Add("Correct_Ans_Key");
                        _with4.Columns.Add("DiffLevel_Name");
                        _with4.Columns.Add("DiffLevel_Id");
                        _with4.Columns.Add("Correct_Marks");
                        _with4.Columns.Add("Wrong_Marks");
                        _with4.Columns.Add("Subject_Name");
                        _with4.Columns.Add("Subject_Code");
                        _with4.Columns.Add("RefCourse_Name");
                        _with4.Columns.Add("RefCourse_Code");
                        _with4.Columns.Add("RefSubject_Name");
                        _with4.Columns.Add("RefSubject_Code");
                        _with4.Columns.Add("Chapter_Name");
                        _with4.Columns.Add("Chapter_Code");
                        _with4.Columns.Add("Topic_Name");
                        _with4.Columns.Add("Topic_Code");
                        _with4.Columns.Add("SubTopic_Name");
                        _with4.Columns.Add("SubTopic_Code");
                        _with4.Columns.Add("Module_Name");
                        _with4.Columns.Add("Module_Code");
                        _with4.Columns.Add("Question_Type_Id");
                        _with4.Columns.Add("Rule_Id");
                        _with4.Columns.Add("ErrrorMessage");

                        var _with2 = dtErrorEntry;
                        _with2.Columns.Add("Question_No");
                        _with2.Columns.Add("Question_Type");
                        _with2.Columns.Add("Rule_Name");
                        _with2.Columns.Add("Correct_Ans_Key");
                        _with2.Columns.Add("DiffLevel_Name");
                        _with2.Columns.Add("DiffLevel_Id");
                        _with2.Columns.Add("Correct_Marks");
                        _with2.Columns.Add("Wrong_Marks");
                        _with2.Columns.Add("Subject_Name");
                        _with2.Columns.Add("Subject_Code");
                        _with2.Columns.Add("RefCourse_Name");
                        _with2.Columns.Add("RefCourse_Code");
                        _with2.Columns.Add("RefSubject_Name");
                        _with2.Columns.Add("RefSubject_Code");
                        _with2.Columns.Add("Chapter_Name");
                        _with2.Columns.Add("Chapter_Code");
                        _with2.Columns.Add("Topic_Name");
                        _with2.Columns.Add("Topic_Code");
                        _with2.Columns.Add("SubTopic_Name");
                        _with2.Columns.Add("SubTopic_Code");
                        _with2.Columns.Add("Module_Name");
                        _with2.Columns.Add("Module_Code");
                        _with2.Columns.Add("Question_Type_Id");
                        _with2.Columns.Add("Rule_Id");
                        _with2.Columns.Add("ErrrorMessage");

                        var _with3 = dtWarningEntry;
                        _with3.Columns.Add("Question_No");
                        _with3.Columns.Add("Question_Type");
                        _with3.Columns.Add("Rule_Name");
                        _with3.Columns.Add("Correct_Ans_Key");
                        _with3.Columns.Add("DiffLevel_Name");
                        _with3.Columns.Add("DiffLevel_Id");
                        _with3.Columns.Add("Correct_Marks");
                        _with3.Columns.Add("Wrong_Marks");
                        _with3.Columns.Add("Subject_Name");
                        _with3.Columns.Add("Subject_Code");
                        _with3.Columns.Add("RefCourse_Name");
                        _with3.Columns.Add("RefCourse_Code");
                        _with3.Columns.Add("RefSubject_Name");
                        _with3.Columns.Add("RefSubject_Code");
                        _with3.Columns.Add("Chapter_Name");
                        _with3.Columns.Add("Chapter_Code");
                        _with3.Columns.Add("Topic_Name");
                        _with3.Columns.Add("Topic_Code");
                        _with3.Columns.Add("SubTopic_Name");
                        _with3.Columns.Add("SubTopic_Code");
                        _with3.Columns.Add("Module_Name");
                        _with3.Columns.Add("Module_Code");
                        _with3.Columns.Add("Question_Type_Id");
                        _with3.Columns.Add("Rule_Id");
                        _with3.Columns.Add("ErrrorMessage");

                        string[] data = null;
                        string Que_No = "";
                        string Que_Type = "";
                        string Rule_Name = "";
                        string Answer_Key = "";
                        string Difficulti_Level = "";
                        string Correct_Marks = "";
                        string Wrong_Marks = "";
                        string Subject = "";
                        string RefCourse = "", RefSubject = "", Chapter = "", RefCourseVal = "", RefSubjectVal = "", ChapterVal = "";
                        string Topic = "", SubTopic = "", Module = "", TopicVal = "", SubTopicVal = "", ModuleVal = "";
                        string Subject_Code = "";
                        string RefCourse_Code = "", RefSubject_Code = "", Chapter_Code = "";
                        string Topic_Code = "", SubTopic_Code = "", Module_Code = "";
                        string DiffLevel_Id = "";
                        string Question_Type_Id = "";
                        string Rule_Id = "";

                        data = reader.GetCSVLine();
                        //Read first line
                        CurRowNo = 1;
                        while (data != null)
                        {
                            dtRaw.Rows.Add(data);
                            Que_No = "";
                            Que_Type = "";
                            Rule_Name = "";
                            Answer_Key = "";
                            Difficulti_Level = "";
                            Correct_Marks = "";
                            Wrong_Marks = "";
                            Subject = "";
                            RefCourse = "";
                            RefSubject = "";
                            Chapter = "";
                            Topic = "";
                            SubTopic = "";
                            Module = "";
                            DiffLevel_Id = "";
                            Subject_Code = "";
                            RefCourse_Code = "";
                            RefSubject_Code = "";
                            Chapter_Code = "";
                            Topic_Code = "";
                            SubTopic_Code = "";
                            Module_Code = "";
                            Question_Type_Id = "";
                            Rule_Id = "";

                            Que_Type = dtRaw.Rows[dtRaw.Rows.Count - 1]["Que_Type"].ToString();
                            Que_No = dtRaw.Rows[dtRaw.Rows.Count - 1]["Que_No"].ToString();
                            Answer_Key = dtRaw.Rows[dtRaw.Rows.Count - 1]["Answer_Key"].ToString();

                            double num2;
                            if (double.TryParse(Answer_Key, out num2))
                            {
                                int anslen1 = Answer_Key.Length;
                                if (anslen1 <= 4)
                                {
                                }
                                else
                                {
                                    Answer_Key = "";
                                }
                            }
                            else
                            {
                                if (Que_Type == "Matrix Match")
                                {
                                    Regex r = new Regex("^[0-9:]*$");
                                    if (r.IsMatch(Answer_Key))
                                    {

                                    }
                                    else
                                    {
                                        Answer_Key = "";
                                    }
                                }
                                else
                                {
                                    Regex r = new Regex("^[a-zA-Z]*$");
                                    if (r.IsMatch(Answer_Key))
                                    {

                                    }
                                    else
                                    {
                                        Answer_Key = "";
                                    }
                                }
                            }

                            //string strAnsKey = Answer_Key.Trim();
                            //string[] strAnsKeyArray = strAnsKey.Split(',');

                            //string[] distinctArray = RemoveDuplicates(strAnsKeyArray);

                            //foreach (string AnsKey in distinctArray)
                            //{
                            //    if (AnsKey.Trim() == "A")
                            //    {

                            //    }
                            //    else if (AnsKey.Trim() == "B")
                            //    {

                            //    }
                            //    else if (AnsKey.Trim() == "C")
                            //    {

                            //    }
                            //    else if (AnsKey.Trim() == "D")
                            //    {

                            //    }
                            //    else if (AnsKey.Trim() == "E")
                            //    {

                            //    }
                            //    else if (AnsKey.Trim() == "F")
                            //    {

                            //    }
                            //    else
                            //    {
                            //        Answer_Key = "";
                            //    }
                            //}

                            Difficulti_Level = dtRaw.Rows[dtRaw.Rows.Count - 1]["Difficulti_Level"].ToString();
                            Correct_Marks = dtRaw.Rows[dtRaw.Rows.Count - 1]["Correct_Marks"].ToString();
                            double num;
                            if (double.TryParse(Correct_Marks, out num))
                            {
                                // It's a number!
                            }
                            else
                            {
                                Correct_Marks = "";
                            }


                            Wrong_Marks = dtRaw.Rows[dtRaw.Rows.Count - 1]["Wrong_Marks(-ve)"].ToString();
                            double num1;
                            if (double.TryParse(Wrong_Marks, out num1))
                            {
                                // It's a number!
                            }
                            else
                            {
                                Wrong_Marks = "";
                            }

                            Wrong_Marks = Wrong_Marks.Replace("-", "");


                            Subject = dtRaw.Rows[dtRaw.Rows.Count - 1]["Subject"].ToString();

                            string strSubNm = lblSubject_Add.Text.Trim();
                            string[] strSubNmArray = strSubNm.Split(',');

                            foreach (string SubNm in strSubNmArray)
                            {
                                if (SubNm.Trim() == Subject)
                                {
                                    DataSet dsSub = ProductController.GetSubjectCodeBY_Course(lblStandardCode.Text.Trim(), Subject, 1);
                                    if (dsSub != null)
                                    {
                                        if (dsSub.Tables.Count != 0)
                                        {
                                            if (dsSub.Tables[0].Rows.Count != 0)
                                            {
                                                Subject_Code = Convert.ToString(dsSub.Tables[0].Rows[0]["Subject_Code"]).ToString().Trim();
                                            }
                                        }
                                    }
                                }
                            }

                            if (Subject_Code == "")
                            {
                                Subject = "";
                            }
                            //Ref_Course</TD><TD>Ref_Subject
                            //Get reference Course
                            RefCourse = dtRaw.Rows[dtRaw.Rows.Count - 1]["Ref_Course"].ToString();
                            RefCourseVal = dtRaw.Rows[dtRaw.Rows.Count - 1]["Ref_Course"].ToString();
                            if (RefCourse != "")
                            {
                                DataSet dsCourse = ProductController.GetTopiCodeBY_Course(lblDivCode.Text.Trim(), "", "", "", RefCourse, 6);
                                if (dsCourse != null)
                                {
                                    if (dsCourse.Tables.Count != 0)
                                    {
                                        if (dsCourse.Tables[0].Rows.Count != 0)
                                        {
                                            RefCourse_Code = Convert.ToString(dsCourse.Tables[0].Rows[0]["Course_Code"]).ToString().Trim();
                                        }
                                    }
                                }
                                if (RefCourse_Code == "")
                                {
                                    RefCourse = "";
                                }
                            }
                            //Get reference Subject
                            RefSubject = dtRaw.Rows[dtRaw.Rows.Count - 1]["Ref_Subject"].ToString();
                            RefSubjectVal = dtRaw.Rows[dtRaw.Rows.Count - 1]["Ref_Subject"].ToString();
                            if (RefSubject != "")
                            {
                                DataSet dsRefSubject = ProductController.GetSubjectCodeBY_Course(RefCourse_Code, RefSubject, 7);
                                if (dsRefSubject != null)
                                {
                                    if (dsRefSubject.Tables.Count != 0)
                                    {
                                        if (dsRefSubject.Tables[0].Rows.Count != 0)
                                        {
                                            RefSubject_Code = Convert.ToString(dsRefSubject.Tables[0].Rows[0]["Subject_Code"]).ToString().Trim();
                                        }
                                    }
                                }
                                if (RefSubject_Code == "")
                                {
                                    RefSubject = "";
                                }
                            }
                            //Get Chapter Detail
                            Chapter = dtRaw.Rows[dtRaw.Rows.Count - 1]["Chapter"].ToString();
                            ChapterVal = dtRaw.Rows[dtRaw.Rows.Count - 1]["Chapter"].ToString();
                            if (Chapter != "")
                            {
                                DataSet dsChap = ProductController.GetChapCodeBY_Course(lblDivCode.Text.Trim(), RefCourse_Code, RefSubject_Code, Chapter, 2);
                                if (dsChap != null)
                                {
                                    if (dsChap.Tables.Count != 0)
                                    {
                                        if (dsChap.Tables[0].Rows.Count != 0)
                                        {
                                            Chapter_Code = Convert.ToString(dsChap.Tables[0].Rows[0]["Chapter_Code"]).ToString().Trim();
                                        }
                                    }
                                }

                                if (Chapter_Code == "")
                                {
                                    Chapter = "";
                                }
                            }



                         

                            DataSet dsqtype = ProductController.GetQuestionTypeIdByName(Que_Type, 1);
                            if (dsqtype != null)
                            {
                                if (dsqtype.Tables.Count != 0)
                                {
                                    if (dsqtype.Tables[0].Rows.Count != 0)
                                    {
                                        Question_Type_Id = Convert.ToString(dsqtype.Tables[0].Rows[0]["Question_Type_Id"]).ToString().Trim();
                                    }

                                }
                            }

                            if (Question_Type_Id == "")
                            {
                                Que_Type = "";
                            }


                            Rule_Name = dtRaw.Rows[dtRaw.Rows.Count - 1]["Question_Rule"].ToString();

                            DataSet dsrulename = ProductController.GetQuestionRuleName(Rule_Name, 1);
                            if (dsrulename != null)
                            {
                                if (dsrulename.Tables.Count != 0)
                                {
                                    if (dsrulename.Tables[0].Rows.Count != 0)
                                    {
                                        Rule_Id = Convert.ToString(dsrulename.Tables[0].Rows[0]["Rule_Id"]).ToString().Trim();
                                    }

                                }
                            }

                            if (Rule_Id == "")
                            {
                                Rule_Name = "";
                            }



                            Topic = dtRaw.Rows[dtRaw.Rows.Count - 1]["Topic"].ToString();
                            TopicVal = dtRaw.Rows[dtRaw.Rows.Count - 1]["Topic"].ToString();
                            if (Topic != "")
                            {
                                DataSet dsTopi = ProductController.GetTopiCodeBY_Course(lblDivCode.Text.Trim(), RefCourse_Code, RefSubject_Code, Chapter_Code, Topic, 3);
                                if (dsTopi != null)
                                {
                                    if (dsTopi.Tables.Count != 0)
                                    {
                                        if (dsTopi.Tables[0].Rows.Count != 0)
                                        {
                                            Topic_Code = Convert.ToString(dsTopi.Tables[0].Rows[0]["Topic_Code"]).ToString().Trim();
                                        }
                                    }
                                }

                                if (Topic_Code == "")
                                {
                                    Topic = "";
                                }

                            }
                            //--------------------------------------Get Subtopic 
                            SubTopic = dtRaw.Rows[dtRaw.Rows.Count - 1]["SubTopic"].ToString();
                            SubTopicVal = dtRaw.Rows[dtRaw.Rows.Count - 1]["SubTopic"].ToString();
                            if (Topic_Code != "" && SubTopic != "")
                            {
                                DataSet dsSubTopi = ProductController.GetTopiCodeBY_Course(lblDivCode.Text.Trim(), RefCourse_Code, RefSubject_Code, Chapter_Code, Topic_Code + '%' + SubTopic, 4);
                                if (dsSubTopi != null)
                                {
                                    if (dsSubTopi.Tables.Count != 0)
                                    {
                                        if (dsSubTopi.Tables[0].Rows.Count != 0)
                                        {
                                            SubTopic_Code = Convert.ToString(dsSubTopi.Tables[0].Rows[0]["SubTopic_Code"]).ToString().Trim();
                                        }

                                    }
                                }

                                if (SubTopic_Code == "")
                                {
                                    SubTopic = "";
                                }
                            }
                            //----------------------------------
                            //--------------------------------------Get Module 
                            Module = dtRaw.Rows[dtRaw.Rows.Count - 1]["Module"].ToString();
                            ModuleVal = dtRaw.Rows[dtRaw.Rows.Count - 1]["Module"].ToString();
                            if (Chapter_Code != "" && Module != "")
                            {
                                DataSet dsModu = ProductController.GetTopiCodeBY_Course(lblDivCode.Text.Trim(), RefCourse_Code, RefSubject_Code, Chapter_Code, Topic_Code + '%' + SubTopic_Code + '%' + Module, 5);
                                if (dsModu != null)
                                {
                                    if (dsModu.Tables.Count != 0)
                                    {
                                        if (dsModu.Tables[0].Rows.Count != 0)
                                        {
                                            Module_Code = Convert.ToString(dsModu.Tables[0].Rows[0]["Module_Code"]).ToString().Trim();
                                        }
                                    }
                                }

                                if (Module_Code == "")
                                {
                                    Module = "";
                                }
                            }
                            //----------------------------------

                            if (Difficulti_Level == "Easy")
                            {
                                DiffLevel_Id = "01";
                            }
                            else if (Difficulti_Level == "Moderate")
                            {
                                DiffLevel_Id = "02";
                            }
                            else if (Difficulti_Level == "High")
                            {
                                DiffLevel_Id = "03";
                            }
                            else
                            {
                                Difficulti_Level = "";
                                DiffLevel_Id = "";
                            }

                            //Check for duplicate que.no in correct records
                            DataRow[] DupliRollNoRow = null;
                            DupliRollNoRow = dtCorrectEntry.Select("Question_No ='" + Que_No + "'");
                            if (DupliRollNoRow.Length > 0)
                            {
                                //Add entry in Error Datatable
                                NewRow = dtWarningEntry.NewRow();
                                NewRow["Question_No"] = Que_No;
                                NewRow["Question_Type"] = Que_Type;
                                NewRow["Rule_Name"] = Rule_Name;
                                NewRow["Correct_Ans_Key"] = Answer_Key;
                                NewRow["DiffLevel_Name"] = Difficulti_Level;
                                NewRow["DiffLevel_Id"] = DiffLevel_Id;
                                NewRow["Correct_Marks"] = Correct_Marks;
                                NewRow["Wrong_Marks"] = Wrong_Marks;
                                NewRow["Subject_Name"] = Subject;
                                NewRow["Subject_Code"] = Subject_Code;
                                NewRow["RefCourse_Name"] = RefCourse;
                                NewRow["RefCourse_Code"] = RefCourse_Code;
                                NewRow["RefSubject_Name"] = RefSubject;
                                NewRow["RefSubject_Code"] = RefSubject_Code;
                                NewRow["Chapter_Name"] = Chapter;
                                NewRow["Chapter_Code"] = Chapter_Code;
                                NewRow["Topic_Name"] = Topic;
                                NewRow["Topic_Code"] = Topic_Code;
                                NewRow["Topic_Name"] = Topic;
                                NewRow["Topic_Code"] = Topic_Code;
                                NewRow["SubTopic_Name"] = SubTopic;
                                NewRow["SubTopic_Code"] = SubTopic_Code;
                                NewRow["Module_Name"] = Module;
                                NewRow["Module_Code"] = Module_Code;
                                NewRow["Question_Type_Id"] = Question_Type_Id;
                                NewRow["Rule_Id"] = Rule_Id;
                                NewRow["ErrrorMessage"] = "Duplicate Que.No";

                                dtWarningEntry.Rows.Add(NewRow);

                                goto NextCSVLine;
                            }




                            foreach (DataListItem dtlItem in dlQuestion.Items)
                            {
                                Label lblQueNo = (Label)dtlItem.FindControl("lblQueNo");
                                Label lblDLAnswerKey_Name = (Label)dtlItem.FindControl("lblDLAnswerKey_Name");
                                Label lblDLDiffLevel_Name = (Label)dtlItem.FindControl("lblDLDiffLevel_Name");
                                Label lblDLCorrectMarks_Name = (Label)dtlItem.FindControl("lblDLCorrectMarks_Name");
                                Label lblDLWrongMarks = (Label)dtlItem.FindControl("lblDLWrongMarks");
                                Label lblDLSubject_Name = (Label)dtlItem.FindControl("lblDLSubject_Name");
                                Label lblDLRefCourse_Name = (Label)dtlItem.FindControl("lblDLRefCourse_Name");
                                Label lblDLRefSubject_Name = (Label)dtlItem.FindControl("lblDLRefSubject_Name");
                                Label lblDLChapter_Name = (Label)dtlItem.FindControl("lblDLChapter_Name");
                                Label lblDLTopic_Name = (Label)dtlItem.FindControl("lblDLTopic_Name");
                                Label lblDLSubTopic_Name = (Label)dtlItem.FindControl("lblDLSubTopic_Name");
                                Label lblDLModule_Name = (Label)dtlItem.FindControl("lblDLModule_Name");
                                Label lblquetypename = (Label)dtlItem.FindControl("lblquetypename");
                                Label lblrulename = (Label)dtlItem.FindControl("lblrulename");
                                Label lblResult = (Label)dtlItem.FindControl("lblResult");


                                if (lblQueNo.Text.Trim() == Que_No)
                                {
                                    //Everything is OK hence add row in correct Datatable
                                    NewRow = dtCorrectEntry.NewRow();
                                    NewRow["Question_No"] = Que_No;
                                    NewRow["Question_Type"] = Que_Type;
                                    NewRow["Rule_Name"] = Rule_Name;
                                    NewRow["Correct_Ans_Key"] = Answer_Key;
                                    NewRow["DiffLevel_Name"] = Difficulti_Level;
                                    NewRow["DiffLevel_Id"] = DiffLevel_Id;
                                    NewRow["Correct_Marks"] = Correct_Marks;
                                    NewRow["Wrong_Marks"] = Wrong_Marks;
                                    NewRow["Subject_Name"] = Subject;
                                    NewRow["Subject_Code"] = Subject_Code;
                                    NewRow["RefCourse_Name"] = RefCourse;
                                    NewRow["RefCourse_Code"] = RefCourse_Code;
                                    NewRow["RefSubject_Name"] = RefSubject;
                                    NewRow["RefSubject_Code"] = RefSubject_Code;
                                    NewRow["Chapter_Name"] = Chapter;
                                    NewRow["Chapter_Code"] = Chapter_Code;
                                    NewRow["Topic_Name"] = Topic;
                                    NewRow["Topic_Code"] = Topic_Code;
                                    NewRow["SubTopic_Name"] = SubTopic;
                                    NewRow["SubTopic_Code"] = SubTopic_Code;
                                    NewRow["Module_Name"] = Module;
                                    NewRow["Module_Code"] = Module_Code;
                                    NewRow["Question_Type_Id"] = Question_Type_Id;
                                    NewRow["Rule_Id"] = Rule_Id;

                                    if (RefCourseVal != "" && RefCourse == "")
                                        NewRow["ErrrorMessage"] = "Entered reference course not found";
                                    else if (RefSubjectVal != "" && RefSubject == "")
                                        NewRow["ErrrorMessage"] = "Entered reference subject not found";
                                    else if (ChapterVal != "" && Chapter == "")
                                        NewRow["ErrrorMessage"] = "Entered chapter not found";
                                    else if (TopicVal != "" && Topic == "")
                                        NewRow["ErrrorMessage"] = "Entered Topic not found";
                                    else if (SubTopicVal != "" && SubTopic == "")
                                        NewRow["ErrrorMessage"] = "Entered SubTopic not found";
                                    else if (ModuleVal != "" && Module == "")
                                        NewRow["ErrrorMessage"] = "Entered Module not found";
                                    else
                                        NewRow["ErrrorMessage"] = "";

                                    dtCorrectEntry.Rows.Add(NewRow);
                                }
                            }

                        NextCSVLine:


                            data = reader.GetCSVLine();
                            //Read next line
                            CurRowNo = CurRowNo + 1;
                        }

                        int overCount = 0;

                        for (int cnt = 0; cnt <= dtCorrectEntry.Rows.Count - 1; cnt++)
                        {

                            string dtQueNo = dtCorrectEntry.Rows[cnt]["Question_No"].ToString();
                            string dtQueType = dtCorrectEntry.Rows[cnt]["Question_Type"].ToString();
                            string dtRuleName = dtCorrectEntry.Rows[cnt]["Rule_Name"].ToString();
                            string dtAnswer = dtCorrectEntry.Rows[cnt]["Correct_Ans_Key"].ToString();
                            string dtDiffName = dtCorrectEntry.Rows[cnt]["DiffLevel_Name"].ToString();
                            string dtDiffCode = dtCorrectEntry.Rows[cnt]["DiffLevel_Id"].ToString();
                            string dtCorrMarks = dtCorrectEntry.Rows[cnt]["Correct_Marks"].ToString();
                            string dtWrgMarks = dtCorrectEntry.Rows[cnt]["Wrong_Marks"].ToString();
                            string dtSubName = dtCorrectEntry.Rows[cnt]["Subject_Name"].ToString();
                            string dtSubCode = dtCorrectEntry.Rows[cnt]["Subject_Code"].ToString();
                            string dtRefCourse = dtCorrectEntry.Rows[cnt]["RefCourse_Name"].ToString();
                            string dtRefCourseCode = dtCorrectEntry.Rows[cnt]["RefCourse_Code"].ToString();
                            string dtRefSubName = dtCorrectEntry.Rows[cnt]["RefSubject_Name"].ToString();
                            string dtRefSubCode = dtCorrectEntry.Rows[cnt]["RefSubject_Code"].ToString();
                            string dtQueTypeName = dtCorrectEntry.Rows[cnt]["Question_Type"].ToString();
                            string dtQueTypeId = dtCorrectEntry.Rows[cnt]["Question_Type_Id"].ToString();
                            string dtChapName = dtCorrectEntry.Rows[cnt]["Chapter_Name"].ToString();
                            string dtChapCode = dtCorrectEntry.Rows[cnt]["Chapter_Code"].ToString();
                            string dtTopiName = dtCorrectEntry.Rows[cnt]["Topic_Name"].ToString();
                            string dtTopiCode = dtCorrectEntry.Rows[cnt]["Topic_Code"].ToString();
                            string dtSubTopiName = dtCorrectEntry.Rows[cnt]["SubTopic_Name"].ToString();
                            string dtSubTopiCode = dtCorrectEntry.Rows[cnt]["SubTopic_Code"].ToString();
                            string dtModuleName = dtCorrectEntry.Rows[cnt]["Module_Name"].ToString();
                            string dtModuleCode = dtCorrectEntry.Rows[cnt]["Module_Code"].ToString();
                            string dtRuleId = dtCorrectEntry.Rows[cnt]["Rule_Id"].ToString();
                            string dtErrorMessage = dtCorrectEntry.Rows[cnt]["ErrrorMessage"].ToString();

                            foreach (DataListItem dtlItem in dlQuestion.Items)
                            {
                                Label lblQueNo = (Label)dtlItem.FindControl("lblQueNo");

                                DropDownList ddlDLQueType = (DropDownList)dtlItem.FindControl("ddlDLQueType");
                                Label lblquetypeid = (Label)dtlItem.FindControl("lblquetypeid");
                                Label lblquetypename = (Label)dtlItem.FindControl("lblquetypename");

                                TextBox txtDLAnswerKey = (TextBox)dtlItem.FindControl("txtDLAnswerKey");
                                Label lblDLAnswerKey_Name = (Label)dtlItem.FindControl("lblDLAnswerKey_Name");

                                Label lblDLDiffLevel = (Label)dtlItem.FindControl("lblDLDiffLevel");
                                Label lblDLDiffLevel_Name = (Label)dtlItem.FindControl("lblDLDiffLevel_Name");
                                DropDownList ddlDLDiffLevel = (DropDownList)dtlItem.FindControl("ddlDLDiffLevel");

                                TextBox txtDLCorrectMarks = (TextBox)dtlItem.FindControl("txtDLCorrectMarks");
                                Label lblDLCorrectMarks_Name = (Label)dtlItem.FindControl("lblDLCorrectMarks_Name");

                                TextBox txtDLWrongMarks = (TextBox)dtlItem.FindControl("txtDLWrongMarks");
                                Label lblDLWrongMarks = (Label)dtlItem.FindControl("lblDLWrongMarks");

                                DropDownList ddlDLSubject = (DropDownList)dtlItem.FindControl("ddlDLSubject");
                                Label lblDLSubject_Code = (Label)dtlItem.FindControl("lblDLSubject_Code");
                                Label lblDLSubject_Name = (Label)dtlItem.FindControl("lblDLSubject_Name");

                                DropDownList ddlDLRefCouse = (DropDownList)dtlItem.FindControl("ddlDLRefCouse");
                                Label lblDLRefCourse_Code = (Label)dtlItem.FindControl("lblDLRefCourse_Code");
                                Label lblDLRefCourse_Name = (Label)dtlItem.FindControl("lblDLRefCourse_Name");

                                DropDownList ddlDLRefSubject = (DropDownList)dtlItem.FindControl("ddlDLRefSubject");
                                Label lblDLRefSubject_Code = (Label)dtlItem.FindControl("lblDLRefSubject_Code");
                                Label lblDLRefSubject_Name = (Label)dtlItem.FindControl("lblDLRefSubject_Name");

                                DropDownList ddlDLChapter = (DropDownList)dtlItem.FindControl("ddlDLChapter");
                                Label lblDLChapter_Code = (Label)dtlItem.FindControl("lblDLChapter_Code");
                                Label lblDLChapter_Name = (Label)dtlItem.FindControl("lblDLChapter_Name");

                                DropDownList ddlDLTopic = (DropDownList)dtlItem.FindControl("ddlDLTopic");
                                Label lblDLTopic_Code = (Label)dtlItem.FindControl("lblDLTopic_Code");
                                Label lblDLTopic_Name = (Label)dtlItem.FindControl("lblDLTopic_Name");

                                DropDownList ddlDLSubTopic = (DropDownList)dtlItem.FindControl("ddlDLSubTopic");
                                Label lblDLSubTopic_Code = (Label)dtlItem.FindControl("lblDLSubTopic_Code");
                                Label lblDLSubTopic_Name = (Label)dtlItem.FindControl("lblDLSubTopic_Name");

                                DropDownList ddlDLModul = (DropDownList)dtlItem.FindControl("ddlDLModul");
                                Label lblDLModule_Code = (Label)dtlItem.FindControl("lblDLModule_Code");
                                Label lblDLModule_Name = (Label)dtlItem.FindControl("lblDLModule_Name");

                                DropDownList ddlquestionrule = (DropDownList)dtlItem.FindControl("ddlquestionrule");
                                Label lblruleid = (Label)dtlItem.FindControl("lblruleid");
                                Label lblrulename = (Label)dtlItem.FindControl("lblrulename");

                                if (lbltestneagtivemarkingflag.Text != "1")
                                {
                                    txtDLWrongMarks.Text = "0";
                                    txtDLWrongMarks.Enabled = false;
                                    lblDLWrongMarks.Text = "0";
                                }

                                if (Convert.ToString(lblQueNo.Text).Trim() == dtQueNo && Convert.ToString(lblDLAnswerKey_Name.Text).Trim() != "")
                                {
                                    overCount = overCount + 1;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }

                        }



                        if (overCount > 0)
                        {
                            fileStream.Close();
                            fileStream.Dispose();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDivOverride();", true);
                        }
                        else
                        {
                            for (int cnt = 0; cnt <= dtCorrectEntry.Rows.Count - 1; cnt++)
                            {

                                string dtQueNo = dtCorrectEntry.Rows[cnt]["Question_No"].ToString();
                                string dtQueType = dtCorrectEntry.Rows[cnt]["Question_Type"].ToString();
                                string dtRuleName = dtCorrectEntry.Rows[cnt]["Rule_Name"].ToString();
                                string dtAnswer = dtCorrectEntry.Rows[cnt]["Correct_Ans_Key"].ToString();
                                string dtDiffName = dtCorrectEntry.Rows[cnt]["DiffLevel_Name"].ToString();
                                string dtDiffCode = dtCorrectEntry.Rows[cnt]["DiffLevel_Id"].ToString();
                                string dtCorrMarks = dtCorrectEntry.Rows[cnt]["Correct_Marks"].ToString();
                                string dtWrgMarks = dtCorrectEntry.Rows[cnt]["Wrong_Marks"].ToString();
                                string dtSubName = dtCorrectEntry.Rows[cnt]["Subject_Name"].ToString();
                                string dtSubCode = dtCorrectEntry.Rows[cnt]["Subject_Code"].ToString();
                                string dtRefCourse = dtCorrectEntry.Rows[cnt]["RefCourse_Name"].ToString();
                                string dtRefCourseCode = dtCorrectEntry.Rows[cnt]["RefCourse_Code"].ToString();
                                string dtRefSubName = dtCorrectEntry.Rows[cnt]["RefSubject_Name"].ToString();
                                string dtRefSubCode = dtCorrectEntry.Rows[cnt]["RefSubject_Code"].ToString();
                                string dtQueTypeName = dtCorrectEntry.Rows[cnt]["Question_Type"].ToString();
                                string dtQueTypeId = dtCorrectEntry.Rows[cnt]["Question_Type_Id"].ToString();
                                string dtChapName = dtCorrectEntry.Rows[cnt]["Chapter_Name"].ToString();
                                string dtChapCode = dtCorrectEntry.Rows[cnt]["Chapter_Code"].ToString();
                                string dtTopiName = dtCorrectEntry.Rows[cnt]["Topic_Name"].ToString();
                                string dtTopiCode = dtCorrectEntry.Rows[cnt]["Topic_Code"].ToString();
                                string dtSubTopiName = dtCorrectEntry.Rows[cnt]["SubTopic_Name"].ToString();
                                string dtSubTopiCode = dtCorrectEntry.Rows[cnt]["SubTopic_Code"].ToString();
                                string dtModuleName = dtCorrectEntry.Rows[cnt]["Module_Name"].ToString();
                                string dtModuleCode = dtCorrectEntry.Rows[cnt]["Module_Code"].ToString();
                                string dtRuleId = dtCorrectEntry.Rows[cnt]["Rule_Id"].ToString();
                                string dtErrorMessage = dtCorrectEntry.Rows[cnt]["ErrrorMessage"].ToString();

                                foreach (DataListItem dtlItem in dlQuestion.Items)
                                {
                                    Label lblQueNo = (Label)dtlItem.FindControl("lblQueNo");

                                    DropDownList ddlDLQueType = (DropDownList)dtlItem.FindControl("ddlDLQueType");
                                    Label lblquetypeid = (Label)dtlItem.FindControl("lblquetypeid");
                                    Label lblquetypename = (Label)dtlItem.FindControl("lblquetypename");

                                    TextBox txtDLAnswerKey = (TextBox)dtlItem.FindControl("txtDLAnswerKey");
                                    Label lblDLAnswerKey_Name = (Label)dtlItem.FindControl("lblDLAnswerKey_Name");

                                    Label lblDLDiffLevel = (Label)dtlItem.FindControl("lblDLDiffLevel");
                                    Label lblDLDiffLevel_Name = (Label)dtlItem.FindControl("lblDLDiffLevel_Name");
                                    DropDownList ddlDLDiffLevel = (DropDownList)dtlItem.FindControl("ddlDLDiffLevel");

                                    TextBox txtDLCorrectMarks = (TextBox)dtlItem.FindControl("txtDLCorrectMarks");
                                    Label lblDLCorrectMarks_Name = (Label)dtlItem.FindControl("lblDLCorrectMarks_Name");

                                    TextBox txtDLWrongMarks = (TextBox)dtlItem.FindControl("txtDLWrongMarks");
                                    Label lblDLWrongMarks = (Label)dtlItem.FindControl("lblDLWrongMarks");

                                    DropDownList ddlDLSubject = (DropDownList)dtlItem.FindControl("ddlDLSubject");
                                    Label lblDLSubject_Code = (Label)dtlItem.FindControl("lblDLSubject_Code");
                                    Label lblDLSubject_Name = (Label)dtlItem.FindControl("lblDLSubject_Name");

                                    DropDownList ddlDLRefCouse = (DropDownList)dtlItem.FindControl("ddlDLRefCouse");
                                    Label lblDLRefCourse_Code = (Label)dtlItem.FindControl("lblDLRefCourse_Code");
                                    Label lblDLRefCourse_Name = (Label)dtlItem.FindControl("lblDLRefCourse_Name");

                                    DropDownList ddlDLRefSubject = (DropDownList)dtlItem.FindControl("ddlDLRefSubject");
                                    Label lblDLRefSubject_Code = (Label)dtlItem.FindControl("lblDLRefSubject_Code");
                                    Label lblDLRefSubject_Name = (Label)dtlItem.FindControl("lblDLRefSubject_Name");

                                    DropDownList ddlDLChapter = (DropDownList)dtlItem.FindControl("ddlDLChapter");
                                    Label lblDLChapter_Code = (Label)dtlItem.FindControl("lblDLChapter_Code");
                                    Label lblDLChapter_Name = (Label)dtlItem.FindControl("lblDLChapter_Name");

                                    DropDownList ddlDLTopic = (DropDownList)dtlItem.FindControl("ddlDLTopic");
                                    Label lblDLTopic_Code = (Label)dtlItem.FindControl("lblDLTopic_Code");
                                    Label lblDLTopic_Name = (Label)dtlItem.FindControl("lblDLTopic_Name");

                                    DropDownList ddlDLSubTopic = (DropDownList)dtlItem.FindControl("ddlDLSubTopic");
                                    Label lblDLSubTopic_Code = (Label)dtlItem.FindControl("lblDLSubTopic_Code");
                                    Label lblDLSubTopic_Name = (Label)dtlItem.FindControl("lblDLSubTopic_Name");

                                    DropDownList ddlDLModul = (DropDownList)dtlItem.FindControl("ddlDLModul");
                                    Label lblDLModule_Code = (Label)dtlItem.FindControl("lblDLModule_Code");
                                    Label lblDLModule_Name = (Label)dtlItem.FindControl("lblDLModule_Name");

                                    DropDownList ddlquestionrule = (DropDownList)dtlItem.FindControl("ddlquestionrule");
                                    Label lblruleid = (Label)dtlItem.FindControl("lblruleid");
                                    Label lblrulename = (Label)dtlItem.FindControl("lblrulename");


                                    Label lblResult = (Label)dtlItem.FindControl("lblResult");



                                    if (Convert.ToString(lblQueNo.Text).Trim() == dtQueNo)
                                    {
                                        txtDLAnswerKey.Text = dtAnswer;
                                        lblDLAnswerKey_Name.Text = dtAnswer;

                                        lblDLDiffLevel.Text = dtDiffCode;
                                        lblDLDiffLevel_Name.Text = dtDiffName;
                                        ddlDLDiffLevel.SelectedValue = dtDiffCode;

                                        txtDLCorrectMarks.Text = dtCorrMarks;
                                        lblDLCorrectMarks_Name.Text = dtCorrMarks;

                                        txtDLWrongMarks.Text = dtWrgMarks;
                                        lblDLWrongMarks.Text = dtWrgMarks;

                                        ddlDLSubject.SelectedValue = dtSubCode;
                                        lblDLSubject_Code.Text = dtSubCode;
                                        lblDLSubject_Name.Text = dtSubName;

                                        ddlDLRefCouse.SelectedValue = dtRefCourseCode;
                                        lblDLRefCourse_Code.Text = dtRefCourseCode;
                                        lblDLRefCourse_Name.Text = dtRefCourse;

                                        ddlDLRefSubject.SelectedValue = dtRefSubCode;
                                        lblDLRefSubject_Code.Text = dtRefSubCode;
                                        lblDLRefSubject_Name.Text = dtRefSubName;

                                        ddlDLQueType.SelectedValue = dtQueTypeId;
                                        lblquetypeid.Text = dtQueTypeId;
                                        lblquetypename.Text = dtQueTypeName;

                                        ddlDLChapter.SelectedValue = dtChapCode;
                                        lblDLChapter_Code.Text = dtChapCode;
                                        lblDLChapter_Name.Text = dtChapName;

                                        ddlDLTopic.SelectedValue = dtTopiCode;
                                        lblDLTopic_Code.Text = dtTopiCode;
                                        lblDLTopic_Name.Text = dtTopiName;

                                        ddlDLSubTopic.SelectedValue = dtSubTopiCode;
                                        lblDLSubTopic_Code.Text = dtSubTopiCode;
                                        lblDLSubTopic_Name.Text = dtSubTopiName;

                                        ddlDLModul.SelectedValue = dtModuleCode;
                                        lblDLModule_Code.Text = dtModuleCode;
                                        lblDLModule_Name.Text = dtModuleName;



                                        ddlquestionrule.SelectedValue = dtRuleId;
                                        lblruleid.Text = dtRuleId;
                                        lblrulename.Text = dtRuleName;


                                        lblResult.Text = dtErrorMessage;
                                        lblResult.ForeColor = System.Drawing.Color.Red;

                                        break; // TODO: might not be correct. Was : Exit For
                                    }

                                    if (lbltestneagtivemarkingflag.Text != "1")
                                    {
                                        txtDLWrongMarks.Text = "0";
                                        txtDLWrongMarks.Enabled = false;
                                        lblDLWrongMarks.Text = "0";
                                    }
                                }

                            }
                        }
                    }
                }
                else
                {
                    Show_Error_Success_Box("E", "0022");
                }
            }
            else
            {
                Show_Error_Success_Box("E", "Please Select QP Set No");
            }
        }
        catch (Exception ex)
        {
        }
    }


    public string[] RemoveDuplicates(string[] inputArray)
    {

        int length = inputArray.Length;
        for (int i = 0; i < length; i++)
        {
            for (int j = (i + 1); j < length; )
            {
                if (inputArray[i] == inputArray[j])
                {
                    for (int k = j; k < length - 1; k++)
                        inputArray[k] = inputArray[k + 1];
                    length--;
                }
                else
                    j++;
            }
        }

        string[] distinctArray = new string[length];
        for (int i = 0; i < length; i++)
            distinctArray[i] = inputArray[i];

        return distinctArray;

    }

    protected void btnDivOverYes_Click(object sender, System.EventArgs e)
    {
        //txtFileName_Add.Text = Path.GetFileName(FileUpload1.FileName);
        string FullName = lblfilepath.Text.Trim();


        //FFLExcel.SaveAs(FullName);

        DataTable dtRaw = new DataTable();
        DataTable dtCorrectEntry = new DataTable();
        DataTable dtErrorEntry = new DataTable();
        DataTable dtWarningEntry = new DataTable();
        DataTable dtUnQue = new DataTable();

        ////////

        foreach (DataListItem dtlItem in dlQuestion.Items)
        {
            Label lblQueNo = (Label)dtlItem.FindControl("lblQueNo");


        }

        ///////


        //create object for CSVReader and pass the stream
        FileStream fileStream = new FileStream(FullName, FileMode.Open);
        CSVReader reader = new CSVReader(fileStream);
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
        _with1.Columns.Add("Question_No");
        _with1.Columns.Add("Question_Type");
        _with1.Columns.Add("Correct_Ans_Key");
        _with1.Columns.Add("DiffLevel_Name");
        _with1.Columns.Add("DiffLevel_Id");
        _with1.Columns.Add("Correct_Marks");
        _with1.Columns.Add("Wrong_Marks");
        _with1.Columns.Add("Subject_Name");
        _with1.Columns.Add("Subject_Code");
        _with1.Columns.Add("RefCourse_Name");
        _with1.Columns.Add("RefCourse_Code");
        _with1.Columns.Add("RefSubject_Name");
        _with1.Columns.Add("RefSubject_Code");
        _with1.Columns.Add("Chapter_Name");
        _with1.Columns.Add("Chapter_Code");
        _with1.Columns.Add("Topic_Name");
        _with1.Columns.Add("Topic_Code");
        _with1.Columns.Add("SubTopic_Name");
        _with1.Columns.Add("SubTopic_Code");
        _with1.Columns.Add("Module_Name");
        _with1.Columns.Add("Module_Code");
        _with1.Columns.Add("Question_Type_Id");
        _with1.Columns.Add("ErrrorMessage");


        var _with4 = dtUnQue;
        _with4.Columns.Add("Question_No");
        _with4.Columns.Add("Question_Type");
        _with4.Columns.Add("Correct_Ans_Key");
        _with4.Columns.Add("DiffLevel_Name");
        _with4.Columns.Add("DiffLevel_Id");
        _with4.Columns.Add("Correct_Marks");
        _with4.Columns.Add("Wrong_Marks");
        _with4.Columns.Add("Subject_Name");
        _with4.Columns.Add("Subject_Code");
        _with4.Columns.Add("RefCourse_Name");
        _with4.Columns.Add("RefCourse_Code");
        _with4.Columns.Add("RefSubject_Name");
        _with4.Columns.Add("RefSubject_Code");
        _with4.Columns.Add("Chapter_Name");
        _with4.Columns.Add("Chapter_Code");
        _with4.Columns.Add("Topic_Name");
        _with4.Columns.Add("Topic_Code");
        _with4.Columns.Add("SubTopic_Name");
        _with4.Columns.Add("SubTopic_Code");
        _with4.Columns.Add("Module_Name");
        _with4.Columns.Add("Module_Code");
        _with4.Columns.Add("Question_Type_Id");
        _with4.Columns.Add("ErrrorMessage");


        var _with2 = dtErrorEntry;
        _with2.Columns.Add("Question_No");
        _with2.Columns.Add("Question_Type");
        _with2.Columns.Add("Correct_Ans_Key");
        _with2.Columns.Add("DiffLevel_Name");
        _with2.Columns.Add("DiffLevel_Id");
        _with2.Columns.Add("Correct_Marks");
        _with2.Columns.Add("Wrong_Marks");
        _with2.Columns.Add("Subject_Name");
        _with2.Columns.Add("Subject_Code");
        _with2.Columns.Add("RefCourse_Name");
        _with2.Columns.Add("RefCourse_Code");
        _with2.Columns.Add("RefSubject_Name");
        _with2.Columns.Add("RefSubject_Code");
        _with2.Columns.Add("Chapter_Name");
        _with2.Columns.Add("Chapter_Code");
        _with2.Columns.Add("Topic_Name");
        _with2.Columns.Add("Topic_Code");
        _with2.Columns.Add("SubTopic_Name");
        _with2.Columns.Add("SubTopic_Code");
        _with2.Columns.Add("Module_Name");
        _with2.Columns.Add("Module_Code");
        _with2.Columns.Add("Question_Type_Id");
        _with2.Columns.Add("ErrrorMessage");

        var _with3 = dtWarningEntry;
        _with3.Columns.Add("Question_No");
        _with3.Columns.Add("Question_Type");
        _with3.Columns.Add("Correct_Ans_Key");
        _with3.Columns.Add("DiffLevel_Name");
        _with3.Columns.Add("DiffLevel_Id");
        _with3.Columns.Add("Correct_Marks");
        _with3.Columns.Add("Wrong_Marks");
        _with3.Columns.Add("Subject_Name");
        _with3.Columns.Add("Subject_Code");
        _with3.Columns.Add("RefCourse_Name");
        _with3.Columns.Add("RefCourse_Code");
        _with3.Columns.Add("RefSubject_Name");
        _with3.Columns.Add("RefSubject_Code");
        _with3.Columns.Add("Chapter_Name");
        _with3.Columns.Add("Chapter_Code");
        _with3.Columns.Add("Topic_Name");
        _with3.Columns.Add("Topic_Code");
        _with3.Columns.Add("SubTopic_Name");
        _with3.Columns.Add("SubTopic_Code");
        _with3.Columns.Add("Module_Name");
        _with3.Columns.Add("Module_Code");
        _with3.Columns.Add("Question_Type_Id");
        _with3.Columns.Add("ErrrorMessage");


        string[] data = null;
        string Que_No = "";
        string Que_Type = "";
        string Answer_Key = "";
        string Difficulti_Level = "";
        string Correct_Marks = "";
        string Wrong_Marks = "";
        string Subject = "", RefCourse = "", RefSubject = "", RefCourseVal = "", RefSubjectVal = "", ChapterVal = "";
        string Chapter = "";
        string Topic = "", SubTopic = "", Module = "", TopicVal = "", SubTopicVal = "", ModuleVal = "";
        string Subject_Code = "", RefCourse_Code = "", RefSubject_Code = "";
        string Chapter_Code = "";
        string Topic_Code = "", SubTopic_Code = "", Module_Code = "";
        string DiffLevel_Id = "";
        string Question_Type_Id = "";


        data = reader.GetCSVLine();
        //Read first line
        CurRowNo = 1;
        while (data != null)
        {
            dtRaw.Rows.Add(data);
            Que_No = "";
            Que_Type = "";
            Answer_Key = "";
            Difficulti_Level = "";
            Correct_Marks = "";
            Wrong_Marks = "";
            Subject = "";
            RefCourse = "";
            RefSubject = "";
            Chapter = "";
            Topic = "";
            SubTopic = "";
            Module = "";
            DiffLevel_Id = "";
            Subject_Code = "";
            RefCourse_Code = "";
            RefSubject_Code = "";
            Chapter_Code = "";
            Topic_Code = "";
            SubTopic_Code = "";
            Module_Code = "";

            Que_No = dtRaw.Rows[dtRaw.Rows.Count - 1]["Que_No"].ToString();
            Answer_Key = dtRaw.Rows[dtRaw.Rows.Count - 1]["Answer_Key"].ToString();

            double num2;
            if (double.TryParse(Answer_Key, out num2))
            {
                int anslen1 = Answer_Key.Length;
                if (anslen1 <= 4)
                {
                }
                else
                {
                    Answer_Key = "";
                }
            }
            else
            {
                if (Que_Type == "Matrix Match")
                {
                    Regex r = new Regex("^[0-9:]*$");
                    if (r.IsMatch(Answer_Key))
                    {

                    }
                    else
                    {
                        Answer_Key = "";
                    }
                }
                else
                {
                    Regex r = new Regex("^[a-zA-Z:]*$");
                    if (r.IsMatch(Answer_Key))
                    {

                    }
                    else
                    {
                        Answer_Key = "";
                    }
                }



            }

            //string strAnsKey = Answer_Key.Trim();
            //string[] strAnsKeyArray = strAnsKey.Split(',');

            //string[] distinctArray = RemoveDuplicates(strAnsKeyArray);

            //foreach (string AnsKey in distinctArray)
            //{
            //    if (AnsKey.Trim() == "A")
            //    {

            //    }
            //    else if (AnsKey.Trim() == "B")
            //    {

            //    }
            //    else if (AnsKey.Trim() == "C")
            //    {

            //    }
            //    else if (AnsKey.Trim() == "D")
            //    {

            //    }
            //    else if (AnsKey.Trim() == "E")
            //    {

            //    }
            //    else if (AnsKey.Trim() == "F")
            //    {

            //    }
            //    else
            //    {
            //        Answer_Key = "";
            //    }
            //}

            Difficulti_Level = dtRaw.Rows[dtRaw.Rows.Count - 1]["Difficulti_Level"].ToString();
            Correct_Marks = dtRaw.Rows[dtRaw.Rows.Count - 1]["Correct_Marks"].ToString();
            double num;
            if (double.TryParse(Correct_Marks, out num))
            {
                // It's a number!
            }
            else
            {
                Correct_Marks = "";
            }


            Wrong_Marks = dtRaw.Rows[dtRaw.Rows.Count - 1]["Wrong_Marks(-ve)"].ToString();
            double num1;
            if (double.TryParse(Wrong_Marks, out num1))
            {
                // It's a number!
            }
            else
            {
                Wrong_Marks = "";
            }

            Wrong_Marks = Wrong_Marks.Replace("-", "");


            Subject = dtRaw.Rows[dtRaw.Rows.Count - 1]["Subject"].ToString();

            string strSubNm = lblSubject_Add.Text.Trim();
            string[] strSubNmArray = strSubNm.Split(',');

            foreach (string SubNm in strSubNmArray)
            {
                if (SubNm.Trim() == Subject)
                {
                    DataSet dsSub = ProductController.GetSubjectCodeBY_Course(lblStandardCode.Text.Trim(), Subject, 1);
                    if (dsSub != null)
                    {
                        if (dsSub.Tables.Count != 0)
                        {
                            if (dsSub.Tables[0].Rows.Count != 0)
                            {
                                Subject_Code = Convert.ToString(dsSub.Tables[0].Rows[0]["Subject_Code"]).ToString().Trim();
                            }

                        }
                    }
                }
            }

            if (Subject_Code == "")
            {
                Subject = "";
            }


            //Get reference Course
            RefCourse = dtRaw.Rows[dtRaw.Rows.Count - 1]["Ref_Course"].ToString();
            RefCourseVal = dtRaw.Rows[dtRaw.Rows.Count - 1]["Ref_Course"].ToString();

            if (RefCourse != "")
            {
                DataSet dsCourse = ProductController.GetTopiCodeBY_Course(lblDivCode.Text.Trim(), "", "", "", RefCourse, 6);
                if (dsCourse != null)
                {
                    if (dsCourse.Tables.Count != 0)
                    {
                        if (dsCourse.Tables[0].Rows.Count != 0)
                        {
                            RefCourse_Code = Convert.ToString(dsCourse.Tables[0].Rows[0]["Course_Code"]).ToString().Trim();
                        }
                    }
                }
                if (RefCourse_Code == "")
                {
                    RefCourse = "";
                }
            }
            //Get reference Subject
            RefSubject = dtRaw.Rows[dtRaw.Rows.Count - 1]["Ref_Subject"].ToString();
            RefSubjectVal = dtRaw.Rows[dtRaw.Rows.Count - 1]["Ref_Subject"].ToString();

            if (RefSubject != "")
            {
                DataSet dsRefSubject = ProductController.GetSubjectCodeBY_Course(RefCourse_Code, RefSubject, 7);
                if (dsRefSubject != null)
                {
                    if (dsRefSubject.Tables.Count != 0)
                    {
                        if (dsRefSubject.Tables[0].Rows.Count != 0)
                        {
                            RefSubject_Code = Convert.ToString(dsRefSubject.Tables[0].Rows[0]["Subject_Code"]).ToString().Trim();
                        }
                    }
                }
                if (RefSubject_Code == "")
                {
                    RefSubject = "";
                }
            }
            Que_Type = dtRaw.Rows[dtRaw.Rows.Count - 1]["Que_Type"].ToString();

            DataSet dsqtype = ProductController.GetQuestionTypeIdByName(Que_Type, 1);
            if (dsqtype != null)
            {
                if (dsqtype.Tables.Count != 0)
                {
                    if (dsqtype.Tables[0].Rows.Count != 0)
                    {
                        Question_Type_Id = Convert.ToString(dsqtype.Tables[0].Rows[0]["Question_Type_Id"]).ToString().Trim();
                    }

                }
            }

            if (Question_Type_Id == "")
            {
                Que_Type = "";
            }


            //Get Chapter Detail
            Chapter = dtRaw.Rows[dtRaw.Rows.Count - 1]["Chapter"].ToString();
            ChapterVal = dtRaw.Rows[dtRaw.Rows.Count - 1]["Chapter"].ToString();
            if (Chapter != "")
            {
                DataSet dsChap = ProductController.GetChapCodeBY_Course(lblDivCode.Text.Trim(), RefCourse_Code, RefSubject_Code, Chapter, 2);
                if (dsChap != null)
                {
                    if (dsChap.Tables.Count != 0)
                    {
                        if (dsChap.Tables[0].Rows.Count != 0)
                        {
                            Chapter_Code = Convert.ToString(dsChap.Tables[0].Rows[0]["Chapter_Code"]).ToString().Trim();
                        }
                    }
                }

                if (Chapter_Code == "")
                {
                    Chapter = "";
                }
            }

            Topic = dtRaw.Rows[dtRaw.Rows.Count - 1]["Topic"].ToString();
            TopicVal = dtRaw.Rows[dtRaw.Rows.Count - 1]["Topic"].ToString();
            if (Topic != "")
            {
                DataSet dsTopi = ProductController.GetTopiCodeBY_Course(lblDivCode.Text.Trim(), RefCourse_Code, RefSubject_Code, Chapter_Code, Topic, 3);
                if (dsTopi != null)
                {
                    if (dsTopi.Tables.Count != 0)
                    {
                        if (dsTopi.Tables[0].Rows.Count != 0)
                        {
                            Topic_Code = Convert.ToString(dsTopi.Tables[0].Rows[0]["Topic_Code"]).ToString().Trim();
                        }

                    }
                }

                if (Topic_Code == "")
                {
                    Topic = "";
                }

            }
            //--------------------------------------Get Subtopic 
            SubTopic = dtRaw.Rows[dtRaw.Rows.Count - 1]["SubTopic"].ToString();
            SubTopicVal = dtRaw.Rows[dtRaw.Rows.Count - 1]["SubTopic"].ToString();
            if (SubTopic != "")
            {
                if (Topic_Code != "" && SubTopic != "")
                {

                    DataSet dsSubTopi = ProductController.GetTopiCodeBY_Course(lblDivCode.Text.Trim(), RefCourse_Code, RefSubject_Code, Chapter_Code, Topic_Code + '%' + SubTopic, 4);
                    if (dsSubTopi != null)
                    {
                        if (dsSubTopi.Tables.Count != 0)
                        {
                            if (dsSubTopi.Tables[0].Rows.Count != 0)
                            {
                                SubTopic_Code = Convert.ToString(dsSubTopi.Tables[0].Rows[0]["SubTopic_Code"]).ToString().Trim();
                            }

                        }
                    }

                    if (SubTopic_Code == "")
                    {
                        SubTopic = "";
                    }
                }
            }
            //----------------------------------
            //--------------------------------------Get Module 
            Module = dtRaw.Rows[dtRaw.Rows.Count - 1]["Module"].ToString();
            ModuleVal = dtRaw.Rows[dtRaw.Rows.Count - 1]["Module"].ToString();
            if (Module != "")
            {
                if (Chapter_Code != "" && Module != "")
                {
                    DataSet dsModu = ProductController.GetTopiCodeBY_Course(lblDivCode.Text.Trim(), RefCourse_Code, RefSubject_Code, Chapter_Code, Topic_Code + '%' + SubTopic_Code + '%' + Module, 5);
                    if (dsModu != null)
                    {
                        if (dsModu.Tables.Count != 0)
                        {
                            if (dsModu.Tables[0].Rows.Count != 0)
                            {
                                Module_Code = Convert.ToString(dsModu.Tables[0].Rows[0]["Module_Code"]).ToString().Trim();
                            }
                        }
                    }

                    if (Module_Code == "")
                    {
                        Module = "";
                    }
                }
            }
            //----------------------------------



            if (Difficulti_Level == "Easy")
            {
                DiffLevel_Id = "01";
            }
            else if (Difficulti_Level == "Moderate")
            {
                DiffLevel_Id = "02";
            }
            else if (Difficulti_Level == "High")
            {
                DiffLevel_Id = "03";
            }
            else
            {
                Difficulti_Level = "";
                DiffLevel_Id = "";
            }

            //Check for duplicate que.no in correct records
            DataRow[] DupliRollNoRow = null;
            DupliRollNoRow = dtCorrectEntry.Select("Question_No ='" + Que_No + "'");
            if (DupliRollNoRow.Length > 0)
            {
                //Add entry in Error Datatable
                NewRow = dtWarningEntry.NewRow();
                NewRow["Question_No"] = Que_No;
                NewRow["Question_Type"] = Que_Type;
                NewRow["Correct_Ans_Key"] = Answer_Key;
                NewRow["DiffLevel_Name"] = Difficulti_Level;
                NewRow["DiffLevel_Id"] = DiffLevel_Id;
                NewRow["Correct_Marks"] = Correct_Marks;
                NewRow["Wrong_Marks"] = Wrong_Marks;
                NewRow["Subject_Name"] = Subject;
                NewRow["Subject_Code"] = Subject_Code;
                NewRow["RefCourse_Name"] = RefCourse;
                NewRow["RefCourse_Code"] = RefCourse_Code;
                NewRow["RefSubject_Name"] = RefSubject;
                NewRow["RefSubject_Code"] = RefSubject_Code;
                NewRow["Chapter_Name"] = Chapter;
                NewRow["Chapter_Code"] = Chapter_Code;
                NewRow["Topic_Name"] = Topic;
                NewRow["Topic_Code"] = Topic_Code;
                NewRow["SubTopic_Name"] = SubTopic;
                NewRow["SubTopic_Code"] = SubTopic_Code;
                NewRow["Module_Name"] = Module;
                NewRow["Module_Code"] = Module_Code;
                NewRow["Question_Type_Id"] = Question_Type_Id;
                NewRow["ErrrorMessage"] = "Duplicate Que.No";

                dtWarningEntry.Rows.Add(NewRow);

                goto NextCSVLine;
            }




            foreach (DataListItem dtlItem in dlQuestion.Items)
            {
                Label lblQueNo = (Label)dtlItem.FindControl("lblQueNo");
                Label lblDLAnswerKey_Name = (Label)dtlItem.FindControl("lblDLAnswerKey_Name");
                Label lblDLDiffLevel_Name = (Label)dtlItem.FindControl("lblDLDiffLevel_Name");
                Label lblDLCorrectMarks_Name = (Label)dtlItem.FindControl("lblDLCorrectMarks_Name");
                Label lblDLWrongMarks = (Label)dtlItem.FindControl("lblDLWrongMarks");
                Label lblDLSubject_Name = (Label)dtlItem.FindControl("lblDLSubject_Name");
                Label lblDLRefCourse_Name = (Label)dtlItem.FindControl("lblDLRefCourse_Name");
                Label lblDLRefSubject_Name = (Label)dtlItem.FindControl("lblDLRefSubject_Name");
                Label lblDLChapter_Name = (Label)dtlItem.FindControl("lblDLChapter_Name");
                Label lblDLTopic_Name = (Label)dtlItem.FindControl("lblDLTopic_Name");
                Label lblDLSubTopic_Name = (Label)dtlItem.FindControl("lblDLSubTopic_Name");
                Label lblDLModule_Name = (Label)dtlItem.FindControl("lblDLModule_Name");
                Label lblquetypename = (Label)dtlItem.FindControl("lblquetypename");
                Label lblResult = (Label)dtlItem.FindControl("lblResult");

                if (lblQueNo.Text.Trim() == Que_No)
                {
                    //Everything is OK hence add row in correct Datatable
                    NewRow = dtCorrectEntry.NewRow();
                    NewRow["Question_No"] = Que_No;
                    NewRow["Question_Type"] = Que_Type;
                    NewRow["Correct_Ans_Key"] = Answer_Key;
                    NewRow["DiffLevel_Name"] = Difficulti_Level;
                    NewRow["DiffLevel_Id"] = DiffLevel_Id;
                    NewRow["Correct_Marks"] = Correct_Marks;
                    NewRow["Wrong_Marks"] = Wrong_Marks;
                    NewRow["Subject_Name"] = Subject;
                    NewRow["Subject_Code"] = Subject_Code;
                    NewRow["RefCourse_Name"] = RefCourse;
                    NewRow["RefCourse_Code"] = RefCourse_Code;
                    NewRow["RefSubject_Name"] = RefSubject;
                    NewRow["RefSubject_Code"] = RefSubject_Code;
                    NewRow["Chapter_Name"] = Chapter;
                    NewRow["Chapter_Code"] = Chapter_Code;
                    NewRow["Topic_Name"] = Topic;
                    NewRow["Topic_Code"] = Topic_Code;
                    NewRow["SubTopic_Name"] = SubTopic;
                    NewRow["SubTopic_Code"] = SubTopic_Code;
                    NewRow["Module_Name"] = Module;
                    NewRow["Module_Code"] = Module_Code;
                    NewRow["Question_Type_Id"] = Question_Type_Id;
                    if (RefCourseVal != "" && RefCourse == "")
                        NewRow["ErrrorMessage"] = "Entered reference course not found";
                    else if (RefSubjectVal != "" && RefSubject == "")
                        NewRow["ErrrorMessage"] = "Entered reference subject not found";
                    else if (ChapterVal != "" && Chapter == "")
                        NewRow["ErrrorMessage"] = "Entered chapter not found";
                    else if (TopicVal != "" && Topic == "")
                        NewRow["ErrrorMessage"] = "Entered Topic not found";
                    else if (SubTopicVal != "" && SubTopic == "")
                        NewRow["ErrrorMessage"] = "Entered SubTopic not found";
                    else if (ModuleVal != "" && Module == "")
                        NewRow["ErrrorMessage"] = "Entered Module not found";
                    else
                        NewRow["ErrrorMessage"] = "";

                    dtCorrectEntry.Rows.Add(NewRow);
                }

            }




        NextCSVLine:


            data = reader.GetCSVLine();
            //Read next line
            CurRowNo = CurRowNo + 1;
        }


        for (int cnt = 0; cnt <= dtCorrectEntry.Rows.Count - 1; cnt++)
        {
            string dtQueNo = dtCorrectEntry.Rows[cnt]["Question_No"].ToString();
            string dtQueType = dtCorrectEntry.Rows[cnt]["Question_Type"].ToString();
            string dtAnswer = dtCorrectEntry.Rows[cnt]["Correct_Ans_Key"].ToString();
            string dtDiffName = dtCorrectEntry.Rows[cnt]["DiffLevel_Name"].ToString();
            string dtDiffCode = dtCorrectEntry.Rows[cnt]["DiffLevel_Id"].ToString();
            string dtCorrMarks = dtCorrectEntry.Rows[cnt]["Correct_Marks"].ToString();
            string dtWrgMarks = dtCorrectEntry.Rows[cnt]["Wrong_Marks"].ToString();
            string dtSubName = dtCorrectEntry.Rows[cnt]["Subject_Name"].ToString();
            string dtSubCode = dtCorrectEntry.Rows[cnt]["Subject_Code"].ToString();
            string dtRefCourse = dtCorrectEntry.Rows[cnt]["RefCourse_Name"].ToString();
            string dtRefCourseCode = dtCorrectEntry.Rows[cnt]["RefCourse_Code"].ToString();
            string dtRefSubName = dtCorrectEntry.Rows[cnt]["RefSubject_Name"].ToString();
            string dtRefSubCode = dtCorrectEntry.Rows[cnt]["RefSubject_Code"].ToString();
            string dtChapName = dtCorrectEntry.Rows[cnt]["Chapter_Name"].ToString();
            string dtChapCode = dtCorrectEntry.Rows[cnt]["Chapter_Code"].ToString();
            string dtTopiName = dtCorrectEntry.Rows[cnt]["Topic_Name"].ToString();
            string dtTopiCode = dtCorrectEntry.Rows[cnt]["Topic_Code"].ToString();
            string dtSubTopiName = dtCorrectEntry.Rows[cnt]["SubTopic_Name"].ToString();
            string dtSubTopiCode = dtCorrectEntry.Rows[cnt]["SubTopic_Code"].ToString();
            string dtModuleName = dtCorrectEntry.Rows[cnt]["Module_Name"].ToString();
            string dtModuleCode = dtCorrectEntry.Rows[cnt]["Module_Code"].ToString();
            string dtQueTypeName = dtCorrectEntry.Rows[cnt]["Question_Type"].ToString();
            string dtQueTypeId = dtCorrectEntry.Rows[cnt]["Question_Type_Id"].ToString();
            string dtErrorMessage = dtCorrectEntry.Rows[cnt]["ErrrorMessage"].ToString();

            foreach (DataListItem dtlItem in dlQuestion.Items)
            {
                Label lblQueNo = (Label)dtlItem.FindControl("lblQueNo");

                TextBox txtDLAnswerKey = (TextBox)dtlItem.FindControl("txtDLAnswerKey");
                Label lblDLAnswerKey_Name = (Label)dtlItem.FindControl("lblDLAnswerKey_Name");

                Label lblDLDiffLevel = (Label)dtlItem.FindControl("lblDLDiffLevel");
                Label lblDLDiffLevel_Name = (Label)dtlItem.FindControl("lblDLDiffLevel_Name");
                DropDownList ddlDLDiffLevel = (DropDownList)dtlItem.FindControl("ddlDLDiffLevel");

                TextBox txtDLCorrectMarks = (TextBox)dtlItem.FindControl("txtDLCorrectMarks");
                Label lblDLCorrectMarks_Name = (Label)dtlItem.FindControl("lblDLCorrectMarks_Name");

                TextBox txtDLWrongMarks = (TextBox)dtlItem.FindControl("txtDLWrongMarks");
                Label lblDLWrongMarks = (Label)dtlItem.FindControl("lblDLWrongMarks");

                DropDownList ddlDLSubject = (DropDownList)dtlItem.FindControl("ddlDLSubject");
                Label lblDLSubject_Code = (Label)dtlItem.FindControl("lblDLSubject_Code");
                Label lblDLSubject_Name = (Label)dtlItem.FindControl("lblDLSubject_Name");

                DropDownList ddlDLRefCouse = (DropDownList)dtlItem.FindControl("ddlDLRefCouse");
                Label lblDLRefCourse_Code = (Label)dtlItem.FindControl("lblDLRefCourse_Code");
                Label lblDLRefCourse_Name = (Label)dtlItem.FindControl("lblDLRefCourse_Name");

                DropDownList ddlDLRefSubject = (DropDownList)dtlItem.FindControl("ddlDLRefSubject");
                Label lblDLRefSubject_Code = (Label)dtlItem.FindControl("lblDLRefSubject_Code");
                Label lblDLRefSubject_Name = (Label)dtlItem.FindControl("lblDLRefSubject_Name");

                DropDownList ddlDLChapter = (DropDownList)dtlItem.FindControl("ddlDLChapter");
                Label lblDLChapter_Code = (Label)dtlItem.FindControl("lblDLChapter_Code");
                Label lblDLChapter_Name = (Label)dtlItem.FindControl("lblDLChapter_Name");

                DropDownList ddlDLTopic = (DropDownList)dtlItem.FindControl("ddlDLTopic");
                Label lblDLTopic_Code = (Label)dtlItem.FindControl("lblDLTopic_Code");
                Label lblDLTopic_Name = (Label)dtlItem.FindControl("lblDLTopic_Name");

                DropDownList ddlDLSubTopic = (DropDownList)dtlItem.FindControl("ddlDLSubTopic");
                Label lblDLSubTopic_Code = (Label)dtlItem.FindControl("lblDLSubTopic_Code");
                Label lblDLSubTopic_Name = (Label)dtlItem.FindControl("lblDLSubTopic_Name");

                DropDownList ddlDLModul = (DropDownList)dtlItem.FindControl("ddlDLModul");
                Label lblDLModule_Code = (Label)dtlItem.FindControl("lblDLModule_Code");
                Label lblDLModule_Name = (Label)dtlItem.FindControl("lblDLModule_Name");

                DropDownList ddlDLQueType = (DropDownList)dtlItem.FindControl("ddlDLQueType");
                Label lblquetypeid = (Label)dtlItem.FindControl("lblquetypeid");
                Label lblquetypename = (Label)dtlItem.FindControl("lblquetypename");

                Label lblResult = (Label)dtlItem.FindControl("lblResult");

                if (Convert.ToString(lblQueNo.Text).Trim() == dtQueNo)
                {
                    txtDLAnswerKey.Text = dtAnswer;
                    lblDLAnswerKey_Name.Text = dtAnswer;

                    lblDLDiffLevel.Text = dtDiffCode;
                    lblDLDiffLevel_Name.Text = dtDiffName;
                    ddlDLDiffLevel.SelectedValue = dtDiffCode;

                    txtDLCorrectMarks.Text = dtCorrMarks;
                    lblDLCorrectMarks_Name.Text = dtCorrMarks;

                    txtDLWrongMarks.Text = dtWrgMarks;
                    lblDLWrongMarks.Text = dtWrgMarks;

                    ddlDLSubject.SelectedValue = dtSubCode;
                    lblDLSubject_Code.Text = dtSubCode;
                    lblDLSubject_Name.Text = dtSubName;

                    ddlDLRefCouse.SelectedValue = dtRefCourseCode;
                    lblDLRefCourse_Code.Text = dtRefCourseCode;
                    lblDLRefCourse_Name.Text = dtRefCourse;

                    //ddlDLRefCouse_SelectedIndexChanged(sender, e);

                    //ddlDLRefSubject.SelectedValue = dtRefSubCode;
                    lblDLRefSubject_Code.Text = dtRefSubCode;
                    lblDLRefSubject_Name.Text = dtRefSubName;

                    //ddlRefSubject_SelectedIndexChanged(sender, e);

                    // ddlDLChapter.SelectedValue = dtChapCode;
                    lblDLChapter_Code.Text = dtChapCode;
                    lblDLChapter_Name.Text = dtChapName;

                    // ddlChapter_SelectedIndexChanged(sender, e);

                    //ddlDLTopic.SelectedValue = dtTopiCode;
                    lblDLTopic_Code.Text = dtTopiCode;
                    lblDLTopic_Name.Text = dtTopiName;

                    //ddlDLTopic_SelectedIndexChanged(sender, e);

                    //ddlDLSubTopic.SelectedValue = dtSubTopiCode;
                    lblDLSubTopic_Code.Text = dtSubTopiCode;
                    lblDLSubTopic_Name.Text = dtSubTopiName;

                    //ddlDLSubTopic_SelectedIndexChanged(sender, e);

                    //ddlDLModul.SelectedValue = dtModuleCode;
                    lblDLModule_Code.Text = dtModuleCode;
                    lblDLModule_Name.Text = dtModuleName;

                    ddlDLQueType.SelectedValue = dtQueTypeId;
                    lblquetypeid.Text = dtQueTypeId;
                    lblquetypename.Text = dtQueTypeName;

                    lblResult.Text = dtErrorMessage;
                    lblResult.ForeColor = System.Drawing.Color.Red;

                    break; // TODO: might not be correct. Was : Exit For
                }
            }

        }

    }


    protected void HLExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;
        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=Test_QPSet.xls");


        Response.Charset = "";


        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

        dlGridExport.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();
        dlGridExport.Visible = false;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the specified ASP.NET
        //     server control at run time. 

    }

    protected void ddlQPSetNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string PKey = null;
        PKey = lblPKey_Edit.Text + "%" + ddlQPSetNo.SelectedValue;

        tdonline.Visible = false;
        tblUploadFile.Visible = false;
        if (ddlQPSetNo.SelectedIndex >= 0)
        {
            DataSet dsTest = ProductController.GetTestMasterBY_PKey(PKey, 6);
            if (dsTest.Tables[0].Rows.Count > 0)
            {
                if (dsTest.Tables[0].Rows[0]["Assesment_Engine_Test_Code"].ToString() != "")
                {
                    txtassesmenttestcode.Text = dsTest.Tables[0].Rows[0]["Assesment_Engine_Test_Code"].ToString();
                    txtassesmenttestcode.Enabled = false;
                    tdonline.Visible = true;
                }
                else
                {
                    txtassesmenttestcode.Text = "";
                    txtassesmenttestcode.Enabled = true;
                    tdonline.Visible = false;
                }
            }
            else
            {
                txtassesmenttestcode.Text = "";
                txtassesmenttestcode.Enabled = true;
                tdonline.Visible = false;
            }
        }
        //  btnShowQPSetDetails_Click(sender, e);
    }
    protected void btnsavveonlinetest_Click(object sender, EventArgs e)
    {
        if (txtassesmenttestcode.Text == "")
        {
            Show_Error_Success_Box("E", "Assesment Test Code Not Found");
            return;
        }
        DataSet dsTest = ProductController.Process_Online_Test_Details(txtassesmenttestcode.Text, 1);

        if (dsTest.Tables[0].Rows[0]["Status"].ToString() == "1")
        {
            Show_Error_Success_Box("S", "Records Processed Sucessfully");
        }
    }
}
