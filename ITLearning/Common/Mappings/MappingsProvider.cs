using AutoMapper;
using ITLearning.Frontend.Web.Contract.Data.Requests;
using ITLearning.Frontend.Web.Contract.Data.Model.User;
using ITLearning.Frontend.Web.Core.Identity.Models;
using ITLearning.Frontend.Web.DAL.Entities;
using ITLearning.Frontend.Web.Contract.Data.Model.News;
using ITLearning.Frontend.Web.ViewModels.Identity;
using ITLearning.Frontend.Web.ViewModels.News;
using ITLearning.Frontend.Web.ViewModels.User;

namespace ITLearning.Frontend.Web.Common.Mappings
{
    public static class MappingsProvider
    {
        public static void ConfigureMappings()
        {
            Mapper.CreateMap<SignUpViewModel, SignUpModel>();
            Mapper.CreateMap<LoginViewModel, LoginModel>();
            Mapper.CreateMap<NewsData, NewsThumbnailViewModel>();

            Mapper.CreateMap<NewsListRequest, NewsListViewModel>();

            Mapper.CreateMap<User, UserProfileData>();
            Mapper.CreateMap<UserProfileData, User>();

            Mapper.CreateMap<UserProfileData, UserProfileViewModel>();

            Mapper.CreateMap<UpdateUserProfileRequestData, UserProfileData>();
        }
    }
}
