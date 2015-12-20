using Microsoft.AspNet.Mvc;
using ITLearning.Frontend.Web.Core.Identity.Attributes;
using System.Linq;
using ITLearning.Frontend.Web.ViewModels.Home;
using ITLearning.Frontend.Web.ViewModels.News;
using ITLearning.Frontend.Web.ViewModels.User;
using AutoMapper;
using Microsoft.Net.Http.Headers;
using ITLearning.Contract.Services;
using ITLearning.Contract.Enums;

namespace ITLearning.Frontend.Web.Controllers
{
    [Route("Home")]
    [AuthorizeClaim(Type = ClaimTypeEnum.Controller, Value = ClaimValueEnum.Controller_HomeController)]
    public class HomeController : BaseController
    {
        private readonly INewsService _newsService;
        private readonly IUserService _userService;

        public HomeController(INewsService newsService, IUserService userService)
        {
            _newsService = newsService;
            _userService = userService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();

            FillModelWithBasicUserData(model);
            FillModelWithNews(model);

            return View(model);
        }

        [HttpGet("Error/{code}")]
        public IActionResult Error(string code)
        {
            switch (code)
            {
                case "404":
                    return View("Errors/ErrorPage404");
                default:
                    return new ContentResult
                    {
                        Content = $"Wystąpił nieoczekiwany błąd. Kod błędu - {code}",
                        ContentType = new MediaTypeHeaderValue("text/plain"),
                        StatusCode = int.Parse(code)
                    };
            }
        }

        private void FillModelWithBasicUserData(HomeViewModel model)
        {
            model.UserData = Mapper.Map<UserProfileViewModel>(_userService.GetUserProfile().Item);
        }

        private void FillModelWithNews(HomeViewModel model)
        {
            var result = _newsService.GetAll(withContent: false);

            if (result.IsSuccess)
            {
                var allNewsItem = result.Item;

                var ordered = allNewsItem
                    .OrderByDescending(x => x.Id);

                if (allNewsItem.Count() > 0)
                {
                    model.MainNews = Mapper.Map<NewsThumbnailViewModel>(ordered.First());
                }

                if (allNewsItem.Count() > 1)
                {
                    model.SmallNews = ordered.Skip(1).Take(3).Select(x => Mapper.Map<NewsThumbnailViewModel>(x));
                }
            }
        }
       
    }
}