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
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;

using System.Web.UI;


public partial class Master_Uploadsaspx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            QuestionGrid.Visible = false;

            FillDDL_TestCategories();
            FillDDL_TestTypes();
            FillDDL_Division();
            FillDDL_AcadYear();
        }
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

    private void BindListBox(ListBox ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
    }

    private void FillDDL_TestTypes()
    {
        DataSet dsTestType = ProductController.GetAllActiveTestType();
        BindListBox(ddlTestType, dsTestType, "TestType_Name", "TestType_Id");

    }

    private void FillDDL_TestCategories()
    {
        DataSet dsTestCategory = ProductController.GetAllActiveTestCategory();
        BindDDL(ddlTestCategory, dsTestCategory, "TestCategory_Name", "TestCategory_Id");
        ddlTestCategory.Items.Insert(0, "Select");
        ddlTestCategory.SelectedIndex = 0;

    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            DivAddPanel.Visible = false;
            //BtnAdd.Visible = True
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = false;
            //BtnAdd.Visible = True
        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            //BtnAdd.Visible = False
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

    protected void HLExport_Click(object sender, EventArgs e)
    {
        dlGridExport.Visible = true;
        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=Test_QPSet.xls");


        Response.Charset = "";


        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

        dlGridExport.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();
        dlGridExport.Visible = false;
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

        ControlVisibility("Result");

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



        DataSet dsGrid = ProductController.GetTestMasterBy_Division_Year_Standard(DivisionCode, YearName, StandardCode, "01", ddlTestCategory.SelectedValue, TestType_ID, TestName, 2);
        dlGridDisplay.DataSource = dsGrid;
        dlGridDisplay.DataBind();

        dlGridExport.DataSource = dsGrid;
        dlGridExport.DataBind();

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

    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        if (e.CommandName == "Upload")
        {
            Clear_AddPanel();
            ControlVisibility("Add");
            lblPKey_Edit.Text = e.CommandArgument.ToString();
            FillTestMasterDetails(lblPKey_Edit.Text, e.CommandName);
        }
    }

    private void FillTestMasterDetails(string PKey, string CommandName)
    {
        DataSet dsTest = ProductController.GetTestMasterBY_PKey(PKey, 1);

        if (dsTest.Tables[0].Rows.Count > 0)
        {
            lblDivision_Add.Text = ddlDivision.SelectedItem.ToString();
            lblAcadYear_Add.Text = ddlAcadyear.SelectedItem.ToString();
            lblStandard_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Standard_Name"]);
            lblTestCategory_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["TestCategory_Name"]);
            lblTestType_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["TestType_Name"]);
            lblTestName_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Test_Name"]);
            lblDivCode.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["division_code"]);
            lblStandardCode.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["standard_code"]);

            lblSubject_Add.Text = Convert.ToString(dsTest.Tables[0].Rows[0]["Subjects"]);
            //FillQPSetNo(Convert.ToInt32(dsTest.Tables[0].Rows[0]["QPSetCnt"]));

            DataSet dsqssetno = ProductController.GetTestMasterBY_PKey(PKey, 5);
            BindDDL(ddlQPSetNo, dsqssetno, "Set_Number", "Set_Number");
            ddlQPSetNo.Items.Insert(0, "Select");
            ddlQPSetNo.SelectedIndex = 0;

            //ddlSubject_Hidden.DataSource = dsTest.Tables[1];
            //ddlSubject_Hidden.DataTextField = "Subject_Name";
            //ddlSubject_Hidden.DataValueField = "Subject_Code";
            //ddlSubject_Hidden.DataBind();

            if (lblTestCategory_Add.Text.Trim() == "Objective")
            {
                //Label2.Visible = true;
                //FFLExcel.Visible = true;
                //BtnUploadExcel.Visible = true;

                //SMSHelpFlag.Visible = true;
            }
            else if (lblTestCategory_Add.Text.Trim() == "Subjective")
            {
                //Label2.Visible = false;
                //FFLExcel.Visible = false;
                //BtnUploadExcel.Visible = false;

               // SMSHelpFlag.Visible = false;
            }
        }
    }
    // private void FillQPSetNo(int QPSetCnt)
    //{
    //    ddlQPSetNo.Items.Clear();
    //    for (int cnt = 1; cnt <= QPSetCnt; cnt++)
    //    {

    //        ddlQPSetNo.Items.Add(cnt.ToString());
    //    }
    //    if (QPSetCnt > 0)
    //    {
    //        ddlQPSetNo.SelectedIndex = 0;
    //        ddlQPSetNo.Enabled = true;
    //    }
    //    else
    //    {
    //        ddlQPSetNo.Enabled = false;

    //    }

    //}

    private void Clear_AddPanel()
    {
        lblerroranswerpaper.Visible = false;
        lblerroranswerkey.Visible = false;
        lblerrorquestionpaper.Visible = false;
        lblfilepathqusetionpaper.Text = "";
        lblfilepathanswerpaper.Text = "";
        lblfilepathanswerkey.Text = "";
        ddlQPSetNo.Items.Clear();
        lblpreviouanswerkeypath.Text = "";
        lblpreviousanswerpaperpath.Text = "";
        lblpreviousqesutionpaperpath.Text = "";
        lnkbtnquestionpaperexitspath.Visible = false;
        lnkbtnanswerpaperexitspath.Visible = false;
        lnkbtnanswerkeypaperexitspath.Visible = false;
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        lblerroranswerpaper.Visible = false;
        lblerroranswerkey.Visible = false;
        lblerrorquestionpaper.Visible = false;
        lblfilepathqusetionpaper.Text = "";
        lblfilepathanswerpaper.Text = "";
        lblfilepathanswerkey.Text = "";
        lblpreviouanswerkeypath.Text = "";
        lblpreviousanswerpaperpath.Text = "";
        lblpreviousqesutionpaperpath.Text = "";
        lnkbtnanswerkeypaperexitspath.Visible = false;
        lnkbtnanswerpaperexitspath.Visible = false;
        lnkbtnquestionpaperexitspath.Visible = false;

        if (ddlQPSetNo.SelectedItem.Text == "Select")
        {

            Show_Error_Success_Box("E", "Select QP Set No.");
            ddlQPSetNo.Focus();
            return;
        }

        if (fileuploadquestionpaper.HasFile)
        {

            checkquestionpaper();
        }
        else
        {

        }

        if (fileuploadanswerpaper.HasFile)
        {

            checkanswerpaper();
        }
        else
        {

        }



        if (fileuploadanswerkey.HasFile)
        {

            checkanswerkey();
        }
        else
        {

        }

        
    }

    protected void checkquestionpaper()
    {
        //lbluploadfileName.Text = Path.GetFileName(uploadfile.FileName);
        string FullName = Server.MapPath("~/Question_Uploads") + "\\" + Path.GetFileName(fileuploadquestionpaper.FileName);
        lblfilepathqusetionpaper.Text = FullName;
        lblfilepathqusetionpaper.Text = Path.GetFileName(fileuploadquestionpaper.FileName);
        string strFileType = Path.GetExtension(fileuploadquestionpaper.FileName).ToLower();
        
        if (strFileType != ".pdf")
        {
            lblerrorquestionpaper.Visible = true;
            lblerrorquestionpaper.ForeColor = System.Drawing.Color.Red;
            lblerrorquestionpaper.Text = "Kindly Select PDF File";
            return;
        }

        else
        {

            try
            {
                if (File.Exists(FullName))
                {
                    lblerrorquestionpaper.Visible = true;
                    lblerrorquestionpaper.ForeColor = System.Drawing.Color.Red;
                    lblerrorquestionpaper.Text = "File Name Already Exists";
                    return;

                }
                else
                {
                    string DBPath = "Question_Uploads/" + fileuploadquestionpaper.FileName ;
                    DataSet dsupdate = ProductController.UpdateQPSetPath(lblPKey_Edit.Text + '%' + ddlQPSetNo.SelectedValue, 1, DBPath);
                    if (dsupdate.Tables[0].Rows[0]["Status"].ToString() == "1")
                    {
                        lblerrorquestionpaper.Visible = true;
                        lblerrorquestionpaper.ForeColor = System.Drawing.Color.Green;
                        lblerrorquestionpaper.Text = "File Saved Sucessfuly";
                        fileuploadquestionpaper.SaveAs(FullName);
                    }
                }
            }
            catch (Exception e)
            {
                Show_Error_Success_Box("E", e.ToString());
            }
        }


    }

    protected void checkanswerpaper()
    {
        //lbluploadfileName.Text = Path.GetFileName(uploadfile.FileName);
        string FullName = Server.MapPath("~/Answer_Upload") + "\\" + Path.GetFileName(fileuploadanswerpaper.FileName);
        lblfilepathanswerpaper.Text = FullName;
        lblfilepathanswerpaper.Text = Path.GetFileName(fileuploadanswerpaper.FileName);
        string strFileType = Path.GetExtension(fileuploadanswerpaper.FileName).ToLower();
        if (strFileType != ".pdf")
        {
            lblerroranswerpaper.Visible = true;
            lblerroranswerpaper.ForeColor = System.Drawing.Color.Red;
            lblerroranswerpaper.Text = "Kindly Select PDF File";
            return;
        }

        else
        {
            lblerroranswerpaper.Visible = false;
            try
            {
                if (File.Exists(FullName))
                {
                    lblerroranswerpaper.Visible = true;
                    lblerroranswerpaper.ForeColor = System.Drawing.Color.Red;
                    lblerroranswerpaper.Text = "File Name Already exists";
                    return;

                }
                else
                {
                    string DBPath = "Answer_Upload/" + fileuploadanswerpaper.FileName ;
                    DataSet dsupdate = ProductController.UpdateQPSetPath(lblPKey_Edit.Text + '%' + ddlQPSetNo.SelectedValue, 2, DBPath);
                    if (dsupdate.Tables[0].Rows[0]["Status"].ToString() == "1")
                    {
                        lblerroranswerpaper.Visible = true;
                        lblerroranswerpaper.ForeColor = System.Drawing.Color.Green;
                        lblerroranswerpaper.Text = "File Saved Sucessfuly";
                        fileuploadanswerpaper.SaveAs(FullName);
                    }
                }
            }
            catch (Exception e)
            {
                Show_Error_Success_Box("E", e.ToString());
            }
        }


    }


    protected void checkanswerkey()
    {
        //lbluploadfileName.Text = Path.GetFileName(uploadfile.FileName);
        string FullName = Server.MapPath("~/AnswerKey_Upload") + "\\" + Path.GetFileName(fileuploadanswerkey.FileName);
        lblfilepathanswerkey.Text = FullName;
        lblfilepathanswerkey.Text = Path.GetFileName(fileuploadanswerkey.FileName);
        string strFileType = Path.GetExtension(fileuploadanswerkey.FileName).ToLower();
        if (strFileType != ".pdf")
        {
            lblerroranswerkey.Visible = true;
            lblerroranswerkey.ForeColor = System.Drawing.Color.Red;
            lblerroranswerkey.Text = "Kindly Select PDF File";
            return;
        }

        else
        {
            lblerroranswerkey.Visible = false;
            try
            {
                if (File.Exists(FullName))
                {
                    lblerroranswerkey.Visible = true;
                    lblerroranswerkey.ForeColor = System.Drawing.Color.Red;
                    lblerroranswerkey.Text = "File Name Already Exists";
                    return;

                }
                else
                {
                    string DBPath = "AnswerKey_Upload/" + fileuploadanswerkey.FileName ;
                    DataSet dsupdate = ProductController.UpdateQPSetPath(lblPKey_Edit.Text + '%' + ddlQPSetNo.SelectedValue, 3, DBPath);
                    if (dsupdate.Tables[0].Rows[0]["Status"].ToString() == "1")
                    {
                        lblerroranswerkey.Visible = true;
                        lblerroranswerkey.ForeColor = System.Drawing.Color.Green;
                        lblerroranswerkey.Text = "File Saved Sucessfuly";
                        fileuploadanswerkey.SaveAs(FullName);
                    }
                }
            }
            catch (Exception e)
            {
                Show_Error_Success_Box("E", e.ToString());
            }
        }


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
    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {
        Clear_AddPanel();
        ControlVisibility("Result");
    }
    protected void ddlQPSetNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblerrorquestionpaper.Text = "";
        lblerroranswerpaper.Text = "";
        lblerroranswerkey.Text = "";

        lnkbtnanswerkeypaperexitspath.Visible = false;
        lnkbtnanswerpaperexitspath.Visible = false;
        lnkbtnquestionpaperexitspath.Visible = false;

        lblpreviouanswerkeypath.Text = "";
        lblpreviousanswerpaperpath.Text = "";
        lblpreviousqesutionpaperpath.Text = "";
        DataSet dsgetpath = ProductController.GetQPSetPath(lblPKey_Edit.Text + '%' + ddlQPSetNo.SelectedValue, 1);

        if (dsgetpath.Tables[0].Rows.Count > 0)
        {
            if (dsgetpath.Tables[0].Rows[0]["Question_Paper_Path"].ToString() != "")
            {
                lnkbtnquestionpaperexitspath.Visible = true;
                lnkbtnquestionpaperexitspath.Text = "View Previous File";
                lblpreviousqesutionpaperpath.Text = dsgetpath.Tables[0].Rows[0]["Question_Paper_Path"].ToString();
            }

            if (dsgetpath.Tables[0].Rows[0]["Answer_Paper_Path"].ToString() != "")
            {
                lnkbtnanswerpaperexitspath.Visible = true;
                lnkbtnanswerpaperexitspath.Text = "View Previous File";
                lblpreviousanswerpaperpath.Text = dsgetpath.Tables[0].Rows[0]["Answer_Paper_Path"].ToString();
            }

            if (dsgetpath.Tables[0].Rows[0]["Answer_Key_Path"].ToString() != "")
            {
                lnkbtnanswerkeypaperexitspath.Visible = true;
                lnkbtnanswerkeypaperexitspath.Text = "View Previous File";
                lblpreviouanswerkeypath.Text =dsgetpath.Tables[0].Rows[0]["Answer_Key_Path"].ToString();
            }
        }
    }
    protected void lnkbtnquestionpaperexitspath_Click(object sender, EventArgs e)
    {
        //string Path_File = Server.MapPath("~/Question_Uploads") + "\\" + Path.GetFileName(lblpreviousqesutionpaperpath.Text);
        //Response.ContentType = "Application/pdf";
        //Response.TransmitFile(Server.MapPath("~/Question_Uploads") + "\\" + Path.GetFileName(lblpreviousqesutionpaperpath.Text));
        //Response.Write("<script>");
        //Response.Write("window.open('../Inventory/pages/printableads.pdf', '_newtab');");
        //Response.Write("</script>");

        try
        {
            System.IO.FileStream fs = new System.IO.FileStream(Server.MapPath("~/Question_Uploads") + "\\" + Path.GetFileName(lblpreviousqesutionpaperpath.Text), System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] ar = new byte[(int)fs.Length];
            fs.Read(ar, 0, (int)fs.Length);
            fs.Close();

            Response.AddHeader("content-disposition", "attachment;filename=" + lblpreviousqesutionpaperpath.Text + ".pdf");
            Response.ContentType = "application/octectstream";
            Response.BinaryWrite(ar);
            Response.End();
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", "File Not Found Contact Administrator");
        }
    }
    protected void lnkbtnanswerpaperexitspath_Click(object sender, EventArgs e)
    {
        try
        {
            System.IO.FileStream fs = new System.IO.FileStream(Server.MapPath("~/Answer_Upload") + "\\" + Path.GetFileName(lblpreviousanswerpaperpath.Text), System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] ar = new byte[(int)fs.Length];
            fs.Read(ar, 0, (int)fs.Length);
            fs.Close();

            Response.AddHeader("content-disposition", "attachment;filename=" + lblpreviousanswerpaperpath.Text + ".pdf");
            Response.ContentType = "application/octectstream";
            Response.BinaryWrite(ar);
            Response.End();
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", "File Not Found Contact Administrator");
        }
    }
    protected void lnkbtnanswerkeypaperexitspath_Click(object sender, EventArgs e)
    {
        try
        {
            System.IO.FileStream fs = new System.IO.FileStream(Server.MapPath("~/AnswerKey_Upload") + "\\" + Path.GetFileName(lblpreviouanswerkeypath.Text), System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] ar = new byte[(int)fs.Length];
            fs.Read(ar, 0, (int)fs.Length);
            fs.Close();

            Response.AddHeader("content-disposition", "attachment;filename=" + lblpreviouanswerkeypath.Text + ".pdf");
            Response.ContentType = "application/octectstream";
            Response.BinaryWrite(ar);
            Response.End();
        }
        catch (Exception ex)
        {

            Show_Error_Success_Box("E", "File Not Found Contact Administrator");
        }
    }
}