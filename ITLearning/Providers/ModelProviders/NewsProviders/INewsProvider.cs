using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLearning.Frontend.Web.Model;

namespace ITLearning.Frontend.Web.Providers.ModelProviders.NewsProviders
{
    public interface INewsProvider
    {
        News GetById(string id);
        IEnumerable<News> GetAll();
        IEnumerable<News> GetAllWithoutContent();
    }
}
