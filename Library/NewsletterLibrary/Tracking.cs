// i386 Newsletter System
// Copyright (c) 2008 i386 - http://www.i386.com
//
// LIMITATION OF LIABILITY
// The software is supplied "as is". Author cannot be held liable to you
// for any direct or indirect damage, or for any loss of income, loss of
// profits, operating losses or any costs incurred whatsoever. The software
// has been designed with care, but the Author does not guarantee that it is
// free of errors.

using System;
using System.Data;
using System.Configuration;
using System.Web;

namespace i386.Newsletter
{
    public class Tracking
    {
        public static string Footer(string NewsletterID, string ListID, string Email, string BodyType, bool Tracking)
        {

            string AppUrl = App.ConfigSettings.ApplicationURL;
            string Unsubscribe = AppUrl + "/subscription.aspx?email=" + Email + "&ListID=" + ListID;
            string ViewHtml = AppUrl + "/viewhtml.aspx?index=" + NewsletterID;
            string NewsletterTracking = "";

            if (Tracking)
                NewsletterTracking = @"<a href=""" + AppUrl + @"""><img alt=""i386.Newsletter"" src=""" + AppUrl + @"/tracking?n=" + NewsletterID + @""" width=""100"" height=""30"" border=""0"" align=""right""></a>";
            else
                NewsletterTracking = @"<a href=""" + AppUrl + @"""><img alt=""i386.Newsletter"" src=""" + AppUrl + @"/../images/tracking.gif"" width=""100"" height=""30"" border=""0"" align=""right""></a>";

            if (BodyType == "Text")
            {
                /// 
                /// i386.Newsletter System - Powered by i386.com must remain - do not alter.
                ///
                string TextFooter = "\n\n\n i386.Newsletter System - Powered by i386.com \n Unsubscribe:" + Unsubscribe + "\n View Online:" + ViewHtml;
                return TextFooter;
            }
            else
            {

                Unsubscribe = @"<a href=""" + Unsubscribe + @""">Unsubscribe</a> | ";
                string ViewProblem = @"<a href=""" + ViewHtml + @""">View Online</a>";
                string HTMLFooter = @"<div align=""center"">" + Unsubscribe + ViewProblem + NewsletterTracking + "</div>";
                return HTMLFooter;
            }

        }
    }
}