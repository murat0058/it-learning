using ITLearning.Frontend.Web.Contract.Data.Results;
using ITLearning.Frontend.Web.ViewModels.News;
using System.Collections.Generic;

namespace ITLearning.Frontend.Web.Contract.Services
{
    public interface INewsThumbnailsService
    {
        CommonResult<IEnumerable<NewsThumbnailViewModel>> GetLatestNewsThumbnails();
    }
}