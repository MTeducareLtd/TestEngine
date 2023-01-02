using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Rpt_FaultyPerformnce_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ControlVisibility("Search");
                SetSearchPanel_UserControl();
                
            }
        }
        catch (Exception ex)
        {
        }
    }
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

        string FromDate = null;
        string ToDate = null;

        ddlMarksType.SelectedIndex = 0;


        //if (Report_Period != "")
        //{
        //    FromDate = Report_Period.Substring(0, 10);//Strings.Left(Report_Period, 10);
        //}
        //if (string.IsNullOrEmpty(FromDate))
        //    FromDate = System.DateTime.Now.ToString("dd MMM yyyy");
        //if (Report_Period != "")
        //{
        //    ToDate = ToDate.Substring(ToDate.Length - 10);//Strings.Right(Report_Period, 10);
        //}
        //if (string.IsNullOrEmpty(ToDate))
        //    ToDate = System.DateTime.Now.ToString("dd MMM yyyy");


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
        if (Convert.ToInt32(SearchPanel1.ReportTypeCode) == 1)
        {
            ReportType = 2;
        }
        else if (Convert.ToInt32(SearchPanel1.ReportTypeCode) == 2)
        {
            ReportType = 1;
        }
        else if (Convert.ToInt32(SearchPanel1.ReportTypeCode) == 3)
        {
            ReportType = 7;
        }




        string Subject_Code = null;
        Subject_Code = SearchPanel1.Subject_Code;


        string Test_Id = null;
        Test_Id = SearchPanel1.Test_Id;

        if (Subject_Code == "All")
        {
            DataSet dsGrid = ProductController.Report_Faculty_Performance(Test_Id, SearchPanel1.UserCode, ReportType, Centre_Code, FromDate, ToDate);
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
                        bfield.ItemStyle.Width = Unit.Pixel(390); //"390";
                    }
                    else
                    {
                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        bfield.ItemStyle.Width = Unit.Pixel(80);// "80";
                    }

                    //Add the newly created bound field to the GridView.
                    dlGridReport1.Columns.Add(bfield);
                    ColCnt = ColCnt + 1;
                }

                //Initialize the DataSource
                dlGridReport1.DataSource = dsGrid.Tables[0];

                //Bind the datatable with the GridView.
                dlGridReport1.DataBind();

                lbltotalcount.Text = Convert.ToString(dlGridReport1.Rows.Count);

                if (dlGridReport1.Rows.Count != 0)
                {
                    //if (ReportType == 2)
                    //{
                        trMarksType.Visible = true;
                    //}
                    //else
                    //{
                    //    trMarksType.Visible = false;
                    //}
                }
                else
                {
                    trMarksType.Visible = false;
                }

            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.Message);
                return;
            }
        }
        else
        {
            int MarksType = Convert.ToInt32(ddlMarksType.SelectedValue);
            lblReportType_Result.Text = SearchPanel1.ReportTypeName + "<br>Subject : " + SearchPanel1.Subject_Name;

            DataSet dsGrid = ProductController.Report_Faculty_Performance(Test_Id, SearchPanel1.UserCode, ReportType, Centre_Code, FromDate, ToDate, MarksType);
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
                        bfield.ItemStyle.Width = Unit.Pixel(390);// "390";
                    }
                    else
                    {
                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        bfield.ItemStyle.Width = Unit.Pixel(80);// "80";
                    }

                    //Add the newly created bound field to the GridView.
                    dlGridReport1.Columns.Add(bfield);
                    ColCnt = ColCnt + 1;
                }

                //Initialize the DataSource
                dlGridReport1.DataSource = dsGrid.Tables[0];

                //Bind the datatable with the GridView.
                dlGridReport1.DataBind();

                lbltotalcount.Text = Convert.ToString(dlGridReport1.Rows.Count);

            }
            catch (Exception ex)
            {
                if (ex.Message == "Cannot find table 0.")
                {
                    Show_Error_Success_Box("E", "0 record found");
                    return;
                }
                else
                {
                    Show_Error_Success_Box("E", ex.Message);
                    return;
                }

            }
        }



        //dlGridSummaryReport.DataSource = dsGrid.Tables(0)
        //dlGridSummaryReport.DataBind()

        ControlVisibility("Result");
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
        SearchPanel1.FillDDL_ReportType(5);
    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void btnExport_Click(object sender, System.EventArgs e)
    {
        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=Report_TestPerformance.xls");

        Response.Charset = "";


        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        dlGridReport1.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();
    }
    public Rpt_FaultyPerformnce_Details()
    {
        Load += Page_Load;
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        SearchPanel1.ClearControl();
    }
    protected void ddlMarksType_SelectedIndexChanged(object sender, EventArgs e)
    {
        dlGridReport1.DataSource = null;
        dlGridReport1.DataBind();
        dlGridReport1.Columns.Clear();        

        string Centre_Code = SearchPanel1.Centre_Code;
        string Report_Period = SearchPanel1.ReportPeriod;

        int MarksType = Convert.ToInt32(ddlMarksType.SelectedValue);


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
        if (Convert.ToInt32(SearchPanel1.ReportTypeCode) == 1)
        {
            ReportType = 2;
        }
        else if (Convert.ToInt32(SearchPanel1.ReportTypeCode) == 2)
        {
            ReportType = 1;
        }
        else if (Convert.ToInt32(SearchPanel1.ReportTypeCode) == 3)
        {
            ReportType = 7;
        }

        string Subject_Code = null;
        Subject_Code = SearchPanel1.Subject_Code;


        string Test_Id = null;
        Test_Id = SearchPanel1.Test_Id;

        if (Subject_Code == "All")
        {
            DataSet dsGrid = ProductController.Report_Faculty_Performance(Test_Id, SearchPanel1.UserCode, ReportType, Centre_Code, FromDate, ToDate, MarksType);
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
                        bfield.ItemStyle.Width = Unit.Pixel(390); //"390";
                    }
                    else
                    {
                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        bfield.ItemStyle.Width = Unit.Pixel(80);// "80";
                    }

                    //Add the newly created bound field to the GridView.
                    dlGridReport1.Columns.Add(bfield);
                    ColCnt = ColCnt + 1;
                }

                //Initialize the DataSource
                dlGridReport1.DataSource = dsGrid.Tables[0];

                //Bind the datatable with the GridView.
                dlGridReport1.DataBind();

                lbltotalcount.Text = Convert.ToString(dlGridReport1.Rows.Count);

            }
            catch (Exception ex)
            {
                Show_Error_Success_Box("E", ex.Message);
                return;
            }
        }
        else
        {
            lblReportType_Result.Text = SearchPanel1.ReportTypeName + "<br>Subject : " + SearchPanel1.Subject_Name;

            DataSet dsGrid = ProductController.Report_Test_MCQ_Ranking_Subject(Test_Id, SearchPanel1.UserCode, ReportType, Centre_Code, FromDate, ToDate, Subject_Code, MarksType);
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
                        bfield.ItemStyle.Width = Unit.Pixel(390);// "390";
                    }
                    else
                    {
                        bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        bfield.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                        bfield.ItemStyle.Width = Unit.Pixel(80);// "80";
                    }

                    //Add the newly created bound field to the GridView.
                    dlGridReport1.Columns.Add(bfield);
                    ColCnt = ColCnt + 1;
                }

                //Initialize the DataSource
                dlGridReport1.DataSource = dsGrid.Tables[0];

                //Bind the datatable with the GridView.
                dlGridReport1.DataBind();

                lbltotalcount.Text = Convert.ToString(dlGridReport1.Rows.Count);

            }
            catch (Exception ex)
            {
                if (ex.Message == "Cannot find table 0.")
                {
                    Show_Error_Success_Box("E", "0 record found");
                    return;
                }
                else
                {
                    Show_Error_Success_Box("E", ex.Message);
                    return;
                }

            }
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
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the specified ASP.NET
        //     server control at run time. 

    }
}