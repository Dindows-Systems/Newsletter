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
using System.Text;
using i386.UI;

namespace i386.Newsletter
{
	/// <summary>
	/// Summary description for Subscriber.
	/// </summary>
	public partial class Subscriber : BasePageGrid
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            dg = DataGrid1;
			// Put user code to initialize the page here
			if (!IsPostBack)
			{
				try
				{
					if (UserLogin.GetCurrentUserRole()=="Admin")
						this.Listbox1.DataSource = Lists.GetLists();
					else
						this.Listbox1.DataSource = Lists.GetCurrentUserLists();
					this.Listbox1.DataTextField = "ListName";
					this.Listbox1.DataValueField = "ID";
					this.Listbox1.DataBind();
                    String allLists = (String)GetGlobalResourceObject("labels", "allLists");
					this.Listbox1.Items.Insert(0, new ListItem("[ " + allLists + " ]",""));

					if (index >0) 
					{
						if (this.Listbox1.Items.FindByValue(index.ToString())!=null)
							this.Listbox1.Items.FindByValue(index.ToString()).Selected=true;
					}
					else
					{
						this.Listbox1.SelectedIndex=0;
					}
				}
				catch(Exception ErrMsg)
				{
					Response.Write(ErrMsg.Message);
				}
				Bind();
			}

		}
		protected override void Bind()
		{
			// make sure ViewState["SQLStringFilter"]=""; is in the Init
			string SqlFilter="", SqlSearchFilter = "", SqlFormatFilter ="", SqlConfirmFilter = "", SqlRoleFilter="", SqlString="";
			string SqlDateFrom="", SqlDateTo="";
			string ViewStateFilter = ViewState["SQLStringFilter"].ToString();

			//  Search Filter
			if (ViewStateFilter !="") SqlSearchFilter = "((Email Like \"%" + ViewStateFilter + "%\") OR " + "(Name Like \"%" + ViewStateFilter + "%\")) ";
			// Format Type Filter
			if (FormatList.SelectedValue!="") SqlFormatFilter="(Format='" + FormatList.SelectedValue +"')";
			// Date Filter			
			if (SubTo.Value!="") SqlDateTo = "(SubscriptionDate<=#" + SubTo.Value + "#)";
			if (SubFrom.Value!="") SqlDateFrom = "(SubscriptionDate>=#" + SubFrom.Value + "#)";

			// Confirm Filter
			if (ConfirmationList.SelectedValue!="") SqlConfirmFilter ="(confirm=" + ConfirmationList.SelectedValue +")";

			// List Filter
			#region List Filter
			string SQLListFilter="";
			for (int i=0; i<Listbox1.Items.Count; i++)
			{
				if (Listbox1.Items[i].Selected && Listbox1.Items[i].Value!="")
					SQLListFilter = SQLListFilter + " (SubscribersLists.lid=" + Listbox1.Items[i].Value +") OR";
			}
			// Remove the last 'OR' statement if it exists
			int FilterLen = SQLListFilter.Length;
			if (FilterLen >2 && SQLListFilter.Substring(FilterLen-2,2)=="OR")
				SQLListFilter=SQLListFilter.Substring(0,FilterLen-2);
			// If the [ALL] is select then remove the filter.
			if (Listbox1.Items.FindByValue("").Selected) SQLListFilter="";
			#endregion


			
			if (UserLogin.GetCurrentUserRole()=="Admin")
			{
				SqlString = " SELECT Subscribers.id, Subscribers.Name, Subscribers.Email, " 
				+ " Count(SubscribersLists.lid) AS CountOflid FROM Subscribers " +
				"INNER JOIN SubscribersLists ON Subscribers.id = SubscribersLists.sid ";
				
			}
			else // Filter for User Login
			{
				SqlRoleFilter = "(UsersLists.uid=" + UserLogin.GetCurrentUserID() + ") ";
				SqlString = " SELECT Subscribers.id, Subscribers.Name, Subscribers.Email, " 
				+ " Count(SubscribersLists.lid) AS CountOflid FROM " +
				"(Subscribers INNER JOIN SubscribersLists ON Subscribers.ID = SubscribersLists.sid) INNER JOIN UsersLists ON SubscribersLists.lid = UsersLists.lid ";
			}

			if (SQLListFilter!="" || SqlSearchFilter!="" || SqlFormatFilter!="" || SqlConfirmFilter!="" || SqlRoleFilter!="" || SqlDateFrom!="" || SqlDateTo!="" ) SqlFilter = " WHERE ";

			if (SqlRoleFilter!="")
			{
				if (SqlFilter!=" WHERE ") SqlFilter=SqlFilter + " AND ";
				SqlFilter = SqlFilter + SqlRoleFilter;
			}
			if (SqlConfirmFilter!="") 
			{
				if (SqlFilter!=" WHERE ") SqlFilter=SqlFilter + " AND ";
				SqlFilter = SqlFilter + SqlConfirmFilter;
			}			
			if (SqlFormatFilter!="")
			{
				if (SqlFilter!=" WHERE ") SqlFilter=SqlFilter + " AND ";
				SqlFilter = SqlFilter + SqlFormatFilter;
			}
			if (SqlSearchFilter!="")
			{
				if (SqlFilter!=" WHERE ") SqlFilter=SqlFilter + " AND ";
				SqlFilter = SqlFilter + SqlSearchFilter;
			}
			if (SQLListFilter!="")
			{
				if (SqlFilter!=" WHERE ") SqlFilter=SqlFilter + " AND ";
				SqlFilter = SqlFilter + SQLListFilter;
			}
			if (SqlDateFrom!="")
			{
				if (SqlFilter!=" WHERE ") SqlFilter=SqlFilter + " AND ";
				SqlFilter = SqlFilter + SqlDateFrom;
			}
			if (SqlDateTo!="")
			{
				if (SqlFilter!=" WHERE ") SqlFilter=SqlFilter + " AND ";
				SqlFilter = SqlFilter + SqlDateTo;
			}
			SqlString =SqlString  + SqlFilter + " GROUP BY Subscribers.id, Subscribers.Name, Subscribers.Email " + ViewState["SQLStringSort"];
		
			try
			{
				DataTable dt = Utils.GetDataTable(SqlString);			
				DataGrid1.DataSource = dt;
				DataGrid1.DataBind();
				//Response.Write(SqlString);
			}
			catch(Exception ErrMsg)
			{
				Response.Write("Error SQL:" + ErrMsg.Message + " " + SqlString);
			}
			

		}
        protected void DataGrid1_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			// Get the ID from the hidden Textbox
			string ID = ((TextBox)e.Item.Cells[0].FindControl("recid")).Text;
			// Sql String
			string strSQL= "DELETE FROM SUBSCRIBERS WHERE ID=" + ID;
			string strList = " DELETE FROM SUBSCRIBERSLISTS WHERE SID=" +ID; 
			// Database conneciton			
			Utils.DbCmdExecute(strList);
			Utils.DbCmdExecute(strSQL);
			Bind();
		}
        protected void DataGrid1_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
            BaseItem(sender, e);
		}
		protected void Button1_Click(object sender, System.EventArgs e)
		{
			DataGrid1.CurrentPageIndex = 0;
			ViewState["SQLStringFilter"]= this.SearchBox.Text.Replace("\"", "\"\"");
			Bind();
		}
		protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "NameUp":
					ViewState["SQLStringSort"] =" ORDER BY [Name]";
					break;
				case "NameDn":
					ViewState["SQLStringSort"] =" ORDER BY [Name] DESC";
					break;
				case "EmailUp":
					ViewState["SQLStringSort"] =" ORDER BY [Email]";
					break;
				case "EmailDn":
					ViewState["SQLStringSort"] =" ORDER BY [Name] DESC";
					break;
				case "SubsDn":					
					ViewState["SQLStringSort"] =" ORDER BY Count(SubscribersLists.lid) DESC";
					break;
				case "SubsUp":					
					ViewState["SQLStringSort"] =" ORDER BY Count(SubscribersLists.lid)";
					break;
			}
            Bind();
		}		
		private void ExcelExport_Click(object sender, EventArgs e)
		{

			Response.Clear(); 
			Response.AddHeader("content-disposition", "attachment;filename=Subscribers.xls");
			Response.Charset = "";
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.ContentType =  "application/vnd.ms-excel";
			string Result="";
			foreach (DataGridItem dgi in DataGrid1.Items)
			{
				Result+=dgi.Cells[2].Text;
				Result+=" ";
			}

			System.IO.StringWriter stringWrite = new System.IO.StringWriter();
				stringWrite.WriteLine(Result);
			HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
		//	ExportOutput.RenderControl(htmlWrite);

			Response.Write(stringWrite.ToString());
			Response.End();
			
		}	
	}
}
