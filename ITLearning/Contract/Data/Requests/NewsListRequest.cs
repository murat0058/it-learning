using ITLearning.Frontend.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Contract.Data.Requests
{
    public class NewsListRequest
    {
        public IEnumerable<News> News { get; set; }
        public IEnumerable<string> Authors { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public NewsFilterRequest FilterRequest { get; set; }

        public NewsListRequest()
        {
            this.FilterRequest = new NewsFilterRequest();
        }
    }
}
