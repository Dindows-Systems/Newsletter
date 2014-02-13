using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace i386.Newsletter
{
	/// <summary>
	/// Summary description for tracking.
	/// </summary>
	public partial class tracking : System.Web.UI.Page
	{


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
			this.Init +=new EventHandler(tracking_Init);
		}
		#endregion

		private void tracking_Init(object sender, EventArgs e)
		{
			// Put user code to initialize the page here
			string ListID = string.Empty;
			if (Request.QueryString["n"]!=null)
			{
				string nID=Request.QueryString["n"];
				Int64 nViewCount=0;
				//Get View Count
				string SqlGetCount = "SELECT VIEWS FROM NEWSLETTERS WHERE ID=" + nID + ";";
				
				object ViewCount = Utils.DbCmdExecuteScalar(SqlGetCount);
				if (ViewCount!=null && ViewCount!=DBNull.Value)
					nViewCount = Convert.ToInt64(ViewCount)+1;

				else
					nViewCount=1;
				

				// UpdateViewCount
				string SqlNewCount = "UPDATE NEWSLETTERS SET VIEWS=" + nViewCount + " WHERE ID=" + nID + ";";
				Utils.DbCmdExecute(SqlNewCount);

				Response.ContentType = "images/gif";
				string _path = Server.MapPath("../../images/tracking.gif");
				Bitmap trackingBitmap = new Bitmap(_path);
				trackingBitmap.Save (Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif); 
				trackingBitmap.Dispose();

			}
		}
	}
}
