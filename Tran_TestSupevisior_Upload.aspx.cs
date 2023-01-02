using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using ShoppingCart.BL;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Text;

public partial class Tran_TestSupevisior_Upload : System.Web.UI.Page
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

    private void FillDDL_AcadYear()
    {
        DataSet dsAcadYear = ProductController.GetAllActiveUser_AcadYear();

        BindDDL(ddlAcadYear_Add, dsAcadYear, "Description", "Id");
        ddlAcadYear_Add.Items.Insert(0, "Select");
        ddlAcadYear_Add.SelectedIndex = 0;

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

    protected void Btndownloadtemplate_Click(object sender, EventArgs e)
    {
        //To Get the physical Path of the file(me2.doc)
        string filepath = Server.MapPath("~/Template/Test_Supervisior_Upload.csv");



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
            string FullName = Server.MapPath("~/Test_Supervisor_Uploads") + "\\" + Path.GetFileName(uploadfile.FileName);
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

                    bool exists = System.IO.Directory.Exists("~/Test_Supervisor_Uploads");


                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Test_Supervisor_Uploads"));
                        uploadfile.SaveAs(FullName);
                    }
                    else
                    {
                        uploadfile.SaveAs(FullName);
                    }
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
        //if (ddlStandard_Add.SelectedIndex == 0)
        //{
        //    Show_Error_Success_Box("E", "Select Course");
        //    return;

        //}


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
            lblcourseresult.Text = "";
                //ddlStandard_Add.SelectedItem.Text;

        }
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
        table.Columns.Add("Category", typeof(string));
        table.Columns.Add("SupervisorName", typeof(string));
        table.Columns.Add("SupervisorCode", typeof(string));
        table.Columns.Add("CentreName", typeof(string));
        //table.Columns.Add("Batch", typeof(string));
        //table.Columns.Add("ConductNo", typeof(string));
        table.Columns.Add("TestDate", typeof(string));
        //table.Columns.Add("Timing", typeof(string));
        //table.Columns.Add("TestCodes", typeof(string));
        table.Columns.Add("Hrs", typeof(string));
        table.Columns.Add("Rate", typeof(string));
        table.Columns.Add("Amt", typeof(string));
        table.Columns.Add("Remarks", typeof(string));
        table.Columns.Add("Status", typeof(string));


        foreach (DataListItem item in datalist_NewUploads1.Items)
        {
            string Category = "", SupervisorName = "", SupervisorCode = "", CentreName = "", Batch = "", ConductNo = "", TestDate = "", Timing = "", TestCodes = "",
            Hrs = "", Rate = "", Amt = "", Remarks = "", Status = "";
            
            Category = ((Label)item.FindControl("lblcategory")).Text;
            SupervisorName = ((Label)item.FindControl("lblsupervisorname")).Text;
            SupervisorCode = ((Label)item.FindControl("lblsupervisorcode")).Text;
            CentreName = ((Label)item.FindControl("lblcentername")).Text;
            //Batch = ((Label)item.FindControl("lblbatch")).Text;
            //ConductNo = ((Label)item.FindControl("lblconductno")).Text;
            TestDate = ((Label)item.FindControl("lbltestdate")).Text;
            //Timing = ((Label)item.FindControl("lbltesttiming")).Text;
            //TestCodes = ((Label)item.FindControl("lbltestcodes")).Text;
            Hrs = ((Label)item.FindControl("lblhrs")).Text;
            Rate = ((Label)item.FindControl("lblrate")).Text;
            Amt = ((Label)item.FindControl("lblamt")).Text;
            Remarks = ((Label)item.FindControl("lblremarks")).Text;
            Status = ((Label)item.FindControl("labelSTATUS")).Text;

            table.Rows.Add(Category, SupervisorName, SupervisorCode, CentreName, TestDate,  Hrs, Rate, Amt, Remarks, Status);



        }


        Response.ContentType = "Application/x-msexcel";
        Response.AddHeader("content-disposition", "attachment;filename=Test Supervisor Upload Status" + " " + DateTime.Now + ".csv");
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
    public DataTable ConvertToDataTable<T>(IList<T> data)
    {
        PropertyDescriptorCollection properties =
           TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();
        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (T item in data)
        {
            DataRow row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }
        return table;

    }
    protected void Btnimport_Click(object sender, EventArgs e)
    {
        try
        {

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            Btnimport.Enabled = false;
            DataSet dsinsertlog = ProductController.INSERT_LOG_EXCEL_IMPORT("1", "", "Test Supervisior Upload", lblfilename.Text, datalist_NewUploads1.Items.Count, UserID);
            string importcode = Convert.ToString(dsinsertlog.Tables[0].Rows[0]["Record_Id"]);

            foreach (DataListItem item in datalist_NewUploads1.Items)
            {
                if (item.ItemType == ListItemType.Item | item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblcategory = (Label)item.FindControl("lblcategory");
                    Label lblsupervisorname = (Label)item.FindControl("lblsupervisorname");
                    Label lblsupervisorcode = (Label)item.FindControl("lblsupervisorcode");
                    Label lblcentername = (Label)item.FindControl("lblcentername");
                    //Label lblbatch = (Label)item.FindControl("lblbatch");
                    //Label lblconductno = (Label)item.FindControl("lblconductno");
                    Label lbltestdate = (Label)item.FindControl("lbltestdate");
                    //Label lbltesttiming = (Label)item.FindControl("lbltesttiming");
                    //Label lbltestcodes = (Label)item.FindControl("lbltestcodes");
                    Label lblhrs = (Label)item.FindControl("lblhrs");
                    Label lblrate = (Label)item.FindControl("lblrate");
                    Label lblamt = (Label)item.FindControl("lblamt");
                    Label lblremarks = (Label)item.FindControl("lblremarks");
                    Label lblstatuss = (Label)item.FindControl("labelSTATUS");


                    //Match matchconductno = Regex.Match(lblconductno.Text, "[^0-9]", RegexOptions.IgnoreCase);





                    if (lblcategory.Text.Trim() == "" || lblsupervisorcode.Text.Trim() == "" || 
                        //lblbatch.Text.Trim() == "" || 
                        lblcentername.Text.Trim() == "" ||
                        //lblconductno.Text.Trim() == "" || 
                        lbltestdate.Text.Trim() == "" || 
                        //lbltesttiming.Text.Trim() == "" || 
                        //lbltestcodes.Text.Trim() == "" ||
                        lblhrs.Text.Trim() == "" || lblrate.Text.Trim() == "" || lblamt.Text.Trim() == "")
                    {
                        lblstatuss.Text = "Error Mandatoty Fields Are Blank";
                        lblstatuss.ForeColor = System.Drawing.Color.Red;
                        DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT("2", importcode, "Test Supervisior Upload", lblfilename.Text, datalist_NewUploads1.Items.Count, UserID);

                    }

                    //if (matchconductno.Success)
                    //{
                    //    lblstatuss.Text = "Conduct No Shoud Be Numeric";
                    //    lblstatuss.ForeColor = System.Drawing.Color.Red;
                    //    DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT("2", importcode, "Test Supervisior Upload", lblfilename.Text, datalist_NewUploads1.Items.Count, UserID);
                    //}


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


                        string ResultId = ProductController.Insert_Test_Supervisor_Details("1", lblcategory.Text.Trim(),ddlDivision_Add.SelectedValue,
                        ddlAcadYear_Add.SelectedValue, 
                        ddlStandard_Add.SelectedValue, 
                        "", 
                        lblcentername.Text.Trim(), 
                        "",
                        Convert.ToInt32(0),
                        //Convert.ToInt32(lblconductno.Text.Trim()), 
                        lbltestdate.Text.Trim(), 
                        "",
                        //lbltesttiming.Text.Trim(), 
                        lblhrs.Text.Trim(),
                        lblrate.Text.Trim(), 
                        lblamt.Text.Trim(), 
                        lblsupervisorcode.Text.Trim(), 
                        lblremarks.Text, CreatedBy, importcode);

                        if (ResultId == "Record Saved Sucessfully")
                        {
                            lblstatuss.Text = "Success";
                            lblstatuss.Visible = true;
                            lblSuccess.Text = ResultId;
                            lblstatuss.ForeColor = System.Drawing.Color.Green;
                        }

                        else
                        {
                            lblstatuss.Text = ResultId;
                            lblstatuss.ForeColor = System.Drawing.Color.Red;
                            //lblstatuss.Text = "Unknown Error Kindly Coordinate With Administrator";
                            //lblstatuss.ForeColor = System.Drawing.Color.Red;
                            //DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT("2", importcode, "QP Set Upload", lblfilename.Text, datalist_NewUploads1.Items.Count, UserID);
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
    protected void ddlAcadYear_Add_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlDivision_Add_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDDL_Standard_Add();
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
}