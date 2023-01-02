using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using ShoppingCart.BL;
using System.Data.SqlClient;

public partial class ContactImageUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Msg_Error.Visible = false;
        Msg_Success.Visible = false;
        lblSuccess.Text = "";
        lblerror.Text = "";
        //GetImagesFromDatabase();
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (fileContact.PostedFile != null)
            {
                if (fileContact.HasFile)
                {
                    string strFilePath = "images/studentphoto/";
                    string strFileName = "";
                    FileInfo fi = new FileInfo(fileContact.FileName);
                    string ext = fi.Extension.ToLower().Trim();

                    if (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".jpeg" || ext == ".bitmap")
                    {
                        //StartUpLoad();
                        if (!System.IO.Directory.Exists(Server.MapPath(strFilePath)))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(strFilePath));
                        }
                        DataSet ds = ProductController.UpdateImagePath("051112013000038", ext);
                        if (ds != null)
                        {
                            if (ds.Tables.Count != 0)
                            {
                                strFileName = ds.Tables[0].Rows[0]["ImagePath"].ToString();
                                fileContact.SaveAs(Server.MapPath(strFilePath + strFileName));
                                Msg_Success.Visible = true;
                                Msg_Error.Visible = false;
                                lblSuccess.Text = "File uploaded successfully";


                            }
                        }                        
                    }
                    else
                    {
                        Msg_Error.Visible = true;
                        Msg_Success.Visible = false;
                        lblerror.Text = "Invalid File";
                    }
                }
                else
                {
                    Msg_Error.Visible = true;
                    Msg_Success.Visible = false;
                    lblerror.Text = "Please upload File";

                }
            }
            else
            {
                Msg_Error.Visible = true;
                Msg_Success.Visible = false;
                lblerror.Text = "Please upload File";
            }
        }
        catch (Exception ex)
        {
            Msg_Error.Visible = true;
            Msg_Success.Visible = false;
            lblerror.Text = ex.ToString(); 
        }
    }


    private void StartUpLoad()
    {

        //get the image file that was posted (binary format)
        byte[] theImage = new byte[fileContact.PostedFile.ContentLength];
        HttpPostedFile Image = fileContact.PostedFile;
        Image.InputStream.Read(theImage, 0, (int)fileContact.PostedFile.ContentLength);
        int length = theImage.Length; //get the length of the image
        string fileName = fileContact.FileName.ToString(); //get the file name of the posted image
        string type = fileContact.PostedFile.ContentType; //get the type of the posted image
        int size = fileContact.PostedFile.ContentLength; //get the size in bytes that
        if (fileContact.PostedFile != null && fileContact.PostedFile.FileName != "")
        {
            //Call the method to execute Insertion of data to the Database
            ExecuteInsert(theImage, "CON1000000001", length);
            Response.Write("Save Successfully!");
        }
    }

    public string GetConnectionString()
    {
        //sets the connection string from your web config file "ConnString" is the name of your Connection String
        return System.Configuration.ConfigurationManager.AppSettings["CONSTR"].ToString();
    }

    private void ExecuteInsert(byte[] Image, string ContactId, int length)
    {
        SqlConnection conn = new SqlConnection(GetConnectionString());
        string sql = "INSERT INTO TOE00028_Contacts_Image (ContactId, Photo) VALUES "

                    + " (@ContactId,@Photo)";
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@ContactId", SqlDbType.VarChar, 20);
            param[1] = new SqlParameter("@Photo", SqlDbType.Image, length);

            param[0].Value = ContactId;
            param[1].Value = Image;
            

            for (int i = 0; i < param.Length; i++)
            {
                cmd.Parameters.Add(param[i]);
            }

            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
        finally
        {
            conn.Close();
        }
    }


    void GetImagesFromDatabase()
    {

        try
        {

            //Initialize SQL Server connection.

            

            SqlConnection CN = new SqlConnection(GetConnectionString());

            //Initialize SQL adapter.

            SqlDataAdapter ADAP = new SqlDataAdapter("Select * from TOE00028_Contacts_Image", CN);



            //Initialize Dataset.

            DataSet DS = new DataSet();

            //Fill dataset with ImagesStore table.

            ADAP.Fill(DS, "TOE00028_Contacts_Image");

            DataTable dt = DS.Tables[0];
            Byte[] bytes = (Byte[])dt.Rows[0]["Photo"];

            Response.Buffer = true;

            Response.Charset = "";

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.ContentType = "image/jpeg";//dt.Rows[0]["ContentType"].ToString();

            Response.AddHeader("content-disposition", "attachment;filename=aa");

            //+ dt.Rows[0]["Name"].ToString());

            Response.BinaryWrite(bytes);

            Response.Flush();

            Response.End();
            //Fill Grid with dataset.
            //  dataGridView1.DataSource = DS.Tables["TOE00028_Contacts_Image"];
        }

        catch (Exception ex)
        {

           // MessageBox.Show(ex.ToString());

        }

    }

    
}