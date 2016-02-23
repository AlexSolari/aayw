using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAYW.Core.Extensions
{
    public static class StringExtensions
    {
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
