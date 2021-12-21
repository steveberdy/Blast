using System;

namespace Blast
{
    public static class BaseTransformer
    {
        public static string IntToString(int value, string chars)
        {
            string result = string.Empty;
            int targetBase = chars.Length;

            do
            {
                result = chars[value % targetBase] + result;
                value /= targetBase;
            }
            while (value > 0);

            return result;
        }

        public static int StringToInt(string value, string chars)
        {
            int result = 0;
            int count = 0;

            while (value.Length > 0)
            {
                result += chars.IndexOf(value[^1..]) * (int)Math.Pow(chars.Length, count);
                value = value[..^1];
                count++;
            }

            return result;
        }
    }
}