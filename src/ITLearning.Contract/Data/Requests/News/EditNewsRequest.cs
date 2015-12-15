using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.Data.Requests.News
{
    public class EditNewsRequest
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string TagsString { get; set; }
        public IFormFile Image { get; set; }
    }
}
