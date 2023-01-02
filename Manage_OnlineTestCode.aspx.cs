using ShoppingCart.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manage_OnlineTestCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");


            FillDDL_TestCategories();
            FillDDL_TestTypes();
            FillDDL_Division();
            FillDDL_AcadYear();
        }
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

    }

    private void FillDDL_AcadYear()
    {
        DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcadyear, dsAcadYear, "Description", "Id");
        ddlAcadyear.Items.Insert(0, "Select");
        ddlAcadyear.SelectedIndex = 0;

    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
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
    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        ddlDivision.SelectedIndex = 0;
        ddlAcadyear.SelectedIndex = 0;
        ddlTestCategory.SelectedIndex = 0;
        ddlStandard.Items.Clear();
        ddlTestType.Items.Clear();
        Msg_Error.Visible = false;
        lblerror.Text = "";
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
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
        //Validate if all information is entered correctly
        if (ddlDivision.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0001");
            ddlDivision.Focus();

            return;
        }

        if (ddlAcadyear.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0002");
            ddlAcadyear.Focus();
            return;
        }

        //If ddlTestMode.SelectedIndex = 0 Then
        //    Show_Error_Success_Box("E", "0011")
        //    ddlTestMode.Focus()
        //    Exit Sub
        //End If

        if (ddlTestCategory.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "0012");
            ddlTestCategory.Focus();
            return;
        }

        string StandardCode = "";
        int StdCnt = 0;
        int StdSelCnt = 0;
        for (StdCnt = 0; StdCnt <= ddlStandard.Items.Count - 1; StdCnt++)
        {
            if (ddlStandard.Items[StdCnt].Selected == true)
            {
                StdSelCnt = StdSelCnt + 1;
            }
        }

        if (StdSelCnt == 0)
        {
            //When all is selected
            for (StdCnt = 0; StdCnt <= ddlStandard.Items.Count - 1; StdCnt++)
            {
                StandardCode = StandardCode + ddlStandard.Items[StdCnt].Value + ",";
            }
            StandardCode = Common.RemoveComma(StandardCode);
            //if (Strings.Right(StandardCode, 1) == ",")
            //    StandardCode = Strings.Left(StandardCode, Strings.Len(StandardCode) - 1);
        }
        else
        {
            for (StdCnt = 0; StdCnt <= ddlStandard.Items.Count - 1; StdCnt++)
            {
                if (ddlStandard.Items[StdCnt].Selected == true)
                {
                    StandardCode = StandardCode + ddlStandard.Items[StdCnt].Value + ",";
                }
            }
            StandardCode = Common.RemoveComma(StandardCode);
            //if (Strings.Right(StandardCode, 1) == ",")
            //    StandardCode = Strings.Left(StandardCode, Strings.Len(StandardCode) - 1);
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
            TestType_ID = Common.RemoveComma(TestType_ID);
            //if (Strings.Right(TestType_ID, 1) == ",")
            //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);
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
            TestType_ID = Common.RemoveComma(TestType_ID);
            //if (Strings.Right(TestType_ID, 1) == ",")
            //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);
        }



        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadyear.SelectedItem.ToString();

        string TestName = null;
        if (string.IsNullOrEmpty(txtTestName.Text.Trim()))
        {
            TestName = "%";
        }
        else
        {
            TestName = "%" + txtTestName.Text.Trim();
        }



        DataSet dsGrid = ProductController.GetTestDetailsForAssigningTestCode(DivisionCode, YearName, StandardCode, "01", ddlTestCategory.SelectedValue, TestType_ID, TestName, 1);

        if (dsGrid.Tables[0].Rows.Count > 0)
        {
            ControlVisibility("Result");
            dlgridonlinetestcode.DataSource = dsGrid;
            dlgridonlinetestcode.DataBind();
            dlexport.DataSource = dsGrid;
            dlexport.DataBind();
        }

        else
        {

            Show_Error_Success_Box("E", "No Records Found");
        }



        //dlGridExport.DataSource = dsGrid;
        //dlGridExport.DataBind();

        lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Standard();
    }

    private void FillDDL_Standard()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadyear.SelectedItem.ToString();

        DataSet dsStandard = ProductController.GetAllActive_Standard_ForYear(Div_Code, YearName);
        BindListBox(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");
    }
    protected void ddlAcadYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Standard();
    }

    protected void chksingle(object sender, EventArgs e)
    {

        foreach (DataListItem dtlItem in dlgridonlinetestcode.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCenter");
            Label lblassesmenttestcode = (Label)dtlItem.FindControl("lblassesmenttestcode");

            //chkitemck.Checked = s.Checked;
            TextBox txtassesmentcode = (TextBox)dtlItem.FindControl("txtassesmentcode");

            if (chkitemck.Visible == true)
            {

                if (chkitemck.Checked == true)
                {
                    lblassesmenttestcode.Visible = false;
                    txtassesmentcode.Visible = true;
                }
                else
                {
                    lblassesmenttestcode.Visible = true;
                    txtassesmentcode.Visible = false;

                }
            }
        }

    }

    protected void chkAttendanceAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox s = sender as CheckBox;

        //Set checked status of hidden check box to items in grid
        foreach (DataListItem dtlItem in dlgridonlinetestcode.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCenter");
            Label lblassesmenttestcode = (Label)dtlItem.FindControl("lblassesmenttestcode");

            chkitemck.Checked = s.Checked;
            TextBox txtassesmentcode = (TextBox)dtlItem.FindControl("txtassesmentcode");

            if (chkitemck.Visible == true)
            {

                if (chkitemck.Checked == true)
                {
                    lblassesmenttestcode.Visible = false;
                    txtassesmentcode.Visible = true;
                }
                else
                {
                    lblassesmenttestcode.Visible = true;
                    txtassesmentcode.Visible = false;

                }
            }

        }


    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }
    protected void BtnClose_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }
    protected void BtnAuthorization_Click(object sender, EventArgs e)
    {
        bool flag = false;
        foreach (DataListItem dtlItem1 in dlgridonlinetestcode.Items)
        {
            CheckBox chkitemck = (CheckBox)dtlItem1.FindControl("chkCenter");
            if (chkitemck != null && chkitemck.Checked == true)
            {
                flag = true;
                break;
            }
        }
        if (flag == false)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Please select atleast one record";
            UpdatePanelMsgBox.Update();
            return;
        }
        else
        {

            foreach (DataListItem dtlItem in dlgridonlinetestcode.Items)
            {
                CheckBox chkitemck = (CheckBox)dtlItem.FindControl("chkCenter");




                //Label lblassesmenttestcode = (Label)dtlItem.FindControl("lblassesmenttestcode");
                Label lbldivcode = (Label)dtlItem.FindControl("lbldivcode");
                Label lblacadyear = (Label)dtlItem.FindControl("lblacadyear");
                Label lblcoursecode = (Label)dtlItem.FindControl("lblcoursecode");
                Label lbltestid = (Label)dtlItem.FindControl("lbltestid");
                Label lblqpsetno = (Label)dtlItem.FindControl("lblqpsetno");
                Label lblResult = (Label)dtlItem.FindControl("lblResult");


                //chkitemck.Checked = s.Checked;
                TextBox txtassesmentcode = (TextBox)dtlItem.FindControl("txtassesmentcode");



                if (chkitemck.Visible == true)
                {

                    if (chkitemck.Checked == true)
                    {
                        lblResult.Text = "";
                        if (txtassesmentcode.Text.Trim() == "")
                        {
                            lblResult.Text = "Enter Assesmnet Test Code";
                            lblResult.ForeColor = System.Drawing.Color.Red;

                        }

                        if (lblResult.Text == "")
                        {
                            string PKey = null;
                            PKey = lbldivcode.Text + '%' + lblacadyear.Text + '%' + lblcoursecode.Text + '%' + lbltestid.Text;

                            int ResultId = 0;

                            Label lblHeader_User_Code = default(Label);
                            lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                            string CreatedBy = null;
                            CreatedBy = lblHeader_User_Code.Text;



                            //int SetNumber = 0,
                            //SetNumber = Convert.ToInt32(lblqpsetno.Text);

                            ResultId = ProductController.Insert_Test_Set(PKey, lblqpsetno.Text, 0, "", "", "", "", 0, 0, "", CreatedBy, 1, txtassesmentcode.Text.Trim(), "", "", "", "", "", "");        //Save Header

                            if (ResultId == 1)
                            {
                                lblResult.Text = "Record Saved Sucessfully";
                                lblResult.ForeColor = System.Drawing.Color.Green;
                            }

                            else if (ResultId == -1)
                            {
                                lblResult.Text = "Assesment Test Code already exists";
                                lblResult.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }

            }
        }
    }
    protected void HLExport_Click(object sender, EventArgs e)
    {
        dlexport.Visible = true;
        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=OnlineTestCodeDetails.xls");


        Response.Charset = "";


        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

        dlexport.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();
        dlexport.Visible = false;
    }
    protected void btnuploadviexcel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Master_QPSet_Upload.aspx");
    }
}