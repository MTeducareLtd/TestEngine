using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using ShoppingCart.BL;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Mail;
using System.Net;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic;
using System.Web.UI;
using System.Web;

public partial class Rpt_Facultywise_TestPerformance_Summary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL_Division();
            FillDDL_AcadYear();
            FillDDL_TestCategories();
            FillDDL_TestTypes();

        }
    }

    public void FillDDL_Division()
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

        BindDDL(ddlDivision, dsDivision, "Division_Name", "Division_Code");
        ddlDivision.Items.Insert(0, "Select");
        ddlDivision.SelectedIndex = 0;
    }

    private void FillDDL_AcadYear()
    {
        DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();
        BindDDL(ddlAcadYear, dsAcadYear, "Description", "Id");
        ddlAcadYear.Items.Insert(0, "Select");
        ddlAcadYear.SelectedIndex = 0;


    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
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

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }



    private void FillDDL_Search_Centre()
    {
        Label lblHeader_Company_Code = default(Label);
        lblHeader_Company_Code = (Label)Master.FindControl("lblHeader_Company_Code");

        Label lblHeader_User_Code = default(Label);
        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

        Label lblHeader_DBName = default(Label);
        lblHeader_DBName = (Label)Master.FindControl("lblHeader_DBName");

        if (string.IsNullOrEmpty(lblHeader_User_Code.Text))
            Response.Redirect("Default.aspx");
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        DataSet dsCentre = ProductController.GetAllActiveUser_Company_Division_Zone_Center(lblHeader_User_Code.Text, lblHeader_Company_Code.Text, Div_Code, "", "5", lblHeader_DBName.Text);

        BindListBox(ddlCentre, dsCentre, "Center_Name", "Center_Code");

    }


    private void FillDDL_Standard()
    {
        ddlStandard.Items.Clear();
        ddlBatch.Items.Clear();


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
        string CentreCode = "";
        int CentreCnt = 0;
        int CentreSelCnt = 0;
        for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
        {
            if (ddlCentre.Items[CentreCnt].Selected == true)
            {
                CentreSelCnt = CentreSelCnt + 1;
            }
        }

        if (CentreSelCnt == 0)
        {
            //When all is selected
            for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
            {
                CentreCode = CentreCode + ddlCentre.Items[CentreCnt].Value + ",";
            }
            //if (Strings.Right(CentreCode, 1) == ",")
            //    CentreCode = Strings.Left(CentreCode, Strings.Len(CentreCode) - 1);
        }
        else
        {
            for (CentreCnt = 0; CentreCnt <= ddlCentre.Items.Count - 1; CentreCnt++)
            {
                if (ddlCentre.Items[CentreCnt].Selected == true)
                {
                    CentreCode = CentreCode + ddlCentre.Items[CentreCnt].Value + ",";
                }
            }
            //if (Strings.Right(CentreCode, 1) == ",")
            //    CentreCode = Strings.Left(CentreCode, Strings.Len(CentreCode) - 1);
        }

        ddlBatch.Items.Clear();

        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        DataSet dsBatch = ProductController.GetAllActive_Batch_ForStandard(Div_Code, YearName, StandardCode, CentreCode);
        BindListBox(ddlBatch, dsBatch, "Batch_Name", "Batch_Code");


    }

    private void FillDDL_TestName()
    {
        ddlTestName.Items.Clear();

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

        for (int CenterCnt = 0; CenterCnt <= ddlCentre.Items.Count - 1; CenterCnt++)
        {
            //CenterCode = CenterCode + ddlCentre.Items[CenterCnt].Value + ",";
            if (ddlCentre.Items[CenterCnt].Selected == true)
            {
                CenterCode = CenterCode + ddlCentre.Items[CenterCnt].Value + ",";
            }
        }

        //if (CenterCode == "")
        //{
        //    return;
        //}
        //If ddlCentre.SelectedIndex = 0 Then
        //    'Show_Error_Success_Box("E", "0006")
        //    'ddlCentre.Focus()
        //    Exit Sub
        //End If

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
            //    TestType_ID = Strings.Left(TestType_ID, Strings.Len(TestType_ID) - 1);//
        }

        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        string StandardCode = null;
        StandardCode = ddlStandard.SelectedValue;

        string TestName = null;
        TestName = "%";

        string DateRange = null;
        DateRange = id_date_range_picker_1.Value;

        string FromDate = null;
        string ToDate = null;
        if (DateRange != "")
        {
            FromDate = DateRange.Substring(0, 10);//Strings.Left(DateRange, 10);
        }
        if (string.IsNullOrEmpty(FromDate))
            FromDate = System.DateTime.Now.ToString("dd MMM yyyy");
        if (DateRange != "")
        {
            ToDate = DateRange.Substring(0, 10);//Strings.Right(DateRange, 10);
        }
        if (string.IsNullOrEmpty(ToDate))
            ToDate = System.DateTime.Now.ToString("dd MMM yyyy");



        DataSet dsTestName = ProductController.GetTestScheduleBy_Division_Year_Standard(DivisionCode, YearName, StandardCode, BatchCode, "01", ddlTestCategory.SelectedValue, TestType_ID, TestName, FromDate, ToDate,
        0, 0, 2, CenterCode);

        BindListBox(ddlTestName, dsTestName, "Test_Name", "PKey");

    }


    //private void Clear_Error_Success_Box()
    //{
    //    Panel Msg_Error = this.Parent.FindControl("Msg_Error") as Panel;
    //    Panel Msg_Success = this.Parent.FindControl("Msg_Success") as Panel;
    //    Label lblSuccess = this.Parent.FindControl("lblSuccess") as Label;
    //    Label lblerror = this.Parent.FindControl("lblerror") as Label;
    //    UpdatePanel UpdatePanelMsgBox = this.Parent.FindControl("UpdatePanelMsgBox") as UpdatePanel;
    //    Msg_Error.Visible = false;
    //    Msg_Success.Visible = false;
    //    lblSuccess.Text = "";
    //    lblerror.Text = "";
    //    UpdatePanelMsgBox.Update();
    //}




    protected void BtnClearSearch_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {

    }
    protected void btnEmail_Click(object sender, EventArgs e)
    {

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {

    }
    protected void ddlAcadYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Standard();
    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_TestTypes();
    }
    protected void ddlCentre_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillDDL_Batch();
        FillDDL_TestName();
        //Clear_Error_Success_Box();
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Search_Centre();
        FillDDL_Standard();
    }
    protected void ddlStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_TestName();
        FillDDL_Batch();
    }
    protected void ddlTestCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_TestName();
    }
    protected void ddlTestType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_TestName();

    }
    protected void Clear()
    {
        ddlDivision.SelectedIndex = 0;
        ddlAcadYear.SelectedIndex = 0;
        ddlCentre.Items.Clear();
        ddlBatch.Items.Clear();
        ddlStandard.Items.Clear();
        id_date_range_picker_1.Value = "";
        ddlTestName.Items.Clear();
        ddlTestType.Items.Clear();
        ddlTestCategory.SelectedIndex = 0;
    }
}