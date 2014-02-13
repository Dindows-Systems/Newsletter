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
	/// Summary description for Config.
	/// </summary>


	public partial class  Config : BasePage
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (UserLogin.GetCurrentUserRole()=="Admin")
			{
				// Put user code to initialize the page here
				if (!IsPostBack)
				{
					CompanyName.Text = App.ConfigSettings.CompanyName;
					this.UnsubscribeSubject.Text = App.ConfigSettings.UnsubscribeSubject;
					this.UnsubscribeBody.Text = App.ConfigSettings.UnsubscribeBody;
					this.SubscribeSubject.Text = App.ConfigSettings.SubscribeSubject;
					this.SubscribeBody.Text = App.ConfigSettings.SubscribeBody;
					this.SMTPServer.Text = App.ConfigSettings.SMTPServer;
					this.ApplicationURL.Text= App.ConfigSettings.ApplicationURL;
				}
			}
			else
			{
				Page.Visible = false;
			}
		}


        protected void Save_Click1(object sender, ImageClickEventArgs e)
        {
            App.ConfigSettings.CompanyName = CompanyName.Text;
            App.ConfigSettings.UnsubscribeSubject = this.UnsubscribeSubject.Text;
            App.ConfigSettings.UnsubscribeBody = this.UnsubscribeBody.Text;
            App.ConfigSettings.SubscribeSubject = this.SubscribeSubject.Text;
            App.ConfigSettings.SubscribeBody = this.SubscribeBody.Text;
            App.ConfigSettings.SMTPServer = this.SMTPServer.Text;
            App.ConfigSettings.ApplicationURL = this.ApplicationURL.Text;
            App.ConfigSettings.WriteKeysToConfig();
        }
}

	
}
