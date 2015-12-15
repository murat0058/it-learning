using System.Collections.Generic;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Data.Model.News;
using ITLearning.Contract.Data.Requests;
using ITLearning.Contract.Data.Requests.News;
using System.Threading.Tasks;
using ITLearning.Contract.Data.Results.News;

namespace ITLearning.Contract.Services
{
    public interface INewsService
    {
        CommonResult<IEnumerable<NewsData>> GetAll(bool withContent);
        CommonResult<IEnumerable<NewsData>> GetFiltered(NewsFilterRequest filterRequest);
        CommonResult<NewsData> GetById(string id, bool contentAsHtml = true);
        CommonResult<NewsListRequest> GetInitialRequest();
        Task<CommonResult<CreateNewsResult>> CreateNewsAsync(CreateNewsRequest createNewsRequest);

        CommonResult EditNews(EditNewsRequest request);
        CommonResult DeleteNews(DeleteNewsRequest request);
    }
}