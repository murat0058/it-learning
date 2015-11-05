using ITLearning.Frontend.Web.Common.Configs;
using ITLearning.Frontend.Web.Contract.Data.Result;
using ITLearning.Frontend.Web.Contract.Services;
using ITLearning.Frontend.Web.ViewModels.News;
using Microsoft.Framework.OptionsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Services
{
    public class NewsThumbnailsService : INewsThumbnailsService
    {
        private IOptions<PathsConfiguration> _pathsConfiguration;

        public NewsThumbnailsService(IOptions<PathsConfiguration> pathsConfiguration)
        {
            _pathsConfiguration = pathsConfiguration;
        }

        public CommonResult<IEnumerable<NewsThumbnailViewModel>> GetLatestNewsThumbnails()
        {
            var imagesBasePath = _pathsConfiguration.Value.NewsImagesPath;

            return CommonResult<IEnumerable<NewsThumbnailViewModel>>.Success<IEnumerable<NewsThumbnailViewModel>>(new List<NewsThumbnailViewModel>
            {
                new NewsThumbnailViewModel
                {
                    Id = 0,
                    Title = "Testowy",
                    BackgroundImagePath = imagesBasePath + "imaginecup.png",
                    SocialNoOfLikes = 10,
                    SocialNoOfShares = 12
                },
                new NewsThumbnailViewModel
                {
                    Id = 1,
                    Title = "Mały 1",
                    BackgroundImagePath = imagesBasePath + "itad.jpg",
                    SocialNoOfLikes = 5,
                    SocialNoOfShares = 6
                },
                new NewsThumbnailViewModel
                {
                    Id = 2,
                    Title = "Mały 2",
                    BackgroundImagePath = imagesBasePath + "itlearning.png",
                    SocialNoOfLikes = 7,
                    SocialNoOfShares = 8
                }
            });
        }
    }
}
