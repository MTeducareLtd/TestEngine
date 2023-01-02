using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.HtmlControls;

partial class Master_Partner : System.Web.UI.Page
{

    

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            txtDOJ.Value = System.DateTime.Now.ToString("dd MMM yyyy");
            txtDOB.Value = System.DateTime.Now.ToString("dd MMM yyyy");
            FillDDL_Company();
            FillDDL_Country();
            FillDDL_Title();
            FillDDL_Gender();
            FillDDL_Status();
        }
    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = true;
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = true;
        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
        }
        Clear_Error_Success_Box();
    }

    protected void BtnAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Add");
        Clear_AddPanel();

        lblHeader_Add.Text = "Create New Partner";
        lblTestPKey_Hidden.Text = "";
        BtnSaveEdit.Visible = false;
        BtnSave.Visible = true;
        FillDL_Activity();
    }

    protected void BtnCloseAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Result");
        Clear_AddPanel();
    }


    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            lbldelCode.Text = e.CommandArgument.ToString();
            txtDeleteItemName.Text = (((Label)e.Item.FindControl("lblModeName")).Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }
        else if (e.CommandName == "Edit")
        {
            ControlVisibility("Add");
            BtnSave.Visible = false;
            BtnSaveEdit.Visible = true;

            lblPKey_Edit.Text = e.CommandArgument.ToString();
            lblHeader_Add.Text = "Edit Partner Details";
            lblTestPKey_Hidden.Text = "";
            FillPartnerMasterDetails(lblPKey_Edit.Text, e.CommandName);
        }
    }

    private void FillPartnerMasterDetails(string PKey, string CommandName)
    {

        try
        {



            FillDL_Activity();

            Label lblHeader_User_Code = default(Label);
            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

            DataSet dsCRoom = ProductController.GetPartnerMaster_ByPKey(PKey, lblHeader_User_Code.Text, 1);


            if (dsCRoom.Tables[0].Rows.Count > 0)
            {
                lblTestPKey_Hidden.Text = PKey;

                string Country_Code = null;
                Country_Code =Convert.ToString(dsCRoom.Tables[0].Rows[0]["Country_Code"]);

                string State_Code = null;
                State_Code = Convert.ToString(dsCRoom.Tables[0].Rows[0]["State_Code"]);

                string City_Code = null;
                City_Code = Convert.ToString(dsCRoom.Tables[0].Rows[0]["City_Code"]);

                ddlCountry_Add.SelectedValue = Country_Code;
                FillDDL_State_Add();

                ddlState_Add.SelectedValue = State_Code;
                FillDDL_City_Add();

                ddlCity_Add.SelectedValue = City_Code;

                //int Cnt = 0;
                //for ( int cnt = 0; cnt <= ddlTitle_Add.Items.Count - 1; cnt++)
                //{
                //    if (ddlTitle_Add.Items[Cnt].Text == dsCRoom.Tables[0].Rows[0]["Title"])
                //    {
                //        ddlTitle_Add.SelectedIndex = Cnt;
                //    }
                //}

                ddlTitle_Add.SelectedIndex = -1;

                ddlTitle_Add.Items.FindByText(dsCRoom.Tables[0].Rows[0]["Title"].ToString()).Selected = true;


                txtFirstName_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["FirstName"]);
                txtMiddleName_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["MiddleName"]);
                txtLastName_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["LastName"]);
                txtHandPhone1_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["HandPhone1"]);
                txtHandPhone2_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["HandPhone2"]);
                txtPhoneNo_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["LandLine"]);
                ddlCompany_Add.SelectedValue = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Company_Code"]);
                txtEmailId_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["EmailId"]);
                ddlGender_Add.SelectedValue = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Gender"]);
                txtAreaName_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Area_Name"]);
                txtRoadName_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["StreetName"]);
                txtBuilding_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["BuildingName"]);
                txtRoomNo_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["FlatNo"]);
                txtPincode_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["PinCode"]);
                txtEmployeeNo_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["EmployeeNo"]);
                txtQualification_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Qualification"]);
                txtRemarks_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Remarks"]);
                txtDOB.Value = Convert.ToString(Convert.ToDateTime(dsCRoom.Tables[0].Rows[0]["DOB"]).ToString("dd MMM yyyy"));
                txtDOJ.Value = Convert.ToString(Convert.ToDateTime(dsCRoom.Tables[0].Rows[0]["DOJ"]).ToString("dd MMM yyyy"));

                lblCompany_Add.Text = ddlCompany_Add.SelectedItem.ToString();
                ddlCompany_Add.Visible = false;
                lblCompany_Add.Visible = true;
                FillDDL_Centre();

                //ddlLocation_Add.SelectedValue = dsCRoom.Tables(0).Rows(0)("Location_Code")

                if (Convert.ToInt32(dsCRoom.Tables[0].Rows[0]["IsActive"]) == 0)
                {
                    chkActiveFlag.Checked = false;
                }
                else
                {
                    chkActiveFlag.Checked = true;
                }

                for (int cnt = 0; cnt <= dsCRoom.Tables[1].Rows.Count - 1; cnt++)
                {

                    foreach (DataListItem dtlItem in dlCentre_Add.Items)
                    {
                        CheckBox chkDL_Select_Centre = (CheckBox)dtlItem.FindControl("chkDL_Select_Centre");
                        Label lblDivisionCode = (Label)dtlItem.FindControl("lblDivisionCode");
                        if (Convert.ToString(lblDivisionCode.Text).Trim() == Convert.ToString(dsCRoom.Tables[1].Rows[cnt]["Division_Code"]).Trim())
                        { 
                            chkDL_Select_Centre.Checked = true;
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }

                }

                for (int cnt = 0; cnt <= dsCRoom.Tables[2].Rows.Count - 1; cnt++)
                {
                    foreach (DataListItem dtlItem in dlCapacity_Add.Items)
                    {
                        CheckBox chkDL_Select_Activity = (CheckBox)dtlItem.FindControl("chkDL_Select_Activity");
                        Label lblDL_Activity_Id = (Label)dtlItem.FindControl("lblDL_Activity_Id");
                        if (lblDL_Activity_Id.Text.Trim() == dsCRoom.Tables[2].Rows[cnt]["Activity_Id"].ToString().Trim())
                        {
                            chkDL_Select_Activity.Checked = true;
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }

                }

                //'dlCentre_Add.DataSource = dsCRoom.Tables(3)
                //'dlCentre_Add.DataBind()

            }
        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
            return;
        }
    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    private void FillDDL_Gender()
    {
        ddlGender_Add.Items.Clear();
        ddlGender_Add.Items.Add("Select");
        ddlGender_Add.Items.Add("Male");
        ddlGender_Add.Items.Add("Female");
        ddlGender_Add.SelectedIndex = 0;
    }

    private void FillDDL_Country()
    {
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        if (string.IsNullOrEmpty(lblHeader_User_Code.Text))
            Response.Redirect("Default.aspx");

        DataSet dsDivision = ProductController.GetAllActiveCountry();
        BindDDL(ddlCountry, dsDivision, "Country_Name", "Country_Code");
        ddlCountry.Items.Insert(0, "Select");
        ddlCountry.SelectedIndex = 0;

        BindDDL(ddlCountry_Add, dsDivision, "Country_Name", "Country_Code");
        ddlCountry_Add.Items.Insert(0, "Select");
        ddlCountry_Add.SelectedIndex = 0;

    }

    private void FillDDL_Centre()
    {
        dlCentre_Add.DataSource = null;
        dlCentre_Add.DataBind();

        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");



        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, "", "", "2", lblHeader_DBName.Text);
        dlCentre_Add.DataSource = dsCentre;
        dlCentre_Add.DataBind();
    }

    private void FillDDL_Title()
    {
        DataSet dsTitle = ProductController.GetAllActivePartnerTitle();
        BindDDL(ddlTitle_Add, dsTitle, "Title_Name", "Title_Id");
        ddlTitle_Add.Items.Insert(0, "Select");
        ddlTitle_Add.SelectedIndex = 0;

        BindDDL(ddlTitle_Gender, dsTitle, "Gender", "Title_Id");
        ddlTitle_Gender.Items.Insert(0, "Select");
        ddlTitle_Gender.SelectedIndex = 0;

    }


    private void FillDDL_Status()
    {
        ddlStatus.Items.Insert(0, "All");
        ddlStatus.Items.Insert(1, "Active");
        ddlStatus.Items.Insert(2, "In Active");
        ddlStatus.SelectedIndex = 0;

    }

    private void FillDDL_Company()
    {
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");


        DataSet dsCompany = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, "", "", "1", lblHeader_DBName.Text);
        BindDDL(ddlCompany_Add, dsCompany, "Company_Name", "Company_Code");
        ddlCompany_Add.Items.Insert(0, "Select");
        ddlCompany_Add.SelectedIndex = 0;
    }

    protected void ddlCountry_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_State_Add();

        Clear_Error_Success_Box();
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_State();
        Clear_Error_Success_Box();
    }

    private void FillDDL_State()
    {
        string Country_Code = null;
        Country_Code = ddlCountry.SelectedValue;

        DataSet dsState = ProductController.GetAllActiveState(Country_Code);
        BindDDL(ddlState, dsState, "State_Name", "State_Code");
        ddlState.Items.Insert(0, "Select");
        ddlState.SelectedIndex = 0;
    }

    private void FillDDL_State_Add()
    {
        string Country_Code = null;
        Country_Code = ddlCountry_Add.SelectedValue;

        DataSet dsState = ProductController.GetAllActiveState(Country_Code);
        BindDDL(ddlState_Add, dsState, "State_Name", "State_Code");
        ddlState_Add.Items.Insert(0, "Select");
        ddlState_Add.SelectedIndex = 0;
    }

    private void FillDDL_City()
    {
        string State_Code = null;
        State_Code = ddlState.SelectedValue;

        DataSet dsCity = ProductController.GetAllActiveCity(State_Code);
        BindDDL(ddlCity, dsCity, "City_Name", "City_Code");
        ddlCity.Items.Insert(0, "Select");
        ddlCity.SelectedIndex = 0;
    }

    private void FillDDL_City_Add()
    {
        string State_Code = null;
        State_Code = ddlState_Add.SelectedValue;

        DataSet dsCity = ProductController.GetAllActiveCity(State_Code);
        BindDDL(ddlCity_Add, dsCity, "City_Name", "City_Code");
        ddlCity_Add.Items.Insert(0, "Select");
        ddlCity_Add.SelectedIndex = 0;
    }

    //Private Sub FillDDL_Location()
    //    Dim City_Code As String
    //    City_Code = ddlCity.SelectedValue

    //    Dim dsLocation As DataSet = ProductController.GetAllActiveLocation(City_Code)
    //    BindListBox(ddlLocation, dsLocation, "Location_Name", "Location_Code")
    //End Sub

    //Private Sub FillDDL_Location_Add()
    //    Dim City_Code As String
    //    City_Code = ddlCity_Add.SelectedValue

    //    Dim dsLocation As DataSet = ProductController.GetAllActiveLocation(City_Code)
    //    BindDDL(ddlLocation_Add, dsLocation, "Location_Name", "Location_Code")
    //    ddlLocation_Add.Items.Insert(0, "Select")
    //    ddlLocation_Add.SelectedIndex = 0
    //End Sub

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    protected void ddlState_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_City();
        Clear_Error_Success_Box();
    }

    protected void ddlState_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_City_Add();
        Clear_Error_Success_Box();
    }

    protected void ddlCity_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        //FillDDL_Location_Add()
        //Clear_Error_Success_Box()
    }

    public void All_Centre_ChkBox_Selected(object sender, System.EventArgs e)//NFFFF
    {
        //Change checked status of a hidden check box
        chkCentreAllHidden_Sel.Checked = !(chkCentreAllHidden_Sel.Checked);

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlCentre_Add.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCentre");

            chkitemck.Checked = chkCentreAllHidden_Sel.Checked;
        }

    }

    public void chkCentre_Checked(object sender, System.EventArgs e)//NFFFFF
    {
        CheckBox chkCentre = (CheckBox)sender;
        DataListItem row = (DataListItem)chkCentre.NamingContainer;

        HtmlInputCheckBox chkDL_PrimaryFlag = (HtmlInputCheckBox)row.FindControl("chkDL_PrimaryFlag");

        if (chkCentre.Checked == true)
        {
            chkDL_PrimaryFlag.Visible = true;
        }
        else
        {
            chkDL_PrimaryFlag.Checked = false;
            chkDL_PrimaryFlag.Visible = false;
        }
    }

    protected void BtnSave_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();

        //Validation
        //Validate if all information is entered correctly
        if (ddlTitle_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0053");
            ddlTitle_Add.Focus();
            return;
        }

        if (string.IsNullOrEmpty(txtFirstName_Add.Text.Trim()))
        {
            Show_Error_Success_Box("E", "0054");
            txtFirstName_Add.Focus();
            return;
        }

        if (string.IsNullOrEmpty(txtHandPhone1_Add.Text.Trim()))
        {
            Show_Error_Success_Box("E", "0055");
            txtHandPhone1_Add.Focus();
            return;
        }

        if ((txtHandPhone1_Add.Text.Trim().Length) < 10)
        {
            Show_Error_Success_Box("E", "0059");
            txtHandPhone1_Add.Focus();
            return;
        }

        if (txtHandPhone2_Add.Text.Length > 0 && txtHandPhone2_Add.Text.Trim().Length < 10)
        {
            Show_Error_Success_Box("E", "0060");
            txtHandPhone1_Add.Focus();
            return;
        }

        if (ddlGender_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0056");
            ddlGender_Add.Focus();
            return;
        }

        if (ddlCompany_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0043");
            ddlCompany_Add.Focus();
            return;
        }

        if (ddlCountry_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0040");
            ddlCountry_Add.Focus();
            return;
        }

        if (ddlState_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0041");
            ddlState_Add.Focus();
            return;
        }

        if (ddlCity_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0042");
            ddlCity_Add.Focus();
            return;
        }

        int SelCnt = 0;
        //Check if unit of measurement is mentioned or not
        SelCnt = 0;
        string ActivityCode = "";
        foreach (DataListItem dtlItem in dlCapacity_Add.Items)
        {
            CheckBox chkDL_Select_Activity = (CheckBox)dtlItem.FindControl("chkDL_Select_Activity");
            Label lblDL_Activity_Id = (Label)dtlItem.FindControl("lblDL_Activity_Id");
            if (chkDL_Select_Activity.Checked == true)
            {
                SelCnt = SelCnt + 1;
                ActivityCode = ActivityCode + lblDL_Activity_Id.Text + ",";
            }
        }
        ActivityCode = Common.RemoveComma(ActivityCode);
        //if (Strings.Right(ActivityCode, 1) == ",")
        //    ActivityCode = Strings.Left(ActivityCode, Strings.Len(ActivityCode) - 1);

        if (SelCnt == 0)
        {
            Show_Error_Success_Box("E", "0057");
            return;
        }

        SelCnt = 0;
        string DivisionCode = "";
        foreach (DataListItem dtlItem in dlCentre_Add.Items)
        {
            CheckBox chkDL_Select_Centre = (CheckBox)dtlItem.FindControl("chkDL_Select_Centre");
            Label lblDivisionCode = (Label)dtlItem.FindControl("lblDivisionCode");

            if (chkDL_Select_Centre.Checked == true)
            {
                SelCnt = SelCnt + 1;
                DivisionCode = DivisionCode + lblDivisionCode.Text + ",";

            }
        }
        DivisionCode = Common.RemoveComma(DivisionCode);
        //if (Strings.Right(DivisionCode, 1) == ",")
        //    DivisionCode = Strings.Left(DivisionCode, Strings.Len(DivisionCode) - 1);

        if (SelCnt == 0)
        {
            Show_Error_Success_Box("E", "0001");
            dlCentre_Add.Focus();
            return;
        }


        //Save
        string ResultId = null;
        string CountryCode = null;
        CountryCode = ddlCountry_Add.SelectedValue;

        string CompanyCode = null;
        CompanyCode = ddlCompany_Add.SelectedValue;

        string StateCode = null;
        StateCode = ddlState_Add.SelectedValue;

        string CityCode = null;
        CityCode = ddlCity_Add.SelectedValue;

        string LocationCode = null;
        LocationCode = "";

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        int ActiveFlag = 0;
        if (chkActiveFlag.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }

        string AreaName = null;
        AreaName = txtAreaName_Add.Text;

        string Remarks = null;
        Remarks = txtRemarks_Add.Text;

        ResultId = ProductController.Insert_Partner(CompanyCode, ddlTitle_Add.SelectedItem.Text, txtFirstName_Add.Text, txtMiddleName_Add.Text, txtLastName_Add.Text, ddlGender_Add.SelectedItem.Text,Convert.ToDateTime(txtDOB.Value),Convert.ToDateTime(txtDOJ.Value), txtQualification_Add.Text, txtHandPhone1_Add.Text,
        txtHandPhone2_Add.Text, txtPhoneNo_Add.Text, txtEmailId_Add.Text, txtRoomNo_Add.Text, txtBuilding_Add.Text, txtRoadName_Add.Text, CountryCode, StateCode, CityCode, LocationCode,
        txtPincode_Add.Text, ActiveFlag, CreatedBy, ActivityCode, DivisionCode, txtEmployeeNo_Add.Text, AreaName, Remarks);

        //Close the Add Panel and go to Search Grid
        if (ResultId == "-1")
        {
            Show_Error_Success_Box("E", "0058");
            txtFirstName_Add.Focus();
            return;
        }
        else
        {
            ControlVisibility("Result");
            BtnSearch_Click(sender, e);
            Show_Error_Success_Box("S", "0000");
            Clear_AddPanel();
        }

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

    private void Clear_AddPanel()
    {
        ddlCompany_Add.SelectedIndex = 0;
        ddlCountry_Add.SelectedIndex = 0;
        ddlState_Add.Items.Clear();
        ddlCity_Add.Items.Clear();
        //ddlLocation_Add.Items.Clear()

        dlCapacity_Add.DataSource = null;
        dlCapacity_Add.DataBind();
        dlCentre_Add.DataSource = null;
        dlCentre_Add.DataBind();

        txtFirstName_Add.Text = "";
        txtMiddleName_Add.Text = "";
        txtLastName_Add.Text = "";
        txtHandPhone1_Add.Text = "";
        txtHandPhone2_Add.Text = "";
        txtPhoneNo_Add.Text = "";
        txtEmailId_Add.Text = "";
        ddlTitle_Add.SelectedIndex = 0;
        txtBuilding_Add.Text = "";
        txtRoomNo_Add.Text = "";
        txtRoadName_Add.Text = "";
        txtPincode_Add.Text = "";
        txtEmployeeNo_Add.Text = "";
        txtRemarks_Add.Text = "";
        txtQualification_Add.Text = "";
        txtAreaName_Add.Text = "";

        ddlCompany_Add.Visible = true;
        lblCompany_Add.Visible = false;

        BtnSaveEdit.Visible = false;
        BtnSave.Visible = true;
        txtDOJ.Value = System.DateTime.Now.ToString("dd MMM yyyy");
        txtDOB.Value = System.DateTime.Now.ToString("dd MMM yyyy");
    }

    //Protected Sub ddlLocation_Add_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLocation_Add.SelectedIndexChanged
    //    Clear_Error_Success_Box()
    //End Sub

    protected void btnDelete_Yes_Click(object sender, System.EventArgs e)
    {
        //Clear_Error_Success_Box()
        //'Authorise the selected test
        //Dim PKey As String
        //PKey = lblPKey_Authorise.Text

        //Dim lblHeader_User_Code As Label
        //lblHeader_User_Code = CType(Master.FindControl("lblHeader_User_Code"), Label)

        //Dim AlteredBy As String
        //AlteredBy = lblHeader_User_Code.Text

        //Dim ResultId As Integer
        //ResultId = ProductController.UpdateTest_Authorise_Block(PKey, 2, AlteredBy)

        //'Close the Add Panel and go to Search Grid
        //If ResultId = 1 Then
        //    ControlVisibility("Result")
        //    BtnSearch_Click(sender, e)
        //    Show_Error_Success_Box("S", "0000")
        //    Clear_AddPanel()
        //End If
    }

    protected void ddlCity_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        //FillDDL_Location()
        //Clear_Error_Success_Box()
    }

    protected void ddlCompany_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Centre();
        Clear_Error_Success_Box();
    }

    private void FillDL_Activity()
    {
        DataSet dsGrid = ProductController.GetAllActiveActivity("", "2");
        dlCapacity_Add.DataSource = dsGrid;
        dlCapacity_Add.DataBind();
    }

    protected void BtnSaveEdit_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();

        //Validation
        //Validate if all information is entered correctly
        if (ddlTitle_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0053");
            ddlTitle_Add.Focus();
            return;
        }

        if (string.IsNullOrEmpty(txtFirstName_Add.Text.Trim()))
        {
            Show_Error_Success_Box("E", "0054");
            txtFirstName_Add.Focus();
            return;
        }

        if (string.IsNullOrEmpty(txtHandPhone1_Add.Text.Trim()))
        {
            Show_Error_Success_Box("E", "0055");
            txtHandPhone1_Add.Focus();
            return;
        }

        if ((txtHandPhone1_Add.Text.Trim().Length) < 10)
        {
            Show_Error_Success_Box("E", "0059");
            txtHandPhone1_Add.Focus();
            return;
        }

        if ((txtHandPhone2_Add.Text.Length) > 0 && txtHandPhone2_Add.Text.Trim().Length < 10)
        {
            Show_Error_Success_Box("E", "0060");
            txtHandPhone1_Add.Focus();
            return;
        }

        if (ddlGender_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0056");
            ddlGender_Add.Focus();
            return;
        }

        if (ddlCompany_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0043");
            ddlCompany_Add.Focus();
            return;
        }

        if (ddlCountry_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0040");
            ddlCountry_Add.Focus();
            return;
        }

        if (ddlState_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0041");
            ddlState_Add.Focus();
            return;
        }

        if (ddlCity_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0042");
            ddlCity_Add.Focus();
            return;
        }

        int SelCnt = 0;
        //Check if unit of measurement is mentioned or not
        SelCnt = 0;
        string ActivityCode = "";
        foreach (DataListItem dtlItem in dlCapacity_Add.Items)
        {
            CheckBox chkDL_Select_Activity = (CheckBox)dtlItem.FindControl("chkDL_Select_Activity");
            Label lblDL_Activity_Id = (Label)dtlItem.FindControl("lblDL_Activity_Id");
            if (chkDL_Select_Activity.Checked == true)
            {
                SelCnt = SelCnt + 1;
                ActivityCode = ActivityCode + lblDL_Activity_Id.Text + ",";
            }
        }
        ActivityCode = Common.RemoveComma(ActivityCode);
        //if (Strings.Right(ActivityCode, 1) == ",")
        //    ActivityCode = Strings.Left(ActivityCode, Strings.Len(ActivityCode) - 1);

        if (SelCnt == 0)
        {
            Show_Error_Success_Box("E", "0057");
            return;
        }

        SelCnt = 0;
        string DivisionCode = "";
        foreach (DataListItem dtlItem in dlCentre_Add.Items)
        {
            CheckBox chkDL_Select_Centre = (CheckBox)dtlItem.FindControl("chkDL_Select_Centre");
            Label lblDivisionCode = (Label)dtlItem.FindControl("lblDivisionCode");

            if (chkDL_Select_Centre.Checked == true)
            {
                SelCnt = SelCnt + 1;
                DivisionCode = DivisionCode + lblDivisionCode.Text + ",";

            }
        }
        DivisionCode = Common.RemoveComma(DivisionCode);
        //if (Strings.Right(DivisionCode, 1) == ",")
        //    DivisionCode = Strings.Left(DivisionCode, Strings.Len(DivisionCode) - 1);

        if (SelCnt == 0)
        {
            Show_Error_Success_Box("E", "0001");
            dlCentre_Add.Focus();
            return;
        }


        //Save
        string ResultId = null;
        string CountryCode = null;
        CountryCode = ddlCountry_Add.SelectedValue;

        string CompanyCode = null;
        CompanyCode = ddlCompany_Add.SelectedValue;

        string StateCode = null;
        StateCode = ddlState_Add.SelectedValue;

        string CityCode = null;
        CityCode = ddlCity_Add.SelectedValue;

        string LocationCode = null;
        LocationCode = "";

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        string CreatedBy = null;
        CreatedBy = lblHeader_User_Code.Text;

        int ActiveFlag = 0;
        if (chkActiveFlag.Checked == true)
        {
            ActiveFlag = 1;
        }
        else
        {
            ActiveFlag = 0;
        }

        string AreaName = null;
        AreaName = txtAreaName_Add.Text;

        string Remarks = null;
        Remarks = txtRemarks_Add.Text;

        ResultId = ProductController.Update_Partner(lblPKey_Edit.Text, CompanyCode, ddlTitle_Add.SelectedItem.Text, txtFirstName_Add.Text, txtMiddleName_Add.Text, txtLastName_Add.Text, ddlGender_Add.SelectedItem.Text,Convert.ToDateTime(txtDOB.Value),Convert.ToDateTime(txtDOJ.Value), txtQualification_Add.Text,
        txtHandPhone1_Add.Text, txtHandPhone2_Add.Text, txtPhoneNo_Add.Text, txtEmailId_Add.Text, txtRoomNo_Add.Text, txtBuilding_Add.Text, txtRoadName_Add.Text, CountryCode, StateCode, CityCode,
        LocationCode, txtPincode_Add.Text, ActiveFlag, CreatedBy, ActivityCode, DivisionCode, txtEmployeeNo_Add.Text, AreaName, Remarks);

        //Close the Add Panel and go to Search Grid
        if (ResultId == "-1")
        {
            Show_Error_Success_Box("E", "0058");
            txtFirstName_Add.Focus();
            return;
        }
        else
        {
            ControlVisibility("Result");
            BtnSearch_Click(sender, e);
            Show_Error_Success_Box("S", "0000");
            Clear_AddPanel();
        }
    }

    protected void btnExport_Click(object sender, System.EventArgs e)
    {
        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=Master_Partners.xls");

        Response.Charset = "";


        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        dlGridExport.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();
    }

    protected void ddlTitle_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        string Gender = null;
        Gender = ddlTitle_Gender.Items[ddlTitle_Add.SelectedIndex].Text;
        if (!string.IsNullOrEmpty(Gender))
        {
            ddlGender_Add.SelectedValue = Gender;
            ddlGender_Add.Enabled = false;
        }
        else
        {
            ddlGender_Add.SelectedIndex = 0;
            ddlGender_Add.Enabled = true;
        }
    }

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        //Validate if all information is entered correctly
        if (ddlCountry.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0040");
            ddlCountry.Focus();
            return;
        }

        if (ddlState.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0041");
            ddlState.Focus();
            return;
        }

        if (ddlCity.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0042");
            ddlCity.Focus();
            return;
        }


        string LocationCode = "";
        //Dim LocationCnt As Integer
        //Dim LocationSelCnt As Integer = 0
        //For LocationCnt = 0 To ddlLocation.Items.Count - 1
        //    If ddlLocation.Items(LocationCnt).Selected = True Then
        //        LocationSelCnt = LocationSelCnt + 1
        //    End If
        //Next

        //If LocationSelCnt = 0 Then
        //    'When all is selected
        //    For LocationCnt = 0 To ddlLocation.Items.Count - 1
        //        LocationCode = LocationCode & ddlLocation.Items(LocationCnt).Value & ","
        //    Next
        //    If Right(LocationCode, 1) = "," Then LocationCode = Left(LocationCode, Len(LocationCode) - 1)
        //Else
        //    For LocationCnt = 0 To ddlLocation.Items.Count - 1
        //        If ddlLocation.Items(LocationCnt).Selected = True Then
        //            LocationCode = LocationCode & ddlLocation.Items(LocationCnt).Value & ","
        //        End If
        //    Next
        //    If Right(LocationCode, 1) = "," Then LocationCode = Left(LocationCode, Len(LocationCode) - 1)
        //End If

        int ActiveStatus = 0;
        if (ddlStatus.SelectedIndex == 0)
        {
            ActiveStatus = -1;
        }
        else if (ddlStatus.SelectedIndex == 1)
        {
            ActiveStatus = 1;
        }
        else
        {
            ActiveStatus = 0;
        }

        ControlVisibility("Result");

        string CountryCode = null;
        CountryCode = ddlCountry.SelectedValue;

        string StateCode = null;
        StateCode = ddlState.SelectedValue;

        string CityCode = null;
        CityCode = ddlCity.SelectedValue;

        string PartnerName = null;
        if (string.IsNullOrEmpty(txtPartnerName.Text.Trim()))
        {
            PartnerName = "%";
        }
        else
        {
            PartnerName = "%" + txtPartnerName.Text.Trim();
        }

        string HandPhone = null;
        if (string.IsNullOrEmpty(txtHandPhone.Text.Trim()))
        {
            HandPhone = "%";
        }
        else
        {
            HandPhone = "%" + txtHandPhone.Text.Trim();
        }

        DataSet dsGrid = ProductController.GetPartnerMasterBy_Country_State_City(CountryCode, StateCode, CityCode, LocationCode, PartnerName, HandPhone, ActiveStatus, "1");
        if (dsGrid != null)
        {
            if (dsGrid.Tables.Count != 0)
            {

                dlGridDisplay.DataSource = dsGrid;
                dlGridDisplay.DataBind();

                dlGridExport.DataSource = dsGrid;
                dlGridExport.DataBind();

                lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
            }
        }
        else
        {
            lbltotalcount.Text = "0";
        }
        
        
    }
    public Master_Partner()
    {
        Load += Page_Load;
    }
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        txtPartnerName.Text = "";
        ddlCountry.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        ddlCity.SelectedIndex = 0;
        txtHandPhone.Text = "";
        ddlStatus.SelectedIndex = 0;
    }
}
