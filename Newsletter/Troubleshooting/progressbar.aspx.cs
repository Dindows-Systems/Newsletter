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

public partial class Troubleshooting_progressbar : System.Web.UI.Page
{
    private void Page_Load(object sender, System.EventArgs e)
    {
        // Put user code to initialize the page here
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
    ///		Required method for Designer support - do not modify
    ///		the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.ProgressBar1.RunTask += new EO.Web.ProgressTaskEventHandler(this.ProgressBar1_RunTask);
        this.Load += new System.EventHandler(this.Page_Load);

    }
    #endregion


    private void ProgressBar1_RunTask(object sender, EO.Web.ProgressTaskEventArgs e)
    {
        //You can not change anything else in the page inside
        //RunTask handler except for calling UpdateProgress.
        //If you do need to change other contents on the page
        //based on progress information, you can make use of
        //the second parameter of UpdateProgress.

        //The following code demonstrates how to pass addtional
        //data to the client side with UpdateProgress.
        //Here we call UpdateProgress with an additonal parameter.
        //This addtional parameter is passed to the client side
        //and can be accessed by getExtraData client side function.
        //The sample code handles ClientSideOnValueChanged on the
        //client side and checks the return value of getExtraData.
        //The handler then displays this extra value to the user 
        //when it sees it.
        e.UpdateProgress(0, "Running...");

        for (int i = 0; i < 100; i++)
        {
            //Stop the task as soon as we can if the user has
            //stopped it from the client side
            if (e.IsStopped)
                break;

            //Here we call Thread.Sleep just for demonstration
            //purpose. You should replace this with code that
            //performs your long running task. 
            System.Threading.Thread.Sleep(500);

            //You should frequently call UpdateProgress in order
            //to notify the client about the current progress
            e.UpdateProgress(i);
        }

        e.UpdateProgress(100, "The task is done!");
    }
}
