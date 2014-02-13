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
using System.Text;

namespace i386.UI
{
	/// <summary>
	/// Summary description for getstatus.
	/// </summary>
	public partial class ProcessStatus : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			int maxWidth = 490;
			string ProcessName =Request.QueryString["process"];
			
			//first check to see if any exceptions occurred
			//if an exception occurred, just return, the status page will display it
			if( Session[ ProcessName + "_Exception" ] != null ) return;

			if(Session[ProcessName +"_Status"] != null )
			{
				Response.Write("<META HTTP-EQUIV=Refresh CONTENT=\"2; URL=\"\"> ");			
				//Get statistics from Session variables			
				int TotalToProcess = Int32.Parse( Session[ ProcessName + "_Total" ].ToString() );
				int TotalCount = Int32.Parse( Session[ ProcessName + "_Count" ].ToString() );
				string statusLine = Session[ ProcessName + "_Status" ].ToString();
				// Work out the percent completed so far
				double percentComplete = 0d;
				if( TotalCount > 0 ) percentComplete  =Convert.ToDouble( TotalCount )/Convert.ToDouble( TotalToProcess );
				string percentMessage = string.Format( "{2}% complete {0} of {1} items.", TotalCount, TotalToProcess,  Math.Round(percentComplete*100,0)  );
				//get the width of the status bar
				int statusWidth = Convert.ToInt32( maxWidth * percentComplete );				
				Response.Write( GetClientJavaScript( statusLine, statusWidth, percentMessage ) );
			}
			else
			{
                string TimeTaken = "Process Complete!";
                if (Session[ProcessName + "_TimeTaken" ] != null)
				    TimeTaken = Session[ProcessName + "_TimeTaken" ].ToString() + " seconds to complete entire process.";
				Response.Write( GetClientJavaScript(TimeTaken, maxWidth, "100%") );
			}
		}

		private string GetClientJavaScript( string statusLine, int statusWidth, string percentComplete )
		{
			StringBuilder sb = new StringBuilder();
			sb.Append( "<script language=\"javascript\">\r\n" );
			sb.Append( "var statusLine = \"" + statusLine + "\";\r\n" );
			sb.Append( "var percentComplete = \"" + percentComplete + "\";\r\n" );
			sb.Append( "var statusWidth = " + statusWidth.ToString() + ";\r\n" );
			sb.Append( "parent.statusWindow.Update( statusLine, statusWidth, percentComplete);\r\n" );
			//sb.Append( "parent.opener.location.href = 'GetLog.aspx';\r\n" );
		//	if (percentComplete=="100%")
		//	sb.Append( "parent.opener.document.location.reload();setTimeout('parent.close()',2000);\r\n");

			sb.Append( "</script>" );
			return sb.ToString();
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
