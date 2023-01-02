using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web;
using System.Web.UI;
public partial class TestSchedule_PaperCorrecter_Assignment : System.Web.UI.Page
{
    int count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();
            FillDDL_TestCategories();
            FillDDL_TestTypes();
        }

    }



    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }


    private void FillDDL_TestCategories()
    {
        DataSet dsTestCategory = ProductController.GetAllActiveTestCategory();
        BindDDL(ddlTestCategory, dsTestCategory, "TestCategory_Name", "TestCategory_Id");
        ddlTestCategory.Items.Insert(0, "Select");
        ddlTestCategory.SelectedIndex = 0;

    }

    private void FillDDL_TestTypes()
    {
        DataSet dsTestType = ProductController.GetAllActiveTestType();
        BindListBox(ddlTestType, dsTestType, "TestType_Name", "TestType_Id");

    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            //DivAddPanel.Visible = false;
            //BtnAdd.Visible = true;
            //DivEditPanel.Visible = false;
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            //DivAddPanel.Visible = false;
            //BtnAdd.Visible = true;
            //DivEditPanel.Visible = false;
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


    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
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

    private void FillDDL_AcadYear()
    {
        DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcadYear, dsAcadYear, "Description", "Id");
        ddlAcadYear.Items.Insert(0, "Select");
        ddlAcadYear.SelectedIndex = 0;


    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivision.Focus();
            return;
        }

        if (ddlAcadYear.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0002");
            ddlAcadYear.Focus();
            return;
        }

        if (ddlCentre.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0006");
            ddlCentre.Focus();
            return;
        }

        if (ddlStandard.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0003");
            ddlStandard.Focus();
            return;
        }

        if (ddlTestCategory.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0012");
            ddlTestCategory.Focus();
            return;
        }

        string BatchCode = "";
        int BatchCnt = 0;
        int BatchSelCnt = 0;
        for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
        {
            if (ddlBatch.Items[BatchCnt].Selected == true)
            {
                BatchSelCnt = BatchSelCnt + 1;
            }
        }

        if (BatchSelCnt == 0)
        {
            //When all is selected
            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
            }
            //if (Strings.Right(BatchCode, 1) == ",")
            //    BatchCode = Strings.Left(BatchCode, Strings.Len(BatchCode) - 1);
            BatchCode = Common.RemoveComma(BatchCode);
        }
        else
        {
            for (BatchCnt = 0; BatchCnt <= ddlBatch.Items.Count - 1; BatchCnt++)
            {
                if (ddlBatch.Items[BatchCnt].Selected == true)
                {
                    BatchCode = BatchCode + ddlBatch.Items[BatchCnt].Value + ",";
                }
            }
            //if (Strings.Right(BatchCode, 1) == ",")
            //    BatchCode = Strings.Left(BatchCode, Strings.Len(BatchCode) - 1);
            BatchCode = Common.RemoveComma(BatchCode);
        }

        string TestType_ID = "";
        int TypeCnt = 0;
        int TypeSelCnt = 0;
        for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
        {
            if (ddlTestType.Items[TypeCnt].Selected == true)
            {
                TypeSelCnt = TypeSelCnt + 1;
            }
        }

        if (TypeSelCnt == 0)
        {
            //When all is selected
            for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
            {
                TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
            }
            //if (Strings.Right(TestType_ID, 1) == ",")
            //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);
            TestType_ID = Common.RemoveComma(TestType_ID);
        }
        else
        {
            for (TypeCnt = 0; TypeCnt <= ddlTestType.Items.Count - 1; TypeCnt++)
            {
                if (ddlTestType.Items[TypeCnt].Selected == true)
                {
                    TestType_ID = TestType_ID + ddlTestType.Items[TypeCnt].Value + ",";
                }
            }
            //if (Strings.Right(TestType_ID, 1) == ",")
            //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);
            TestType_ID = Common.RemoveComma(TestType_ID);
        }

        ControlVisibility("Result");

        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        string TestName = null;
        if (string.IsNullOrEmpty(txtTestName.Text.Trim()))
        {
            TestName = "%";
        }
        else
        {
            TestName = "%" + txtTestName.Text.Trim();
        }

        string DateRange = null;
        DateRange = id_date_range_picker_1.Value;

        string FromDate = null;
        string ToDate = null;
        //FromDate = Strings.Left(DateRange, 10);
        if (DateRange != "")
        {
            FromDate = DateRange.Substring(0, 10);
        }
        if (string.IsNullOrEmpty(FromDate))
            FromDate = System.DateTime.Now.ToString("dd MMM yyyy");
        if (DateRange != "")
        //ToDate = Strings.Right(DateRange, 10);
        {
            ToDate = DateRange.Substring(DateRange.Length - 10);
        }
        if (string.IsNullOrEmpty(ToDate))
            ToDate = System.DateTime.Now.ToString("dd MMM yyyy");

        string Centre_Code = null;
        Centre_Code = ddlCentre.SelectedItem.Value;

        DataSet dsGrid = ProductController.GetTestScheduleBy_Division_Year_Standard_Centre(DivisionCode, YearName, StandardCode, BatchCode, "01,02", ddlTestCategory.SelectedValue, TestType_ID, TestName, FromDate, ToDate,
        0, 0, Centre_Code, 4);
        dlGridDisplay.DataSource = dsGrid.Tables[0];
        dlGridDisplay.DataBind();

        dlgridexport.DataSource = dsGrid.Tables[0];
        dlgridexport.DataBind();

        lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
        lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
        lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
        lblStandard_Result.Text = ddlStandard.SelectedItem.ToString();
        lblTestCategory_Result.Text = ddlTestCategory.SelectedItem.ToString();
        lblCentre_Result.Text = ddlCentre.SelectedItem.ToString();

    }

    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        try
        {
            TextBox txtfromtime = (TextBox)e.Item.FindControl("txtfromtime");
            TextBox txttotime = (TextBox)e.Item.FindControl("txttotime");

            Label lblpartnername = (Label)e.Item.FindControl("lblpartnername");
            Label lblpartnercode = (Label)e.Item.FindControl("lblpartnercode");
            Label lblpcslab = (Label)e.Item.FindControl("lblpcslab");
            Label lblpcslabid = (Label)e.Item.FindControl("lblpcslabid");

            Label lblTestTimeStr = (Label)e.Item.FindControl("lblTestTimeStr");

            Label lblDLFromTime = (Label)e.Item.FindControl("lblDLFromTime");
            Label lblDLToTime = (Label)e.Item.FindControl("lblDLToTime");
            Label lblbagpkey = (Label)e.Item.FindControl("lblbagpkey");


            LinkButton lnkDLEdit = (LinkButton)e.Item.FindControl("lnkDLEdit");
            LinkButton lnkDLSave = (LinkButton)e.Item.FindControl("lnkDLSave");

            DropDownList ddlPCSlab = (DropDownList)e.Item.FindControl("ddlPCSlab");
            DropDownList ddlpartner = (DropDownList)e.Item.FindControl("ddlpartner");
            Panel icon_Error = (Panel)e.Item.FindControl("icon_Error");


            HtmlAnchor lbl_DLError = (HtmlAnchor)e.Item.FindControl("lbl_DLError");
            string Pkey = e.CommandArgument.ToString();

            string[] Pkey_Split = Pkey.Split('%');


            string Div_Code = Pkey_Split[0];
            string AcadYear = Pkey_Split[1];
            string StandardCode = Pkey_Split[2];
            string CenterCode = Pkey_Split[3];
            string BatchCode = Pkey_Split[4];
            string TestId = Pkey_Split[5];
            int ConductNo = Convert.ToInt32(Pkey_Split[6]);

            string TestBagPkey = lblbagpkey.Text;

            if (e.CommandName == "Edit")
            {



                //foreach (var item in Pkey_Split)
                //{
                //    Div_Code = item;
                //}


                lnkDLEdit.Visible = false;
                lnkDLSave.Visible = true;
                lblTestTimeStr.Visible = false;
                txtfromtime.Visible = true;
                txtfromtime.Text = lblDLFromTime.Text;

                txttotime.Visible = true;
                txttotime.Text = lblDLToTime.Text;

                lblpartnername.Visible = false;
                lblpcslab.Visible = false;

                DataSet dsSlab = ProductController.Get_SlabBy_Division(Div_Code);
                BindDDL(ddlPCSlab, dsSlab, "Slab_Name", "Slab_Code");
                ddlPCSlab.Items.Insert(0, "Select");
                ddlPCSlab.SelectedIndex = 0;
                ddlPCSlab.Visible = true;

                if (lblpcslabid.Text.Length > 0)
                {
                    ddlPCSlab.SelectedValue = lblpcslabid.Text;
                }

                DataSet dsPaperChecker = ProductController.GetPartnerMasterBy_Division(Div_Code, "1");
                BindDDL(ddlpartner, dsPaperChecker, "Partner_Name", "Partner_Code");
                ddlpartner.Items.Insert(0, "Select");
                ddlpartner.SelectedIndex = 0;
                ddlpartner.Visible = true;

                if (lblpartnercode.Text.Length > 0)
                {
                    ddlpartner.SelectedValue = lblpartnercode.Text;
                }



                txtfromtime.Focus();
            }

            else if (e.CommandName == "Save")
            {
                    lbl_DLError.Title = "";
                    icon_Error.Visible = false;

                    int TestStartTime = 0;
                    int TestEndTime = 0;

                    TestStartTime = Convert.ToInt32(Convert.ToInt32(txtfromtime.Text.Substring(0, 2)) * 60) + Convert.ToInt32(txtfromtime.Text.Substring(txtfromtime.Text.Length - 2));//Strings.Left(txtfromtime.Text, 2)) * 60 + Conversion.Val(Strings.Right(txtfromtime.Text, 2));
                    TestEndTime = Convert.ToInt32(Convert.ToInt32(txttotime.Text.Substring(0, 2)) * 60) + Convert.ToInt32(txttotime.Text.Substring(txttotime.Text.Length - 2));//Strings.Left(txttotime.Text, 2)) * 60 + Conversion.Val(Strings.Right(txttotime.Text, 2));


                    if (TestStartTime <= 0 | TestStartTime >= 1440)
                    {
                        lbl_DLError.Title = "Invalid Start Time";
                        icon_Error.Visible = true;
                        lbl_DLError.Focus();
                        return;
                    }

                    if (TestEndTime <= 0 | TestEndTime >= 1440)
                    {

                        lbl_DLError.Title = "Invalid End Time";
                        icon_Error.Visible = true;
                        lbl_DLError.Focus();
                        return;
                    }

                    if (TestStartTime >= TestEndTime)
                    {
                        lbl_DLError.Title = "Start Time can't be after End Time";
                        icon_Error.Visible = true;
                        lbl_DLError.Focus();
                        return;
                    }



                    if (ddlPCSlab.SelectedValue != "Select" || ddlpartner.SelectedValue != "Select")
                    {
                        if (ddlPCSlab.SelectedValue == "Select")
                        {
                            lbl_DLError.Title = "Select Slab";
                            icon_Error.Visible = true;
                            lbl_DLError.Focus();
                            return;
                        }

                        if (ddlpartner.SelectedValue == "Select")
                        {
                            lbl_DLError.Title = "Select Paper Checker";
                            icon_Error.Visible = true;
                            lbl_DLError.Focus();
                            return;
                        }
                    }


                    Label lblHeader_User_Code = default(Label);
                    lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                    string CreatedBy = null;
                    CreatedBy = lblHeader_User_Code.Text;

                    DataSet ds = ProductController.UpdateTest_Schedule_Paper_Corrector_Details("1", Div_Code, AcadYear, StandardCode, CenterCode, BatchCode,
                    TestId, ConductNo, TestBagPkey, TestStartTime, TestEndTime, txtfromtime.Text, txttotime.Text, ddlpartner.SelectedValue, ddlPCSlab.SelectedValue, CreatedBy);

                    if (ds.Tables[0].Rows[0]["Status"].ToString() == "1")
                    {
                        BtnSearch_Click("", e);
                    }
                    lnkDLEdit.Focus();
                    //up.Update();
              
            }
        }

        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }
    }

    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlAcadYear.SelectedIndex = 0;
        ddlTestCategory.SelectedIndex = 0;
        ddlStandard.Items.Clear();
        ddlTestType.Items.Clear();
        Msg_Error.Visible = false;
        lblerror.Text = "";
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        ddlBatch.Items.Clear();
        txtTestName.Text = "";
        id_date_range_picker_1.Value = "";
    }

    protected void ddlAcadYear_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
        Clear_Error_Success_Box();
    }

    protected void ddlStandard_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Batch();
        Clear_Error_Success_Box();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
        FillDDL_Search_Centre();
        Clear_Error_Success_Box();
    }


    private void FillDDL_Search_Centre()
    {
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);

        BindDDL(ddlCentre, dsCentre, "Center_Name", "Center_Code");
        ddlCentre.Items.Insert(0, "Select");
        ddlCentre.SelectedIndex = 0;
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


    private void FillDDL_Batch()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        string CentreCode = null;
        CentreCode = ddlCentre.SelectedValue;

        DataSet dsBatch = ProductController.GetAllActive_Batch_ForStandard(Div_Code, YearName, StandardCode, CentreCode);
        BindListBox(ddlBatch, dsBatch, "Batch_Name", "Batch_Code");


    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        dlgridexport.Visible = true;
        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=Test_Schedule.xls");

        Response.Charset = "";


        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        dlgridexport.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();
        dlgridexport.Visible = false;
    }
}