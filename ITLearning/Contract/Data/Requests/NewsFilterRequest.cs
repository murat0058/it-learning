using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Contract.Data.Requests
{
    public class NewsFilterRequest
    {
        public string Query { get; set; }
        public string Author { get; set; }
        public string Tag { get; set; }
        public int Page { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
