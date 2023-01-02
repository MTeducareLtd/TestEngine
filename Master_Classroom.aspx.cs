using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

partial class Master_Classroom : System.Web.UI.Page
{

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

        if (ddlPremisesType.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0063");
            ddlPremisesType.Focus();
            return;
        }


        string LocationCode = "";
        int LocationCnt = 0;
        int LocationSelCnt = 0;
        for (LocationCnt = 0; LocationCnt <= ddlLocation.Items.Count - 1; LocationCnt++)
        {
            if (ddlLocation.Items[LocationCnt].Selected == true)
            {
                LocationSelCnt = LocationSelCnt + 1;
            }
        }

        if (LocationSelCnt == 0)
        {
            //When all is selected
            for (LocationCnt = 0; LocationCnt <= ddlLocation.Items.Count - 1; LocationCnt++)
            {
                LocationCode = LocationCode + ddlLocation.Items[LocationCnt].Value + ",";
            }
            LocationCode = Common.RemoveComma(LocationCode);
            //if (Strings.Right(LocationCode, 1) == ",")
            //    LocationCode = Strings.Left(LocationCode, Strings.Len(LocationCode) - 1);
        }
        else
        {
            for (LocationCnt = 0; LocationCnt <= ddlLocation.Items.Count - 1; LocationCnt++)
            {
                if (ddlLocation.Items[LocationCnt].Selected == true)
                {
                    LocationCode = LocationCode + ddlLocation.Items[LocationCnt].Value + ",";
                }
            }
            LocationCode = Common.RemoveComma(LocationCode);
            //if (Strings.Right(LocationCode, 1) == ",")
            //    LocationCode = Strings.Left(LocationCode, Strings.Len(LocationCode) - 1);
        }

        ControlVisibility("Result");

        string CountryCode = null;
        CountryCode = ddlCountry.SelectedValue;

        string StateCode = null;
        StateCode = ddlState.SelectedValue;

        string CityCode = null;
        CityCode = ddlCity.SelectedValue;

        string CRName = null;
        if (string.IsNullOrEmpty(txtClassroomName.Text.Trim()))
        {
            CRName = "%";
        }
        else
        {
            CRName = "%" + txtClassroomName.Text.Trim();
        }

        DataSet dsGrid = ProductController.GetPremisesMasterBy_Country_State_City(CountryCode, StateCode, CityCode, LocationCode, CRName, ddlPremisesType.SelectedValue, "1");
        dlGridDisplay.DataSource = dsGrid;
        dlGridDisplay.DataBind();

        dlGridExport.DataSource = dsGrid;
        dlGridExport.DataBind();

        lbltotalcount.Text =Convert.ToString(dsGrid.Tables[0].Rows.Count);
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            FillDDL_Company();
            FillDDL_ClassroomType();
            FillDDL_PremisesType();
            FillDDL_Country();
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
            DivAddPanel_Classroom.Visible = false;
            BtnAdd.Visible = true;


        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = false;
            DivAddPanel_Classroom.Visible = false;
            BtnAdd.Visible = true;
        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel_Classroom.Visible = false;
            BtnAdd.Visible = false;
        }
        else if (Mode == "ClassRoom")
        {
            DivAddPanel.Visible = false;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel_Classroom.Visible = true;
            BtnAdd.Visible = false;


        }
        Clear_Error_Success_Box();
    }

    protected void BtnAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Add");
        lblCompany_Add.Visible = false;
        lblCountry_Add.Visible = false;
        lblState_Add.Visible = false;
        ddlCountry_Add.Visible = true;
        ddlState_Add.Visible = true;
        ddlCity_Add.Visible = true;
        ddlCompany_Add.Visible = true;
        lblCity_Add.Visible = false;
        lblHeader_Add.Text = "Create New Premises";
        lblTestPKey_Hidden.Text = "";
        BtnSaveEdit.Visible = false;
        BtnSave.Visible = true;
        DivClassRoomAdd.Visible = false;
        dlClassRoom.Visible = false;


        txtPremisesName_Add.Text = "";
        txtPremisesShortName_Add.Text = "";
        ddlCompany_Add.SelectedIndex = 0;
        ddlCountry_Add.SelectedIndex = 0;        
        ddlPremisesType_Add.SelectedIndex = 0;
        chkActiveFlag.Checked = false;
        txtPremisesSize_Add.Text = "";
        txtAddress_Add.Text = "";

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
            lblCity_Add.Visible = true;
            lblCountry_Add.Visible = true;
            lblState_Add.Visible = true;
            ddlCountry_Add.Visible = false;
            ddlState_Add.Visible = false;
            ddlCity_Add.Visible = false;
            lblCompany_Add.Visible = true;
            ddlCompany_Add.Visible = false;

            BtnSave.Visible = false;
            BtnSaveEdit.Visible = true;

            lblPKey_PremisesCode.Text = e.CommandArgument.ToString();
            lblHeader_Add.Text = "Edit Premises";
            lblTestPKey_Hidden.Text = "";

            DivClassRoomAdd.Visible = true;
            dlClassRoom.Visible = true;

            FillPremisesMasterDetails(lblPKey_PremisesCode.Text, e.CommandName);


        }
    }

    private void FillPremisesMasterDetails(string PKey, string CommandName)
    {
        DataSet dsCRoom = ProductController.GetPremisesMaster_ByPKey(PKey, "1");

        if (dsCRoom.Tables[0].Rows.Count > 0)
        {
            lblTestPKey_Hidden.Text = PKey;

            string Country_Code = null;
            Country_Code =Convert.ToString(dsCRoom.Tables[0].Rows[0]["Country_Code"]);

            string State_Code = null;
            State_Code =Convert.ToString(dsCRoom.Tables[0].Rows[0]["State_Code"]);

            string City_Code = null;
            City_Code =Convert.ToString(dsCRoom.Tables[0].Rows[0]["City_Code"]);

            lblCountry_Add.Text =Convert.ToString(ddlCountry.SelectedItem);
            lblState_Add.Text = ddlState.SelectedItem.ToString();
            lblCity_Add.Text = ddlCity.SelectedItem.ToString();

            txtPremisesName_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Premises_LName"]);
            txtPremisesShortName_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Premises_SName"]);

            ddlCompany_Add.SelectedValue = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Company_Code"]);
            lblCompany_Add.Text = Convert.ToString(ddlCompany_Add.SelectedItem);
            // dsCRoom.Tables(0).Rows(0)("Classroom_SName")
            lblCompanyCode_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Company_Code"]);

            FillDDL_Location_Add(City_Code);
            ddlLocation_Add.SelectedValue = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Location_Code"]);

            txtAddress_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Premises_Address"]);
            txtPremisesSize_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Area_inSQFeet"]);
            ddlPremisesType_Add.SelectedValue = Convert.ToString(dsCRoom.Tables[0].Rows[0]["PremisesType_Id"]);


            if (Convert.ToInt32(dsCRoom.Tables[0].Rows[0]["IsActive"]) == 0)
            {
                chkActiveFlag.Checked = false;
            }
            else
            {
                chkActiveFlag.Checked = true;
            }

            dlClassRoom.DataSource = dsCRoom.Tables[1];
            dlClassRoom.DataBind();

            lblRoomCnt_InPremises.Text =Convert.ToString(dsCRoom.Tables[1].Rows.Count);

        }
    }

    private void FillClassroomMasterDetails(string PKey, string CommandName)
    {
        DataSet dsCRoom = ProductController.GetClassroomMaster_ByPKey(PKey, 1);

        if (dsCRoom.Tables[0].Rows.Count > 0)
        {
            lblTestPKey_Hidden.Text = PKey;

            //Dim Country_Code As String
            //Country_Code = dsCRoom.Tables(0).Rows(0)("Country_Code")

            //Dim State_Code As String
            //State_Code = dsCRoom.Tables(0).Rows(0)("State_Code")

            //Dim City_Code As String
            //City_Code = dsCRoom.Tables(0).Rows(0)("City_Code")

            //lblCountry_Add.Text = ddlCountry.SelectedItem.ToString
            //lblState_Add.Text = ddlState.SelectedItem.ToString
            //lblCity_Add.Text = ddlCity.SelectedItem.ToString

            txtClassroomName_Add.Text  = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Classroom_LName"]);
            txtClassroom_ShortName_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Classroom_SName"]);

            ddlCompany_Add.SelectedValue = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Company_Code"]);
            //lblCompany_Add.Text = ddlCompany_Add.SelectedItem.ToString    ' dsCRoom.Tables(0).Rows(0)("Classroom_SName")
            FillDDL_Centre();

            txtWidth_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Width_inFeet"]);
            txtHeight_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Height_inFeet"]);
            txtLength_Add.Text = Convert.ToString(dsCRoom.Tables[0].Rows[0]["Length_inFeet"]);

            ddlClassroomType_Add.SelectedValue = Convert.ToString(dsCRoom.Tables[0].Rows[0]["ClassroomType_Id"]);
            lblPremisesName_InClassroom.Text = txtPremisesName_Add.Text;
            lblPremisesShortName_InClassroom.Text = txtPremisesShortName_Add.Text;

            //ddlLocation_Add.DataSource = dsCRoom.Tables(1)
            //ddlLocation_Add.DataTextField = "Location_Name"
            //ddlLocation_Add.DataValueField = "Location_Code"
            //ddlLocation_Add.DataBind()
            //ddlLocation_Add.Items.Insert(0, "Select")
            //ddlLocation_Add.SelectedIndex = 0

            //ddlLocation_Add.SelectedValue = dsCRoom.Tables(0).Rows(0)("Location_Code")

            if (Convert.ToInt32(dsCRoom.Tables[0].Rows[0]["IsActive"]) == 0)
            {
                chkActiveFlag.Checked = false;
            }
            else
            {
                chkActiveFlag.Checked = true;
            }

            dlCapacity_Add.DataSource = dsCRoom.Tables[2];
            dlCapacity_Add.DataBind();

            for (int cnt = 0; cnt <= dsCRoom.Tables[3].Rows.Count - 1; cnt++)
            {
                foreach (DataListItem dtlItem in dlCentre_Add.Items)
                {
                    CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCentre");
                    Label lblCenterCode = (Label)dtlItem.FindControl("lblCenterCode");
                    HtmlInputCheckBox chkDL_PrimaryFlag = (HtmlInputCheckBox)dtlItem.FindControl("chkDL_PrimaryFlag");
                    if (lblCenterCode.Text == dsCRoom.Tables[3].Rows[cnt]["Centre_Code"])
                    {
                        chkitemck.Checked = true;
                        chkDL_PrimaryFlag.Visible = true;
                        if (Convert.ToInt32(dsCRoom.Tables[3].Rows[cnt]["Primary_Ownership_Status"]) == 1)
                        {
                            chkDL_PrimaryFlag.Checked = true;
                        }
                        else
                        {
                            chkDL_PrimaryFlag.Checked = false;
                        }
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }

            }

            //dlCentre_Add.DataSource = dsCRoom.Tables(3)
            //dlCentre_Add.DataBind()
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

    private void FillDDL_PremisesType()
    {
        DataSet dsPremisesType = ProductController.GetAllActivePremisesType();
        BindDDL(ddlPremisesType_Add, dsPremisesType, "PremisesType_Name", "Premisestype_Id");
        ddlPremisesType_Add.Items.Insert(0, "Select");
        ddlPremisesType_Add.SelectedIndex = 0;

        BindDDL(ddlPremisesType, dsPremisesType, "PremisesType_Name", "Premisestype_Id");
        ddlPremisesType.Items.Insert(0, "Select");
        ddlPremisesType.SelectedIndex = 0;


    }

    private void FillDDL_ClassroomType()
    {
        DataSet dsCRoomModes = ProductController.GetAllActiveClassroomType();
        BindDDL(ddlClassroomType_Add, dsCRoomModes, "ClassroomType_Name", "Classroomtype_Id");
        ddlClassroomType_Add.Items.Insert(0, "Select");
        ddlClassroomType_Add.SelectedIndex = 0;

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


        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");



        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblCompanyCode_Add.Text, "", "", "6", lblHeader_DBName.Text);
        dlCentre_Add.DataSource = dsCentre;
        dlCentre_Add.DataBind();
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

    private void FillDDL_Location()
    {
        string City_Code = null;
        City_Code = ddlCity.SelectedValue;

        DataSet dsLocation = ProductController.GetAllActiveLocation(City_Code);
        BindListBox(ddlLocation, dsLocation, "Location_Name", "Location_Code");
    }

    private void FillDDL_Location_Add(string City_Code)
    {
        DataSet dsLocation = ProductController.GetAllActiveLocation(City_Code);
        BindDDL(ddlLocation_Add, dsLocation, "Location_Name", "Location_Code");
        ddlLocation_Add.Items.Insert(0, "Select");
        ddlLocation_Add.SelectedIndex = 0;
    }

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
        string City_Code = null;
        City_Code = ddlCity_Add.SelectedValue;
        FillDDL_Location_Add(City_Code);
        Clear_Error_Success_Box();
    }

    public void All_Centre_ChkBox_Selected(object sender, System.EventArgs e)
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

    public void chkCentre_Checked(object sender, System.EventArgs e)
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

        if (ddlLocation_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0044");
            ddlLocation_Add.Focus();
            return;
        }

        if (ddlPremisesType_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0063");
            ddlPremisesType_Add.Focus();
            return;
        }

        if (Convert.ToInt32(txtPremisesSize_Add.Text.Length) <= 0)
        {
            Show_Error_Success_Box("E", "0061");
            txtPremisesSize_Add.Focus();
            return;
        }



        //'Validate if classroom Name is duplicate or not


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
        LocationCode = ddlLocation_Add.SelectedValue;

        string PremisesTypeCode = null;
        PremisesTypeCode = ddlPremisesType_Add.SelectedValue;

        double CRArea = 0;
        CRArea = Convert.ToDouble(txtPremisesSize_Add.Text);

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

        ResultId = ProductController.Insert_Premises(CompanyCode, CountryCode, StateCode, CityCode, LocationCode, txtPremisesName_Add.Text, txtPremisesShortName_Add.Text, CRArea, PremisesTypeCode, ActiveFlag,
        txtAddress_Add.Text, CreatedBy);

        //Close the Add Panel and go to Search Grid
        if (ResultId != "-1")
        {
            ControlVisibility("Result");
            BtnSearch_Click(sender, e);
            Show_Error_Success_Box("S", "0000");
            Clear_AddPanel();
        }
        else if (ResultId == "-1")
        {
            Show_Error_Success_Box("E", "0062");
            txtPremisesName_Add.Focus();
            return;
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
        ddlLocation_Add.Items.Clear();

        dlCapacity_Add.DataSource = null;
        dlCapacity_Add.DataBind();
        dlCentre_Add.DataSource = null;
        dlCentre_Add.DataBind();

        txtPremisesName_Add.Text = "";
        txtPremisesShortName_Add.Text = "";
        txtLength_Add.Text = "";
        txtHeight_Add.Text = "";
        txtWidth_Add.Text = "";
        ddlPremisesType_Add.SelectedIndex = 0;

        BtnSaveEdit.Visible = false;
        BtnSave.Visible = true;
    }

    private void Clear_AddPanel_Room()
    {
        lblPremisesName_InClassroom.Text = "";
        lblPremisesShortName_InClassroom.Text = "";
        ddlClassroomType_Add.SelectedIndex = 0;
        txtClassroomName_Add.Text = "";
        txtClassroom_ShortName_Add.Text = "";
        txtLength_Add.Text = "";
        txtHeight_Add.Text = "";
        txtWidth_Add.Text = "";

        btnSave_Classroom_Edit.Visible = false;
        btnSave_Classroom.Visible = true;
    }

    protected void ddlLocation_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();
    }

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
        FillDDL_Location();
        Clear_Error_Success_Box();
    }

    protected void ddlCompany_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        //FillDDL_Centre()
        Clear_Error_Success_Box();
    }

    private void FillDL_Activity()
    {
        string ClassroomId = null;
        ClassroomId = ddlClassroomType_Add.SelectedValue;
        DataSet dsGrid = ProductController.GetAllActiveActivity(ClassroomId, "1");
        dlCapacity_Add.DataSource = dsGrid;
        dlCapacity_Add.DataBind();
    }

    protected void dlCapacity_Add_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList ddlDLUOM =(DropDownList)e.Item.FindControl("ddlDLUOM");
            Label lblDLUOM =(Label)e.Item.FindControl("lblDLUOM");

            DataSet dsUOM = ProductController.GetAllActiveUnitOfMeasurement();
            BindDDL(ddlDLUOM, dsUOM, "UOM_Name", "UOM_Id");

            try
            {
                ddlDLUOM.SelectedValue = lblDLUOM.Text;
            }
            catch (Exception ex)
            {
                ddlDLUOM.SelectedIndex = 0;
            }

        }
    }

    protected void BtnSaveEdit_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();

        //Validate if all information is entered correctly


        if (ddlLocation_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0044");
            ddlLocation_Add.Focus();
            return;
        }

        if (ddlPremisesType_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0063");
            ddlPremisesType_Add.Focus();
            return;
        }

        if (Convert.ToInt32(txtPremisesSize_Add.Text) <= 0)
        {
            Show_Error_Success_Box("E", "0061");
            txtPremisesSize_Add.Focus();
            return;
        }



        //'Validate if classroom Name is duplicate or not


        //Save
        string ResultId = null;

        string LocationCode = null;
        LocationCode = ddlLocation_Add.SelectedValue;

        string PremisesTypeCode = null;
        PremisesTypeCode = ddlPremisesType_Add.SelectedValue;

        float CRArea = 0;
        CRArea = float.Parse(txtPremisesSize_Add.Text);

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

        string Premises_Code = null;
        Premises_Code = lblPKey_PremisesCode.Text;

        ResultId = ProductController.Update_Premises(Premises_Code, LocationCode, txtPremisesName_Add.Text, txtPremisesShortName_Add.Text, CRArea, PremisesTypeCode, ActiveFlag, txtAddress_Add.Text, CreatedBy);

        //Close the Add Panel and go to Search Grid
        if (ResultId != "-1")
        {
            ControlVisibility("Result");
            BtnSearch_Click(sender, e);
            Show_Error_Success_Box("S", "0000");
            Clear_AddPanel();
        }
        else if (ResultId == "-1")
        {
            Show_Error_Success_Box("E", "0062");
            txtPremisesName_Add.Focus();
            return;
        }
    }

    protected void btnExport_Click(object sender, System.EventArgs e)
    {
        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=Master_Classroom.xls");

        Response.Charset = "";


        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        dlGridExport.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();
    }

    protected void btnSave_Classroom_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();

        //Validation
        //Validate if all information is entered correctly
        //If ddlCompany_Add.SelectedIndex = 0 Then
        //    Show_Error_Success_Box("E", "0043")
        //    ddlCompany_Add.Focus()
        //    Exit Sub
        //End If

        //If ddlCountry_Add.SelectedIndex = 0 Then
        //    Show_Error_Success_Box("E", "0040")
        //    ddlCountry_Add.Focus()
        //    Exit Sub
        //End If

        //If ddlState_Add.SelectedIndex = 0 Then
        //    Show_Error_Success_Box("E", "0041")
        //    ddlState_Add.Focus()
        //    Exit Sub
        //End If

        //If ddlCity_Add.SelectedIndex = 0 Then
        //    Show_Error_Success_Box("E", "0042")
        //    ddlCity_Add.Focus()
        //    Exit Sub
        //End If

        //If ddlLocation_Add.SelectedIndex = 0 Then
        //    Show_Error_Success_Box("E", "0044")
        //    ddlLocation_Add.Focus()
        //    Exit Sub
        //End If

        if (ddlClassroomType_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0045");
            ddlClassroomType_Add.Focus();
            return;
        }

        if (Convert.ToInt32(txtLength_Add.Text) <= 0)
        {
            Show_Error_Success_Box("E", "0046");
            txtLength_Add.Focus();
            return;
        }

        if (Convert.ToInt32(txtWidth_Add.Text) <= 0)
        {
            Show_Error_Success_Box("E", "0047");
            txtWidth_Add.Focus();
            return;
        }

        if (Convert.ToInt32(txtHeight_Add.Text) <= 0)
        {
            Show_Error_Success_Box("E", "0048");
            txtHeight_Add.Focus();
            return;
        }

        int SelCnt = 0;
        //Check if unit of measurement is mentioned or not
        //SelCnt = 0
        //Dim ChapterCode As String = ""
        foreach (DataListItem dtlItem in dlCapacity_Add.Items)
        {
            DropDownList ddlDLUOM = (DropDownList)dtlItem.FindControl("ddlDLUOM");
            TextBox txtDL_Capacity = (TextBox)dtlItem.FindControl("txtDL_Capacity");
            if (Convert.ToInt32(txtDL_Capacity.Text) > 0)
            {
                if (ddlDLUOM.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "0052");
                    return;
                }

            }
        }

        SelCnt = 0;
        string CentreCode = "";
        string PrimaryCentreCode = "";
        foreach (DataListItem dtlItem in dlCentre_Add.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCentre");
            Label lblCenterCode = (Label)dtlItem.FindControl("lblCenterCode");
            HtmlInputCheckBox chkDL_PrimaryFlag = (HtmlInputCheckBox)dtlItem.FindControl("chkDL_PrimaryFlag");

            if (chkitemck.Checked == true)
            {
                if (chkDL_PrimaryFlag.Checked == true)
                {
                    SelCnt = SelCnt + 1;
                    PrimaryCentreCode = lblCenterCode.Text;
                }
                else
                {
                    CentreCode = CentreCode + lblCenterCode.Text + ",";
                }
            }
        }
        CentreCode = Common.RemoveComma(CentreCode);
        //if (Strings.Right(CentreCode, 1) == ",")
        //    CentreCode = Strings.Left(CentreCode, Strings.Len(CentreCode) - 1);

        if (SelCnt == 0)
        {
            Show_Error_Success_Box("E", "0050");
            dlCentre_Add.Focus();
            return;
        }

        if (SelCnt > 1)
        {
            Show_Error_Success_Box("E", "0051");
            dlCentre_Add.Focus();
            return;
        }

        //'Validate if classroom Name is duplicate or not


        //Save
        string ResultId = null;

        string CRTypeCode = null;
        CRTypeCode = ddlClassroomType_Add.SelectedValue;

        float CRLength = 0;
        CRLength = float.Parse(txtLength_Add.Text);

        float CRWidth = 0;
        CRWidth = float.Parse(txtWidth_Add.Text);

        float CRHeight = 0;
        CRHeight = float.Parse(txtHeight_Add.Text);

        float CRArea = 0;
        CRArea =float.Parse(Math.Round(CRLength * CRWidth, 1).ToString());

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

        string Premises_Code = null;
        Premises_Code = lblPKey_PremisesCode.Text;

        ResultId = ProductController.Insert_Classroom(txtClassroomName_Add.Text, txtClassroom_ShortName_Add.Text, CRLength, CRWidth, CRHeight, CRArea, CRTypeCode, ActiveFlag, Premises_Code, CreatedBy);

        //Close the Add Panel and go to Search Grid
        if (ResultId != "-1")
        {
            //Save Centre
            int Result1 = 0;
            string Classroom_Code = ResultId;
            Result1 = ProductController.Insert_ClassroomCentre(Classroom_Code, PrimaryCentreCode, CentreCode, CreatedBy);

            //Save Capacity

            string ActivityCode = "";

            int Classroom_Capacity = 0;
            foreach (DataListItem dtlItem in dlCapacity_Add.Items)
            {
                Label lblDL_Activity = (Label)dtlItem.FindControl("lblDL_Activity_Id");
                TextBox txtDL_Capacity = (TextBox)dtlItem.FindControl("txtDL_Capacity");
                DropDownList ddlDLUOM = (DropDownList)dtlItem.FindControl("ddlDLUOM");
                Classroom_Capacity = Convert.ToInt32(txtDL_Capacity.Text);

                Result1 = ProductController.Insert_ClassroomCapacity(Classroom_Code, lblDL_Activity.Text, Classroom_Capacity, ddlDLUOM.SelectedValue);
            }

            ControlVisibility("Add");
            FillPremisesMasterDetails(lblPKey_PremisesCode.Text, "Edit");
            Show_Error_Success_Box("S", "0000");
            Clear_AddPanel_Room();
        }
        else if (ResultId == "-1")
        {
            Show_Error_Success_Box("E", "0049");
            txtClassroomName_Add.Focus();
            return;
        }

    }

    protected void btnNewRoom_Click(object sender, System.EventArgs e)
    {
        lblPremisesName_InClassroom.Text = txtPremisesName_Add.Text;
        lblPremisesShortName_InClassroom.Text = txtPremisesShortName_Add.Text;
        lblHeader_Room_Add.Text = "Create New Room";
        FillDDL_Centre();

        ControlVisibility("ClassRoom");
    }

    protected void btnClose_Classroom_Click(object sender, System.EventArgs e) // NFFFFFF
    {
        ControlVisibility("Add");
        Clear_AddPanel_Room();
    }

    protected void ddlClassroomType_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDL_Activity();
    }

    protected void dlClassRoom_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            //lbldelCode.Text = e.CommandArgument
            //txtDeleteItemName.Text = (DirectCast(e.Item.FindControl("lblModeName"), Label).Text)
            //ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Pop", "openModalDelete();", True)
        }
        else if (e.CommandName == "Edit")
        {
            ControlVisibility("ClassRoom");
            //lblCity_Add.Visible = True
            //lblCountry_Add.Visible = True
            //lblState_Add.Visible = True
            //ddlCountry_Add.Visible = False
            //ddlState_Add.Visible = False
            //ddlCity_Add.Visible = False
            //lblCompany_Add.Visible = True
            //ddlCompany_Add.Visible = False

            btnSave_Classroom.Visible = false;
            btnSave_Classroom_Edit.Visible = true;
            lblPKey_ClassroomCode.Text = e.CommandArgument.ToString();
            lblHeader_Room_Add.Text = "Edit Room";
            FillClassroomMasterDetails(lblPKey_ClassroomCode.Text, e.CommandName);


        }
    }

    protected void btnSave_Classroom_Edit_Click(object sender, System.EventArgs e)
    {
        Clear_Error_Success_Box();

        //Validation
        //Validate if all information is entered correctly

        if (ddlClassroomType_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0045");
            ddlClassroomType_Add.Focus();
            return;
        }

        if (Convert.ToInt32(txtLength_Add.Text) <= 0)
        {
            Show_Error_Success_Box("E", "0046");
            txtLength_Add.Focus();
            return;
        }

        if (Convert.ToInt32(txtWidth_Add.Text) <= 0)
        {
            Show_Error_Success_Box("E", "0047");
            txtWidth_Add.Focus();
            return;
        }

        if (Convert.ToInt32(txtHeight_Add.Text) <= 0)
        {
            Show_Error_Success_Box("E", "0048");
            txtHeight_Add.Focus();
            return;
        }

        int SelCnt = 0;
        //Check if unit of measurement is mentioned or not
        //SelCnt = 0
        //Dim ChapterCode As String = ""
        foreach (DataListItem dtlItem in dlCapacity_Add.Items)
        {
            DropDownList ddlDLUOM = (DropDownList)dtlItem.FindControl("ddlDLUOM");
            TextBox txtDL_Capacity = (TextBox)dtlItem.FindControl("txtDL_Capacity");
            if (Convert.ToInt32(txtDL_Capacity.Text) > 0)
            {
                if (ddlDLUOM.SelectedIndex == 0)
                {
                    Show_Error_Success_Box("E", "0052");
                    return;
                }

            }
        }

        SelCnt = 0;
        string CentreCode = "";
        string PrimaryCentreCode = "";
        foreach (DataListItem dtlItem in dlCentre_Add.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCentre");
            Label lblCenterCode = (Label)dtlItem.FindControl("lblCenterCode");
            HtmlInputCheckBox chkDL_PrimaryFlag = (HtmlInputCheckBox)dtlItem.FindControl("chkDL_PrimaryFlag");

            if (chkitemck.Checked == true)
            {
                if (chkDL_PrimaryFlag.Checked == true)
                {
                    SelCnt = SelCnt + 1;
                    PrimaryCentreCode = lblCenterCode.Text;
                }
                else
                {
                    CentreCode = CentreCode + lblCenterCode.Text + ",";
                }
            }
        }
        CentreCode = Common.RemoveComma(CentreCode);
        //if (Strings.Right(CentreCode, 1) == ",")
        //    CentreCode = Strings.Left(CentreCode, Strings.Len(CentreCode) - 1);

        if (SelCnt == 0)
        {
            Show_Error_Success_Box("E", "0050");
            dlCentre_Add.Focus();
            return;
        }

        if (SelCnt > 1)
        {
            Show_Error_Success_Box("E", "0051");
            dlCentre_Add.Focus();
            return;
        }

        //'Validate if classroom Name is duplicate or not


        //Save
        string ResultId = null;

        string CRTypeCode = null;
        CRTypeCode = ddlClassroomType_Add.SelectedValue;

        float CRLength = 0;
        CRLength = float.Parse(txtLength_Add.Text);

        float CRWidth = 0;
        CRWidth = float.Parse(txtWidth_Add.Text);

        float CRHeight = 0;
        CRHeight = float.Parse(txtHeight_Add.Text);

        float CRArea = 0;
        CRArea =float.Parse(Math.Round(CRLength * CRWidth, 1).ToString());

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

        string Classroom_Code = null;
        Classroom_Code = lblPKey_ClassroomCode.Text;

        string Premises_Code = null;
        Premises_Code = lblPKey_PremisesCode.Text;

        ResultId = ProductController.Update_Classroom(Classroom_Code, Premises_Code, txtClassroomName_Add.Text, txtClassroom_ShortName_Add.Text, CRLength, CRWidth, CRHeight, CRArea, CRTypeCode, ActiveFlag,
        CreatedBy);

        //Close the Add Panel and go to Search Grid
        if (ResultId != "-1")
        {
            //Save Centre
            int Result1 = 0;
            Result1 = ProductController.Insert_ClassroomCentre(Classroom_Code, PrimaryCentreCode, CentreCode, CreatedBy);

            //Save Capacity

            string ActivityCode = "";

            int Classroom_Capacity = 0;
            foreach (DataListItem dtlItem in dlCapacity_Add.Items)
            {
                Label lblDL_Activity = (Label)dtlItem.FindControl("lblDL_Activity_Id");
                TextBox txtDL_Capacity = (TextBox)dtlItem.FindControl("txtDL_Capacity");
                DropDownList ddlDLUOM = (DropDownList)dtlItem.FindControl("ddlDLUOM");
                Classroom_Capacity = Convert.ToInt32(txtDL_Capacity.Text);

                Result1 = ProductController.Insert_ClassroomCapacity(Classroom_Code, lblDL_Activity.Text, Classroom_Capacity, ddlDLUOM.SelectedValue);
            }

            ControlVisibility("Add");
            FillPremisesMasterDetails(lblPKey_PremisesCode.Text, "Edit");
            Show_Error_Success_Box("S", "0000");
            Clear_AddPanel_Room();
        }
        else if (ResultId == "-1")
        {
            Show_Error_Success_Box("E", "0049");
            txtClassroomName_Add.Focus();
            return;
        }
    }
    public Master_Classroom()
    {
        Load += Page_Load;
    }
}
