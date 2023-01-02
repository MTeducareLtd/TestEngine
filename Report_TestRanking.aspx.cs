using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI;


partial class Report_TestRanking : System.Web.UI.Page
{

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        dlGridReport1.DataSource = null;
        dlGridReport1.DataBind();
        dlGridReport1.Columns.Clear();

        //Validate if all information is entered correctly
        SearchPanel1.Validate_Search();
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
        //if (SearchPanel1.Center_Name == "")
        //{
        //    Show_Error_Success_Box("E", "select Centre");
        //    return;
        //}

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
        lblReportType_Result.Text = SearchPanel1.ReportTypeName;
        lblTestCategory_Result.Text = SearchPanel1.TestCategoryName;
        string Centre_Code = SearchPanel1.Centre_Code;
        string Report_Period = SearchPanel1.ReportPeriod;
        string Batch_Code = SearchPanel1.Batch_Code;

        string FromDate = null;
        string ToDate = null;
       

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



        int ReportType = 0;
        ReportType =Convert.ToInt32(SearchPanel1.ReportTypeCode);

        if (SearchPanel1.ReportTypeCode == "3")
        {
            ReportType = 5;
        }
        else if (SearchPanel1.ReportTypeCode == "4")
        {
            ReportType = 6;
        }

        string Test_Id = null;
        Test_Id = SearchPanel1.Test_Id;
        DataSet dsGrid = ProductController.Report_Test_MCQ_Ranking(Test_Id, SearchPanel1.UserCode, ReportType, Centre_Code, FromDate, ToDate, Batch_Code);
        //DataSet dsGrid = ProductController.Report_Test_MCQ_Ranking1(Test_Id, SearchPanel1.UserCode, ReportType, Centre_Code, FromDate, ToDate,Batch_Code);
        int ColCnt = 0;
        try
        {
            foreach (DataColumn col in dsGrid.Tables[0].Columns)
            {
                //Declare the bound field and allocate memory for the bound field.
                BoundField bfield = new BoundField();



                //Initalize the DataField value.
                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;

                if (ColCnt == 0)
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                    bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                }
                else if (ColCnt == 1)
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                    bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                    bfield.ItemStyle.Width = Unit.Pixel(200);//"200";
                }
                else
                {
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    bfield.ItemStyle.Width = Unit.Pixel(80); //"80";
                }

                //If ColName = "Centre Name" Then
                //    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Left
                //    bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Left
                //Else
                //    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                //    bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                //End If


                //Add the newly created bound field to the GridView.
                dlGridReport1.Columns.Add(bfield);
                ColCnt = ColCnt + 1;
            }

            //Initialize the DataSource
            dlGridReport1.DataSource = dsGrid.Tables[0];

            //Bind the datatable with the GridView.
            dlGridReport1.DataBind();

            lbltotalcount.Text =Convert.ToString(dlGridReport1.Rows.Count);

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.Message);
            return;
        }

        //dlGridSummaryReport.DataSource = dsGrid.Tables(0)
        //dlGridSummaryReport.DataBind()

        ControlVisibility("Result");
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
            SetSearchPanel_UserControl();
        }
    }

    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    // Confirms that an HtmlForm control is rendered for the specified ASP.NET
    //    //     server control at run time. 

    //}


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
        SearchPanel1.FillDDL_ReportType(1);
    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void btnExport_Click(object sender, System.EventArgs e)
    {
        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=Report_TestRanking.xls");

        Response.Charset = "";


        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

        dlGridReport1.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the specified ASP.NET
        //     server control at run time. 

    }
    public Report_TestRanking()
    {
        Load += Page_Load;
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        SearchPanel1.ClearControl();
    }
}
