using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;

namespace i386.Newsletter
{
    /// <summary>
    /// Summary description for Localisation
    /// </summary>
    public class BasePage  : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = "Normal";
        }
        /// <summary>
        /// Return a string for a Javascript alert question for an action in current spoken language
        /// </summary>
        /// <param name="item">resource name, such as 'theUser', 'theNewsletter' or 'theMailingList'</param>
        /// <param name="action">resource name, such as 'delete'</param>
        /// <returns></returns>
        protected string javascriptAreYouSure(string item, string action)
        {
            //Javascript for confirmation of deletion.
            String areYouSure =(String)GetGlobalResourceObject("labels", "areYouSure");
            String _action = (String)GetGlobalResourceObject("labels", action);
            String _item = (String)GetGlobalResourceObject("labels", item);
            areYouSure = String.Format(areYouSure, _action, _item);
            return "javascript:return confirm('" + areYouSure + "');"; 
        }
        /// <summary>
        /// Return a resource string for localization purposes
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string resource(string item)
        {
            return (String)GetGlobalResourceObject("labels", item);
        }
    }
}