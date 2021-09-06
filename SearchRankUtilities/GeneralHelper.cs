using System;

namespace SearchRankUtilities
{
    public static class GeneralHelper
    {
        public static string[] StringSplit(this string value)
        {
            return value.Split(",");
        }

        public static string CleanUrl(this string value)
        {
            try
            {
                Uri urlDomain = new Uri(value.Trim().ValidUrl());
                return urlDomain.Host.ToLower();

            }
            catch (Exception ex)
            {
                return "";
            }
           
        }

        public static string ValidUrl(this string value)
        {
            if (value.Contains("http")){
                return value;
            }
            return "http://" + value;

        }
    }
}
