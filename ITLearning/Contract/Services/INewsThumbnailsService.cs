using ITLearning.Frontend.Web.Contract.Data.Result;
using ITLearning.Frontend.Web.ViewModels.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Contract.Services
{
    public interface INewsThumbnailsService
    {
        CommonResult<IEnumerable<NewsThumbnailViewModel>> GetLatestNewsThumbnails();
    }
}
