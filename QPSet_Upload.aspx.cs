using ShoppingCart.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QPSet_Upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string path = Server.MapPath("~/QPSets/");
            DataTable dt = new DataTable();
            dt.Columns.Add("Folder_Name");

            foreach (string s in Directory.GetDirectories(path))
            {
                //Console.WriteLine(s.Remove(0, path.Length));
                //string a=s.Remove(0, path.Length);
                dt.Rows.Add(s.Remove(0, path.Length));
            }

            dlGridDisplayfolders.DataSource = dt;
            dlGridDisplayfolders.DataBind();
            ControlVisibility("Result");
        }
    }

    private void ControlVisibility(string Mode)
    {

        if (Mode == "Upload")
        {

            DivUploadPannel.Visible = true;
            DivResultPanel.Visible = false;

        }

        else if (Mode == "Result")
        {

            DivUploadPannel.Visible = false;

            DivResultPanel.Visible = true;
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
    protected void BtnSaveUpload_Click(object sender, EventArgs e)
    {
        divwaitsave.Visible = true;
        if (txtuploadfoldername.Text.Trim() == "")
        {
            divwaitsave.Visible = false;
            Show_Error_Success_Box("E", "Enter Folder Name");
            txtuploadfoldername.Focus();
            return;
        }



        string path = Server.MapPath("~/QPSets/" + txtuploadfoldername.Text + "/");


        if (txtuploadfoldername.Enabled == true)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            else
            {
                divwaitsave.Visible = false;
                Show_Error_Success_Box("E", "Folder Name Already Exists");
                txtuploadfoldername.Focus();
                return;
            }
        }


        HttpFileCollection files = Request.Files;

        if (files.Count > 0)
        {

            int sucesscount = 0;
            int errorcount = 0;

            for (int i = 0; i < files.Count; i++)
            {

                HttpPostedFile file = files[i];


                string fileName = file.FileName;
                string extension = Path.GetExtension(fileName);

                if (File.Exists(path + fileName))
                {

                }
                else
                {
                    if (extension == ".csv" || extension == ".CSV")
                    {
                        sucesscount = sucesscount + 1;
                        file.SaveAs(path + fileName);
                    }

                    else
                    {
                        errorcount = errorcount + 1;
                    }

                }


            }

            if (errorcount > 0)
            {
                divwaitsave.Visible = false;
                Show_Error_Success_Box("E", "No of files saved  sucessfully " + sucesscount + " files eliminated " + errorcount);

            }

            else
            {
                divwaitsave.Visible = false;
                Show_Error_Success_Box("S", "All files saved sucessfully");
            }

        }

        else
        {
            divwaitsave.Visible = false;
            Show_Error_Success_Box("E", "Select at least one file");

        }

    }
}