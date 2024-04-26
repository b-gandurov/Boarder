using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Boarder.Tests.Helpers
{
    public static class AdjustTestDataHelper
    {
        public static string RemoveTimestamp(string input)
        {
            string pattern = @"\[\d{8}\|\d{2}:\d{2}:\d{2}\.\d{4}\]";
            return Regex.Replace(input, pattern, "");
        }
    }
}
