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
	/// Summary description for url.
	/// </summary>
	public partial class url : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			try
			{
				Int64 l = Convert.ToInt64(Request.QueryString["l"]);
				Utils.DbCmdExecute("UPDATE LINKTRACKING SET Clicks=Clicks+1 WHERE ID=" + l);
				Response.Redirect(Request.QueryString["url"]);

			}
			catch
			{
				Response.Write("Error with link!!");
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
		}
		#endregion
	}

