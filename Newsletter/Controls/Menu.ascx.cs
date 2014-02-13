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
	/// Summary description for Menu.
	/// </summary>#

	public partial class Menu : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Admin Only
			if (UserLogin.GetCurrentUserRole()=="Admin") 
			{
			
			}
		}
        protected void Logout_Click(object sender, ImageClickEventArgs e)
		{
			UserLogin.Logout();
		}

    }
}
