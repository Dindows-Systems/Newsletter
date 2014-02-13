using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Controls_ViewNewsletter : System.Web.UI.UserControl
{
    protected int _index;
    public int NewsletterID
    {
        set
        {
            _index = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string iframe = @"<iframe width=""100%"" height=""400px""  marginheight=0 marginwidth=0  src=""../ViewHtml.aspx?index={0}{1}""></iframe>";
        iframe = String.Format(iframe, _index.ToString(), "&tracking=off");
        ltrl.Text = iframe;
    }
}
