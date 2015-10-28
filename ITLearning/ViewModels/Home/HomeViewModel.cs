using ITLearning.Frontend.Web.ViewModels.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public NewsThumbnailViewModel MainNews { get; set; }
        public IEnumerable<NewsThumbnailViewModel> SmallNews { get; set; }
    }
}
