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
using i386;
using i386.Newsletter;

	/// <summary>
	/// Summary description for EditList.
	/// </summary>
	public partial class EditList : BasePage
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!IsPostBack) Bind();

			
		}
		private void Bind()
		{
			string index = Request.QueryString["index"];
			if (UserLogin.RightsToList(index))
			{
				DataTable dt = Utils.GetDataTable("SELECT * FROM LISTS WHERE ID="+ index);
				DataList1.DataSource = dt;
				int i=0;
				foreach (DataRow dr in dt.Rows)
				{
					if (dr["ID"].ToString()==index) DataList1.EditItemIndex = i;
					i++;
				}
				DataList1.DataBind();
			}
			else
			{
				Response.Write("Access Denied!");
			}
		}




        protected void DataList1_UpdateCommand(object source, DataListCommandEventArgs e)
		{
			string index = Request.QueryString["index"];
			string Name = ((TextBox)e.Item.FindControl("Name")).Text;
			string Description = ((TextBox)e.Item.FindControl("Description")).Text;
			Name=Name.Replace("'", "''");
			Description=Description.Replace("'","''");
			string SqlUpdate  = "UPDATE LISTS SET [ListName]='" + Name + "', [Description]='" + Description + "' WHERE ID=" +index;
			
			Utils.DbCmdExecute(SqlUpdate);
			Response.Redirect("default.aspx");
		}
		/// <summary>
		/// Delete Newsletter List and Newsletters which are not in any other group.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		protected void DataList1_DeleteCommand(object source, DataListCommandEventArgs e)
		{
			string index = Request.QueryString["index"];
		
			
			ArrayList Lists = new ArrayList();
			Lists.Add(index);
            ArrayList Subscribers = SubscriberMember.GetFromList(index);


			i386.Newsletter.BatchRemoveProcess bulkProcess = new i386.Newsletter.BatchRemoveProcess();
			bulkProcess.DemoMode = false;
			bulkProcess.ProcessMode = "Remove";
			bulkProcess.Name = "bulk";
			bulkProcess.Subscribers= 
			bulkProcess.Lists =Lists;
			bulkProcess.Page = Page;
			bulkProcess.RunAsThread();
			i386.Newsletter.Lists.Delete(index);
			Response.Redirect("lists.aspx");
		}


        protected void DataList1_ItemCreated(object sender, DataListItemEventArgs e)
		{
			if (DataList1.EditItemIndex == e.Item.ItemIndex)
			{
				ImageButton deleteButton = (ImageButton)e.Item.FindControl("Delete");        
				//Javascript for confirmation of deletion.
                deleteButton.Attributes["onclick"] = javascriptAreYouSure("theList", "delete");			
			}
		}

	}

