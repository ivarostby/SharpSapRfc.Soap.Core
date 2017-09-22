﻿using System;
using System.Globalization;

namespace SharpSapRfc
{
    public class AbapDateTime
    {
        private static CultureInfo enUS = new CultureInfo("en-US");

        public static DateTime? FromString(string value)
        {
            return FromString(value, false);
        }

        public static DateTime? FromString(string value, bool acceptNull)
        {
            DateTime date;

            // ABAP Date and Time initial value
            if (value == "00000000" || value == "000000" ||
                value == "00:00:00" || value == "0000-00-00")
            {
                if (acceptNull)
                    return null;
                return DateTime.MinValue;
            } 

            if (DateTime.TryParseExact(value, new string[] { "yyyy-MM-dd", "yyyyMMdd" }, enUS, DateTimeStyles.AssumeLocal, out date))
                return date;

            if (DateTime.TryParseExact(value, new string[] { "HH:mm:ss", "HHmmss" }, enUS, DateTimeStyles.AssumeLocal, out date))
                return DateTime.MinValue.Add(new TimeSpan(date.Hour, date.Minute, date.Second));

            string message = string.Format("{0} is not a valid Date format.", value);
            throw new RfcMappingException(message);
        }

        public static string ToDateString(DateTime value)
        {
            return value.ToString("yyyyMMdd");
        }

        public static string ToTimeString(DateTime value)
        {
            return value.ToString("HHmmss");
        }
    }
}
