using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;

partial class UserDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        string FromDate = null;
        string ToDate = null;
        ToDate = System.DateTime.Now.ToString("dd MMM yyyy");        
        FromDate = System.DateTime.Now.AddMonths(-1).ToString("dd MMM yyyy");
        lblReportPeriod.Text = "Period: " + FromDate + " - " + ToDate;
        //FillAbsentRelatedItems(false);
    }

    private void FillAbsentRelatedItems(bool ReloadFlag)
    {
        //Check if data exists in session variable
        //Load from session 
        DataTable dtCentreSummary = (DataTable)Session["dtCentreSummary"];
        DataTable dtStudentSummary = (DataTable)Session["dtStudentSummary"];
        DataTable dtCentreRank = (DataTable)Session["dtCentreRank"];
        string CurrentCentreCode = (string)Session["CurrentCentreCode"];

        if (dtCentreSummary == null | dtStudentSummary == null | dtCentreRank == null | ReloadFlag == true)
        {
            //If not exits then retrieve from database

            string FromDate = null;
            string ToDate = null;
            ToDate = System.DateTime.Now.ToString("dd MMM yyyy");
            FromDate = System.DateTime.Now.AddMonths(-1).ToString("dd MMM yyyy");
            lblReportPeriod.Text = "Period: " + FromDate + " - " + ToDate;

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            Label lblHeader_Company_Code = default(Label);
            lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

            Label lblHeader_DBName = default(Label);
            lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

            try
            {

                
                DataSet dsGrid = ProductController.Dashboard_Test(lblHeader_Company_Code.Text, lblHeader_User_Code.Text, FromDate, ToDate, "MTEducare", "1");
                if (dsGrid != null)
                {
                    if (dsGrid.Tables.Count != 0)
                    {
                        Session["dtCentreSummary"] = dsGrid.Tables[0];
                        Session["dtStudentSummary"] = dsGrid.Tables[1];
                        Session["dtCentreRank"] = dsGrid.Tables[2];

                        dlGrid_CentreAbsent.DataSource = dsGrid.Tables[0];
                        dlGrid_CentreAbsent.DataBind();

                        dlGrid_StudentAbsent.DataSource = dsGrid.Tables[1];
                        dlGrid_StudentAbsent.DataBind();

                        string CentreCode = "";
                        FillCentreRankBoard(dsGrid.Tables[2], CentreCode);

                    }
                }
            }
            catch (Exception ex)
            {
            }


        }
        else
        {

            dlGrid_CentreAbsent.DataSource = dtCentreSummary;
            dlGrid_CentreAbsent.DataBind();

            dlGrid_StudentAbsent.DataSource = dtStudentSummary;
            dlGrid_StudentAbsent.DataBind();


            FillCentreRankBoard(dtCentreRank, CurrentCentreCode);
        }

    }

    private void FillCentreRankBoard(DataTable dt, string CentreCode)
    {
        try
        {


            //If centrecode is blank then show data of first centre that is available
            if (string.IsNullOrEmpty(CentreCode))
            {
                if (dt != null)
                {
                    if (dt.Rows.Count != 0)
                    {
                        CentreCode = Convert.ToString(dt.Rows[0]["Centre_Code"]);
                        lblCentreDashboard_CentreNumber.Text = "0";

                    }
                    else
                    {
                        lblCentreDashboard_CentreNumber.Text = "0";
                    }

                }
                else
                {
                    lblCentreDashboard_CentreNumber.Text = "0";
                }
                
            }

            int RowCnt = 0;
            foreach (DataRow dtitem in dt.Rows)
            {
                if (dtitem["Centre_Code"] == CentreCode)
                {
                    lblCentreDashboard_CentreName.Text = Convert.ToString(dtitem["Centre_Name"]);
                    lblCentreDashboard_TestCount.Text = Convert.ToString(dtitem["TotalTestCount"]);
                    lblCentreDashboard_ReTestCount.Text = Convert.ToString(dtitem["ReTestCount"]);
                    lblCentreDashboard_AttendPending.Text = Convert.ToString(dtitem["PendingAttendanceAuth"]);
                    lblCentreDashboard_AttendTAT.Text = Convert.ToString(dtitem["AttendanceTAT"]);
                    lblCentreDashboard_MarkPending.Text = Convert.ToString(dtitem["PendingMarksAuth"]);
                    lblCentreDashboard_MarkTAT.Text = Convert.ToString(dtitem["MarksTAT"]);
                    lblCentreDashboard_CentreNumber.Text = RowCnt.ToString();

                    break; // TODO: might not be correct. Was : Exit For
                }
                RowCnt = RowCnt + 1;
            }

            Session["CurrentCentreCode"] = CentreCode;
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btn_NextCentre_ServerClick(object sender, System.EventArgs e)
    {
        try
        {
            DataTable dtCentreRank = (DataTable)Session["dtCentreRank"];

            int NewCentreNo = 0;
            NewCentreNo = Convert.ToInt32(lblCentreDashboard_CentreNumber.Text) + 1;
            string CentreCode = null;

            if (NewCentreNo < dtCentreRank.Rows.Count)
            {
                CentreCode = Convert.ToString(dtCentreRank.Rows[NewCentreNo]["Centre_Code"]);
                FillCentreRankBoard(dtCentreRank, CentreCode);
            }
        }
        catch (Exception)
        {

            throw;
        }


    }

    protected void btn_PreviousCentre_ServerClick(object sender, System.EventArgs e)
    {

        try
        {
            DataTable dtCentreRank = (DataTable)Session["dtCentreRank"];
            if (dtCentreRank != null)
            {
                int NewCentreNo = 0;
                NewCentreNo = Convert.ToInt32(lblCentreDashboard_CentreNumber.Text) - 1;
                string CentreCode = null;

                if (NewCentreNo >= 0)
                {
                    CentreCode = Convert.ToString(dtCentreRank.Rows[NewCentreNo]["Centre_Code"]);
                    FillCentreRankBoard(dtCentreRank, CentreCode);
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public UserDashboard()
    {
        Load += Page_Load;
    }
}
