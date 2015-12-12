using ITLearning.Contract.Data.Model.News;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITLearning.Contract.Providers
{
    public interface INewsProvider
    {
        NewsData GetById(string id);
        IEnumerable<NewsData> GetAll();
        IEnumerable<NewsData> GetAllWithoutContent();
        Task SaveDataAsync(NewsData data);
        Task SaveContentAsync(NewsContentData data);
        string GetNewNewsId();
    }
}
