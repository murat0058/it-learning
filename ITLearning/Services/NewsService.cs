using ITLearning.Frontend.Web.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLearning.Frontend.Web.Model;
using ITLearning.Frontend.Web.Contract.Data.Result;
using ITLearning.Frontend.Web.Contract.Providers.ModelProviders;

namespace ITLearning.Frontend.Web.Services
{
    public class NewsService : INewsService
    {
        private INewsProvider _newsProvider;

        public NewsService(INewsProvider newsProvider)
        {
            _newsProvider = newsProvider;
        }

        public CommonResult<IEnumerable<News>> GetAll(bool withContent)
        {
            var newsCollection = withContent ? _newsProvider.GetAll() : _newsProvider.GetAllWithoutContent();

            return CommonResult<IEnumerable<News>>.Success(newsCollection);
        }

        public CommonResult<News> GetById(string id)
        {
            var news = _newsProvider.GetById(id);

            if(news != null)
            {
                return CommonResult<News>.Success(news);
            }
            else
            {
                return CommonResult<News>.Failure<News>("News o podanym identyfikatorze nie istnieje.");
            }

            
        }
    }
}
