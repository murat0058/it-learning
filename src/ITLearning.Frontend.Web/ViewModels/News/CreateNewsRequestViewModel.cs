using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace ITLearning.Frontend.Web.ViewModels.News
{
    public class CreateNewsRequestViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string TagsString { get; set; }
        public IFormFile Image { get; set; }
    }
}
