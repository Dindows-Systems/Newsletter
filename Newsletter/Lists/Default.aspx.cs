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

namespace i386.Newsletter.Forms
{
	/// <summary>
	/// Summary description for Lists.
	/// </summary>
	public partial class Lists : BasePageGrid
	{
		
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            dg = DataGrid1;
			// Put user code to initialize the page here
			if (!IsPostBack) Bind();
		}
		protected override void Bind()
		{
			// make sure ViewState["SQLStringFilter"]=""; is in the Init
			string SQLFilter = "";
			string ViewStateFilter = ViewState["SQLStringFilter"].ToString();

			// Admin Role
			//string SqlString = "SELECT * FROM ListCount " + SQLFilter +  ViewState["SQLStringSort"];

			string UserName = Page.User.Identity.Name;
			UserName = UserName.Replace("'", "''"); // SQL hack
			
			if (UserLogin.GetCurrentUserRole()=="Admin")
			{
				// Admin Mode
				if (ViewStateFilter !="") SQLFilter = " WHERE (ListNewsletters.ListName Like \"%" + ViewStateFilter + "%\") ";
			}
			else // Filter for User Login
			{
				if (ViewStateFilter !="")
					SQLFilter = " WHERE ((ListNewsletters.ListName Like \"%" + ViewStateFilter + "%\") AND ((Users.UserName)='"+ UserName + "')) ";
				else
					SQLFilter = "WHERE ((Users.UserName)='"+ UserName + "')";
			}
			
			string SqlString = "SELECT ListNewsletters.ListName, ListNewsletters.CountOfrid AS CountOfnid, ListSubscribers.CountOfsid, ListNewsletters.id " +
								"FROM Users INNER JOIN ((ListNewsletters INNER JOIN ListSubscribers ON ListNewsletters.id = ListSubscribers.id) " + 
								"INNER JOIN UsersLists ON ListNewsletters.id = UsersLists.lid) ON Users.ID = UsersLists.uid " +
								SQLFilter +
								"GROUP BY ListNewsletters.ListName, ListNewsletters.CountOfrid, ListSubscribers.CountOfsid, ListNewsletters.id " +
								ViewState["SQLStringSort"];										
			try	
			{
				DataTable dt = Utils.GetDataTable(SqlString);
				DataGrid1.DataSource = dt;
				DataGrid1.DataBind();
				
			}
			catch
			{
				Response.Write	("Error Sql:" + SqlString);
			}
			
		}
	

		protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "NameUp":
					ViewState["SQLStringSort"] =" ORDER BY [ListNewsletters.ListName]";
					break;
				case "NameDn":
					ViewState["SQLStringSort"] =" ORDER BY [ListNewsletters.ListName] DESC";
					break;
				case "NewsLUp":
					ViewState["SQLStringSort"] =" ORDER BY [ListNewsletters.CountOfrid]";
					break;
				case "NewsLDn":
					ViewState["SQLStringSort"] =" ORDER BY [ListNewsletters.CountOfrid] DESC";
					break;
				case "SubsUp":
					ViewState["SQLStringSort"] =" ORDER BY [ListSubscribers.CountOfsid]";
					break;
				case "SubsDn":
					ViewState["SQLStringSort"] =" ORDER BY [ListSubscribers.CountOfsid] DESC";					
					break;
			}
            Bind();
		}


		protected void DataGrid1_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
            BaseItem(sender, e);										  
		}
        protected void Button1_Click1(object sender, EventArgs e)
        {
			DataGrid1.CurrentPageIndex = 0;
			ViewState["SQLStringFilter"]= this.SearchBox.Text.Replace("\"", "\"\"");
			Bind();
        }
        protected void DataGrid1_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            string ID = ((TextBox)e.Item.Cells[0].FindControl("recid")).Text;
            i386.Newsletter.Lists.Delete(ID);
            Bind();
        }
}
}
