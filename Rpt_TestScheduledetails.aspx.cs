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
using System.Web;




partial class Rpt_TestScheduledetails : System.Web.UI.Page
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            // txtTestDate_Add.Value = System.DateTime.Now.ToString("dd MMM yyyy");
            FillDDL_Division();
            FillDDL_AcadYear();
            FillDDL_TestCategories();
            FillDDL_TestTypes();
            divPrint.Visible = false;
        }
    }

    private void FillDDL_TestCategories()
    {
        DataSet dsTestCategory = ProductController.GetAllActiveTestCategory();
        BindListBox(ddlTestCategory, dsTestCategory, "TestCategory_Name", "TestCategory_Id");
        ddlTestCategory.Items.Insert(0, "All");


    }

    private void FillDDL_TestTypes()
    {
        DataSet dsTestType = ProductController.GetAllActiveTestType();
        BindListBox(ddlTestType, dsTestType, "TestType_Name", "TestType_Id");
        ddlTestType.Items.Insert(0, "All");

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
            BtnAdd.Visible = false;
            divPrint.Visible = false;

        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
            divPrint.Visible = false;

        }

        Clear_Error_Success_Box();
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
        BindListBox(ddlCentre, dsCentre, "Center_Name", "Center_Code");

        ddlCentre.Items.Insert(0, "All");
        //ddlCentre.SelectedIndex = " ";


    }

    private void FillDDL_Standard()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        DataSet dsStandard = ProductController.GetAllActive_Standard_ForYear(Div_Code, YearName);
        BindListBox(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");
        //BindDDL(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");
        //ddlStandard.Items.Insert(0, "Select");
        //ddlStandard.SelectedIndex = 0;
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
        FillDDL_Centre_Add();
        Clear_Error_Success_Box();
    }



    protected void ddlDivision_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
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



        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, "", "", "5", lblHeader_DBName.Text);

    }

    protected void ddlStandard_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
    }

    protected void ddlBatch_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
    }

    protected void ddlTestName_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        //string PKey = null;

        Clear_TestDetails();
        //FillTestMasterDetails(PKey);
    }

    private void Clear_AddPanel()
    {

        //ddlBatch_Add.Items.Clear();
        //lstboxbatch.Items.Clear();


        Clear_TestDetails();
    }

    private void Clear_TestDetails()
    {

    }


    protected void ddlStandard_SelectedIndexChanged(object sender, System.EventArgs e)
    {

        FillDDL_Batch();
        Clear_Error_Success_Box();

    }

    protected void ddlCentre_SelectedIndexChanged(object sender, System.EventArgs e)
    {

        int count = ddlCentre.GetSelectedIndices().Length;

        if (ddlCentre.SelectedValue == "All")
        {
            ddlCentre.Items.Clear();
            ddlCentre.Items.Insert(0, "All");
            ddlCentre.SelectedIndex = 0;
            FillDDL_Standard();

        }
        else if (count == 0)
        {

            FillDDL_Search_Centre();
            Clear_Error_Success_Box();
        }
        FillDDL_Batch();
    }

    protected void ddlTestCategory_SelectedIndexChanged(object sender, System.EventArgs e)
    {

        int count = ddlTestCategory.GetSelectedIndices().Length;

        if (ddlTestCategory.SelectedValue == "All")
        {
            ddlTestCategory.Items.Clear();
            ddlTestCategory.Items.Insert(0, "All");
            ddlTestCategory.SelectedIndex = 0;
            FillDDL_TestTypes();

        }
        else if (count == 0)
        {

            FillDDL_TestCategories();
            Clear_Error_Success_Box();
        }

    }

    protected void ddlTestType_SelectedIndexChanged(object sender, System.EventArgs e)
    {

        int count = ddlTestType.GetSelectedIndices().Length;

        if (ddlTestType.SelectedValue == "All")
        {
            ddlTestType.Items.Clear();
            ddlTestType.Items.Insert(0, "All");
            ddlTestType.SelectedIndex = 0;


        }
        else if (count == 0)
        {

            FillDDL_TestTypes();
            Clear_Error_Success_Box();
        }

    }

    protected void ddlBatch_SelectedIndexChanged(object sender, System.EventArgs e)
    {

        int count = ddlBatch.GetSelectedIndices().Length;

        if (ddlBatch.SelectedValue == "All")
        {
            ddlBatch.Items.Clear();
            ddlBatch.Items.Insert(0, "All");
            ddlBatch.SelectedIndex = 0;

        }
        else if (count == 0)
        {

            FillDDL_Batch();
            Clear_Error_Success_Box();
        }


    }


    private void FillDDL_Batch()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string StandardCode = "";
        //StandardCode = ddlStandard.SelectedValue;

        string CentreCode = "";
        //CentreCode = ddlCentre.SelectedValue;
        List<string> list = new List<string>();
        List<string> list1 = new List<string>();

        foreach (ListItem li in ddlCentre.Items)
        {
            if (li.Selected == true)
            {
                list.Add(li.Value);
                CentreCode = string.Join(",", list.ToArray());
            }
        }

        foreach (ListItem li in ddlStandard.Items)
        {
            if (li.Selected == true)
            {
                list1.Add(li.Value);
                StandardCode = string.Join(",", list1.ToArray());
            }
        }


        DataSet dsBatch = ProductController.GetAllActive_Batch_ForStandard(Div_Code, YearName, StandardCode, CentreCode,"2");
        BindListBox(ddlBatch, dsBatch, "Batch_Name", "Batch_Code");
        ddlBatch.Items.Insert(0, "All");


    }


    protected void btnExport_Click(object sender, System.EventArgs e)
    {
        {
            dlGridExport.Visible = true;
            //Response.Clear();
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            string filenamexls1 = "Test Schedule Details" + DateTime.Now + ".xls";
            Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='2'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:left;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='9'>Test Schedule Details </b></TD></TR>");
            Response.Charset = "";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
            //this.ClearControls(dladmissioncount)
            dlGridExport.RenderControl(oHtmlTextWriter1);
            Response.Write(oStringWriter1.ToString());
            Response.Flush();
            Response.End();
            //UpdatePanel1.Update();
        }



        //dlGridExport.Visible = true;
        //Response.Clear();

        //Response.AddHeader("content-disposition", "attachment;filename=Test_Schedule.xls");

        //Response.Charset = "";


        //Response.ContentType = "application/vnd.xls";

        //System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        //dlGridExport.RenderControl(htmlWrite);

        //Response.Write(stringWrite.ToString());

        //Response.End();
        //dlGridExport.Visible = false;
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

        //if (ddlStandard.SelectedIndex == 0)
        //{
        //    Show_Error_Success_Box("E", "0003");
        //    ddlStandard.Focus();
        //    return;
        //}
        List<string> list123 = new List<string>();
        string CourseCode = "";
        foreach (ListItem li in ddlStandard.Items)
        {
            if (li.Selected == true)
            {
                list123.Add(li.Value);
                CourseCode = string.Join(",", list123.ToArray());
            }
        }

        if (CourseCode == "")
        {
            Show_Error_Success_Box("E", "Select atleast one Course");
            return;
        }

        //center Comma Separated
        List<string> list = new List<string>();
        List<string> List1 = new List<string>();
        List<string> List2 = new List<string>();

        string center = "";
        foreach (ListItem li in ddlCentre.Items)
        {
            if (li.Selected == true)
            {
                list.Add(li.Value);
                center = string.Join(",", list.ToArray());
            }
        }
        string centercode = center;
        if (centercode == "")
        {
            centercode = "All";
        }

        List<string> list1 = new List<string>();
        List<string> List11 = new List<string>();
        List<string> List12 = new List<string>();

        string Testcategory = "";
        foreach (ListItem li1 in ddlTestCategory.Items)
        {
            if (li1.Selected == true)
            {
                list1.Add(li1.Value);
                Testcategory = string.Join(",", list1.ToArray());
            }
        }

        string Testcategorycode = Testcategory;
        if (Testcategorycode == "")
        {
            Testcategorycode = "All";
        }


        List<string> list2 = new List<string>();
        List<string> List21 = new List<string>();
        List<string> List22 = new List<string>();
        string batch = "";
        foreach (ListItem li2 in ddlBatch.Items)
        {
            if (li2.Selected == true)
            {
                list2.Add(li2.Value);
                batch = string.Join(",", list2.ToArray());
            }
        }


        string batchcode = batch;
        if (batchcode == "")
        {
            batchcode = "All";
        }


        List<string> list3 = new List<string>();
        List<string> List31 = new List<string>();
        List<string> List32 = new List<string>();
        string TestType = "";

        foreach (ListItem li3 in ddlTestType.Items)
        {
            if (li3.Selected == true)
            {
                list3.Add(li3.Value);
                TestType = string.Join(",", list3.ToArray());
            }
        }


        string TestTypecode = TestType;
        if (TestTypecode == "")
        {
            TestTypecode = "All";
        }


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

        string DivisionCode = ddlDivision.SelectedValue;
        string YearName = ddlAcadYear.SelectedValue;
       // string StandardCode = ddlStandard.SelectedValue;


        DataSet dsGrid = ProductController.rpt_GetTestScheduleBy_Division_Year_Standard_Centre(DivisionCode, YearName, CourseCode, centercode, Testcategorycode, TestTypecode, batchcode, TestName, FromDate, ToDate,
         1);

        //if  (dsGrid.Tables[0].Rows.Count > 0 && dsGrid != null)

        if (dsGrid != null && dsGrid.Tables[0].Rows.Count != 0)
        //if (dsGrid != null || dsGrid.Tables[0].Rows.Count > 0)
        {
            {
                dlGridDisplay.DataBind();
                DataTable table1 = new DataTable();
                table1.Columns.Add("Test_Date");
                table1.Columns.Add("centername");
                table1.Columns.Add("BatchName");
                table1.Columns.Add("TestCategory_Name");
                table1.Columns.Add("testname");
                table1.Columns.Add("Subjects");
                table1.Columns.Add("CHAPTERNAME");
                table1.Columns.Add("marks");
                table1.Columns.Add("TestTimeStr");

                dlGridExport.DataBind();
                dlGridDisplay.DataSource = dsGrid.Tables[0];
                dlGridExport.DataSource = dsGrid.Tables[0];
                dlGridExport.DataBind();

                lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
                //lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
                //lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
                //lblStandard_Result.Text = ddlStandard.SelectedItem.ToString();
                //lblTestCategory_Result.Text = ddlTestCategory.SelectedItem.ToString();
                //lblCentre_Result.Text = ddlCentre.SelectedItem.ToString();


                //lblPrintTestDivision.Text = ddlDivision.SelectedItem.ToString();
                //lblPrintTestAcademicYear.Text = ddlAcadYear.SelectedItem.ToString();
                //lblPrintTestCourse.Text = ddlStandard.SelectedItem.ToString();
                //lblPrintTestCategory.Text = ddlTestCategory.SelectedItem.ToString();
                //lblPrintTestCentre.Text = ddlCentre.SelectedItem.ToString();

                ControlVisibility("Result");

            }
        }
        else
        {
            Msg_Error.Visible = true;
            lblerror.Visible = true;
            lblerror.Text = " Record Not Found.";
        }

    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlAcadYear.SelectedIndex = 0;
        ddlTestCategory.SelectedIndex = 0;
        ddlStandard.Items.Clear();
        ddlTestType.Items.Clear();
        ddlCentre.SelectedIndex = 0;
        ddlTestCategory.SelectedIndex = 0;
        Msg_Error.Visible = false;
        lblerror.Text = "";
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        ddlBatch.Items.Clear();
        txtTestName.Text = "";
        id_date_range_picker_1.Value = "";
    }


}





