using System;
using System.Text;
using System.Text.RegularExpressions;

namespace i386
{
    /// <summary>
    /// Summary description for TextUtils
    /// </summary>
    public class TextUtils
    {
        public TextUtils()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// Regular Expression Replacer
        /// </summary>
        /// <param name="ReplaceString">Replace the match with this string value.</param>
        /// <param name="Pat">Matching Pattern</param>
        /// <param name="TextToParse">string to perform the replacement on.</param>
        /// <param name="ReturnAllCompleteMatches">This needs to be true</param>
        /// <returns></returns>
        public static string ReplaceRegExp(string ReplaceString, string Pat, string TextToParse, bool ReturnAllCompleteMatches)
        {
            Regex RegularExpObj = new Regex(Pat, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection MatchList = RegularExpObj.Matches(TextToParse);
            if (MatchList.Count > 0)
            {
                if (ReturnAllCompleteMatches)
                {

                    for (int Occurance = 0; Occurance < MatchList.Count; Occurance++)
                    {
                        Match FirstOccuranceOfPattern = MatchList[Occurance];
                        TextToParse = TextToParse.Replace(FirstOccuranceOfPattern.ToString(), ReplaceString);
                    }
                }
                else
                {
                    Match FirstOccuranceOfPattern = MatchList[0];
                    TextToParse = TextToParse.Replace(FirstOccuranceOfPattern.ToString(), ReplaceString);

                }


            }
            return TextToParse;
        }
        /// <summary>
        /// Removes All Pattern which match and returns the first match on the pattern.
        /// </summary>
        /// <param name="Pat"></param>
        /// <param name="TextToParse"></param>
        /// <returns></returns>
        public static string RemoveRegExp(string Pat, string TextToParse)
        {
            Regex RegularExpObj = new Regex(Pat, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection MatchList = RegularExpObj.Matches(TextToParse);
            string FirstMatch = "";
            if (MatchList.Count > 0)
            {
                for (int Occurance = 0; Occurance < MatchList.Count; Occurance++)
                {

                    Match FirstOccuranceOfPattern = MatchList[Occurance];
                    if (FirstMatch == "") FirstMatch = FirstOccuranceOfPattern.ToString();
                    TextToParse = TextToParse.Replace(FirstOccuranceOfPattern.ToString(), "");
                }

            }
            return FirstMatch;
        }

        public static int StandardAscii(string s)
        {
            return (int)Encoding.ASCII.GetBytes(s)[0];
        }

        public static string ConvertCodes(string str)
        {
            string temp = "";
            for (int i = 0; i < str.Length; i++)
            {
                string letter = str.Substring(i, 1);
                int ascLetter = StandardAscii(letter);
                if (ascLetter == 63)
                {
                    char lc = Convert.ToChar(letter);
                    temp = temp + "&#" + Convert.ToInt32(lc) + ";";
                }
                else
                    temp = temp + letter;
            }
            return temp;
        }
    }
}