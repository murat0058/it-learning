using AutoMapper;
using ITLearning.Contract.Data.Requests;
using ITLearning.Contract.Providers;
using ITLearning.Contract.Services;
using ITLearning.Frontend.Web.Core.Identity.Attributes;
using ITLearning.Frontend.Web.Core.Identity.Enums;
using ITLearning.Frontend.Web.ViewModels.News;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ITLearning.Frontend.Web.Controllers
{
    [Route("News")]
    [AuthorizeClaim(Type = ClaimTypeEnum.Controller, Value = ClaimValueEnum.Controller_NewsController)]
    public class NewsController : BaseController
    {
        private INewsService _newsService;
        private IAppConfigurationProvider _configurationProvider;

        public NewsController(INewsService newsService, IAppConfigurationProvider configurationProvider)
        {
            _newsService = newsService;
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
    }
}
