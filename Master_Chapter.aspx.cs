using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

partial class Master_Chapter : System.Web.UI.Page
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            FillDDL_Division();
        }
    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            //BtnAdd.Visible = True
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            //BtnAdd.Visible = True
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
        //DataSet dsDivision = null;
        //OrderDataService.OrderDataServiceSoapClient client = new OrderDataService.OrderDataServiceSoapClient();
        //dsDivision = client.GetCompany_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, "", "", "2", lblHeader_DBName.Text);
        //if (dsDivision != null)
        //{
        //    if (dsDivision.Tables.Count != 0)
        //    {
        //        BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
        //        ddlDivision.Items.Insert(0, "Select");
        //        ddlDivision.SelectedIndex = 0;
        //    }
        //    else
        //    {
                
        //        ddlDivision.Items.Insert(0, "Select");
        //        ddlDivision.SelectedIndex = 0;
        //    }
            
        //}
        //else
        //{

        //    ddlDivision.Items.Insert(0, "Select");
        //    ddlDivision.SelectedIndex = 0;
        //}

        

    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    private void FillGrid_Chapter()
    {
        //Validate if all information is entered correctly
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivision.Focus();
            return;
        }

        if (ddlStandard.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0003");
            ddlStandard.Focus();
            return;
        }

        if (ddlSubject.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0005");
            ddlSubject.Focus();
            return;
        }

        ControlVisibility("Result");

        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string StandardCode = "";
        StandardCode = ddlStandard.SelectedValue;

        string SubjectCode = "";
        SubjectCode = ddlSubject.SelectedValue;

        DataSet dsGrid = ProductController.GetAllChaptersBy_Division_Year_Standard_Subject(DivisionCode, "", StandardCode, SubjectCode);

        //Copy dsGrid content from DataSet to DataTable
        DataTable dtGrid = null;
        dtGrid = dsGrid.Tables[0];

        //Add 1 Blank records
        dtGrid.Rows.Add("", "", "", "", "", 0, 0, 0, 1, 1);

        dlGridDisplay.DataSource = dtGrid;

        dlGridDisplay.DataSource = dsGrid;
        dlGridDisplay.DataBind();

        dlGridExport.DataSource = dsGrid;
        dlGridExport.DataBind();

        lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
        lblStandard_Result.Text = ddlStandard.SelectedItem.ToString();
        lblSubject_Result.Text = ddlSubject.SelectedItem.ToString();

        lbltotalcount.Text = dsGrid.Tables[0].Rows.Count.ToString();
    }

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        FillGrid_Chapter();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
    }

    private void FillDDL_Standard()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        DataSet dsAllStandard = ProductController.GetAllActive_AllStandard(Div_Code);
        BindDDL(ddlStandard, dsAllStandard, "Standard_Name", "Standard_Code");
        ddlStandard.Items.Insert(0, "Select");
        ddlStandard.SelectedIndex = 0;

        //Dim YearName As String
        //YearName = ddlAcadyear.SelectedItem.ToString

        //Dim dsStandard As DataSet = ProductController.GetAllActive_Standard_ForYear(Div_Code, YearName)
        //BindDDL(ddlStandard, dsStandard, "Standard_Name", "Standard_Code")
        //ddlStandard.Items.Insert(0, "Select")
        //ddlStandard.SelectedIndex = 0
    }

    private void FillDDL_Subject_Add()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = "";

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        //DataSet dsStandard = ProductController.GetAllSubjectsBy_Division_Year_Standard(Div_Code, YearName, StandardCode);

        DataSet dsStandard = ProductController.GetAllSubjectsByStandard(StandardCode);

        BindDDL(ddlSubject, dsStandard, "Subject_ShortName", "Subject_Code");
        ddlSubject.Items.Insert(0, "Select");
        ddlSubject.SelectedIndex = 0;
    }

    protected void ddlStandard_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Subject_Add();
    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        TextBox txtDLChapterShortName = (TextBox)e.Item.FindControl("txtDLChapterShortName");
        TextBox txtDLChapterName = (TextBox)e.Item.FindControl("txtDLChapterName");
        TextBox txtDLLectureCnt = (TextBox)e.Item.FindControl("txtDLLectureCnt");
        TextBox txtDLLectureMin = (TextBox)e.Item.FindControl("txtDLLectureMin");
        HtmlAnchor lbl_DLError = (HtmlAnchor)e.Item.FindControl("lbl_DLError");

        Label lblDLChapterShortName = (Label)e.Item.FindControl("lblDLChapterShortName");
        Label lblDLChapterName = (Label)e.Item.FindControl("lblDLChapterName");
        Label lblDLLectureCnt = (Label)e.Item.FindControl("lblDLLectureCnt");
        Label lblDLLectureMin = (Label)e.Item.FindControl("lblDLLectureMin");

        LinkButton lnkDLEdit = (LinkButton)e.Item.FindControl("lnkDLEdit");
        LinkButton lnkDLSave = (LinkButton)e.Item.FindControl("lnkDLSave");

        Panel icon_Error = (Panel)e.Item.FindControl("icon_Error");

        if (e.CommandName == "Edit")
        {
            txtDLChapterName.Visible = true;
            txtDLChapterShortName.Visible = true;
            txtDLLectureCnt.Visible = true;
            txtDLLectureMin.Visible = true;

            lblDLChapterName.Visible = false;
            lblDLChapterShortName.Visible = false;
            lblDLLectureCnt.Visible = false;
            lblDLLectureMin.Visible = false;

            lnkDLEdit.Visible = false;
            lnkDLSave.Visible = true;
            icon_Error.Visible = false;

            txtDLChapterShortName.Focus();
        }
        else if (e.CommandName == "Save")
        {
            //Validation
            if (string.IsNullOrEmpty(txtDLChapterName.Text.Trim()))
            {
                lbl_DLError.Title = "Enter Chapter Name";
                icon_Error.Visible = true;
                txtDLChapterName.Focus();
                return;
            }

            //Check if lecture count is a numeric value
            if (string.IsNullOrEmpty(txtDLLectureCnt.Text))
            {
                lbl_DLError.Title = "Enter number of lectures required for this Chapter";
                icon_Error.Visible = true;
                txtDLLectureCnt.Focus();
                return;
            }

            if (Convert.ToBoolean((Convert.ToInt32(txtDLLectureCnt.Text))) == false)
            {
                lbl_DLError.Title = "Invalid entry in 'No. of Lectures' field";
                icon_Error.Visible = true;
                txtDLLectureCnt.Focus();
                return;
            }


            //Check if lecture min is a numeric value
            if (string.IsNullOrEmpty(txtDLLectureMin.Text))
            {
                lbl_DLError.Title = "Enter duration in minutes required for this Chapter";
                icon_Error.Visible = true;
                txtDLLectureMin.Focus();
                return;
            }

            if (Convert.ToBoolean(Convert.ToInt32(txtDLLectureMin.Text)) == false)
            {
                lbl_DLError.Title = "Invalid entry in 'Time in min' field";
                icon_Error.Visible = true;
                txtDLLectureMin.Focus();
                return;
            }

            //Saving part
            string DivisionCode = null;
            DivisionCode = ddlDivision.SelectedValue;

            string YearName = null;
            YearName = "";

            string StandardCode = "";
            StandardCode = ddlStandard.SelectedValue;

            string SubjectCode = "";
            SubjectCode = ddlSubject.SelectedValue;

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            string CreatedBy = null;
            CreatedBy = lblHeader_User_Code.Text;

            string ChapterCodeForEdit = null;
            ChapterCodeForEdit = e.CommandArgument.ToString();

            int ResultId = 0;
            //Mark exemption/absent/present for those students who are selected
            ResultId = ProductController.Insert_Chapter(DivisionCode, YearName, StandardCode, SubjectCode, txtDLChapterName.Text.Trim(), Convert.ToDouble(txtDLLectureCnt.Text), Convert.ToInt32(txtDLLectureMin.Text), txtDLChapterShortName.Text, ChapterCodeForEdit, CreatedBy);

            if (ResultId == -1)
            {
                lbl_DLError.Title = "Duplicate chapter name or code";
                icon_Error.Visible = true;
                txtDLChapterName.Focus();
                return;
            }
            else
            {
                icon_Error.Visible = false;
            }

            //Change look
            txtDLChapterName.Visible = false;
            txtDLChapterShortName.Visible = false;
            txtDLLectureCnt.Visible = false;
            txtDLLectureMin.Visible = false;

            lblDLChapterName.Visible = true;
            lblDLChapterShortName.Visible = true;
            lblDLLectureCnt.Visible = true;
            lblDLLectureMin.Visible = true;

            lnkDLEdit.Visible = true;
            lnkDLSave.Visible = false;

            FillGrid_Chapter();
        }

    }

    public Master_Chapter()
    {
        Load += Page_Load;
    }
}
