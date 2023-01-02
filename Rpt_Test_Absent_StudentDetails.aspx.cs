using ShoppingCart.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
//using System.Data.SqlClient.SqlDataReader;
//using Exportxls.BL;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class Rpt_Test_Absent_StudentDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
            FillDDL_Division();
            FillDDL_AcadYear();

        }
    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;

        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;

        }


        Clear_Error_Success_Box();
    }

    private void BindDDL(DropDownList ddl, DataSet ds, string txtField, string valField)
    {
        ddl.DataSource = ds;
        ddl.DataTextField = txtField;
        ddl.DataValueField = valField;
        ddl.DataBind();
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

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        DataSet ds = ProductController.GetUser_Company_Division_Zone_Center(2, UserID, "", "", "MT");
        BindListBox(ddlDivision, ds, "Division_Name", "Division_Code");
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

    private void FillDDL_Search_Centre()
    {
        List<string> list = new List<string>();
        string division = "";
        foreach (ListItem li in ddlDivision.Items)
        {
            if (li.Selected == true)
            {
                list.Add(li.Value);
                division = string.Join(",", list.ToArray());
            }
        }
        string dlDivision = division;

        HttpCookie cookie = Request.Cookies.Get("MyCookiesLoginInfo");
        string UserID = cookie.Values["UserID"];
        string UserName = cookie.Values["UserName"];
        DataSet dsCentre = ProductController.GetUser_Company_Division_Zone_Center(19, UserID, dlDivision, "", "MT");
        BindListBox(ddlCentre, dsCentre, "Center_Name", "Center_Code");
        ddlCentre.Items.Insert(0, "Select");


    }

    private void FillDDL_Standard()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();

        DataSet dsStandard = ProductController.GetAllActive_Standard_ForYear(Div_Code, YearName);
        BindListBox(ddlStandard, dsStandard, "Standard_Name", "Standard_Code");
        ddlStandard.Items.Insert(0, "Select");
        //ddlStandard.SelectedIndex = 0;
    }

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        //Validate if all information is entered correctly
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

        if (ddlStandard.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Kindly Select Course");
            ddlStandard.Focus();
            return;
        }
        if (ddlCentre.SelectedIndex <= 0)
        {
            Show_Error_Success_Box("E", "Kindly Select Center");
            return;
        }
        if (ddlProduct.SelectedIndex == 0)
        {
            Show_Error_Success_Box("E", "Kindly Select Product");
            ddlStandard.Focus();
            return;
        }
           if (id_date_range_picker_1.Value == "")
        {
            Show_Error_Success_Box("E", "Kindly Select Date");
            
            return;
        }


        ControlVisibility("Result");

        List<string> list = new List<string>();
        List<string> list2 = new List<string>();
        List<string> list3 = new List<string>();
        List<string> list4 = new List<string>();

        string division = "";
        foreach (ListItem li in ddlDivision.Items)
        {
            if (li.Selected == true)
            {
                list.Add(li.Value);
                division = string.Join(",", list.ToArray());
            }
        }
        string dlDivision = division;

        string Course = "";
        foreach (ListItem li2 in ddlStandard.Items)
        {
            if (li2.Selected == true)
            {
                list2.Add(li2.Value);
                Course = string.Join(",", list2.ToArray());
            }
        }
        string coursecode = Course;

        string center = "";
        foreach (ListItem li3 in ddlCentre.Items)
        {
            if (li3.Selected == true)
            {
                list3.Add(li3.Value);
                center = string.Join(",", list3.ToArray());
            }
        }
        string centercode = center;

        string streamcode = "";
        streamcode = txtstreamcode.Text;

        string product = "";
        foreach (ListItem li4 in ddlProduct.Items)
        {
            if (li4.Selected == true)
            {
                list4.Add(li4.Value);
                product = string.Join(",", list4.ToArray());
            }
        }
        string Productcode = product;
        string DateRange = id_date_range_picker_1.Value;
        string FromDate = DateRange.Substring(0, 10);
        string Todate = (DateRange.Length > 9) ? DateRange.Substring(DateRange.Length - 10, 10) : DateRange;

        string DivisionCode = null;
        DivisionCode = ddlDivision.SelectedValue;
     

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();


        DataSet dsGrid = ProductController.GetStudentBy_Division_Year_Standard_Centre(dlDivision, coursecode, centercode, Productcode, YearName, FromDate, Todate, "2");
        if (dsGrid.Tables[0].Rows.Count > 0)
        {
            dlGridDisplay.DataSource = dsGrid;
            dlGridDisplay.DataBind();


            lblDivision_Result.Text = ddlDivision.SelectedItem.ToString();
            lblAcadYear_Result.Text = ddlAcadYear.SelectedItem.ToString();
            Lblcenter_Result.Text = ddlCentre.SelectedItem.ToString();

            lbltotalcount.Text = Convert.ToString(dsGrid.Tables[0].Rows.Count);
        }
        else
        {
            Msg_Error.Visible = true;
            lblerror.Visible = true;
            lblerror.Text = "No Record Found For Selected Search Criteria";
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
        }




    }

    protected void ddlAcadYear_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Standard();
        Clear_Error_Success_Box();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Search_Centre();
    }

    protected void ddlCentre_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_Product();
    }

    protected void ddlProduct_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        FillDDL_classroomcourse();
      

    }

    private void FillDDL_Product()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        List<string> list = new List<string>();
        List<string> list2 = new List<string>();
        List<string> list3 = new List<string>();

        string division = "";
        foreach (ListItem li in ddlDivision.Items)
        {
            if (li.Selected == true)
            {
                list.Add(li.Value);
                division = string.Join(",", list.ToArray());
            }
        }
        string dlDivision = division;

        string Course = "";
        foreach (ListItem li2 in ddlStandard.Items)
        {
            if (li2.Selected == true)
            {
                list2.Add(li2.Value);
                Course = string.Join(",", list2.ToArray());
            }
        }
        string coursecode = Course;

        string center = "";
        foreach (ListItem li3 in ddlCentre.Items)
        {
            if (li3.Selected == true)
            {
                list3.Add(li3.Value);
                center = string.Join(",", list3.ToArray());
            }
        }
        string centercode = center;


        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();
       


        DataSet dsproduct = ProductController.GetAllActive_Product(dlDivision, YearName, coursecode, "", centercode, "1");
        BindListBox(ddlProduct, dsproduct, "ProductName", "ProductCode");
        ddlProduct.Items.Insert(0, "Select");
        //ddlProduct.SelectedIndex = 0;
    }

    private void FillDDL_classroomcourse()
    {
        string Div_Code = null;
        Div_Code = ddlDivision.SelectedValue;

        string YearName = null;
        YearName = ddlAcadYear.SelectedItem.ToString();
        string coursecode = null;
        coursecode = ddlStandard.SelectedValue;
        string productcode = null;
        productcode = ddlProduct.SelectedValue;


        DataSet dsclassroom = ProductController.GetAllActive_Product(Div_Code, YearName, coursecode, productcode, ddlCentre.SelectedValue, "2");
        if (dsclassroom.Tables[0].Rows.Count > 0)
        {
            //LBLStreamname.Text = dsclassroom.Tables[0].Rows[0]["streamname"].ToString();
            //LBLStreamname.Enabled = true;

            txtstreamcode.Text = dsclassroom.Tables[0].Rows[0]["streamcode"].ToString();
            txtstreamcode.Enabled = false;

        }



    }

    


    protected void Btnclose_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }
    protected void Btnsearch_Click(object sender, EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        string filenamexls1 = "Student Test Absent" + DateTime.Now + ".xls";
        Response.AddHeader("Content-Disposition", "inline;filename=" + filenamexls1);
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1'  borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; text-align:center;'> <TR style='color: #fff; background: black;text-align:center;'><TD Colspan='9'>Student Test Absent </TD></TR>");
        Response.Charset = "";
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter1 = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter1 = new System.Web.UI.HtmlTextWriter(oStringWriter1);
        //this.ClearControls(dladmissioncount);
        dlGridDisplay.RenderControl(oHtmlTextWriter1);
        Response.Write(oStringWriter1.ToString());
        Response.Flush();
        Response.End();
    }


   
}