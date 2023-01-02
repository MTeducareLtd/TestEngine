using ShoppingCart.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_QPSet_Upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Add");

        }
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

    protected void BtnShowSearchPanel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Master_QPSet.aspx");
    }
    protected void Btndownloadtemplate_Click(object sender, EventArgs e)
    {
        //To Get the physical Path of the file(me2.doc)
        string filepath = Server.MapPath("~/Template/QPSet_Upload.csv");



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
            //lbldivisionresult.Text = ddlDivision_Add.SelectedItem.Text;
            //lblacadyearresult.Text = ddlAcadYear_Add.SelectedItem.Text;
            //lblcourseresult.Text = ddlStandard_Add.SelectedItem.Text;

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

    private void Clear_Error_Success_Box()
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        UpdatePanelMsgBox.Update();
    }

    protected void Checkexcel()
    {

        if (!string.IsNullOrEmpty(uploadfile.FileName))
        {
            //lbluploadfileName.Text = Path.GetFileName(uploadfile.FileName);
            string FullName = Server.MapPath("~/QPSetUploads") + "\\" + Path.GetFileName(uploadfile.FileName);
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

    protected void Btnimport_Click(object sender, EventArgs e)
    {
        try
        {

            HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
            string UserID = cookie.Values["UserID"];
            Btnimport.Enabled = false;
            DataSet dsinsertlog = ProductController.INSERT_LOG_EXCEL_IMPORT("1", "", "QP Set Upload", lblfilename.Text, datalist_NewUploads1.Items.Count, UserID);
            string importcode = Convert.ToString(dsinsertlog.Tables[0].Rows[0]["Record_Id"]);

            foreach (DataListItem item in datalist_NewUploads1.Items)
            {
                if (item.ItemType == ListItemType.Item | item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblassesmentcode = (Label)item.FindControl("lblassesmentcode");
                    Label lblqueno = (Label)item.FindControl("lblqueno");
                    Label lblquetype = (Label)item.FindControl("lblquetype");
                    Label lblanswerkey = (Label)item.FindControl("lblanswerkey");
                    Label lbldifficultylevel = (Label)item.FindControl("lbldifficultylevel");
                    Label lblcorrectmarks = (Label)item.FindControl("lblcorrectmarks");
                    Label lblwrongmarks = (Label)item.FindControl("lblwrongmarks");
                    Label lblsubject = (Label)item.FindControl("lblsubject");
                    Label lblrefcourse = (Label)item.FindControl("lblrefcourse");
                    Label lblrefsubject = (Label)item.FindControl("lblrefsubject");
                    Label lblchapter = (Label)item.FindControl("lblchapter");
                    Label lbltopic = (Label)item.FindControl("lbltopic");
                    Label lblsubtopic = (Label)item.FindControl("lblsubtopic");
                    Label lblmodule = (Label)item.FindControl("lblmodule");
                    Label lblquestionrule = (Label)item.FindControl("lblquestionrule");
                    Label lblstatuss = (Label)item.FindControl("labelSTATUS");


                    Match matchqueno = Regex.Match(lblqueno.Text, "[^0-9]", RegexOptions.IgnoreCase);
                    //Match matchcorrectmarks = Regex.Match(lblcorrectmarks.Text, "^[1-9][\\.\\d]*(,\\d+)?$", RegexOptions.IgnoreCase);
                    //Match matchwrongmarks = Regex.Match(lblwrongmarks.Text, "[^0-9]", RegexOptions.IgnoreCase);




                    if (lblassesmentcode.Text.Trim() == "" || lblqueno.Text.Trim() == "" || lblquetype.Text.Trim() == "" || lblanswerkey.Text.Trim() == "" ||
                        lbldifficultylevel.Text.Trim() == "" || lblcorrectmarks.Text.Trim() == "" || lblwrongmarks.Text.Trim() == "" || lblsubject.Text.Trim() == "")
                    {
                        lblstatuss.Text = "Error Mandatoty Fileds Are Blank";
                        lblstatuss.ForeColor = System.Drawing.Color.Red;
                        DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT("2", importcode, "QP Set Upload", lblfilename.Text, datalist_NewUploads1.Items.Count, UserID);

                    }

                    else if (matchqueno.Success)
                    {
                        lblstatuss.Text = "Correct Marks Shoud Be Numeric";
                        lblstatuss.ForeColor = System.Drawing.Color.Red;
                        DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT("2", importcode, "QP Set Upload", lblfilename.Text, datalist_NewUploads1.Items.Count, UserID);
                    }

                    //else if (matchcorrectmarks.Success)
                    //{
                    //    lblstatuss.Text = "Correct Marks Shoud Be Numeric";
                    //    lblstatuss.ForeColor = System.Drawing.Color.Red;
                    //    DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT("2", importcode, "QP Set Upload", lblfilename.Text, datalist_NewUploads1.Items.Count, UserID);
                    //}


                    //else if (matchwrongmarks.Success)
                    //{
                    //    lblstatuss.Text = "Wrong Marks Shoud Be Numeric";
                    //    lblstatuss.ForeColor = System.Drawing.Color.Red;
                    //    DataSet ds2 = ProductController.INSERT_LOG_EXCEL_IMPORT("2", importcode, "QP Set Upload", lblfilename.Text, datalist_NewUploads1.Items.Count, UserID);
                    //}



                    else
                    {
                        lblstatuss.Text = "Success";

                    }




                    if (lblstatuss.Text == "Success")
                    {

                        Label lblHeader_User_Code = default(Label);
                        lblHeader_User_Code = (Label)Master.FindControl("lblHeader_User_Code");

                        string CreatedBy = null;
                        CreatedBy = lblHeader_User_Code.Text;

                        string ResultId = ProductController.Insert_QPSetUpload(lblassesmentcode.Text.Trim(), Convert.ToInt32(lblqueno.Text.Trim()),
                            lblquetype.Text.Trim(), lblanswerkey.Text.Trim(), lbldifficultylevel.Text.Trim(), lblcorrectmarks.Text.Trim(), lblwrongmarks.Text.Trim(),
                            lblsubject.Text.Trim(), lblrefcourse.Text.Trim(), lblrefsubject.Text.Trim(), lblchapter.Text.Trim(), lbltopic.Text.Trim(), lblsubtopic.Text.Trim(),
                            lblmodule.Text.Trim(), lblquestionrule.Text.Trim(), CreatedBy, importcode);

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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Clear_Error_Success_Box();
        ControlVisibility("Add");
    }
    protected void btnsaveexcel_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        //adding column
        table.Columns.Add("Assesment_TestCode", typeof(string));
        table.Columns.Add("Que_No", typeof(string));
        table.Columns.Add("Que_Type", typeof(string));
        table.Columns.Add("Answer_Key", typeof(string));
        table.Columns.Add("Difficulti_Level", typeof(string));
        table.Columns.Add("Correct_Marks", typeof(string));
        table.Columns.Add("Wrong_Marks", typeof(string));
        table.Columns.Add("Subject", typeof(string));
        table.Columns.Add("Ref_Course", typeof(string));
        table.Columns.Add("Ref_Subject", typeof(string));
        table.Columns.Add("Chapter", typeof(string));
        table.Columns.Add("Topic", typeof(string));
        table.Columns.Add("SubTopic", typeof(string));
        table.Columns.Add("Module", typeof(string));
        table.Columns.Add("Question_Rule", typeof(string));
        table.Columns.Add("Status", typeof(string));


        foreach (DataListItem item in datalist_NewUploads1.Items)
        {

            Label lblassesmentcode = (Label)item.FindControl("lblassesmentcode");
            Label lblqueno = (Label)item.FindControl("lblqueno");
            Label lblquetype = (Label)item.FindControl("lblquetype");
            Label lblanswerkey = (Label)item.FindControl("lblanswerkey");
            Label lbldifficultylevel = (Label)item.FindControl("lbldifficultylevel");
            Label lblcorrectmarks = (Label)item.FindControl("lblcorrectmarks");
            Label lblwrongmarks = (Label)item.FindControl("lblwrongmarks");
            Label lblsubject = (Label)item.FindControl("lblsubject");
            Label lblrefcourse = (Label)item.FindControl("lblrefcourse");
            Label lblrefsubject = (Label)item.FindControl("lblrefsubject");
            Label lblchapter = (Label)item.FindControl("lblchapter");
            Label lbltopic = (Label)item.FindControl("lbltopic");
            Label lblsubtopic = (Label)item.FindControl("lblsubtopic");
            Label lblmodule = (Label)item.FindControl("lblmodule");
            Label lblquestionrule = (Label)item.FindControl("lblquestionrule");
            Label lblstatuss = (Label)item.FindControl("labelSTATUS");

            table.Rows.Add(lblassesmentcode.Text.Trim(), lblqueno.Text.Trim(), lblquetype.Text.Trim(), lblanswerkey.Text.Trim(), lbldifficultylevel.Text.Trim()
            , lblcorrectmarks.Text.Trim(), lblwrongmarks.Text.Trim(), lblsubject.Text.Trim(), lblrefcourse.Text.Trim(), lblrefsubject.Text.Trim(),
            lblchapter.Text.Trim(), lbltopic.Text.Trim(), lblsubtopic.Text.Trim(), lblmodule.Text.Trim(), lblquestionrule.Text.Trim(), lblstatuss.Text.Trim());



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
    protected void BtnShowSearchPanel_Click1(object sender, EventArgs e)
    {
        Response.Redirect("Master_QPSet.aspx");
    }
}