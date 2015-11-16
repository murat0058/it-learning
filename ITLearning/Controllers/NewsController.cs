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

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ITLearning.Frontend.Web.Controllers
{
    [AuthorizeClaim(Type = ClaimTypeEnum.Controller, Value = ClaimValueEnum.Controller_NewsController)]
    public class NewsController : BaseController
    {
        private INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        public IActionResult List(NewsListFilterRequest filterRequest)
        {
            var result = _newsService.GetAll(withContent: false);
            var newsCollection = result.Item;
            var model = new NewsListViewModel();

            model.Authors = result.Item.Select(x => x.Author).Distinct();
            model.Tags = result.Item.SelectMany(x => x.Tags).Distinct();

            model.News = result.Item.Select(x => Mapper.Map<NewsThumbnailViewModel>(x));

            return View("List", model);
        }

        public IActionResult Single(string id)
        {
            var result = _newsService.GetById(id);

            if (result.IsSuccess)
            {
                return View(result.Item);
            }
            else
            {
                return RedirectToAction("List");
            }
        }
    }
}
