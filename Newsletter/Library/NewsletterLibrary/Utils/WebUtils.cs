using System;
using System.Net;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;


namespace i386
{
    public class WebUtils
    {

        public string GetStringWithinTags(string Tag, string TextToParse, bool Attributes, bool ReturnAllCompleteMatches)

            /* Tag, eg. BODY which would match boDY or BODY everything between <BODY> and </BODY>
                 TextToParse this is the input string
                Attributes if set to true will return the string inside the BODY tag 
                        i.e <BODY background="white"> will return background="white"
                ReturnAllCompleteMatches this pends each match found to the return string.
            */
        {
            string Pat = @"(\<" + Tag + "( .*?)>|<" + Tag + ">)(.*?)</" + Tag + ">";
            string StringWithinTags = "";
            Regex RegularExpObj = new Regex(Pat, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection MatchList = RegularExpObj.Matches(TextToParse);
            if (MatchList.Count > 0)
            {
                if (ReturnAllCompleteMatches)
                {
                    StringWithinTags = "<!--i386Parser " + Tag + " Start-->";
                    for (int Occurance = 0; Occurance < MatchList.Count; Occurance++)
                    {
                        Match FirstOccuranceOfPattern = MatchList[Occurance];
                        if (Attributes)
                        {
                            StringWithinTags = FirstOccuranceOfPattern.Groups[2].ToString();//What's within the Tag
                        }
                        else
                        {
                            StringWithinTags = StringWithinTags + FirstOccuranceOfPattern.ToString();
                        }
                    }
                    StringWithinTags = StringWithinTags + "<!--i386Parser " + Tag + " End-->";
                }
                else
                {
                    Match FirstOccuranceOfPattern = MatchList[0];
                    if (Attributes)
                    {
                        StringWithinTags = FirstOccuranceOfPattern.Groups[2].ToString();//What's within the Tag
                    }
                    else
                    {
                        StringWithinTags = FirstOccuranceOfPattern.Groups[3].ToString();
                    }
                }

            }
            return StringWithinTags;
        }

        public static string GrabUrl(string Url)
        {
            try
            {
                WebRequest mywebReq;
                WebResponse mywebResp;
                StreamReader sr;
                mywebReq = WebRequest.Create(Url);
                mywebResp = mywebReq.GetResponse();
                sr = new StreamReader(mywebResp.GetResponseStream());
                string strHTML = sr.ReadToEnd();
                sr.Close();
                return strHTML;
            }
            catch (Exception ErrMsg)
            {
                return ErrMsg.Message;
            }
        }

        public static bool IsEmail(string inputEmail)
        {
            if (inputEmail != "" && inputEmail != null)
            {
                string strRegex = @"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$";
                Regex re = new Regex(strRegex);
                if (re.IsMatch(inputEmail))
                    return (true);
                else
                    return (false);
            }
            else
                return (false);
        }
    }

}
