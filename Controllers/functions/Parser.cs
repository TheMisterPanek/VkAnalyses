using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VK_Analyze.Controllers.functions
{
    public class Parser
    {
        public string Text { get; set; }

        public Parser(string text)
        {
            Text = text;
        }

        public string ParseToken()
        {
            string pattern = @".*access_token=([a-z0-9]+)\&.*";
            string result = System.Text.RegularExpressions.Regex.Match(Text, pattern).Groups[1].Value;
            if (string.IsNullOrEmpty(result))
            {
                return Text;
            }
            else
            {
                return result;
            }
        }
    }
}