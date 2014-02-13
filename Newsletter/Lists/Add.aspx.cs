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

using i386;
using i386.Newsletter;

public partial class Lists_Add : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Lists list = new Lists();
        list.ListName = ListName.Text.Trim();
        list.Create();
        Response.Redirect("Edit.aspx?index=" + list.RecordID.ToString());
    }
}
