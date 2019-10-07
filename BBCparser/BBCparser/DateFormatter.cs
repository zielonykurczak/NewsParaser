using System;
using System.Text.RegularExpressions;

namespace BBCparser
{
    class DateFormatter
    {
        public static DateTime ChangePubdateFormat(string pubDate)
        {
            var regex = ",";
            var stringDate = Regex.Split(pubDate,regex);
            DateTime formatteDateTime = DateTime.Parse(stringDate[1]);

            return formatteDateTime;
        }
    }
}