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
using ITLearning.Frontend.Web.Contract.Enums;
using ITLearning.Frontend.Web.Providers.Home;
using ITLearning.Frontend.Web.Contract.Providers.ViewModelProviders;
using ITLearning.Frontend.Web.Contract.Services;
using System.Linq;

namespace ITLearning.Frontend.Web.Controllers
{
    [AuthorizeClaim(Type = ClaimTypeEnum.Controller, Value = ClaimValueEnum.HomeController)]
    public class HomeController : BaseController
    {
        private IUserBasicDataViewModelProvider _userBasicDataViewModelProvider;
        private INewsThumbnailsService _newsThumbnailsService;

        public HomeController(IUserBasicDataViewModelProvider userBasicDataViewModelProvider, INewsThumbnailsService newsThumbnailsService)
        {
            _userBasicDataViewModelProvider = userBasicDataViewModelProvider;
            _newsThumbnailsService = newsThumbnailsService;
        }

        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();

            FillModelWithBasicUserData(model);
            FillModelWithNews(model);
            CreateUserShortcutsWidget(model);

            return View(model);
        }

        private void FillModelWithBasicUserData(HomeViewModel model)
        {
            model.UserData = _userBasicDataViewModelProvider.GetUserBasicDataViewModel();
        }

        private void FillModelWithNews(HomeViewModel model)
        {
            var newsThumbnails = _newsThumbnailsService.GetLatestNewsThumbnails();
            if (newsThumbnails.IsSuccess)
            {
                var thumbnails = newsThumbnails.Item;

                if (newsThumbnails.Item.Count() > 0)
                {
                    model.MainNews = newsThumbnails.Item.First();
                }

                if (newsThumbnails.Item.Count() > 1)
                {
                    model.SmallNews = newsThumbnails.Item.Skip(1).Take(3);
                }
            }
        }

        private void CreateUserShortcutsWidget(HomeViewModel model)
        {
            //todo
        }
    }
}
