using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI;

partial class UserDashboard_Level1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            string Mode = null;
            Mode = Request.QueryString["Mode"];

            ControlVisibility(Mode);
        }
    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "1")
        {
            DivResult_TodaysTest.Visible = true;
            DivResult_AttendAuthorise.Visible = false;
            DivResult_MarksAuthorise.Visible = false;
            DivResult_TestCancellation.Visible = false;
            DivResult_ReTest.Visible = false;

            lblHead_PageName.Text = "Todays Test";
            ShowTodaysTests();
        }
        else if (Mode == "2")
        {
            DivResult_TodaysTest.Visible = false;
            DivResult_AttendAuthorise.Visible = true;
            DivResult_MarksAuthorise.Visible = false;
            DivResult_TestCancellation.Visible = false;
            DivResult_ReTest.Visible = false;

            lblHead_PageName.Text = "Pending Attendance Authorisations";
            ShowPendingAttendAuthorisation();
        }
        else if (Mode == "3")
        {
            DivResult_TodaysTest.Visible = false;
            DivResult_AttendAuthorise.Visible = false;
            DivResult_MarksAuthorise.Visible = true;
            DivResult_TestCancellation.Visible = false;
            DivResult_ReTest.Visible = false;

            lblHead_PageName.Text = "Pending Marks Authorisations";
            ShowPendingMarksAuthorisation();
        }
        else if (Mode == "4")
        {
            DivResult_TodaysTest.Visible = false;
            DivResult_AttendAuthorise.Visible = false;
            DivResult_MarksAuthorise.Visible = false;
            DivResult_TestCancellation.Visible = true;
            DivResult_ReTest.Visible = false;

            lblHead_PageName.Text = "Pending Test Cancellation Authorisations";
            ShowPendingTestCancellation();
        }
        else if (Mode == "5")
        {
            DivResult_TodaysTest.Visible = false;
            DivResult_AttendAuthorise.Visible = false;
            DivResult_MarksAuthorise.Visible = false;
            DivResult_TestCancellation.Visible = false;
            DivResult_ReTest.Visible = true;

            lblHead_PageName.Text = "ReTest";
            ShowReTestDetail();
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


    protected void BtnClose_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("UserDashboard.aspx");
    }

    protected void btnExport_TodaysTest_Click(object sender, System.EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=DashboardTodaysTests.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        dlGrid_TodaysTest.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }

    private void ShowTodaysTests()
    {
        string FromDate = null;
        string ToDate = null;
        FromDate = System.DateTime.Now.ToString("dd MMM yyyy");
        ToDate = System.DateTime.Now.ToString("dd MMM yyyy");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        DataSet dsGrid = ProductController.Dashboard_TestSchedule(lblHeader_Company_Code.Text, lblHeader_User_Code.Text, FromDate, ToDate, lblHeader_DBName.Text, "1");

        dlGrid_TodaysTest.DataSource = dsGrid;
        dlGrid_TodaysTest.DataBind();
        lbltotalcount_TodaysTest.Text =Convert.ToString(dsGrid.Tables[0].Rows.Count);
    }

    private void ShowPendingAttendAuthorisation()
    {
        string FromDate = null;
        string ToDate = null;
        FromDate = System.DateTime.Now.ToString("dd MMM yyyy");
        ToDate = System.DateTime.Now.ToString("dd MMM yyyy");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        DataSet dsGrid = ProductController.Dashboard_PendingAttendAuthorisation(lblHeader_Company_Code.Text, lblHeader_User_Code.Text, FromDate, ToDate, lblHeader_DBName.Text, "1");

        dlGrid_AttendAuthorise.DataSource = dsGrid;
        dlGrid_AttendAuthorise.DataBind();
        lblTotalCount_AttendAuthorise.Text =Convert.ToString(dsGrid.Tables[0].Rows.Count);
    }

    private void ShowPendingMarksAuthorisation()
    {
        string FromDate = null;
        string ToDate = null;
        FromDate = System.DateTime.Now.ToString("dd MMM yyyy");
        ToDate = System.DateTime.Now.ToString("dd MMM yyyy");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        DataSet dsGrid = ProductController.Dashboard_PendingMarksAuthorisation(lblHeader_Company_Code.Text, lblHeader_User_Code.Text, FromDate, ToDate, lblHeader_DBName.Text, "1");

        dlGrid_MarksAuthorise.DataSource = dsGrid;
        dlGrid_MarksAuthorise.DataBind();
        lblTotalCount_MarksAuthorise.Text =Convert.ToString(dsGrid.Tables[0].Rows.Count);
    }


    private void ShowPendingTestCancellation()
    {
        string FromDate = null;
        string ToDate = null;
        FromDate = System.DateTime.Now.ToString("dd MMM yyyy");
        ToDate = System.DateTime.Now.ToString("dd MMM yyyy");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        DataSet dsGrid = ProductController.Dashboard_PendingTestCancellationAuthorisation(lblHeader_Company_Code.Text, lblHeader_User_Code.Text, FromDate);

        dlTestCancellation.DataSource = dsGrid;
        dlTestCancellation.DataBind();
        lblTestCancellation.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
    }



    private void ShowReTestDetail()
    {
        string FromDate = null;
        string ToDate = null;
        FromDate = System.DateTime.Now.ToString("dd MMM yyyy");
        ToDate = System.DateTime.Now.ToString("dd MMM yyyy");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        DataSet dsGrid = ProductController.Dashboard_ReTest(lblHeader_User_Code.Text);

        dlReTest.DataSource = dsGrid.Tables[0];
        dlReTest.DataBind();
        lblReTestCount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
    }


    protected void btnExport_AttendAuthorise_Click(object sender, System.EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=DashboardPendingAttendAuthorisation.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        dlGrid_AttendAuthorise.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }

    protected void btnExport_MarksAuthorise_Click(object sender, System.EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=DashboardPendingMarksAuthorisation.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        dlGrid_MarksAuthorise.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }
    public UserDashboard_Level1()
    {
        Load += Page_Load;
    }
    protected void lnkTestCancellation_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=TestCancellationPendingApproval.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        dlTestCancellation.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }
    protected void lnkReTest_Click(object sender, EventArgs e)
    {
        Response.Clear();
        string filenamexls1 = "ReTestDetail_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        dlReTest.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }
}
