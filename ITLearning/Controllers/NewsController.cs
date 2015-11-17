using System.Linq;
using AutoMapper;
using ITLearning.Frontend.Web.Contract.Services;
using ITLearning.Frontend.Web.Core.Identity.Attributes;
using ITLearning.Frontend.Web.Core.Identity.Enums;
using ITLearning.Frontend.Web.ViewModels.News;
using Microsoft.AspNet.Mvc;
using ITLearning.Frontend.Web.Contract.Data.Requests;
using System;
using System.Collections.Generic;
using ITLearning.Frontend.Web.Model;
using Microsoft.AspNet.Http;
using ITLearning.Frontend.Web.Common.Extensions;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ITLearning.Frontend.Web.Controllers
{
    [Route("News")]
    [AuthorizeClaim(Type = ClaimTypeEnum.Controller, Value = ClaimValueEnum.Controller_NewsController)]
    public class NewsController : BaseController
    {
        private readonly string NEWS_FILTER_REQUEST = "newsFilterRequest";
        private readonly string NEWS_MODEL = "newsModel";

        private INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("All")]
        public IActionResult All()
        {
            var result = _newsService.GetAll(withContent: false);
            var newsCollection = result.Item;

            var request = new NewsListRequest
            {
                News = result.Item,
                Authors = result.Item.Select(x => x.Author).Distinct(),
                Tags = result.Item.SelectMany(x => x.Tags).Distinct(),
            };

            var viewModel = Mapper.Map<NewsListViewModel>(request);

            return View("List", JsonConvert.SerializeObject(viewModel));
        }

        [HttpGet("Single/{id}")]
        public IActionResult Single(string id)
        {
            var result = _newsService.GetById(id);

            if (result.IsSuccess)
            {
                return View(result.Item);
            }
            else
            {
                return RedirectToAction("All");
            }
        }

        [HttpPost("List")]
        public IActionResult List([FromBody]NewsFilterRequest request)
        {
            var result = _newsService.GetAll(withContent: false);
            var newsCollection = result.Item.Where(x => x.Tags.Contains(request.Tag));

            return new JsonResult(newsCollection);
        }
    }
}
