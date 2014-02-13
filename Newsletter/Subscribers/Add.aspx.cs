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

public partial class Subscribers_Add : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (WebUtils.IsEmail(this.Email.Text))
        {
            SubscriberMember subscriber = new SubscriberMember();
            DataRow dr = subscriber.GetRecord(this.Email.Text, false);
            if (dr == null)
            {
                subscriber.Email = this.Email.Text;
                subscriber.Create();
                Response.Redirect("Edit.aspx?index=" + subscriber.RecordID.ToString());
            }
            else
            {
                Label_Error.Visible = true;
            }

        }
        else
        {
            Label_Error.Visible = false;

        }
    }
}
