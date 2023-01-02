using ShoppingCart.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tran_Test_Schedule_Upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Add");
            FillDDL_Division();
            FillDDL_AcadYear();
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


    protected void Checkexcel()
    {

        if (!string.IsNullOrEmpty(uploadfile.FileName))
        {
            //lbluploadfileName.Text = Path.GetFileName(uploadfile.FileName);
            string FullName = Server.MapPath("~/Test_Schedule_Uploads") + "\\" + Path.GetFileName(uploadfile.FileName);
            lblfilepath.Text = FullName;
            lblfilename.Text = Path.GetFileName(uploadfile.FileName);
            string strFileType = Path.GetExtension(uploadfile.FileName).ToLower();
            if (strFileType != ".csv")
            {
                Show_Error_Success_Box("E", "Kindly Select Excel File With .CSV Extension");
                return;
            }

            else
            {
                try
                {

                    if (File.Exists(lblfilepath.Text))
                    {
                        Show_Error_Success_Box("E", "File Name Already Exists");
                        return;

                    }
                    uploadfile.SaveAs(FullName);

                    DataTable dtRaw = new DataTable();



                    //create object for CSVReader and pass the stream
                    ////CSVReader reader = new CSVReader(FFLExcel.PostedFile.InputStream);
                    FileStream fileStream = new FileStream(FullName, FileMode.Open);
                    CSVReader reader = new CSVReader(fileStream);
                    //get the header
                    string[] headers = reader.GetCSVLine();

                    //add headers
                    foreach (string strHeader in headers)
                    {
                        dtRaw.Columns.Add(strHeader);

                    }
                    DataRow NewRow = null;
                    int CurRowNo = 0;




                    string[] data = null;


                    data = reader.GetCSVLine();
                    //Read first line
                    CurRowNo = 1;
                    while (data != null)
                    {
                        dtRaw.Rows.Add(data);

                    NextCSVLine:


                        data = reader.GetCSVLine();
                        //Read next line
                        CurRowNo = CurRowNo + 1;
                    }
                    datalist_NewUploads1.DataSource = dtRaw;
                    datalist_NewUploads1.DataBind();
                    New_UploadGrid.Visible = true;
                    datalist_NewUploads1.Visible = true;
                    Divbtnimport.Visible = true;
                    Msg_Error.Visible = false;
                    DivNew_Upload.Visible = false;
                }
                catch (Exception e)
                {
                    Show_Error_Success_Box("E", "Excel File Not Matching With The Template, Kindly Click On Download Template Button And Use That Template");
                }

            }

        }
        else
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = "Please Select File...!";
            Divbtnimport.Visible = false;
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

    protected void ddlDivision_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard_Add();
        // FillDDL_Centre_Add();
        Clear_Error_Success_Box();
    }


    private void FillDDL_Standard_Add()
    {
        string Div_Code = null;
        Div_Code = ddlDivision_Add.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear_Add.SelectedItem.ToString();

        DataSet dsStandard = ProductController.GetAllActive_Standard_ForYear(Div_Code, YearName);
        BindDDL(ddlStandard_Add, dsStandard, "Standard_Name", "Standard_Code");
        ddlStandard_Add.Items.Insert(0, "Select");
        ddlStandard_Add.SelectedIndex = 0;


    }


    protected void ddlAcadYear_Add_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard_Add();

        Clear_Error_Success_Box();
    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            //Clear_Error_Success_Box();
            //DivAddPanel.Visible = false;
            //DivSearchPanel.Visible = true;
            //BtnShowSearchPanel.Visible = false;
            //BtnAdd.Visible = true;

        }
        else if (Mode == "TopSearch")
        {
            //DivAddPanel.Visible = false;
            //DivSearchPanel.Visible = true;
            //BtnShowSearchPanel.Visible = false;
            //BtnAdd.Visible = true;
            //DivResultPanel.Visible = false;
        }
        else if (Mode == "Result")
        {
            //DivAddPanel.Visible = false;
            //DivSearchPanel.Visible = false;
            //BtnShowSearchPanel.Visible = false;
            //BtnAdd.Visible = true;
            //DivResultPanel.Visible = true;
            //BtnShowSearchPanel.Visible = true;


        }
        else if (Mode == "Add")
        {
            Btndownloadtemplate.Visible = true;
            New_UploadGrid.Visible = false;
            DivNew_Upload.Visible = true;
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


        BindDDL(ddlDivision_Add, dsDivision, "Division_Name", "Division_Code");
        ddlDivision_Add.Items.Insert(0, "Select");
        ddlDivision_Add.SelectedIndex = 0;


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

        BindDDL(ddlAcadYear_Add, dsAcadYear, "Description", "Id");
        ddlAcadYear_Add.Items.Insert(0, "Select");
        ddlAcadYear_Add.SelectedIndex = 0;

    }

    protected void Btndownloadtemplate_Click(object sender, EventArgs e)
    {
        //To Get the physical Path of the file(me2.doc)
        string filepath = Server.MapPath("~/Template/Test_Schedule_Upload.csv");



        // Create New instance of FileInfo class to get the properties of the file being downloaded
        FileInfo myfile = new FileInfo(filepath);

        // Checking if file exists
        if (myfile.Exists)
        {
            // Clear the content of the response
            Response.ClearContent();

            // Add the file name and attachment, which will force the open/cancel/save dialog box to show, to the header
            Response.AddHeader("Content-Disposition", "attachment; filename=" + DateTime.Now + " " + myfile.Name);

            // Add the file size into the response header
            Response.AddHeader("Content-Length", myfile.Length.ToString());

            // Set the ContentType
            //Response.ContentType = ReturnExtension(myfile.Extension.ToLower());

            // Write the file into the response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)
            Response.TransmitFile(myfile.FullName);

            // End the response
            Response.End();
        }

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (ddlDivision_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Division");
            return;

        }

        if (ddlAcadYear_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Acad Year");
            return;

        }


        if (ddlStandard_Add.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Select Course");
            return;

        }

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string CreatedBy = null;
        CreatedBy = cookie.Values["UserID"];

        Checkexcel();
        UpdatePanel1.Update();


        if (New_UploadGrid.Visible == true)
        {
            btnsaveexcel.Visible = false;
            Btnimport.Visible = true;
            Btnimport.Enabled = true;
            Btndownloadtemplate.Visible = false;
            lbldivisionresult.Text = ddlDivision_Add.SelectedItem.Text;
            lblacadyearresult.Text = ddlAcadYear_Add.SelectedItem.Text;
            lblcourseresult.Text = ddlStandard_Add.SelectedItem.Text;

        }
    }
    protected void Btnimport_Click(object sender, EventArgs e)
    {
        try
        {

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            Btnimport.Enabled = false;
            DataSet dsinsertlog = ProductController.INSERT_LOG_EXCEL_IMPORT("1", "", "Test Schedule Upload", lblfilename.Text, datalist_NewUploads1.Items.Count, UserID);
            string importcode = Convert.ToString(dsinsertlog.Tables[0].Rows[0]["Record_Id"]);

            foreach (DataListItem item in datalist_NewUploads1.Items)
            {
                if (item.ItemType == ListItemType.Item | item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lbltestname = (Label)item.FindControl("lbltestname");
                    Label lblcentername = (Label)item.FindControl("lblcentername");
                    Label lblbatchname = (Label)item.FindControl("lblbatchname");
                    Label lblmaxmarks = (Label)item.FindControl("lblmaxmarks");
                    Label lbltestdate = (Label)item.FindControl("lbltestdate");
                    Label lblfromtime = (Label)item.FindControl("lblfromtime");
                    Label lbltotime = (Label)item.FindControl("lbltotime");
                    Label lblstatuss = (Label)item.FindControl("labelSTATUS");


                    Match matchmaxmarks = Regex.Match(lblmaxmarks.Text, "[^0-9]", RegexOptions.IgnoreCase);

                    int TestStartTime = 0;
                    int TestEndTime = 0;



                    if (lbltestname.Text.Trim() == "" || lblcentername.Text.Trim() == "" || lblbatchname.Text.Trim() == "" || lblmaxmarks.Text.Trim() == "" ||
                        lbltestdate.Text.Trim() == "" || lblfromtime.Text.Trim() == "" || lbltotime.Text.Trim() == "")
                    {
                        lblstatuss.Text = "Error Mandatoty Fileds Are Blank";
                        lblstatuss.ForeColor = System.Drawing.Color.Red;
                        DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT("2", importcode, "Test Schedule Upload", lblfilename.Text, datalist_NewUploads1.Items.Count, UserID);

                    }

                    if (matchmaxmarks.Success)
                    {
                        lblstatuss.Text = "Max Marks Shoud Be Numeric";
                        lblstatuss.ForeColor = System.Drawing.Color.Red;
                        DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT("2", importcode, "Test Schedule Upload", lblfilename.Text, datalist_NewUploads1.Items.Count, UserID);
                    }

                    if (lblstatuss.Text == "")
                    {
                        if (Convert.ToInt32(lblmaxmarks.Text) <= 0)
                        {
                            lblstatuss.Text = "Max Marks Shoud Be Greater Then Zero";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                            DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT("2", importcode, "Test Schedule Upload", lblfilename.Text, datalist_NewUploads1.Items.Count, UserID);

                        }
                    }

                    if (lblstatuss.Text == "")
                    {

                        DateTime Test;
                        if (DateTime.TryParseExact(lbltestdate.Text, "MM/dd/yyyy", null, DateTimeStyles.None, out Test) == true)
                        {

                        }

                        else
                        {
                            lblstatuss.Text = "Date Format Should Be (MM/dd/yyyy)";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                            DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT("2", importcode, "Test Schedule Upload", lblfilename.Text, datalist_NewUploads1.Items.Count, UserID);
                        }
                    }



                    if (lblstatuss.Text == "")
                    {


                        TestStartTime = Convert.ToInt32(Convert.ToInt32(lblfromtime.Text.Substring(0, 2)) * 60) + Convert.ToInt32(lblfromtime.Text.Substring(lblfromtime.Text.Length - 2));//Strings.Left(txtfromtime.Text, 2)) * 60 + Conversion.Val(Strings.Right(txtfromtime.Text, 2));
                        TestEndTime = Convert.ToInt32(Convert.ToInt32(lbltotime.Text.Substring(0, 2)) * 60) + Convert.ToInt32(lbltotime.Text.Substring(lbltotime.Text.Length - 2));//Strings.Left(txttotime.Text, 2)) * 60 + Conversion.Val(Strings.Right(txttotime.Text, 2));

                        if (TestStartTime <= 0 | TestStartTime >= 1440)
                        {
                            lblstatuss.Text = "Invalid Start Time";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                            DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT("2", importcode, "Test Schedule Upload", lblfilename.Text, datalist_NewUploads1.Items.Count, UserID);
                        }

                        else if (TestEndTime <= 0 | TestEndTime >= 1440)
                        {
                            lblstatuss.Text = "Invalid End Time";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                            DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT("2", importcode, "Test Schedule Upload", lblfilename.Text, datalist_NewUploads1.Items.Count, UserID);
                        }

                        else if (TestStartTime >= TestEndTime)
                        {
                            lblstatuss.Text = "Start Time can't be after End Time";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                            DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT("2", importcode, "Test Schedule Upload", lblfilename.Text, datalist_NewUploads1.Items.Count, UserID);
                        }

                    }
                    if (lblstatuss.Text == "")
                    {
                        lblstatuss.Text = "Success";

                    }




                    if (lblstatuss.Text == "Success")
                    {

                        Label lblHeader_User_Code = default(Label);
                        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                        string CreatedBy = null;
                        CreatedBy = lblHeader_User_Code.Text;

                        string ResultId = ProductController.Insert_Test_Schedule_Upload(ddlDivision_Add.SelectedValue.ToString(), ddlAcadYear_Add.SelectedValue.ToString(),
                            ddlStandard_Add.SelectedValue.ToString(), lbltestname.Text.Trim(), lblcentername.Text.Trim(), lblbatchname.Text.Trim(), lbltestdate.Text.Trim(),
                            TestStartTime, TestEndTime, lblfromtime.Text.Trim(), lbltotime.Text.Trim(), Convert.ToInt32(lblmaxmarks.Text), "", CreatedBy, importcode);

                        if (ResultId == "Record Saved Sucessfully")
                        {
                            lblstatuss.Text = "Success";
                            lblstatuss.Visible = true;
                            lblSuccess.Text = ResultId;
                            lblstatuss.ForeColor = System.Drawing.Color.Green;
                        }
                        else if (ResultId == "Test Name Not Found")
                        {
                            lblstatuss.Text = ResultId;
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                        }
                        else if (ResultId == "Center Short Name Not Found")
                        {
                            lblstatuss.Text = ResultId;
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                        }

                        else if (ResultId == "Batch Short Name Not Found")
                        {
                            lblstatuss.Text = ResultId;
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                        }

                        else if (ResultId == "Test Not Assigned To Center")
                        {
                            lblstatuss.Text = ResultId;
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                        }

                        else if (ResultId == "Test Not Authorised")
                        {
                            lblstatuss.Text = ResultId;
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                        }

                        else
                        {
                            lblstatuss.Text = "Unknown Error Kindly Coordinate With Administrator";
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                            DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT("2", importcode, "Test Schedule Upload", lblfilename.Text, datalist_NewUploads1.Items.Count, UserID);
                        }

                    }

                }


            }

        }
        catch (Exception ex)
        {
            Show_Error_Success_Box("E", ex.ToString());
        }

        Btnimport.Visible = false;
        btnsaveexcel.Visible = true;
    }

    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Tran_TestSchedule.aspx");
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ControlVisibility("Add");
    }
    protected void btnsaveexcel_Click(object sender, EventArgs e)
    {

        DataTable table = new DataTable();
        //adding column
        table.Columns.Add("Test Name", typeof(string));
        table.Columns.Add("Center Short Name", typeof(string));
        table.Columns.Add("Batch Short Name", typeof(string));
        table.Columns.Add("Max Marks", typeof(string));
        table.Columns.Add("Test Date", typeof(string));
        table.Columns.Add("From Time", typeof(string));
        table.Columns.Add("To Time", typeof(string));
        table.Columns.Add("Status", typeof(string));


        foreach (DataListItem item in datalist_NewUploads1.Items)
        {
            string TestName = "", CenterShortName = "", BatchShortName = "", MaxMarks = "", TestDate = "", FromTime = "", ToTime = "", Status = "";





            TestName = ((Label)item.FindControl("lbltestname")).Text;
            CenterShortName = ((Label)item.FindControl("lblcentername")).Text;
            BatchShortName = ((Label)item.FindControl("lblbatchname")).Text;
            MaxMarks = ((Label)item.FindControl("lblmaxmarks")).Text;
            TestDate = ((Label)item.FindControl("lbltestdate")).Text;
            FromTime = ((Label)item.FindControl("lblfromtime")).Text;
            ToTime = ((Label)item.FindControl("lbltotime")).Text;
            Status = ((Label)item.FindControl("labelSTATUS")).Text;

            table.Rows.Add(TestName, CenterShortName, BatchShortName, MaxMarks, TestDate, FromTime, ToTime, Status);



        }


        Response.ContentType = "Application/x-msexcel";
        Response.AddHeader("content-disposition", "attachment;filename=Test Schedule Upload Status" + " " + DateTime.Now + ".csv");
        Response.Write(ExportToCSVFile(table));
        Response.End();
    }


    private string ExportToCSVFile(DataTable dtTable)
    {
        StringBuilder sbldr = new StringBuilder();
        if (dtTable.Columns.Count != 0)
        {

            foreach (DataColumn col in dtTable.Columns)
            {
                sbldr.Append(col.ColumnName + ',');
            }
            sbldr.Append("\r\n");
            foreach (DataRow row in dtTable.Rows)
            {
                foreach (DataColumn column in dtTable.Columns)
                {

                    sbldr.Append(row[column].ToString() + ',');
                }
                sbldr.Append("\r\n");
            }
        }
        return sbldr.ToString();
    }

}