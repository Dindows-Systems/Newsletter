// i386 Newsletter System
// Copyright (c) 2007 i386 - http://www.i386.com
//
// LIMITATION OF LIABILITY
// The software is supplied "as is". Author cannot be held liable to you
// for any direct or indirect damage, or for any loss of income, loss of
// profits, operating losses or any costs incurred whatsoever. The software
// has been designed with care, but the Author does not guarantee that it is
// free of errors.

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using i386.Newsletter;
using i386.UI;

namespace i386.Newsletter
{
	/// <summary>
	/// Summary description for manage.
	/// </summary>
	/// 
	
	public partial class manage : BasePageGrid
	{
	
		protected System.Web.UI.WebControls.DropDownList DropdownlistHtml;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            dg = DataGrid1;
			// Put user code to initialize the page here
			
			if (!IsPostBack)
			{
				
				if (UserLogin.GetCurrentUserRole()=="Admin")
					this.Listbox1.DataSource = Lists.GetLists();
				else
					this.Listbox1.DataSource = Lists.GetCurrentUserLists();
				this.Listbox1.DataTextField = "ListName";
				this.Listbox1.DataValueField = "ID";
				this.Listbox1.DataBind();
                String allLists = (String)GetGlobalResourceObject("labels", "allLists");
				this.Listbox1.Items.Insert(0, new ListItem("[" + allLists  + "]",""));
				if (index>0) 
				{
					if (this.Listbox1.Items.FindByValue(index.ToString())!=null)
                        this.Listbox1.Items.FindByValue(index.ToString()).Selected = true;
				}
				else
				{
					this.Listbox1.SelectedIndex=0;
				}
				
				Bind();
			}
		}
        protected override void Bind()
		{
			// make sure ViewState["SQLStringFilter"]=""; is in the Init
			string SQLListFilter = "",SQLSearchFilter="", SqlFilter="",SqlRoleFilter="";
			string SqlSentDateTo = "", SqlSentDateFrom="", SqlModifiedTo="", SqlModifiedFrom="";
			// Sent Date
			if (SentDateTo.Value!="") SqlSentDateTo  = "(Sent<=#" + SentDateTo.Value + "#)";
			if (SentDateFrom.Value!="") SqlSentDateFrom = "(Sent>=#" + SentDateFrom.Value + "#)";
			// Modified Date
			if (ModifiedDateTo.Value!="") SqlModifiedTo = "(LastModified<=#" + ModifiedDateTo.Value + "#)";
			if (ModifiedDateFrom.Value!="") SqlModifiedFrom = "(LastModified>=#" + ModifiedDateFrom.Value + "#)";

			// Search
			string ViewStateFilter = ViewState["SQLStringFilter"].ToString();
			if (ViewStateFilter !="") SQLSearchFilter = " (Newsletters.Name Like \"%" + ViewStateFilter + "%\") ";
			// Lists
			if (Listbox1.SelectedValue!="") SQLListFilter= " NewsletterLists.lid=" +Listbox1.SelectedValue +" ";
			// Roles
			if (UserLogin.GetCurrentUserRole()!="Admin")
				SqlRoleFilter = "(UsersLists.uid=" + UserLogin.GetCurrentUserID() + ") "; 			

			if (SQLListFilter !="" || SQLSearchFilter!="" || SqlRoleFilter!="" || SqlSentDateTo != "" || SqlSentDateFrom!="" || SqlModifiedTo!=""|| SqlModifiedFrom!="") SqlFilter=" WHERE ";
			
			if (SqlSentDateTo!="")
			{
				if (SqlFilter!=" WHERE ") SqlFilter=SqlFilter + " AND ";
				SqlFilter = SqlFilter + SqlSentDateTo;
			}
			if (SqlSentDateFrom!="")
			{
				if (SqlFilter!=" WHERE ") SqlFilter=SqlFilter + " AND ";
				SqlFilter = SqlFilter + SqlSentDateFrom;
			}
			if (SqlModifiedTo!="")
			{
				if (SqlFilter!=" WHERE ") SqlFilter=SqlFilter + " AND ";
				SqlFilter = SqlFilter + SqlModifiedTo;
			}
			if (SqlModifiedFrom!="")
			{
				if (SqlFilter!=" WHERE ") SqlFilter=SqlFilter + " AND ";
				SqlFilter = SqlFilter + SqlModifiedFrom;
			}

			if (SqlRoleFilter!="")
			{
				if (SqlFilter!=" WHERE ") SqlFilter=SqlFilter + " AND ";
				SqlFilter = SqlFilter + SqlRoleFilter;
			}
			if (SQLListFilter!="")
			{
				if (SqlFilter!=" WHERE ") SqlFilter=SqlFilter + " AND ";
				SqlFilter = SqlFilter + SQLListFilter;
			}
			if (SQLSearchFilter!="")
			{
				if (SqlFilter!=" WHERE ") SqlFilter=SqlFilter + " AND ";
				SqlFilter = SqlFilter + SQLSearchFilter;
			}

			string SqlString = "SELECT Newsletters.Id, Newsletters.Name, Newsletters.Status, Newsletters.Sent, " +
								"Count(SubscribersLists.sid) AS CountOfListID, Newsletters.LastModified " +
								"FROM ((Newsletters LEFT JOIN NewsletterLists ON Newsletters.Id = NewsletterLists.nid) LEFT JOIN SubscribersLists ON NewsletterLists.lid = SubscribersLists.lid) LEFT JOIN UsersLists ON NewsletterLists.lid = UsersLists.lid " +
								SqlFilter + " GROUP BY Newsletters.Id, Newsletters.Name, Newsletters.Status, Newsletters.Sent, Newsletters.LastModified " +
								 ViewState["SQLStringSort"];
			try
			{
				DataTable dt = Utils.GetDataTable(SqlString);
				DataGrid1.DataSource = dt;
				DataGrid1.DataBind();
			}
			catch(Exception ErrMsg)
			{
				Response.Write("Error:" + ErrMsg.Message + SqlString);
				if (Page.Trace.IsEnabled) Response.Write(SqlString);
			}
			
		}

        protected void DataGrid1_ItemCreated(object sender, DataGridItemEventArgs e)
		{
            BaseItem(sender, e);
            if (e.Item.ItemType ==ListItemType.Item)
			{
                // Copy / Clone
                String ConfirmCloneNewsletter = (String)GetGlobalResourceObject("labels", "confirmclonenewsletter");
				ImageButton Clone = (ImageButton)e.Item.Cells[4].FindControl("clone");
				Clone.Attributes["onclick"] = "javascript:return " + 
					"confirm('" + ConfirmCloneNewsletter.Replace("'","\\'") + "');" ; 					
			}
		}
        /// <summary>
        /// Datagrid Comands
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
		{

			switch (e.CommandName)
			{
				case "NameUp":
					ViewState["SQLStringSort"] =" ORDER BY [Newsletters.Name]";					
					break;
				case "NameDn":
					ViewState["SQLStringSort"] =" ORDER BY [Newsletters.Name] DESC";
					break;
				case "RecipsUp":
					ViewState["SQLStringSort"] =" ORDER BY Count(SubscribersLists.sid)";
					break;
				case "RecipsDn":
					ViewState["SQLStringSort"] =" ORDER BY Count(SubscribersLists.sid) DESC";
					break;
				case "SentDateUp":
					ViewState["SQLStringSort"] =" ORDER BY Sent";
					break;
				case "SentDateDn":
					ViewState["SQLStringSort"] =" ORDER BY Sent DESC";
					break;
				case "StatusUp":
					ViewState["SQLStringSort"] =" ORDER BY Status";
					break;
				case "StatusDn":
					ViewState["SQLStringSort"] =" ORDER BY Status DESC";
					break;
				case "Clone":
					string ID = ((TextBox)e.Item.Cells[0].FindControl("recid")).Text;
                    Newsletter newsletter = new Newsletter();
                    newsletter.Copy(ID, User.Identity.Name);
					// Redirect to the Edit Page for the new Article.
					Response.Redirect("Edit.aspx?index=" + newsletter.RecordID.ToString());
					break;
			}
            Bind();
		}

        protected void DataGrid1_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			string ID = ((TextBox)e.Item.Cells[0].FindControl("recid")).Text;
			i386.Newsletter.Letters.Delete(ID);
			Bind();
		}

        protected void Button1_Click1(object sender, EventArgs e)
        {
            DataGrid1.CurrentPageIndex = 0;
            ViewState["SQLStringFilter"] = this.SearchBox.Text.Replace("\"", "\"\"");
            Bind();
        }
}
}
