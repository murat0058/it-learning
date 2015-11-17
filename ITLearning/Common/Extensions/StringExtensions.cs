using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool NotNullNorEmpty(this String str)
        {
            return !string.IsNullOrEmpty(str);
        }
    }
}
