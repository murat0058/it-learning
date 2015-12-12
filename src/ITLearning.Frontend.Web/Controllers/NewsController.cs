using System;
using AutoMapper;
using ITLearning.Contract.Data.Model.User;
using ITLearning.Contract.Data.Requests;
using ITLearning.Contract.Data.Requests.News;
using ITLearning.Contract.Providers;
using ITLearning.Contract.Services;
using ITLearning.Frontend.Web.Core.Identity.Attributes;
using ITLearning.Frontend.Web.Core.Identity.Enums;
using ITLearning.Frontend.Web.ViewModels.News;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Controllers
{
    [Route("News")]
    [AuthorizeClaim(Type = ClaimTypeEnum.Controller, Value = ClaimValueEnum.Controller_NewsController)]
    public class NewsController : BaseController
    {
        private INewsService _newsService;
        private IUserService _userService;
        private IAppConfigurationProvider _configurationProvider;

        public NewsController(INewsService newsService, IUserService userService, IAppConfigurationProvider configurationProvider)
        {
            _newsService = newsService;
            _userService = userService;
            _configurationProvider = configurationProvider;
        }

        [HttpGet("All")]
        public IActionResult All()
        {
            var request = _newsService.GetInitialRequest().Item;

            return View("List", JsonConvert.SerializeObject(Mapper.Map<NewsListViewModel>(request)));
        }

        [HttpGet("Tag/{tag}")]
        public IActionResult Tag(string tag)
        {
            var request = _newsService.GetInitialRequest().Item;
            request.FilterRequest = new NewsFilterRequest { Tags = new string[] { tag } }; ;

            return View("List", JsonConvert.SerializeObject(Mapper.Map<NewsListViewModel>(request)));
        }

        [HttpGet("Author/{author}")]
        public IActionResult Author(string author)
        {
            var request = _newsService.GetInitialRequest().Item;
            request.FilterRequest = new NewsFilterRequest { Authors = new string[] { author } };

            return View("List", JsonConvert.SerializeObject(Mapper.Map<NewsListViewModel>(request)));
        }

        [AllowAnonymous]
        [HttpGet("Single/{id}")]
        public IActionResult Single(string id)
        {
            var result = _newsService.GetById(id);

            if (result.IsSuccess)
            {
                ViewBag.DisqusUrl = _configurationProvider.GetDisqusPageUrl();
            }

            return result.IsSuccess ?
                (ActionResult)View(result.Item) :
                (ActionResult)RedirectToAction("All");
        }

        [HttpPost("List")]
        public IActionResult List(NewsFilterRequest request)
        {
            var result = _newsService.GetFiltered(request);

            return new JsonResult(result);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            var viewModel = new CreateNewsViewModel();

            return View("Create", JsonConvert.SerializeObject(viewModel));
        }

        [HttpPost("CreateNews")]
        public async Task<IActionResult> CreateNews(CreateNewsRequestViewModel model)
        {
            var request = Mapper.Map<CreateNewsRequest>(model);

            var userData = _userService.GetUserProfile();
            request.Author = GetUserName(userData.Item);

            await _newsService.CreateNewsAsync(request);

            return View("Create", JsonConvert.SerializeObject(Mapper.Map<CreateNewsViewModel>(model)));
        }

        #region Helpers
        private string GetUserName(UserProfileData data)
        {
            if (string.IsNullOrEmpty(data.FirstName) || string.IsNullOrEmpty(data.LastName))
            {
                return data.UserName;
            }
            else
            {
                return $"{data.FirstName} {data.LastName}";
            }
        }
        #endregion
    }
}
