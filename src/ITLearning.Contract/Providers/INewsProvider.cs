using ITLearning.Contract.Data.Model.News;
using ITLearning.Contract.Data.Requests.News;
using ITLearning.Contract.Data.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITLearning.Contract.Providers
{
    public interface INewsProvider
    {
        NewsData GetById(string id, bool contentAsHtml = true);
        IEnumerable<NewsData> GetAll();
        IEnumerable<NewsData> GetAllWithoutContent();
        Task SaveDataAsync(NewsData data);
        Task SaveContentAsync(NewsContentData data);
        string GetNewNewsId();
        Task<CommonResult> EditNewsAsync(EditNewsRequest request);
        Task<CommonResult> DeleteNewsAsync(DeleteNewsRequest request);
    }
}
