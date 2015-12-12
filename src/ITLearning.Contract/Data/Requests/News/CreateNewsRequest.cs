using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.Data.Requests.News
{
    public class CreateNewsRequest
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string TagsString { get; set; }
    }
}
