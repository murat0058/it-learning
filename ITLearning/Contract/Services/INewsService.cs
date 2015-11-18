using ITLearning.Frontend.Web.Contract.Data.Requests;
using ITLearning.Frontend.Web.Contract.Data.Results;
using ITLearning.Frontend.Web.Contract.Data.Model.News;
using System.Collections.Generic;

namespace ITLearning.Frontend.Web.Contract.Services
{
    public interface INewsService
    {
        CommonResult<IEnumerable<NewsData>> GetAll(bool withContent);
        CommonResult<IEnumerable<NewsData>> GetFiltered(NewsFilterRequest filterRequest);
        CommonResult<NewsData> GetById(string id);
        CommonResult<NewsListRequest> GetInitialRequest();

    }
}