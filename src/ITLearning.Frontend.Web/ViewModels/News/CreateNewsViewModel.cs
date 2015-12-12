using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.ViewModels.News
{
    public class CreateNewsViewModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string TagsString { get; set; }

        public IFormFile Image { get; set; }
    }
}
