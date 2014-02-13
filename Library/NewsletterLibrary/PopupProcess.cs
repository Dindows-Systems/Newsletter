using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;

namespace i386.Newsletter
{

    public class PopUpProcess
    {
        private string _Name;
        private string _PopupMode = "Layer";
        private string _Redirect;
        private Page _Page;
        private bool _DemoMode = false;
        private int _ItemCount = 1;
        private int _ItemCurrentCount = 1;
        private string _StatusMessage;
        public string Redirect
        {
            get { return _Redirect; }
            set { _Redirect = value; }
        }
        public string PopupMode
        {
            get { return _PopupMode; }
            set { _PopupMode = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public bool DemoMode
        {
            get { return _DemoMode; }
            set { _DemoMode = value; }
        }
        public Page Page
        {
            set { _Page = value; }
            get { return _Page; }
        }
        public int ItemCount
        {
            set
            {
                _ItemCount = value;
                lock (Page.Session.Contents.SyncRoot)//lock the session object
                {
                    Page.Session.Contents[Name + "_Total"] = value;
                }
            }
            get { return _ItemCount; }
        }

        public int ItemCurrentCount
        {
            set
            {
                _ItemCurrentCount = value;
                lock (Page.Session.Contents.SyncRoot)//lock the session object
                {
                    Page.Session.Contents[Name + "_Count"] = value;
                }
            }
            get { return _ItemCurrentCount; }
        }
        public string StatusMessage
        {
            set
            {
                lock (Page.Session.Contents.SyncRoot)//lock the session object
                {
                    Page.Session.Contents[Name + "_Status"] = value;
                }
                _StatusMessage = value;
            }
            get { return _StatusMessage; }
        }
        public virtual void ProcessToExecute() { }
        public void Run()
        {
            try
            {
                int startAt = Environment.TickCount;
                StatusMessage = "Initialising...";
                ItemCurrentCount = 0;

                ProcessToExecute();
                #region Sessions Final Values
                lock (Page.Session.Contents.SyncRoot)
                {
                    int ms = Environment.TickCount - startAt;
                    int seconds = ms / 1000;
                    Page.Session.Contents[Name + "_TimeTaken"] = seconds.ToString();
                }
                StatusMessage = "";
                #endregion
            }
            catch (Exception ex)
            {
                #region Session Report with Error
                lock (Page.Session.Contents.SyncRoot)
                {
                    Page.Session.Contents[Name + "_Exception"] = ex;
                }
                #endregion
            }
        }
        public void RunAsThread()
        {
            //Threading... 
            Thread thread = new Thread(new ThreadStart(this.Run));
            thread.Priority = ThreadPriority.Lowest;
            thread.Start();
            Thread.Sleep(500);

            if (PopupMode == "Popup")
            {
                string strScript = @"<script  language=""JavaScript1.2"" type=""text/javascript"">
						var newWin" + this.Name + @" = window.open('processstatus.htm','" + this.Name + @"','Width=600 height=70');
						newWin" + this.Name + @".focus();
							</" + "script>";


                Page.RegisterClientScriptBlock("ProcessPopup_" + Name, strScript);
            }
            else if (PopupMode == "Layer")
            {
                string strScript = @"<script>
if (ns6) window.onload=new Function('loadwindow(""processstatus.htm"",600,70)')
else loadwindow(""processstatus.htm"",600,70)
</script>";
                Page.RegisterClientScriptBlock("ProcessLayer_" + Name, strScript);
            }
            else
            {
                if (Redirect == null) Page.Response.Redirect("processstatus.htm");
                else Page.Response.Redirect(Redirect);

            }

        }
    }
}
