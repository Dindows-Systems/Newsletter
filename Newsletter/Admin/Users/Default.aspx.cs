using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using i386;
using i386.Newsletter;

public partial class Admin_Users_Default : BasePageGrid
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
        dg = DataGrid1;
        if (UserLogin.GetCurrentUserRole() == "Admin")
        {
            // Put user code to initialize the page here
            if (!IsPostBack)
            {
                this.Listbox1.DataSource = Lists.GetLists();
                this.Listbox1.DataTextField = "ListName";
                this.Listbox1.DataValueField = "ID";
                this.Listbox1.DataBind();
                String allLists = (String)GetGlobalResourceObject("labels", "allLists");
                this.Listbox1.Items.Insert(0, new ListItem("[" + allLists + "]", ""));

                if (index > 0)
                {
                    if (this.Listbox1.Items.FindByValue(index.ToString()) != null)
                        this.Listbox1.Items.FindByValue(index.ToString()).Selected = true;
                }
                else
                {
                    this.Listbox1.SelectedIndex = 0;
                }

                Bind();
            }
        }
        else
        {
            Page.Visible = false;
        }
    }


    protected override void Bind()
    {
        // make sure ViewState["SQLStringFilter"]=""; is in the Init
        string SQLFilter = "";
        string ViewStateFilter = ViewState["SQLStringFilter"].ToString();

        if (ViewStateFilter != "")
        {
            SQLFilter = " WHERE ((Email Like \"%" + ViewStateFilter + "%\") OR " +
                "(Name Like \"%" + ViewStateFilter + "%\")) ";
        }
        // Build Format Type Filter

        // Build Role Filter
        string SQLRoleFilter = "";
        if (RoleList.SelectedValue != "") SQLRoleFilter = "(Role='" + RoleList.SelectedValue + "')";

        // Build a SQL Filter string from the listbox
        string SQLListFilter = "";

        for (int i = 0; i < Listbox1.Items.Count; i++)
        {
            if (Listbox1.Items[i].Selected && Listbox1.Items[i].Value != "")
                SQLListFilter = SQLListFilter + " (UsersLists.lid=" + Listbox1.Items[i].Value + ") OR";
        }
        // Remove the last 'OR' statement if it exists
        int FilterLen = SQLListFilter.Length;
        if (FilterLen > 2 && SQLListFilter.Substring(FilterLen - 2, 2) == "OR")
            SQLListFilter = SQLListFilter.Substring(0, FilterLen - 2);

        // --- Build the list Filter.
        // If the [ALL] is select then remove the filter.
        if (Listbox1.Items.FindByValue("").Selected) SQLListFilter = "";
        if (SQLListFilter != "")
        {
            if (ViewStateFilter != "") SQLListFilter = " AND (" + SQLListFilter + ")";
            else SQLListFilter = " WHERE (" + SQLListFilter + ")";
            if (SQLRoleFilter != "") SQLListFilter = SQLListFilter + " AND " + SQLRoleFilter;

        }
        else
        {
            // When [All List]

            if (ViewStateFilter != "")
            {
                //	
                if (SQLRoleFilter != "") SQLListFilter = SQLListFilter + " AND " + SQLRoleFilter;
            }
            else
            {
                if (SQLRoleFilter != "") SQLListFilter = "WHERE " + SQLRoleFilter;
            }


        }


        string SqlString = " SELECT users.id, users.Name, users.Email, "
            + " Count(UsersLists.lid) AS CountOflid FROM Users " +
            "LEFT JOIN UsersLists ON Users.id = UsersLists.uid " + SQLFilter + SQLListFilter +
            " GROUP BY Users.id, Users.Name, Users.Email " + ViewState["SQLStringSort"];
        try
        {
            DataTable dt = Utils.GetDataTable(SqlString);
            DataGrid1.DataSource = dt;
            DataGrid1.DataBind();
        }
        catch (Exception ErrMsg)
        {
            Response.Write("Error SQL:" + ErrMsg.Message + " " + SqlString);
        }


    }

    protected void DataGrid1_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        // Get the ID from the hidden Textbox
        string ID = ((TextBox)e.Item.Cells[0].FindControl("recid")).Text;
        UserLogin.Delete(ID);
        Bind();
    }

    protected void DataGrid1_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        BaseItem(sender, e);
    }

    protected void Button1_Click(object sender, System.EventArgs e)
    {
        DataGrid1.CurrentPageIndex = 0;
        ViewState["SQLStringFilter"] = this.SearchBox.Text.Replace("\"", "\"\"");
        Bind();
    }

    protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "NameUp":
                ViewState["SQLStringSort"] = " ORDER BY [Name]";
                break;
            case "NameDn":
                ViewState["SQLStringSort"] = " ORDER BY [Name] DESC";
                break;
            case "EmailUp":
                ViewState["SQLStringSort"] = " ORDER BY [Email]";
                break;
            case "EmailDn":
                ViewState["SQLStringSort"] = " ORDER BY [Name] DESC";
                break;
            case "SubsDn":
                ViewState["SQLStringSort"] = " ORDER BY Count(UsersLists.lid) DESC";
                break;
            case "SubsUp":
                ViewState["SQLStringSort"] = " ORDER BY Count(UsersLists.lid)";
                break;
        }
        Bind();
    }
}
