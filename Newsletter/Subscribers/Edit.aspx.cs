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

public partial class Subscribers_Edit : BasePage
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
            Bind();
    }
    protected void Bind()
    {
        string index = Request.QueryString["index"];
        DataTable dt = Utils.GetDataTable("SELECT * FROM SUBSCRIBERS WHERE ID=" + index);
        DataList1.DataSource = dt;
        DataList1.EditItemIndex = 0;
        DataList1.DataBind();


        // Populate the Listbox
        ListBox Listbox1 = (ListBox)this.DataList1.Controls[0].FindControl("Listbox1");
        if (UserLogin.GetCurrentUserRole() == "Admin")
            Listbox1.DataSource = Lists.GetLists();
        else
            Listbox1.DataSource = Lists.GetCurrentUserLists();
        Listbox1.DataTextField = "ListName";
        Listbox1.DataValueField = "id";
        Listbox1.DataBind();

        // Select Items in the ListBox
        DataTable dtSelItems = Utils.GetDataTable("SELECT * FROM SubscribersLists WHERE sid=" + index);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dtSelItems.Rows)
            {
                if (Listbox1.Items.FindByValue(dr["lid"].ToString()) != null) Listbox1.Items.FindByValue(dr["lid"].ToString()).Selected = true;
            }

        }

    }
    protected void DataList1_DeleteCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        string sid = Request.QueryString["index"];
        string Name = ((TextBox)e.Item.FindControl("Name")).Text;
        string Email = ((TextBox)e.Item.FindControl("Email")).Text;

        string SqlList = "DELETE * FROM SUBSCRIBERSLISTS WHERE ";

        ListBox Listbox1 = (ListBox)this.DataList1.Controls[0].FindControl("Listbox1");
        string DeleteID = "LID=";
        foreach (ListItem li in Listbox1.Items)
        {
            if (li.Selected)
            {
                if (DeleteID == "LID=") DeleteID = DeleteID + li.Value;
                else DeleteID = DeleteID + " OR LID=" + li.Value;
            }
        }
        SqlList = SqlList + "((sid=" + sid + ") AND (" + DeleteID + "));";
        Utils.DbCmdExecute(SqlList);
        // Check if the subscribers exist in any other lists
        string SqlSub = "SELECT slid FROM SubscribersLists WHERE SID=" + sid;
        object slid = Utils.DbCmdExecuteScalar(SqlSub);
        if (slid == null)
        {
            // Delete the subscriber
            string SqlString = "DELETE * FROM SUBSCRIBERS WHERE ID=" + sid;
            Utils.DbCmdExecute(SqlString);
        }
        Response.Redirect("default.aspx");
    }
    protected void DataList1_UpdateCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        string index = Request.QueryString["index"];
        string Name = ((TextBox)e.Item.FindControl("Name")).Text;
        string Email = ((TextBox)e.Item.FindControl("Email")).Text;
        Name = Name.Replace("'", "''");
        Email = Email.Replace("'", "''");
        string SqlUpdate = "UPDATE SUBSCRIBERS SET [Name]='" + Name + "', [Email] = '" + Email + "'  WHERE ID=" + index;
        Utils.DbCmdExecute(SqlUpdate);

        /// Update List Subscriptions
        ArrayList AddLists = new ArrayList();
        ArrayList RemoveLists = new ArrayList();
        ListBox Listbox1 = (ListBox)this.DataList1.Controls[0].FindControl("Listbox1");
        foreach (ListItem li in Listbox1.Items)
        {
            if (li.Selected) AddLists.Add(li.Value); else RemoveLists.Add(li.Value);
        }


        SubscriberMember.InsertIntoList(Email, Name, AddLists);
        SubscriberMember.RemoveFromList(Email, Name, RemoveLists);
        //Response.Write(SqlUpdate);
        Response.Redirect("default.aspx");
    }
    protected void DataList1_ItemCreated(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
    {
        if (DataList1.EditItemIndex == e.Item.ItemIndex)
        {
            ImageButton deleteButton = (ImageButton)e.Item.FindControl("Delete");
            //Javascript for confirmation of deletion.
            deleteButton.Attributes["onclick"] = javascriptAreYouSure("theSubscriber", "delete");
        }
    }
}
