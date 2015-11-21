using System;
using System.Collections.Generic;

namespace ITLearning.Contract.Data.Requests
{
    public class NewsFilterRequest
    {
        public string Query { get; set; }
        public IEnumerable<string> Authors { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
