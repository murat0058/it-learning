using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.ViewModels.News
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BackgroundImagePath { get; set; }
        public string HtmlContent { get; set; }

        public int SocialNoOfLikes { get; set; }
        public string SocialNoOfLikesTitle
        {
            get
            {
                return string.Format("Polubiono {0} {1}", this.SocialNoOfLikes, this.SocialNoOfLikes == 1 ? "raz" : "razy");
            }
        }

        public int SocialNoOfShares { get; set; }
        public string SocialNoOfSharesTitle
        {
            get
            {
                return string.Format("Udostępniono {0} {1}", this.SocialNoOfShares, this.SocialNoOfShares == 1 ? "raz" : "razy");
            }
        }
    }
}
