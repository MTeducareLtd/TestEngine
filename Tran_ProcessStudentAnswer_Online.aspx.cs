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
using System.Web;

public partial class Tran_ProcessStudentAnswer_Online : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");

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


    private void FillDDL_TestCategories()
    {
        DataSet dsTestCategory = ProductController.GetAllActiveTestCategory();
        BindDDL(ddlTestCategory, dsTestCategory, "TestCategory_Name", "TestCategory_Id");
        ddlTestCategory.Items.Insert(0, "Select");
        ddlTestCategory.SelectedIndex = 0;

    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
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

        //BindDDL(ddlDivision_Add, dsDivision, "Division_Name", "Division_Code");
        //ddlDivision_Add.Items.Insert(0, "Select");
        //ddlDivision_Add.SelectedIndex = 0;

    }

    private void FillDDL_AcadYear()
    {
        DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcadyear, dsAcadYear, "Description", "Id");
        ddlAcadyear.Items.Insert(0, "Select");
        ddlAcadyear.SelectedIndex = 0;

        //BindDDL(ddlAcadYear_Add, dsAcadYear, "Description", "Id");
        //ddlAcadYear_Add.Items.Insert(0, "Select");
        //ddlAcadYear_Add.SelectedIndex = 0;

    }
    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }
    private void FillDDL_TestTypes()
    {
        DataSet dsTestType = ProductController.GetAllActiveTestType();
        BindListBox(ddlTestType, dsTestType, "TestType_Name", "TestType_Id");

        //BindDDL(ddlTestType_Add, dsTestType, "TestType_Name", "TestType_Id");
        //ddlTestType_Add.Items.Insert(0, "Select");
        //ddlTestType_Add.SelectedIndex = 0;

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
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
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
    protected void ddlAcadYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Standard();
    }


    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        try
        {
            string Pkey = e.CommandArgument.ToString();
            int ResultId = 0;
            int ResultId1 = 0;

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];

            ResultId = ProductController.InsertStudent_Answer_Import_Background_Process(Pkey, UserID);


            if (ResultId == 1)
            {
                ResultId1 = ProductController.AutoMarksClosureForOnlineTest(Pkey, UserID);

                if (ResultId1 == 1)
                {

                    Show_Error_Success_Box("S", "Records Processed Sucessfully");
                }
            }
        }

        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
        }

    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {

        try
        {


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

            //if (ddlTestMode.SelectedIndex == 0)
            //{
            //    Show_Error_Success_Box("E", "0011");
            //    ddlTestMode.Focus();
            //    return;
            //}

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

            DataSet dsGrid = ProductController.GetTestMasterBy_Division_Year_Standard(DivisionCode, YearName, StandardCode, "", ddlTestCategory.SelectedValue, TestType_ID, TestName, 3);
            dlGridDisplay.DataSource = dsGrid;
            dlGridDisplay.DataBind();


            lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }
}