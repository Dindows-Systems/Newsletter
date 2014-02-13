using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.IO;
using i386.Newsletter;

public partial class Newsletters_Send : BasePage
{
    protected string _defaultStatus = "Processing {0} of {1} Newsletters... {2}% Complete";
  
    protected void Page_Load(object sender, System.EventArgs e)
    {
        ViewNewsletter1.NewsletterID = int.Parse(Request.QueryString["index"].ToString());
    }
    protected void ProgressBar1_RunTask(object sender, EO.Web.ProgressTaskEventArgs e)
    {
        
        Mailer mailer = new Mailer();
        Newsletter newsletter = new Newsletter();
        newsletter.GetRecord(Request.QueryString["index"].ToString());
        mailer.Newsletter = newsletter;
        mailer.ProgressTaskEventArgs = e;
        mailer.Send();
        lblErrorMsg.Text = mailer.GetLastErrorMessages();
    }
    /// <summary>
    /// Send the test mail
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkSendTest_Click(object sender, EventArgs e)
    {
   
        Mailer mailer = new Mailer();
        Newsletter newsletter = new Newsletter();
        newsletter.GetRecord(Request.QueryString["index"].ToString());
        mailer.Send(newsletter, newsletter.From);
        lblErrorMsg.Text = mailer.GetLastErrorMessages();
    }
 }
