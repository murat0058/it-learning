using AutoMapper;
using ITLearning.Frontend.Web.Contract.Data.Requests;
using ITLearning.Frontend.Web.Core.Identity.Models;
using ITLearning.Frontend.Web.Model;
using ITLearning.Frontend.Web.ViewModels.Identity;
using ITLearning.Frontend.Web.ViewModels.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Contract.Mappings
{
    public static class MappingsProvider
    {
        public static void ConfigureMappings()
        {
            Mapper.CreateMap<SignUpViewModel, SignUpModel>();
            Mapper.CreateMap<LoginViewModel, LoginModel>();
            Mapper.CreateMap<News, NewsThumbnailViewModel>();

            Mapper.CreateMap<NewsListRequest, NewsListViewModel>()
                .ForMember(dest => dest.News, opt => opt.MapFrom(src => src.News.Select(x => Mapper.Map<NewsThumbnailViewModel>(x))));
        }
    }
}
