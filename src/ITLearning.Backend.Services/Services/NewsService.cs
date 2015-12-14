using System.Collections.Generic;
using System.Linq;
using ITLearning.Contract.Services;
using ITLearning.Contract.Providers;
using ITLearning.Contract.Data.Model.News;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Data.Requests;
using ITLearning.Shared.Extensions;
using ITLearning.Contract.Data.Requests.News;
using System;
using System.Threading.Tasks;
using ITLearning.Contract.Data.Results.News;
using ITLearning.Shared.Extensions;

namespace ITLearning.Backend.Business.Services
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

            if (newsCollection.Any())
            {
                return CommonResult<IEnumerable<NewsData>>.Success(newsCollection);
            }
            else
            {
                return CommonResult<IEnumerable<NewsData>>.Failure("Nie znaleziono newsów spełniających podane kryteria.");
            }
        }

        public CommonResult<NewsData> GetById(string id, bool contentAsHtml = true)
        {
            var news = _newsProvider.GetById(id, contentAsHtml);

            if(news != null)
            {
                return CommonResult<NewsData>.Success(news);
            }
            else
            {
                return CommonResult<NewsData>.Failure("News o podanym identyfikatorze nie istnieje.");
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

        public async Task<CommonResult<CreateNewsResult>> CreateNewsAsync(CreateNewsRequest request)
        {
            var newsId = GetNewsId();

            var data = new NewsData
            {
                Id = newsId,
                Author = request.Author,
                Title = request.Title,
                Date = DateTime.Now,
                ImageName = "google-campus.jpg",
                Tags = GetTagsFromTagsString(request.TagsString),
            };

            var contentData = new NewsContentData
            {
                Id = newsId,
                Content = request.Content
            };

            await _newsProvider.SaveDataAsync(data);
            await _newsProvider.SaveContentAsync(contentData);

            return CommonResult<CreateNewsResult>.Success(new CreateNewsResult { Id = newsId });
        }

        #region Helpers
        private string GetNewsId()
        {
            return _newsProvider.GetNewNewsId();
        }

        private IEnumerable<string> GetTagsFromTagsString(string tagsString)
        {
            return tagsString.Split(new char[] { ' ' }).Where(tag => tag.Length > 0);
        }

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

        public CommonResult EditNews(EditNewsRequest request)
        {
            var data = _newsProvider.GetById(request.Id);

            if (request.Title.NotNullNorEmpty())
            {
                data.Title = request.Title;
            }

            if (request.TagsString.NotNullNorEmpty() && GetTagsFromTagsString(request.TagsString).Any())
            {
                data.Tags = GetTagsFromTagsString(request.TagsString);
            }

            var contentData = new NewsContentData
            {
                Id = request.Id,
                Content = request.Content
            };

            _newsProvider.SaveDataAsync(data);
            _newsProvider.SaveContentAsync(contentData);

            return CommonResult.Success();
        }

        public CommonResult DeleteNews(DeleteNewsRequest request)
        {
            return _newsProvider.DeleteNews(request);
        }
        #endregion
    }
}