using Microsoft.AspNet.Mvc;
using ITLearning.Frontend.Web.Core.Identity.Attributes;
using ITLearning.Frontend.Web.Core.Identity.Enums;
using ITLearning.Frontend.Web.ViewModels.Home;
using System.Collections.Generic;
using ITLearning.Frontend.Web.ViewModels.User;
using ITLearning.Frontend.Web.Contract.Providers.ViewModelProviders;
using ITLearning.Frontend.Web.Contract.Services;
using System.Linq;

namespace ITLearning.Frontend.Web.Controllers
{
    [AuthorizeClaim(Type = ClaimTypeEnum.Controller, Value = ClaimValueEnum.Controller_HomeController)]
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
            model.UserWidgetViewModel = new List<UserWidgetDirectiveViewModel>
            {
                new UserWidgetDirectiveViewModel {
                    DirectiveId = 0,
                    DirectiveString = "<itl-widget-poc-first id=\"0\" type=\"tab-primary\" parent-vm=\"vm\"></itl-widget-poc-first>",
                    TabType = "tab-primary",
                    TabIcon = "fa-user",
                    TabTitle = "First directive"
                },
                new UserWidgetDirectiveViewModel {
                    DirectiveId = 1,
                    DirectiveString = "<itl-widget-poc-second id=\"1\" type=\"tab-success\" parent-vm=\"vm\"></itl-widget-poc-second>",
                    TabType = "tab-success",
                    TabIcon = "fa-cog",
                    TabTitle = "Second directive"
                }
            };
        }
    }
}
