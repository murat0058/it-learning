using System;
using System.Linq;
using AutoMapper;
using ITLearning.Contract.Data.Model.User;
using ITLearning.Contract.Data.Requests;
using ITLearning.Contract.Data.Requests.News;
using ITLearning.Contract.Providers;
using ITLearning.Contract.Services;
using ITLearning.Frontend.Web.Core.Identity.Attributes;
using ITLearning.Frontend.Web.ViewModels.News;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Enums;

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

            var viewModel = Mapper.Map<SingleNewsViewModel>(result.Item);

            bool isCurrentUserAnAuthor = CheckIfAuthor(viewModel.AuthorUserName);

            viewModel.CanDelete = isCurrentUserAnAuthor;
            viewModel.CanEdit = isCurrentUserAnAuthor;

            return result.IsSuccess ?
                (ActionResult)View(viewModel) :
                (ActionResult)RedirectToAction("All");
        }

        [HttpGet("Single/{id}/Edit")]
        public IActionResult Edit(string id)
        {
            return CommonEditDeleteGetAction<CreateUpdateNewsViewModel>(id, "Edit", false);
        }

        [HttpGet("Single/{id}/Delete")]
        public IActionResult Delete(string id)
        {
            return CommonEditDeleteGetAction<SingleNewsViewModel>(id, "Delete");
        }

        [HttpPost("EditNews")]
        public IActionResult EditNews(CreateUpdateNewsViewModel viewModel)
        {
            var result = CommonEditDeletePostActionAsync<CreateUpdateNewsViewModel, EditNewsRequest>(viewModel.Id, viewModel, _newsService.EditNews);

            if (result.IsSuccess)
            {
                return RedirectToAction("Single", new { id = viewModel.Id });
            }
            else
            {
                return RedirectToAction("Edit", new { id = viewModel.Id });
            }
        }

        [HttpPost("DeleteNews")]
        public IActionResult DeleteNews(DeleteNewsViewModel viewModel)
        {
            var result = CommonEditDeletePostActionAsync<DeleteNewsViewModel, DeleteNewsRequest>(viewModel.NewsId, viewModel, _newsService.DeleteNews);

            if (result.IsSuccess)
            {
                return RedirectToAction("All");
            }
            else
            {
                return RedirectToAction("Delete", new { id = viewModel.NewsId });
            }
        }

        [HttpPost("List")]
        public IActionResult List(NewsFilterRequest request)
        {
            var result = _newsService.GetFiltered(request);

            return new JsonResult(result);
        }

        [HttpGet("Create")]
        [AuthorizeClaim(Type = ClaimTypeEnum.News, Value = ClaimValueEnum.News_Create)]
        public IActionResult Create()
        {
            return View("Create", new CreateUpdateNewsViewModel());
        }

        [HttpPost("CreateNews")]
        [AuthorizeClaim(Type = ClaimTypeEnum.News, Value = ClaimValueEnum.News_Create)]
        public async Task<IActionResult> CreateNews(CreateUpdateNewsViewModel model)
        {
            var request = Mapper.Map<CreateNewsRequest>(model);

            var userData = _userService.GetUserProfile();
            request.Author = GetUserFormattedName(userData.Item);
            request.AuthorUserName = userData.Item.UserName;

            var result = await _newsService.CreateNewsAsync(request);

            if (result.IsSuccess)
            {
                return RedirectToAction("Single", new { id = result.Item.Id });
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View("Create", model);
            }
        }

        private IActionResult CommonEditDeleteGetAction<TViewModel>(string id, string viewName, bool contentAsHtml = true)
        {
            var news = _newsService.GetById(id, contentAsHtml);

            if (news.IsSuccess)
            {
                var newsItem = news.Item;

                if (CheckIfAuthor(newsItem.AuthorUserName))
                {
                    var vm = Mapper.Map<TViewModel>(newsItem);
                    return View(viewName, vm);
                }
                else
                {
                    return RedirectToAction("Single", new { id = newsItem.Id });
                }
            }
            else
            {
                return RedirectToAction("All");
            }
        }

        private CommonResult CommonEditDeletePostActionAsync<TViewModel, TRequest>(string id, TViewModel viewModel, Func<TRequest, CommonResult> func)
        {
            var news = _newsService.GetById(id);

            if (news.IsSuccess && CheckIfAuthor(news.Item.AuthorUserName))
            {
                var request = Mapper.Map<TRequest>(viewModel);

                return func(request);
            }
            else
            {
                return CommonResult.Failure("Wystąpił błąd.");
            }
        }

        #region Helpers
        private string GetUserFormattedName(UserProfileData data)
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

        private bool CheckIfAuthor(string newsAuthorName)
        {
            var isCurrentUserAnAuthor = newsAuthorName == User.Identity.Name;
            return isCurrentUserAnAuthor;
        }
        #endregion
    }
}
