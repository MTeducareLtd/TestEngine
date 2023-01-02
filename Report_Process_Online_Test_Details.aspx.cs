using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.UI.HtmlControls;

partial class Report_Process_Online_Test_Details : System.Web.UI.Page
{


    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL_Division();
            FillDDL_AcadYear();
            FillDDL_TestCategories();
            ControlVisibility("Search");   
            ddlStandard.Items.Insert(0, "Select");
            ddlStandard.SelectedIndex = 0;
            ddlTest.Items.Insert(0, "Select");
            ddlTest.SelectedIndex = 0;
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

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        if (ds != null)
        {
            if (ds.Tables.Count != 0)
            {
                ddl.DataSource = ds;
                ddl.DataTextField = txtField;
                ddl.DataValueField = valField;
                ddl.DataBind();
            }
        }

    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    private void FillDDL_Standard()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        DataSet dsStandard = ProductController.GetAllActive_Standard_ForYear(Div_Code, YearName);
        BindDDL(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");
        ddlStandard.Items.Insert(0, "Select");
        ddlStandard.SelectedIndex = 0;
    }

    private void FillDDL_AcadYear()
    {
        DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcadYear, dsAcadYear, "Description", "Id");
        ddlAcadYear.Items.Insert(0, "Select");
        ddlAcadYear.SelectedIndex = 0;
    }




    private void FillDDL_TestCategories()
    {
        DataSet dsTestCategory = ProductController.GetAllActiveTestCategory();
        BindDDL(ddlTestCategory, dsTestCategory, "TestCategory_Name", "TestCategory_Id");
        ddlTestCategory.Items.Insert(0, "Select");
        ddlTestCategory.SelectedIndex = 0;

        if (dsTestCategory.Tables[0].Rows.Count > 0)
        {
            try
            {
                ddlTestCategory.SelectedValue = "002";
            }
            catch
            {
            }
        }
    }

    

    private void FillDDL_TestName()
    {
        ddlTest.Items.Clear();

        //Validate if all information is entered correctly
        if (ddlDivision.SelectedIndex == 0)
        {
            //Show_Error_Success_Box("E", "0001")
            //ddlDivision.Focus()
            return;
        }

        if (ddlAcadYear.SelectedIndex == 0)
        {
            //Show_Error_Success_Box("E", "0002")
            //ddlAcadYear.Focus()
            return;
        }
        string CenterCode = "";
                        
        if (ddlStandard.SelectedIndex == 0)
        {
            //Show_Error_Success_Box("E", "0003")
            //ddlStandard.Focus()
            return;
        }

        if (ddlTestCategory.SelectedIndex == 0)
        {
            //Show_Error_Success_Box("E", "0012")
            //ddlTestCategory.Focus()
            return;
        }

               

        string TestType_ID = "";        

        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;       

        DataSet dsTestName = ProductController.GetTestScheduleBy_Division_Year_Standard(DivisionCode, YearName, StandardCode, "", "01", ddlTestCategory.SelectedValue, "", "", "", "",
        0, 0, 6, CenterCode);
        BindDDL(ddlTest, dsTestName, "Test_Name", "PKey");
        ddlTest.Items.Insert(0, "Select");
        ddlTest.SelectedIndex = 0;
    }

    protected void btnTestName_Click(object sender, System.EventArgs e)
    {
        FillDDL_TestName();
    }


    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_Standard();
        FillDDL_TestName();        
    }

    protected void ddlAcadYear_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_Standard();
        FillDDL_TestName();         
    }

    protected void btnExport_Click(object sender, System.EventArgs e)
    {        
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Process_Online_Test_Details_" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='5'><b>Process_Online_Test_Details</b></TD></TR><TR><TD Colspan='3'><b>Divison : " + lblDivision_Result.Text + " </b></TD><TD Colspan='2'><b>Acad Year : " + lblAcadYear_Result.Text + " </b></TD></TR><TR><TD Colspan='3'><b>Course : " + lblStandard_Result.Text + " </b></TD><TD Colspan='2'><b>Category : " + lblTestCategory_Result.Text + " </b></TD></TR>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount)
        dlGridReport1.RenderControl(oHtmlTextWriter1);
        //string style = @"<style> td { mso-number-format:\@;} </style>";
        //Response.Write(style);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
    }
    public Report_Process_Online_Test_Details()
    {
        Load += Page_Load;
    }


    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlAcadYear.SelectedIndex = 0;
        ddlStandard.SelectedIndex = 0;
        ddlTest.Items.Clear();
        ddlTest.Items.Insert(0, "Select");
        ddlTest.SelectedIndex = 0;
        txtassesmenttestcode.Text = "";
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Division");            
            return;
        }
        if (ddlAcadYear.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Acad Year");
            return;
        }
        if (ddlStandard.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Course");
            return;
        }
        if (ddlTestCategory.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Test Category");
            return;
        }

        string TestCode="";
        if (ddlTest.SelectedIndex != 0)
        {
            TestCode=ddlTest.SelectedValue;
        }
                

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");
        string Userid = lblHeader_User_Code.Text;

        DataSet dsTestReport = ProductController.Get_Rpt_Process_Online_Test_Details(ddlDivision.SelectedValue, ddlAcadYear.SelectedValue, ddlStandard.SelectedValue, ddlTestCategory.SelectedValue, TestCode, txtassesmenttestcode.Text.Trim(), 1);
        if (dsTestReport != null)
        {
            if (dsTestReport.Tables.Count > 0)
            {
                if (dsTestReport.Tables[0].Rows.Count > 0)
                {
                    ControlVisibility("Result");

                    dlGridReport1.DataSource = dsTestReport.Tables[0];
                    dlGridReport1.DataBind();

                    lbltotalcount.Text = dsTestReport.Tables[0].Rows.Count.ToString();
                    lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
                    lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
                    lblStandard_Result.Text = ddlStandard.SelectedItem.ToString();
                    lblTestCategory_Result.Text = ddlTestCategory.SelectedItem.ToString();
                    //lblCenter_Result .Text= CenterName;
                    //lblBatch_Result.Text = BatchName;
                }
                else
                {
                    Show_Error_Success_Box("E", "Records not found");
                    return;
                }

            }
            else
            {
                Show_Error_Success_Box("E", "Records not found");
                return;
            }
        }
      


    }
    
    protected void ddlStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
       // FillDDL_Batch();
        FillDDL_TestName();  
    }

    protected void ddlTestCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        FillDDL_TestName();
    }

    protected void ddlCentre_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        //FillDDL_Batch();
        FillDDL_TestName();
        Clear_Error_Success_Box();
    }

    protected void ddlBatch_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_TestName();
        Clear_Error_Success_Box();
    }

    protected void dlGridReport1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
        {
            //QpSet_Process
            Label lblDLLast_QpSet_Process = e.Item.FindControl("lblDLLast_QpSet_Process") as Label;

            if (lblDLLast_QpSet_Process.Text == "Not Processed")
                lblDLLast_QpSet_Process.ForeColor = System.Drawing.Color.Red; ;

            //AnswerKey_Process
            Label lblDLLast_AnswerKey_Process = e.Item.FindControl("lblDLLast_AnswerKey_Process") as Label;

            if (lblDLLast_AnswerKey_Process.Text == "Not Processed")
                lblDLLast_AnswerKey_Process.ForeColor = System.Drawing.Color.Red; ;

        }
    }
}
