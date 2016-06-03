using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System
{
    public static class StringExtensions
    {
        static Regex htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        public static string StripTags(this string source)
        {
            return htmlRegex.Replace(source, string.Empty);
        }
        
        public static string FormatWith(this string value, params object[] args)
        {
            return String.Format(value, args);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return String.IsNullOrWhiteSpace(value);
        }
    }
}
