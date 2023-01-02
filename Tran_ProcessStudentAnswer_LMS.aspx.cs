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
using System.Net.Http;
using System.Net.Http.Headers;
using LMSIntegration;
using System.Web;

public partial class Tran_Tran_ProcessStudentAnswer_LMS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                ControlVisibility("Search");
                FillDDL_TestTypes();
                FillDDL_Division();
                FillDDL_AcadYear();

            }
            catch (Exception ex)
            {

                Show_Error_Success_Box("E", ex.ToString());
                return;
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
        BindDDL(ddlDivision_Add, dsDivision, "Division_Name", "Division_Code");
        ddlDivision_Add.Items.Insert(0, "Select");
        ddlDivision_Add.SelectedIndex = 0;

    }

    private void FillDDL_AcadYear()
    {
        DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcadYear_Add, dsAcadYear, "Description", "Id");
        ddlAcadYear_Add.Items.Insert(0, "Select");
        ddlAcadYear_Add.SelectedIndex = 0;

    }


    private void FillDDL_TestTypes()
    {
        DataSet dsTestType = ProductController.GetAllActiveTestType();


        BindDDL(ddlTestType_Add, dsTestType, "TestType_Name", "TestType_Id");
        ddlTestType_Add.Items.Insert(0, "Select");
        ddlTestType_Add.SelectedIndex = 0;

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


    protected void ddlDivision_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard_Add();
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

        ddlTestName_Add.Items.Clear();

        icon_NegativeMarking_Add.Visible = false;
    }

    protected void ddlAcadYear_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard_Add();
        Clear_Error_Success_Box();
    }

    protected void ddlStandard_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_TestName_Add();
        Clear_Error_Success_Box();
    }

    private void FillDDL_TestName_Add()
    {
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

        if (ddlStandard_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0003");
            ddlStandard_Add.Focus();
            return;
        }

        if (ddlTestType_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0013");
            ddlTestType_Add.Focus();
            return;
        }

        string DivisionCode = null;
        DivisionCode = ddlDivision_Add.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear_Add.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard_Add.SelectedValue;

        string TestTypeCode = null;
        TestTypeCode = ddlTestType_Add.SelectedValue;

        string TestMode = null;
        TestMode = "01";
        //Offline

        string TestCategory = null;
        TestCategory = "002";
        //MCQ

        DataSet dsTestName = ProductController.GetTestMasterBy_Division_Year_Standard(DivisionCode, YearName, StandardCode, TestMode, TestCategory, TestTypeCode, "%", 2);
        BindDDL(ddlTestName_Add, dsTestName, "Test_Name", "PKey");
        ddlTestName_Add.Items.Insert(0, "Select");
        ddlTestName_Add.SelectedIndex = 0;
    }

    protected void ddlTestType_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_TestName_Add();
        Clear_Error_Success_Box();
    }

    protected void ddlTestName_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillTestMasterDetails(ddlTestName_Add.SelectedValue);
    }

    private void FillTestMasterDetails(string PKey)
    {
        try
        {


            ddlConductNo_Add.Items.Clear();


            icon_NegativeMarking_Add.Visible = false;

            DataSet dsTest = ProductController.GetTestMasterBY_PKey(PKey, 1);

            if (dsTest.Tables[0].Rows.Count > 0)
            {
                //lblSubject_Add.Text = dsTest.Tables(0).Rows(0)("Subjects")


                int ConductCount = 0;
                ConductCount = Convert.ToInt32(dsTest.Tables[3].Rows[0]["Conduct_No"]);

                if (Convert.ToInt32(dsTest.Tables[0].Rows[0]["NegativeMarkingFlag"]) == 1)
                {
                    icon_NegativeMarking_Add.Visible = true;
                }
                else
                {
                    icon_NegativeMarking_Add.Visible = false;
                }


                for (int cnt = 1; cnt <= ConductCount; cnt++)
                {
                    ddlConductNo_Add.Items.Add(Convert.ToString(cnt));
                }


            }
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        if (ddlDivision_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Division");
            ddlDivision_Add.Focus();
            return;

        }

        if (ddlAcadYear_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Acad Year");
            ddlAcadYear_Add.Focus();
            return;
        }

        if (ddlStandard_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Course");
            ddlStandard_Add.Focus();
            return;
        }

        if (ddlTestType_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Test Type");
            ddlTestType_Add.Focus();
            return;
        }

        if (ddlTestName_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Test Name");
            ddlTestName_Add.Focus();
            return;
        }

        if (ddlConductNo_Add.Items.Count == 0)
        {
            Show_Error_Success_Box("E", "0028");
            ddlConductNo_Add.Focus();
            return;
        }

        int Conduct_No = 0;
        Conduct_No = Convert.ToInt32(ddlConductNo_Add.SelectedItem.ToString());

        string PKey = null;
        PKey = ddlTestName_Add.SelectedValue;

        DataSet dsTest1 = ProductController.GetTestPresentStudent_ByPKey(PKey, Conduct_No, 2);
        if (dsTest1.Tables[0].Rows.Count > 0)
        {
            ControlVisibility("Result");
            lbltotalcount.Text = dsTest1.Tables[0].Rows.Count.ToString();
            dlGridDisplay.DataSource = dsTest1;
            dlGridDisplay.DataBind();
        }

        else
        {
            ControlVisibility("Search");
            Show_Error_Success_Box("E", "No Record Found For Selected Criteria");

        }

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (DataListItem dtlItem in dlGridDisplay.Items)
            {

                Label lblsbentrycode = (Label)dtlItem.FindControl("lblsbentrycode");
                Label lblanswerkey = (Label)dtlItem.FindControl("lblanswerkey");
                Label lblcentercode = (Label)dtlItem.FindControl("lblcentercode");
                Label lblSetNumber = (Label)dtlItem.FindControl("lblSetNumber");
                Label lblstudentcode = (Label)dtlItem.FindControl("lblstudentcode");
                Label lbltestcode = (Label)dtlItem.FindControl("lbltestcode");
                Label lblbatchcode = (Label)dtlItem.FindControl("lblbatchcode");
                Label lblcoursecode = (Label)dtlItem.FindControl("lblcoursecode");
                Label lblteststardatetime = (Label)dtlItem.FindControl("lblteststardatetime");
                Label lblrollno = (Label)dtlItem.FindControl("lblrollno");
                Label lblproductcode = (Label)dtlItem.FindControl("lblproductcode");
                Label lblassesmentcode = (Label)dtlItem.FindControl("lblassesmentcode");


                Send_Details_LMS(lblstudentcode.Text, lblrollno.Text, lblassesmentcode.Text, lblcentercode.Text, lblbatchcode.Text, lblproductcode.Text,
                lblcoursecode.Text, lblteststardatetime.Text, lblanswerkey.Text, lblsbentrycode.Text, lbltestcode.Text);
            }

            BtnSearch_Click(sender, e);
        }
        catch (Exception ex)
        {
            BtnSearch_Click(sender, e);
            Show_Error_Success_Box("E", "Something Went Wrong Kindly Post Again");
        }
    }

    private void Send_Details_LMS(string Student_Code, string Roll_Number, string Test_Code, string Center_Code, string Batch_Code,
        string Product_Code, string Course_Code, string Test_Start_Date_Time, string Awnser_Key, string SBEntryCode, string Test_Code_OE)
    {
        string Response_Status_Code = "";
        string Response_Return_Phrase = "";
        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        //DateTime Test_Start_Date_Time_DT = DateTime.Now;

        try
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(DBConnection.connStringLMS);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var testdetails = new testdetails();
            testdetails.StudentCode = Student_Code;
            testdetails.RollNumber = Roll_Number;
            testdetails.TestCode = Test_Code;
            testdetails.CenterCode = Center_Code;
            testdetails.BatchCode = Batch_Code;
            testdetails.ProductCode = Product_Code;
            testdetails.CourseCode = Course_Code;
            testdetails.TestStartDateTime = Test_Start_Date_Time;

            string[] answers = Awnser_Key.Split(',');

            DataTable userresponses = new DataTable("userresponses");
            userresponses.Columns.Add("qid");
            userresponses.Columns.Add("res");
            DataRow NewRow = null;
            int i = 0;
            foreach (string s in answers)
            {
                i = i + 1;

                NewRow = userresponses.NewRow();
                NewRow["qid"] = "Q" + i;
                NewRow["res"] = s;
                userresponses.Rows.Add(NewRow);

            }
            testdetails.userresponse = userresponses;


            var response = client.PostAsJsonAsync("OfflineTest/AddStudentResponse", testdetails).Result;

            Response_Status_Code = response.StatusCode.ToString();
            Response_Return_Phrase = response.ReasonPhrase;

            if (response.StatusCode.ToString() == "OK")
            {


                DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(2, 1, SBEntryCode + '%' + Test_Code_OE + '%' + ddlStandard_Add.SelectedValue + '%' + Batch_Code
                + '%' + Center_Code + '%' + ddlConductNo_Add.SelectedValue,
                response.StatusCode.ToString(), response.ReasonPhrase, UserID);


            }
            else
            {
                DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(2, -1, SBEntryCode + '%' + Test_Code_OE + '%' + ddlStandard_Add.SelectedValue + '%' + Batch_Code
                + '%' + Center_Code + '%' + ddlConductNo_Add.SelectedValue,
                response.StatusCode.ToString(), response.ReasonPhrase, UserID);
            }




        }
        catch (Exception e)
        {
            Show_Error_Success_Box("E", e.ToString());

            DataSet dsreturn = ProductController.UPDATE_DBSYNCFLAG_LMSSERVICE(2, -1, SBEntryCode + '%' + Test_Code_OE + '%' + ddlStandard_Add.SelectedValue + '%' + Batch_Code
                    + '%' + Center_Code + '%' + ddlConductNo_Add.SelectedValue,

            Response_Status_Code, Response_Return_Phrase, UserID);
        }
    }
    //class user_response
    //{
    //    public List<string> qid { get; set; }
    //    public List<string> res { get; set; }

    //}
    class testdetails
    {
        public string StudentCode { get; set; }
        public string RollNumber { get; set; }
        public string TestCode { get; set; }
        public string CenterCode { get; set; }
        public string BatchCode { get; set; }
        public string ProductCode { get; set; }
        public string CourseCode { get; set; }
        public string TestStartDateTime { get; set; }
        public DataTable userresponse { get; set; }
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision_Add.SelectedIndex = 0;
        ddlAcadYear_Add.SelectedIndex = 0;
        ddlStandard_Add.Items.Clear();
        ddlTestType_Add.SelectedIndex = 0;
        ddlTestName_Add.Items.Clear();
        ddlConductNo_Add.Items.Clear();
    }
    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }
}