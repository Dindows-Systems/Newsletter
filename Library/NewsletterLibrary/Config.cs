using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace i386.Newsletter
{
    /// <summary>
    /// Summary description for Config
    /// </summary>
    public class Config : BaseConfig
    {
        public string UnsubscribeSubject;
        public string SubscribeSubject;
        public string SubscribeBody;
        public string UnsubscribeBody;
        public string SMTPServer = "127.0.0.1";
        public string CompanyName;
    }
}