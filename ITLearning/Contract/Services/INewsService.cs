using ITLearning.Frontend.Web.Contract.Data.Result;
using ITLearning.Frontend.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Contract.Services
{
    public interface INewsService
    {
        CommonResult<IEnumerable<News>> GetAll(bool withContent);
        CommonResult<News> GetById(string id);
    }
}
