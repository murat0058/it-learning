using System;

namespace ITLearning.Shared.Extensions
{
    public static class StringExtensions
    {
        public static bool NotNullNorEmpty(this String str)
        {
            return !string.IsNullOrEmpty(str);
        }
    }
}
