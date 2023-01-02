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


partial class Report_Assessment_Code : System.Web.UI.Page
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
        
        

        string StandardCode = "";
        int StdCnt = 0;
        for (StdCnt = 0; StdCnt <= ddlStandard.Items.Count - 1; StdCnt++)
        {
            if (ddlStandard.Items[StdCnt].Selected == true)
            {
                StandardCode = StandardCode + ddlStandard.Items[StdCnt].Value + ",";
            }
        }

        if (StandardCode == "")
        {
            Show_Error_Success_Box("E", "Select atleast one Course");
            return;
        }
        

        string TestCategory_Id= "";
        if (ddlTestCategory.SelectedIndex != 0)
        {
            TestCategory_Id = ddlTestCategory.SelectedValue;
        }

        string TestId = "";        
        for (StdCnt = 0; StdCnt <= ddlTest.Items.Count - 1; StdCnt++)
        {
            if (ddlTest.Items[StdCnt].Selected == true)
            {
                TestId = TestId + ddlTest.Items[StdCnt].Value + ",";
            }
        }

        string DivisionCode = "";
        DivisionCode = ddlDivision.SelectedValue;

        string YearName = "";
        YearName = ddlAcadyear.SelectedItem.ToString();

        DataSet dsGrid = ProductController.Get_Report_Assessment_Code_Status(DivisionCode, YearName, StandardCode, TestCategory_Id, TestId, 1);
        if (dsGrid != null)
        {
            if (dsGrid.Tables[0].Rows.Count > 0)
            {
                ControlVisibility("Result");
                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();

                lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
            }
            else
            {
                Show_Error_Success_Box("E", "No records found");
                return;
            }
        }
    }

    private void FillDDL_TestName()
    {
        string DivCode = "", AcadYear = "", StandardCode = "", TestCategoryId = "";

        DivCode = ddlDivision.SelectedValue;
        AcadYear = ddlAcadyear.SelectedValue;
        int StdCnt = 0;
        for (StdCnt = 0; StdCnt <= ddlStandard.Items.Count - 1; StdCnt++)
        {
            if (ddlStandard.Items[StdCnt].Selected == true)
            {
                StandardCode = StandardCode + ddlStandard.Items[StdCnt].Value + ",";
            }
        }

        if (ddlTestCategory.SelectedIndex != 0)
        {
            TestCategoryId = ddlTestCategory.SelectedValue;
        }
        DataSet dsTestName = ProductController.GetTestScheduleBy_Division_Year_Standard(DivCode, AcadYear, StandardCode, "", "", TestCategoryId, "", "", "", "", 0, 0, 5, "");
        BindListBox(ddlTest, dsTestName, "Test_Name", "PKey");        
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

            FillDDL_TestCategories();
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
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
        }
        
        Clear_Error_Success_Box();
    }

    //Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
    //    ControlVisibility("Add")
    //End Sub



    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

   
    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_Standard();
        FillDDL_TestName();
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
        Clear_Error_Success_Box();
        FillDDL_Standard();
        FillDDL_TestName();
    }
    
    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }

    public Report_Assessment_Code()
    {
        Load += Page_Load;
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlAcadyear.SelectedIndex = 0;
        ddlTestCategory.SelectedIndex = 0;
        ddlStandard.Items.Clear();
        ddlTest.Items.Clear();
        Msg_Error.Visible = false;
        lblerror.Text = "";
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
}


    protected void HLExport_Click(object sender, EventArgs e)
    {
        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=Assessment_Code_Status_" + DateTime.Now + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        dlGridDisplay.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the specified ASP.NET
        //     server control at run time. 

    }

    protected void ddlTestCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_TestName();
    }
    protected void ddlStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_TestName();
    }
}
