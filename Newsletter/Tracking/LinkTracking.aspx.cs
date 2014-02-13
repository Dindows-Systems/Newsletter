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
using i386.Newsletter;
using i386;

	/// <summary>
	/// Summary description for LinkTracking.
	/// </summary>
	public partial class LinkTracking : BasePage
	{
		
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				Bind();
			}
		}

		private void Bind()
		{
			string SqlString = "SELECT * FROM LINKTRACKING WHERE NID=" + Request.QueryString["n"] + " "  + ViewState["SQLStringSort"];
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
			ListItemType itemType= e.Item.ItemType;
			
			if (itemType==ListItemType.Pager)
			{
				// Extract the Pager 
				TableCell pager = (TableCell)e.Item.Controls[0]; 

				//Add Cell to Row to Hold Row Count Label 
				TableCell newcell = new TableCell(); 
				newcell.ColumnSpan = 1; 
				newcell.HorizontalAlign = HorizontalAlign.Center;
				newcell.Style["border-color"] = pager.Style["border-color"]; //For a Seamless Look 

				//Add Label Indicating Row Count 
				Label lblNumRecords = new Label(); 
				lblNumRecords.ID = "lblNumRecords"; 
				newcell.Controls.Add(lblNumRecords); 

				//Add Table Cell to Pager 
				e.Item.Controls.AddAt(0, newcell); 

				//Subtract from Colspan of Original Pager to Account for New Row 
				pager.ColumnSpan = pager.ColumnSpan - 1; 
			}
			else if (itemType==ListItemType.Item)
			{
				//Delete
                string confirmdeletenewsletter = (String)GetGlobalResourceObject("labels", "confirmdeletenewsletter");
				ImageButton Delete  = (ImageButton)e.Item.Cells[3].FindControl("delete");
				Delete.Attributes["onclick"] = "javascript:return " +
                    "confirm('" + confirmdeletenewsletter + "');"; 	
	
/*
				// Link Box
				PlaceHolder LinkBox = (PlaceHolder)e.Item.Cells[1].FindControl("LinkBox");
				string recID = ((DataRowView)e.Item.DataItem).Row["ID"].ToString();
				//LinkBox.Name = "clip" +recID;
				string NewsletterID = "0";
				HtmlInputText InputBox = new HtmlInputText("Text");
				LinkBox.Controls.Add(InputBox);
				//LinkBox.Value = "clip" +recID;
					//i386.Newsletter.Config.App.ConfigSettings.ApplicationURL + "/tracking/url?l=" + recID + "&n=" + NewsletterID;
		*/		
			}
		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.DataGrid1.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemCreated);
			this.DataGrid1.ItemCommand +=new DataGridCommandEventHandler(DataGrid1_ItemCommand);
			this.DataGrid1.PreRender +=new EventHandler(DataGrid1_PreRender);
			this.DataGrid1.PageIndexChanged+=new DataGridPageChangedEventHandler(DataGrid1_PageIndexChanged);
			this.DataGrid1.DeleteCommand +=new DataGridCommandEventHandler(DataGrid1_DeleteCommand);
			this.Reset.Click+=new EventHandler(Reset_Click);
			this.Add.Click+=new EventHandler(Add_Click);
			ViewState["SQLStringFilter"]="";
			

		}
		#endregion
		protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			DataGrid1.EditItemIndex = -1;
			DataGrid1.CurrentPageIndex = e.NewPageIndex;
			Bind();
		}

        protected void DataGrid1_PreRender(object sender, EventArgs e)
        {
            try
            {
                string numberOfDisplayedItems = (String)GetGlobalResourceObject("labels", "numberOfDisplayedItems");
                string strCount = ((DataTable)DataGrid1.DataSource).Rows.Count.ToString() + numberOfDisplayedItems;
                ((Label)DataGrid1.Controls[0].Controls[0].FindControl("lblNumRecords")).Text = strCount;
                ((Label)DataGrid1.Controls[0].Controls[DataGrid1.Controls[0].Controls.Count - 1].FindControl("lblNumRecords")).Text = strCount;
            }
            catch
            { }
        }

		protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "ClicksUp":
					ViewState["SQLStringSort"] =" ORDER BY [Clicks]";
					DataGrid1.EditItemIndex = -1;
					Bind();
					break;
				case "ClicksDn":
					ViewState["SQLStringSort"] =" ORDER BY [Clicks] DESC";
					DataGrid1.EditItemIndex = -1;
					Bind();
					break;
				case "UrlUp":
					ViewState["SQLStringSort"] =" ORDER BY [Url]";
					DataGrid1.EditItemIndex = -1;
					Bind();
					break;
				case "UrlDn":
					ViewState["SQLStringSort"] =" ORDER BY [Url] DESC";
					DataGrid1.EditItemIndex = -1;
					Bind();
					break;

					
			}
		}

		private void Add_Click(object sender, System.EventArgs e)
		{
			// Insert into 
			string LinkToTrack = Url.Text.Replace("''","'");
			string NewsletterID =  Request.QueryString["n"];
			string SqlString = "INSERT INTO LINKTRACKING  ([URL], [Created], [CreatedBy], [NID]) VALUES ('" + LinkToTrack + "'," + Utils.GetTimeStamp() + ",'" + User.Identity.Name +"'," +NewsletterID +");";
			if (LinkToTrack!="") 
			{
				Utils.DbCmdExecute(SqlString);
				Bind();
			}
			

		}

		private void Reset_Click(object sender, EventArgs e)
		{
			string NewsletterID = Request.QueryString["n"];
			string SqlString = "UPDATE LINKTRACKING SET CLICKS=0 WHERE NID=" + NewsletterID;
			Utils.DbCmdExecute(SqlString);
			Bind();
		}

		protected void DataGrid1_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
				string RecId = ((TextBox)e.Item.Cells[3].FindControl("RecId")).Text;
					Utils.DbCmdExecute("DELETE * FROM LinkTracking WHERE ID="+ RecId);
					Bind();
		}


	}

