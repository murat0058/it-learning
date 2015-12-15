using ITLearning.Contract.Data.Model.News;
using ITLearning.Contract.Data.Requests.News;
using ITLearning.Contract.Data.Results;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using ITLearning.Contract.Data.Results.News;

namespace ITLearning.Contract.Providers
{
    public interface INewsProvider
    {
        NewsData GetById(string id, bool contentAsHtml = true);
        IEnumerable<NewsData> GetAll();
        IEnumerable<NewsData> GetAllWithoutContent();

        Task SaveDataAsync(NewsData data);
        Task SaveContentAsync(NewsContentData data);

        CommonResult DeleteNews(DeleteNewsRequest request);

        string GetNewNewsId();

        Task<SaveNewsImageResult> SaveImageAsync(IFormFile image);
    }
}
