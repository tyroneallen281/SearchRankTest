using System;

namespace SearchRankUtilities
{
    public static class GeneralHelper
    {
        public static string[] StringSplit(this string value)
        {
            return value.Split(",");
        }
    }
}
