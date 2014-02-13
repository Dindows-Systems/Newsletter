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
    /// Summary description for Login.
    /// </summary>
    public partial class Login : BasePage
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (User.Identity.Name.Length > 0)
                Response.Redirect(this.ForwardUrl.Value, true);
        }

        protected void Signin_Click(object sender, System.EventArgs e)
        {
            //CheckBox chkPersistCookie = ((CheckBox)logon.FindControl("chkPersistCookie"));
            if (UserLogin.Login(UserID.Text, Password.Text, false))
            {
                if (Request.QueryString["ReturnUrl"] != null)
                    Response.Redirect(Request.QueryString["ReturnUrl"], true);
                else
                {
                    if (Request.Form["ForwardUrl"] != null)
                        Response.Redirect(Request.Form["ForwardUrl"], true);
                    else
                        Response.Redirect(Request.Url.PathAndQuery, true);
                }
            }
            else
            {
                if (Request.Form["ErrorUrl"] != null)
                    Response.Redirect(Request.Form["ErrorUrl"], true);
                else
                    Requiredfieldvalidator3.Text = resource("errorinvalidlogin");
            }
        }



    }
}
