using System;
using System.Data;
using ShoppingCart.BL;

public partial class DisplayStudentData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["sb"] != null)
            {
                string strSBEntrycode = Request.QueryString["sb"].ToString();
                BindData(strSBEntrycode);
            }            
        }
    }
    
    /// <summary>
    /// Print Student details based on SBEntrycode
    /// </summary>
    /// <param name="strSBEntrycode"></param>
    private void BindData(string strSBEntrycode)
    {
        DataSet dsDetails = new DataSet();
        dsDetails = ProductController.GetStudentDetailsBySBEntrycode(strSBEntrycode, 1);
        DataList1.DataSource = dsDetails.Tables[0];
        DataList1.DataBind();
    }
}