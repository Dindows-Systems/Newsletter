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
using i386.Newsletter;

public partial class MainMenu : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (UserLogin.GetCurrentUserRole() == "Admin")
        {
            plAdmin.Visible = true;
        }
        else
            plAdmin.Visible = false;
    }

    private void Logout_Click(object sender, ImageClickEventArgs e)
    {
        UserLogin.Logout();
    }
}
