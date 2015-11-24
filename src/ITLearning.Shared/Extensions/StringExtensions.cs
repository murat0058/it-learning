using System;
using System.Text;

namespace ITLearning.Shared.Extensions
{
    public static class StringExtensions
    {
        public static bool NotNullNorEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static string ToBase64(this string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }
    }
}
