using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.HTTP.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string str)
        {
            var result = str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length - 1).ToLower();
            return result;
        }
    }
}
