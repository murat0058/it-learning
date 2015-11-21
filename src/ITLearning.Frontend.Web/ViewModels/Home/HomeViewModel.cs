using ITLearning.Frontend.Web.ViewModels.News;
using ITLearning.Frontend.Web.ViewModels.User;
using System.Collections.Generic;

namespace ITLearning.Frontend.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public UserProfileViewModel UserData { get; set; }
        public NewsThumbnailViewModel MainNews { get; set; }
        public IEnumerable<NewsThumbnailViewModel> SmallNews { get; set; }
        public IEnumerable<UserWidgetDirectiveViewModel> UserWidgetViewModel {get;set;}
    }
}
