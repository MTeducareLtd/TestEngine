using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Diagnostics;
using System.Web.UI.WebControls;

partial class Config_Category : System.Web.UI.Page
{

    

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlVisibility("Search");
        }
    }

    private void ControlVisibility(string Mode)
    {
        if (Mode == "Search")
        {
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = true;
            BtnShowSearchPanel.Visible = false;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = true;
        }
        else if (Mode == "Result")
        {
            DivResultPanel.Visible = true;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            DivAddPanel.Visible = false;
            BtnAdd.Visible = true;
        }
        else if (Mode == "Add")
        {
            DivAddPanel.Visible = true;
            DivResultPanel.Visible = false;
            DivSearchPanel.Visible = false;
            BtnShowSearchPanel.Visible = true;
            BtnAdd.Visible = false;
        }
    }

    protected void BtnAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Add");
    }

    protected void BtnCloseAdd_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Result");

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] {
			new DataColumn("ModeCode", typeof(int)),
			new DataColumn("ModeName", typeof(string)),
			new DataColumn("Status", typeof(string))
		});
        dt.Rows.Add(1, "Theory", "Active");
        dt.Rows.Add(2, "MCQ", "Active");

        dlGridDisplay.DataSource = dt;
        dlGridDisplay.DataBind();

        lbltotalcount.Text = "2";
    }

    protected void dlGridDisplay_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            lbldelCode.Text =Convert.ToString(e.CommandArgument);
            txtDeleteItemName.Text = (((Label)e.Item.FindControl("lblModeName")).Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDelete();", true);
        }
    }

    protected void BtnShowSearchPanel_Click(object sender, System.EventArgs e)
    {
        ControlVisibility("Search");
    }
    public Config_Category()
    {
        Load += Page_Load;
    }
}
