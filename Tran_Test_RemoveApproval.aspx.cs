using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI;
using System.Web.UI.WebControls;

partial class Tran_Test_RemoveApproval : System.Web.UI.Page
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
            FillDDL_Action();
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
    }

    private void FillDDL_AcadYear()
    {
        DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcadYear, dsAcadYear, "Description", "Id");
        ddlAcadYear.Items.Insert(0, "Select");
        ddlAcadYear.SelectedIndex = 0;
    }

    private void FillDDL_Action()
    {
        ddlAction.Items.Insert(0, "");
        ddlAction.Items.Insert(1, "Approve");
        ddlAction.Items.Insert(2, "Reject");
        ddlAction.SelectedIndex = 0;
    }

    private void FillDDL_TestCategories()
    {
        DataSet dsTestCategory = ProductController.GetAllActiveTestCategory();
        //BindDDL(ddlTestCategory, dsTestCategory, "TestCategory_Name", "TestCategory_Id");
        //ddlTestCategory.Items.Insert(0, "Select");
        //ddlTestCategory.SelectedIndex = 0;
        BindListBox(ddlTestCategory, dsTestCategory, "TestCategory_Name", "TestCategory_Id");
    }

    private void FillDDL_TestTypes()
    {
        DataSet dsTestType = ProductController.GetAllActiveTestType();
        BindListBox(ddlTestType, dsTestType, "TestType_Name", "TestType_Id");

    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
        Clear_Error_Success_Box();
    }
    protected void ddlAcadYear_SelectedIndexChanged(object sender, System.EventArgs e)
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
        //BindDDL(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");
        //ddlStandard.Items.Insert(0, "Select");
        //ddlStandard.SelectedIndex = 0;
        BindListBox(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");

    }

    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }

    protected void BtnClearSearch_Click(object sender, System.EventArgs e)
    {
        txtTestName.Text = "";
        ddlDivision.SelectedIndex = 0;
        ddlAcadYear.SelectedIndex = 0;
        
       // ddlTestCategory.SelectedIndex = 0;
        ddlStandard.Items.Clear();
        ddlTestType.Items.Clear();
        id_date_range_picker_1.Value = "";
        Msg_Error.Visible = false;
        lblerror.Text = "";
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        FillDDL_TestCategories();
    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    
    protected void BtnSearch_Click(object sender, System.EventArgs e)
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
        string Course_Code = "", TestCategoryCode = "";
        for (int TypeCnt1 = 0; TypeCnt1 <= ddlStandard.Items.Count - 1; TypeCnt1++)
        {
            if (ddlStandard.Items[TypeCnt1].Selected == true)
            {
                Course_Code = Course_Code + ddlStandard.Items[TypeCnt1].Value + ","; ;
            }
        }

        //if (ddlStandard.SelectedIndex == 0)
        //{
        //    Show_Error_Success_Box("E", "0003");
        //    ddlStandard.Focus();
        //    return;
        //}
        if (Course_Code == "")
        {
            Show_Error_Success_Box("E", "Select atleast one course");
            return;
        }

        for (int TypeCnt1 = 0; TypeCnt1 <= ddlTestCategory.Items.Count - 1; TypeCnt1++)
        {
            if (ddlTestCategory.Items[TypeCnt1].Selected == true)
            {
                TestCategoryCode = TestCategoryCode + ddlTestCategory.Items[TypeCnt1].Value + ","; ;
            }
        }

        if (TestCategoryCode == "")
        {
            Show_Error_Success_Box("E", "Select atleast one test category");
            return;
        }

        //if (ddlTestCategory.SelectedIndex == 0)
        //{
        //    Show_Error_Success_Box("E", "0012");
        //    ddlTestCategory.Focus();
        //    return;
        //}

        string BatchCode = "";
        int BatchCnt = 0;
        int BatchSelCnt = 0;


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
        }

        ControlVisibility("Result");

        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = Course_Code;

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

        string FromDate = "";
        string ToDate = "";
        if (DateRange != "")
        {
            FromDate  = (DateRange.Substring(0, DateRange.Length-13)).ToString();
            ToDate = DateRange.Substring(DateRange.Length - 10);
        }
        //FromDate = Strings.Left(DateRange, 10);
        //if (string.IsNullOrEmpty(FromDate))
        //{
        //    FromDate = System.DateTime.Now.ToString("dd MMM yyyy");
        //}
        //if (DateRange != "")
        //{
        //    ToDate = DateRange.Substring(DateRange.Length - 10);
        //}
        ////ToDate = Strings.Right(DateRange, 10);
        //if (string.IsNullOrEmpty(ToDate))
        //{
        //    ToDate = System.DateTime.Now.ToString("dd MMM yyyy");
        //}

        DataSet dsGrid = ProductController.GetTestForCancellationBy_Division_Year_Standard(DivisionCode, YearName, StandardCode, "01", TestCategoryCode, TestType_ID, TestName, FromDate, ToDate, 1);
        dlGridDisplay.DataSource = dsGrid;
        dlGridDisplay.DataBind();
        lbltotalcount.Text =Convert.ToString(dsGrid.Tables[0].Rows.Count);
        dlGridExport.DataSource = dsGrid;
        dlGridExport.DataBind();
    }


    protected void btnReqApr_Click(object sender, System.EventArgs e)
    {
        if (ddlAction.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0066");

            return;
        }
        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;
        int ResultId = 0;
        ResultId = ProductController.UpdateTestCancellation_Authorise(lbldelCode.Text, ddlAction.SelectedIndex, txtRemoveReason.Text, CreatedBy);

        //Close the Add Panel and go to Search Grid
        if (ResultId == 1)
        {
            ControlVisibility("Result");
            BtnSearch_Click(sender, e);
            if (ddlAction.SelectedIndex == 1)
            {
                Show_Error_Success_Box("S", "0064");
            }
            else
            {
                Show_Error_Success_Box("S", "0065");
            }
        }
    }

    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        if (e.CommandName == "Aprove")
        {
            lbldelCode.Text = e.CommandArgument.ToString();
            //txtDeleteItemName.Text = (DirectCast(e.Item.FindControl("lblModeName"), Label).Text)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalRemoveTestAprove();", true);
        }
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
        else if (Mode == "Add")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;

        }
        else if (Mode == "Edit")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;

        }
        Clear_Error_Success_Box();
    }
    public Tran_Test_RemoveApproval()
    {
        Load += Page_Load;
    }


   
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the specified ASP.NET
        //     server control at run time. 

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;
        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=Test_RemoveApproval.xls");


        Response.Charset = "";


        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

        dlGridExport.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();
        dlGridExport.Visible = false;
    }
}
