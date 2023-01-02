using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;

partial class Report_Marksheet_Print : System.Web.UI.Page
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
        Response.Clear();

        string Test_ID = null;
        Test_ID = Request.QueryString["Test_Id"];

        string SBEntryCode = null;
        SBEntryCode = Request.QueryString["SBEntryCode"];

      //  DataSet dsGrid = ProductController.Report_Test_MCQ_Test_Subject_Student_Rank(Test_ID, SBEntryCode, 1);
        DataSet dsGrid = null;
        dlGridSummaryReport.DataSource = dsGrid.Tables[0];
        dlGridSummaryReport.DataBind();

        //dlGridDetailsofAnswering.DataSource = dsGrid.Tables(1)
        //dlGridDetailsofAnswering.DataBind()

        //dlGridOverallToppers.DataSource = dsGrid.Tables(2)
        //dlGridOverallToppers.DataBind()

        ClientScript.RegisterClientScriptBlock(this.GetType(), "PrintOperation", "window.print();", true);
        Response.Clear();
    }
    public Report_Marksheet_Print()
    {
        Load += Page_Load;
    }
}
