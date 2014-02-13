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


namespace i386.Newsletter
{
	/// <summary>
	/// Summary description for Subscribe.
	/// </summary>
	public partial class Subscribe : BasePage
	{
		protected System.Web.UI.WebControls.TextBox Textbox1;


		protected void Page_Load(object sender, System.EventArgs e)
		{
			
			this.TableFriends.Visible=false;
			this.SubscriptionMessages.Visible=false;
			// Put user code to initialize the page here
			string qEmail = string.Empty;
			string qName = string.Empty;
			string qFormat =string.Empty;
			string qListID = string.Empty;

			if (Request.QueryString["ListID"]!=null)
			{
				// Newsletter Lists - should add the support multiple list. 
				string ListID = Request.QueryString["ListID"];				
				ArrayList Lists = new ArrayList();
				Lists.Add(ListID);
				if (!Page.IsPostBack)
				{
					if (Request.QueryString["Format"]!=null) qFormat =Request.QueryString["Format"];	
					if (Request.QueryString["Name"]!=null) 	
					{
						qName = Request.QueryString["Name"];
						Name.Text=qName;
					}
					if (Request.QueryString["Email"]!=null)
					{
						qEmail = Request.QueryString["Email"];
						Email.Text = qEmail;
					}
				}
				if (Request.QueryString["RedirectUrl"]==null)
				{
					
					if (!Page.IsPostBack)
					{
						// Get Newsletter ListName
						
						string SqlListName = "SELECT ListName FROM LISTS WHERE id=" + ListID + ";";
						object oListName = Utils.DbCmdExecuteScalar(SqlListName);
						if (oListName!=null) this.Label_ListName.Text = Convert.ToString(oListName);

						// Web Page Title
						
						//Title=Title.Replace(" | ", " | " + this.Label_ListName.Text + " ");
					
		
						
						//  Get Subscriber from the database to fill in the form boxes
						string SqlString ="SELECT * FROM SUBSCRIBERS WHERE EMAIL='" + qEmail + "';";
						DataTable dt = Utils.GetDataTable(SqlString);
						if (dt.Rows.Count>0)
						{
							if (qName==string.Empty) qName = dt.Rows[0]["Name"].ToString();
							if (qFormat==string.Empty) qFormat = dt.Rows[0]["Format"].ToString();
						}
						if (FormatList.Items.FindByValue(qFormat)!=null) FormatList.Items.FindByValue(qFormat).Selected =true;
					}
					
				}
				else
				{ // by GET Request from external form with RedirectURL
					if (WebUtils.IsEmail(qEmail))
					{
						if (qEmail.Length<100 && qName.Length<100)
						{
							if (qName==string.Empty)  qName = qEmail;					
							if (Request.QueryString["Subscribe"]==null)
							{ // Add to List
								if (qFormat==null)
                                    SubscriberMember.InsertIntoList(qEmail, qName, Lists);
								else
                                    SubscriberMember.InsertIntoList(qEmail, qName, qFormat, Lists);
							}
							else // Remove from List
                                SubscriberMember.RemoveFromList(qEmail, qName, Lists);
							// Redirect to URL
							Response.Redirect(Request.QueryString["RedirectUrl"]);					
						}
						else
							Label_Error.Text = "Error:Length of Email Address or Name is too long!";
					}

				}
			}
			else
			{
				Label_Error.Text = "Error:Please provide Newsletter List ID";
				this.Send.Enabled = false;
			}
		}


		protected void Send_Click(object sender, System.EventArgs e)
		{
			string ListID = Request.QueryString["ListID"];
			ArrayList Lists = new ArrayList();
			Lists.Add(ListID);

			/*
			if (Utils.IsEmail(this.Email1.Text))
				i386.Newsletter.Subscribers.InsertIntoList(Email1.Text,Name1.Text,"HTML",Lists);
			if (Utils.IsEmail(this.Email2.Text))
				i386.Newsletter.Subscribers.InsertIntoList(Email2.Text,Name1.Text,"HTML",Lists);
			if (Utils.IsEmail(this.Email3.Text))
				i386.Newsletter.Subscribers.InsertIntoList(Email3.Text,Name1.Text,"HTML",Lists);
				*/
			this.SubscriptionForm.Visible=false;
			this.SubscriptionMessages.Visible=true;
			if (Unsubscribe.Checked)
			{
                SubscriberMember.RemoveFromList(Email.Text, Name.Text, Lists);
				this.Label_Unsubscribe.Visible=true;
				Label_Unsubscribe.Text = Label_Unsubscribe.Text.Replace("$NEWSLETTER$",this.Label_ListName.Text);
			}
			else
			{
				if (FormatList.SelectedValue==null)
                    SubscriberMember.InsertIntoList(Email.Text, Name.Text, Lists);
				else
                    SubscriberMember.InsertIntoList(Email.Text, Name.Text, FormatList.SelectedValue, Lists);
				this.Label_Subscribe.Visible=true;
				Label_Subscribe.Text = Label_Subscribe.Text.Replace("$NEWSLETTER$",this.Label_ListName.Text);
				
			}
		}

		protected void Submit_Click(object sender, System.EventArgs e)
		{
			this.Send_Click(null,null);
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			if (Request.QueryString["Lang"]!=null) Session["i386.Newsletter.Language"]=Request.QueryString["Lang"];

		}
	}
}
