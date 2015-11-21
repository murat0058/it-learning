using System.Collections.Generic;

namespace ITLearning.Contract.Data.Requests
{
    public class NewsListRequest
    {
        public IEnumerable<string> Authors { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public NewsFilterRequest FilterRequest { get; set; }

        public NewsListRequest()
        {
            this.FilterRequest = new NewsFilterRequest();
        }
    }
}
