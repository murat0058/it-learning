using ITLearning.Frontend.Web.Contract.Data.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.ViewModels.News
{
    public class NewsListViewModel
    {
        public IEnumerable<NewsThumbnailViewModel> News { get; set; }
        public IEnumerable<string> Authors { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public NewsFilterRequest FilterRequest { get; set; }
    }
}
