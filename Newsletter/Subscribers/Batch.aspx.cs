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
using System.Xml;
using System.Text;
using System.Threading;

namespace i386.Newsletter
{
	/// <summary>
	/// Summary description for Batch.
	/// </summary>
	public partial class Batch : BasePage
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
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
				
			}
			Progress.Attributes["OnClick"]="javascript:if (confirm('Are you sure you wish to contiune?'));";
			
		}



		protected void Button1_Click(object sender, System.EventArgs e)
		{
			emails.Attributes["OnClick"] ="javascript:document.getElementById('Progress').disabled=true";
			ParserEmailAddress();
		}
		private void ParserEmailAddress()
		{
			int numOfInvalidEmail=0;
			int numOfValidEmail=0;
			// fix up Listbox for valid and invalid emails
			this.ListInvalid.Visible=true;
			this.ListValid.Visible=true;
			ListValid.Items.Clear();
			ListInvalid.Items.Clear();


			Char[] Seperator = new Char[]{Convert.ToChar(this.SepartorChar.SelectedValue)};
			char Sep = '\n';
			string layout = this.Layout.SelectedValue;
			int EmailField = 0;
			int NameField =0;
			if (layout=="1") NameField =1;
			else if (layout=="2") EmailField = 1;
			//else if (layout=="3") Sep = ';';

			string[] Emails = this.emails.Text.Split(new Char[]{Sep});
			foreach (string emailname in Emails)
			{
				string[] NameEmail = emailname.Split(Seperator);
			
				if (NameEmail.Length>1) 
				{
					string EmailAddress = NameEmail[EmailField].Trim();
					string Name = NameEmail[0].Trim();
					if (EmailAddress.Length>0)
					{
						
						if (WebUtils.IsEmail(EmailAddress))
						{
							ListValid.Items.Add(new ListItem(EmailAddress,Name));
							numOfValidEmail++;
						}
						else
						{
							numOfInvalidEmail++;
							ListInvalid.Items.Add(EmailAddress);
						}
					
					}
				}
				
				
			}
			if (numOfValidEmail>0)
			{
				this.Progress.Visible=true;
				this.Progress.Enabled=true;
			}
			Label_Invalid.Text = numOfInvalidEmail.ToString();
			Label_Valid.Text = numOfValidEmail.ToString();
		}


		protected void AddEmails_Click(object sender, System.EventArgs e)
		{
			// Arraylist of Newsletter Lists
			ArrayList selectLists = new ArrayList();
			foreach (ListItem li in this.Listbox1.Items) if (li.Selected) selectLists.Add(li.Value);

			// Arraylist of Subscribers
			ArrayList subscribersList = new ArrayList();
			foreach (ListItem li in this.ListValid.Items) subscribersList.Add(li.Text+ ";" +li.Value);

			#region XML BulkProcess
			/*
			// BulkProcess.XML file
			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);
			XmlTextWriter writer = new XmlTextWriter(sw);
			writer.Formatting = Formatting.Indented;
			writer.WriteStartElement(this.Action.SelectedValue);
			foreach(ListItem li in ListValid.Items)
			{
				writer.WriteStartElement("Subscriber");
				// Email element
				writer.WriteStartElement("Email");
				writer.WriteString(li.Text);
				writer.WriteEndElement();
				// Name element
				writer.WriteStartElement("Name");
				writer.WriteString(li.Value);
				writer.WriteEndElement();
				// List element
				foreach (string list in selectLists)
				{
					writer.WriteStartElement("List");
					writer.WriteString(list);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
			writer.Flush();				
			// Write XML to the file
			string strPath = Server.MapPath("bulkprocess.xml");
			File.Exists(strPath);
			StreamWriter swFile = File.CreateText(strPath);
			swFile.Write(sb.ToString());
			swFile.Close();
			*/
		#endregion
			Progress.Enabled=false;


			i386.Newsletter.BatchRemoveProcess bulkProcess = new i386.Newsletter.BatchRemoveProcess();
			bulkProcess.DemoMode = false;
			bulkProcess.ProcessMode = this.Action.SelectedValue;
			bulkProcess.Name = "bulk";
			bulkProcess.PopupMode = "Popup";

			bulkProcess.Subscribers= subscribersList;
			bulkProcess.Lists = selectLists;
			bulkProcess.Page = Page;
			bulkProcess.RunAsThread();


			
		}

		

	}
}
