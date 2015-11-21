using System;

namespace ITLearning.Frontend.Web.ViewModels.News
{
    public class NewsThumbnailViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string BackgroundImagePath { get; set; }
        public int SocialNoOfLikes { get; set; }
        public int SocialNoOfShares { get; set; }
    }
}