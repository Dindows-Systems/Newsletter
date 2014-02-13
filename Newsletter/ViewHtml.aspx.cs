//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_ViewHtml_aspx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'ViewHtml.aspx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
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
namespace i386.Newsletter
{
	/// <summary>
	/// Summary description for ViewHtml.
	/// </summary>
	public partial class ViewHtml :Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			string index = Request.QueryString["index"];
			string version = Request.QueryString["version"];
			string tracking = Request.QueryString["tracking"];
			bool bTracking = true;
			string SqlString = "SELECT Newsletters.*, NewsletterLists.lid FROM Newsletters " +
				"INNER JOIN NewsletterLists ON Newsletters.Id = NewsletterLists.nid WHERE Newsletters.Id=" + index + ";";
			DataTable dt = Utils.GetDataTable(SqlString);	
			if (dt.Rows.Count>0)
			{
			string ListID=dt.Rows[0]["lid"].ToString();
			string URL = dt.Rows[0]["url"].ToString();
			string URLText = dt.Rows[0]["urltext"].ToString();
			if (tracking =="off") bTracking = false;
				if (version=="Text")
				{
					Response.Write("<pre>");
					if (URLText!="")
						Response.Write(WebUtils.GrabUrl(URLText));
					else					
						Response.Write(dt.Rows[0]["text"].ToString());
                	Response.Write(Tracking.Footer(index,ListID,null,"Text",bTracking ));	
					Response.Write("</pre>");
				}
				else
				{
					if (URL!="")
                        Response.Write(WebUtils.GrabUrl(URL));
					else
						Response.Write(dt.Rows[0]["html"].ToString());
					Response.Write(Tracking.Footer(index,ListID,null,"HTML",bTracking ));	
				}
			}

		}


	}
}
