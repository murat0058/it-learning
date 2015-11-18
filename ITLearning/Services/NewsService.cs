using System.Collections.Generic;
using ITLearning.Frontend.Web.Contract.Data.Model.News;
using ITLearning.Frontend.Web.Contract.Providers.ModelProviders;
using ITLearning.Frontend.Web.Contract.Data.Results;
using ITLearning.Frontend.Web.Contract.Services;
using ITLearning.Frontend.Web.Contract.Data.Requests;
using System.Linq;
using System;
using ITLearning.Frontend.Web.Common.Extensions;

namespace ITLearning.Frontend.Web.Services
{
    public class NewsService : INewsService
    {
        private INewsProvider _newsProvider;

        public NewsService(INewsProvider newsProvider)
        {
            _newsProvider = newsProvider;
        }

        public CommonResult<IEnumerable<NewsData>> GetAll(bool withContent)
        {
            var newsCollection = withContent ? _newsProvider.GetAll() : _newsProvider.GetAllWithoutContent();

            return CommonResult<IEnumerable<NewsData>>.Success(newsCollection);
        }

        public CommonResult<IEnumerable<NewsData>> GetFiltered(NewsFilterRequest filterRequest)
        {
            var newsCollection = _newsProvider.GetAllWithoutContent();

            if(filterRequest.Tags != null && filterRequest.Tags.Any())
            {
                newsCollection = FilterByTags(filterRequest, newsCollection);
            }

            if (filterRequest.Authors != null && filterRequest.Authors.Any())
            {
                newsCollection = FilterByAuthors(filterRequest, newsCollection);
            }

            if (filterRequest.Query.NotNullNorEmpty())
            {
                newsCollection = FilterByQuery(filterRequest, newsCollection);
            }

            return CommonResult<IEnumerable<NewsData>>.Success(newsCollection);
        }

        public CommonResult<NewsData> GetById(string id)
        {
            var news = _newsProvider.GetById(id);

            if(news != null)
            {
                return CommonResult<NewsData>.Success(news);
            }
            else
            {
                return CommonResult<NewsData>.Failure<NewsData>("News o podanym identyfikatorze nie istnieje.");
            }
        }

        public CommonResult<NewsListRequest> GetInitialRequest()
        {
            var result = this.GetAll(withContent: false);

            var request = new NewsListRequest
            {
                Authors = result.Item.Select(x => x.Author).Distinct(),
                Tags = result.Item.SelectMany(x => x.Tags).Distinct()
            };

            return CommonResult<NewsListRequest>.Success(request);
        }

        #region Helpers
        private static IEnumerable<NewsData> FilterByTags(NewsFilterRequest filterRequest, IEnumerable<NewsData> newsCollection)
        {
            return newsCollection.Where(news => filterRequest.Tags.All(x => news.Tags.Contains(x)));
        }

        private static IEnumerable<NewsData> FilterByAuthors(NewsFilterRequest filterRequest, IEnumerable<NewsData> newsCollection)
        {
            return newsCollection.Where(news => filterRequest.Authors.Contains(news.Author));
        }

        private static IEnumerable<NewsData> FilterByQuery(NewsFilterRequest filterRequest, IEnumerable<NewsData> newsCollection)
        {
            return newsCollection.Where(news => news.Title.ToLower().Contains(filterRequest.Query.ToLower()));
        }
        #endregion
    }
}