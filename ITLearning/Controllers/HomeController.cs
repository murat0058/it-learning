using Microsoft.AspNet.Mvc;
using ITLearning.Frontend.Web.Core.Identity.Attributes;
using ITLearning.Frontend.Web.Core.Identity.Enums;
using System.Linq;
using ITLearning.Frontend.Web.ViewModels.Home;
using ITLearning.Frontend.Web.ViewModels.News;
using System.Collections.Generic;
using ITLearning.Frontend.Web.ViewModels.User;
using ITLearning.Frontend.Web.Contract.Providers.ViewModelProviders;
using ITLearning.Frontend.Web.Contract.Services;
using AutoMapper;

namespace ITLearning.Frontend.Web.Controllers
{
    [Route("Home")]
    [AuthorizeClaim(Type = ClaimTypeEnum.Controller, Value = ClaimValueEnum.Controller_HomeController)]
    public class HomeController : BaseController
    {
        private IUserBasicDataViewModelProvider _userBasicDataViewModelProvider;
        private INewsService _newsService;

        public HomeController(IUserBasicDataViewModelProvider userBasicDataViewModelProvider, INewsService newsService)
        {
            _userBasicDataViewModelProvider = userBasicDataViewModelProvider;
            _newsService = newsService;
        }

        [HttpGet("Index")]
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
            var result = _newsService.GetAll(withContent: false);

            if (result.IsSuccess)
            {
                var allNewsItem = result.Item;

                if (allNewsItem.Count() > 0)
                {
                    model.MainNews = Mapper.Map<NewsThumbnailViewModel>(allNewsItem.First());
                }

                if (allNewsItem.Count() > 1)
                {
                    model.SmallNews = result.Item.Skip(1).Take(3).Select(x => Mapper.Map<NewsThumbnailViewModel>(x));
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
