using ITLearning.Frontend.Web.Contract.Enums;
using ITLearning.Frontend.Web.ViewModels.News;
using ITLearning.Frontend.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public UserBasicDataViewModel UserData { get; set; }
        public NewsThumbnailViewModel MainNews { get; set; }
        public IEnumerable<NewsThumbnailViewModel> SmallNews { get; set; }
    }
}
