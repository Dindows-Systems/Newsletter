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
public partial class Admin_Users_Edit : BasePage
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (UserLogin.GetCurrentUserRole() == "Admin")
        {
            // Put user code to initialize the page here
            if (!IsPostBack) Bind();
        }
        else Page.Visible = false;


    }
    protected void Bind()
    {
        string index = Request.QueryString["index"];

        DataTable dt = Utils.GetDataTable("SELECT * FROM USERS WHERE ID=" + index);
        DataList1.DataSource = dt;
        int i = 0;
        string UserRole = "";
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["ID"].ToString() == index)
            {
                DataList1.EditItemIndex = i;
                UserRole = dr["Role"].ToString();
            }
            i++;
        }
        DataList1.DataBind();

        //Roles
        DropDownList ListOfRoles = (DropDownList)this.DataList1.Controls[0].FindControl("ListOfRoles");
        if (ListOfRoles.Items.FindByValue(UserRole) != null) ListOfRoles.Items.FindByValue(UserRole).Selected = true;

        // Populate the Listbox
        ListBox Listbox1 = (ListBox)this.DataList1.Controls[0].FindControl("Listbox1");
        Listbox1.DataSource = Lists.GetLists();
        Listbox1.DataTextField = "ListName";
        Listbox1.DataValueField = "id";
        Listbox1.DataBind();

        // Select Items in the ListBox
        DataTable dsSelItems = Utils.GetDataTable("SELECT * FROM UsersLists WHERE uid=" + index);
        foreach (DataRow dr in dsSelItems.Rows)
        {
            if (Listbox1.Items.FindByValue(dr["lid"].ToString()) != null) Listbox1.Items.FindByValue(dr["lid"].ToString()).Selected = true;
        }

    }
    protected void DataList1_UpdateCommand(object source, DataListCommandEventArgs e)
    {
        string index = Request.QueryString["index"];
        string Name = ((TextBox)e.Item.FindControl("Name")).Text;
        string Email = ((TextBox)e.Item.FindControl("Email")).Text;
        string Description = ((TextBox)e.Item.FindControl("Description")).Text;
        string Password = ((TextBox)e.Item.FindControl("Password")).Text;
        string UserName = ((TextBox)e.Item.FindControl("UserName")).Text;
        DropDownList ListOfRoles = (DropDownList)this.DataList1.Controls[0].FindControl("ListOfRoles");
        string Role = ListOfRoles.SelectedValue;
        Role = Role.Replace("'", "''");
        Name = Name.Replace("'", "''");
        Email = Email.Replace("'", "''");
        Password = Password.Replace("'", "''");
        UserName = UserName.Replace("'", "''");
        Description = Description.Replace("'", "''");
        // Check the UserName doesn't exist already!!!


        string SqlUpdate = "UPDATE USERS SET [Name]='" + Name + "', Email ='" + Email + "', [Role]='" + Role + "', [Password]='" + Password + "', [Description]='" + Description + "', [UserName]='" + UserName + "'WHERE ID=" + index;
        try
        {

            // Execute SQL
            Utils.DbCmdExecute(SqlUpdate);

            ListBox Listbox1 = ((ListBox)e.Item.FindControl("Listbox1"));
            // Update the UsersLists Table
            string SqlDelString = " DELETE * FROM UsersLists WHERE uid=" + index;
            // Execute SQL
            Utils.DbCmdExecute(SqlDelString);
            foreach (ListItem li in Listbox1.Items)
            {
                if (li.Selected)
                {
                    string SqlInsert = " INSERT INTO UsersLists (uid,lid) VALUES (" + index + "," + li.Value + ")";
                    // Execute SQL
                    Utils.DbCmdExecute(SqlInsert);
                }

            }
            // Redirect to Users
            Response.Redirect("Users.aspx");
        }
        catch (Exception ErrMsg)
        {
            Response.Write("Error: " + ErrMsg.Message + " " + SqlUpdate);
        }
    }
    /// <summary>
    /// Delete Newsletter List and Newsletters which are not in any other group.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void DataList1_DeleteCommand(object source, DataListCommandEventArgs e)
    {
        string index = Request.QueryString["index"];
        UserLogin.Delete(index);
        Response.Redirect("Users.aspx");
    }
    protected void DataList1_ItemCreated(object sender, DataListItemEventArgs e)
    {
        if (DataList1.EditItemIndex == e.Item.ItemIndex)
        {
            ImageButton deleteButton = (ImageButton)e.Item.FindControl("Delete");
            //Javascript for confirmation of deletion.
            javascriptAreYouSure("theUser", "delete");

        }
    }



}
