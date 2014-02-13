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

public partial class Admin_Users_Add : BasePage
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // Put user code to initialize the page here
        if (UserLogin.GetCurrentUserRole() != "Admin") Page.Visible = false;

    }


    protected void BtnAdd_Click(object sender, System.EventArgs e)
    {
        if (WebUtils.IsEmail(Email.Text))
        {
            this.Label_InvalidEmail.Visible = false;
            long uid = UserLogin.Create(UserName.Text, Email.Text);
            if (uid == -1)
            {
                Label_Error.Visible = true;
            }
            else
            {
                // Redirect to the Edit Page for the new Article.
                Response.Redirect("Edit.aspx?index=" + uid.ToString());
            }
        }
        else
        {
            Label_Error.Visible = false;
            this.Label_InvalidEmail.Visible = true;
        }
    }
}
