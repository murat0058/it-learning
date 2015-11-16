using ITLearning.Frontend.Web.Contract.Data.Results;
using ITLearning.Frontend.Web.Model;
using System.Collections.Generic;

namespace ITLearning.Frontend.Web.Contract.Services
{
    public interface INewsService
    {
        CommonResult<IEnumerable<News>> GetAll(bool withContent);
        CommonResult<News> GetById(string id);
    }
}