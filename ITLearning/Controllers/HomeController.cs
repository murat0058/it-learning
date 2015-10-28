using Microsoft.AspNet.Mvc;
using ITLearning.Frontend.Web.Core.Identity.Attributes;
using ITLearning.Frontend.Web.Core.Identity.Enums;
using System;
using ITLearning.Frontend.Web.ViewModels.Home;
using ITLearning.Frontend.Web.ViewModels.News;
using System.Collections.Generic;
using Microsoft.Framework.OptionsModel;
using ITLearning.Frontend.Web.Common.Configs;
using ITLearning.Frontend.Web.ViewModels.User;

namespace ITLearning.Frontend.Web.Controllers
{
    [AuthorizeClaim(Type = ClaimTypeEnum.Controller, Value = ClaimValueEnum.HomeController)]
    public class HomeController : BaseController
    {
        private IOptions<PathsConfiguration> _pathsConfiguration;

        public HomeController(IOptions<PathsConfiguration> pathsConfiguration)
        {
            _pathsConfiguration = pathsConfiguration;
        }

        public IActionResult Index()
        {
            string imagesBasePath = _pathsConfiguration.Value.NewsImagesPath;

            #region Fake viewmodel, to be removed
            var viewModel = new HomeViewModel
            {
                UserData = new UserBasicDataViewModel
                {
                    DisplayName = "nazwa usera",
                    ProfileImagePath = _pathsConfiguration.Value.ProfileImagesPath + "default.jpg"
                },
                MainNews = new NewsThumbnailViewModel
                {
                    Id = 0,
                    Title = "Testowy",
                    BackgroundImagePath = imagesBasePath + "imaginecup.png",
                    SocialNoOfLikes = 10,
                    SocialNoOfShares = 12
                },
                SmallNews = new List<NewsThumbnailViewModel>
                {
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
                    },
                    new NewsThumbnailViewModel
                    {
                        Id = 3,
                        Title = "Mały 3",
                        BackgroundImagePath = imagesBasePath + "wbmii.png",
                        SocialNoOfLikes = 9,
                        SocialNoOfShares = 10
                    },
                }
            };
            #endregion

            return View(viewModel);
        }
    }
}
