using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLearning.Frontend.Web.Contract.Data.Model.News;

namespace ITLearning.Frontend.Web.Contract.Providers.ModelProviders
{
    public interface INewsProvider
    {
        NewsData GetById(string id);
        IEnumerable<NewsData> GetAll();
        IEnumerable<NewsData> GetAllWithoutContent();
    }
}
