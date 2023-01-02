using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShoppingCart.BL;
using System.Data;

public partial class Report_TestScheduled : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            SetSearchPanel_UserControl();
            SearchPanel1.FillDDL_ReportType(6);
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

    private void SetSearchPanel_UserControl()
    {
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        if (string.IsNullOrEmpty(lblHeader_User_Code.Text))
            Response.Redirect("Default.aspx");

        SearchPanel1.UserCode = lblHeader_User_Code.Text;
        SearchPanel1.CompanyCode = lblHeader_Company_Code.Text;
        SearchPanel1.DBName = lblHeader_DBName.Text;
        SearchPanel1.FillDDL_Division();
        SearchPanel1.FillDDL_ReportType(6);
    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        SearchPanel1.ClearControl_MarksEntry();
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

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            SearchPanel1.Validate_Search_Marks_Entry();
            if (SearchPanel1.DivisionName == "Select")
            {
                Show_Error_Success_Box("E", "select Division");
                return;
            }
            if (SearchPanel1.AcadYearName == "Select")
            {
                Show_Error_Success_Box("E", "select AcadYear");
                return;
            }

            if (SearchPanel1.StandardName == "Select")
            {
                Show_Error_Success_Box("E", "select Course");
                return;
            }
            if (SearchPanel1.TestCategoryName == "Select")
            {
                Show_Error_Success_Box("E", "select Category");
                return;
            }


            lblDivision_Result.Text = SearchPanel1.DivisionName;
            lblAcadYear_Result.Text = SearchPanel1.AcadYearName;
            lblStandard_Result.Text = SearchPanel1.StandardName;

            lblTestCategory_Result.Text = SearchPanel1.TestCategoryName;
            string Centre_Code = SearchPanel1.Centre_Code;
            string Report_Period = SearchPanel1.ReportPeriod;
            string Batch_Code = SearchPanel1.Batch_Code;

            string FromDate = null;
            string ToDate = null;
            string Test_Id = SearchPanel1.Test_Id;


            if (Report_Period != "")
            {
                FromDate = Report_Period.Substring(0, 10);//Strings.Left(Report_Period, 10);            
                DateTime result = DateTime.ParseExact(FromDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                FromDate = result.ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(FromDate))
            {

                FromDate = "2010-01-01";
            }
            if (Report_Period != "")
            {
                ToDate = Report_Period.Substring(Report_Period.Length - 10);//Strings.Right(Report_Period, 10);            
                DateTime result = DateTime.ParseExact(ToDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                ToDate = result.ToString("yyyy-MM-dd");

            }
            if (string.IsNullOrEmpty(ToDate))
            {
                ToDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            }

            DataSet dsGrid = ProductController.Report_TestScheduled("1", Test_Id, Centre_Code, Batch_Code, FromDate, ToDate);


            if (dsGrid != null && dsGrid.Tables.Count > 0)
            {
                if (dsGrid.Tables[0].Rows.Count > 0)
                {
                    lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
                    dlGridSummaryReport.DataSource = dsGrid;
                    dlGridSummaryReport.DataBind();
                    ControlVisibility("Result");
                }

                else
                {
                    Show_Error_Success_Box("E", "No Records Found");
                }
            }

            else
            {
                Show_Error_Success_Box("E", "No Records Found");
            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=Report_TestScheduled.xls");

        Response.Charset = "";


        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        dlGridSummaryReport.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();
    }
    
}