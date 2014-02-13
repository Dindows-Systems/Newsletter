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

namespace i386.Newsletter
{
	/// <summary>
	/// Summary description for ViewLog.
	/// </summary>
	public partial class ViewLog : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			string apppath =  "Logging/";
			
			try
			{
				Int64 NewsletterID = Convert.ToInt64(Request.QueryString["n"]);
				string LogFile = Server.MapPath(apppath + NewsletterID.ToString() + ".log");
				if (File.Exists(LogFile))
				{
					StreamReader sr= File.OpenText(LogFile);
					int lines=0;
					while (lines<50)
					{
						this.Label1.Text += sr.ReadLine() + "<br/>";
						sr.Read();
						lines++;
						
					}

					sr.Close();
				}
				else
				{
					this.Label1.Text = "Log is empty.";
				}
			}
			catch //(Exception ErrMsg)
			{
				this.Label1.Text = "Error: Not a valid log file.";
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
}
