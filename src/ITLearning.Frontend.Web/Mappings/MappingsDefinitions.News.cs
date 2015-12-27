using AutoMapper;
using ITLearning.Contract.Data.Model.News;
using ITLearning.Contract.Data.Requests;
using ITLearning.Contract.Data.Requests.News;
using ITLearning.Frontend.Web.ViewModels.News;
using System.Linq;

namespace ITLearning.Frontend.Web.Mappings
{
    public static partial class MappingsDefinitions
    {
        private static void CreateNewsMappings()
        {
            Mapper.CreateMap<NewsData, NewsThumbnailViewModel>();
            Mapper.CreateMap<NewsData, SingleNewsViewModel>();
            Mapper.CreateMap<NewsData, CreateUpdateNewsViewModel>()
                .ForMember(dest => dest.TagsString, opt => opt.ResolveUsing(src => src.Tags.Aggregate((prev, next) => $"{prev} {next}")));
            Mapper.CreateMap<NewsListRequest, NewsListViewModel>();
            Mapper.CreateMap<CreateUpdateNewsViewModel, CreateNewsRequest>();
            Mapper.CreateMap<CreateUpdateNewsViewModel, EditNewsRequest>();
            Mapper.CreateMap<DeleteNewsViewModel, DeleteNewsRequest>();
        }
    }
}
