using AutoMapper;
using ITLearning.Frontend.Web.Contract.Data.Requests;
using ITLearning.Frontend.Web.Contract.Data.User;
using ITLearning.Frontend.Web.Core.Identity.Models;
using ITLearning.Frontend.Web.DAL.Entities;
using ITLearning.Frontend.Web.Model;
using ITLearning.Frontend.Web.ViewModels.Identity;
using ITLearning.Frontend.Web.ViewModels.News;

namespace ITLearning.Frontend.Web.Common.Mappings
{
    public static class MappingsDefinitions
    {
        //TODO separate to partial classes
        public static void ConfigureMappings()
        {
            Mapper.CreateMap<SignUpViewModel, SignUpModel>();
            Mapper.CreateMap<LoginViewModel, LoginModel>();
            Mapper.CreateMap<News, NewsThumbnailViewModel>();

            Mapper.CreateMap<User, UserProfileData>();
            Mapper.CreateMap<UserProfileData, User>();

            Mapper.CreateMap<UpdateUserProfileRequestData, UserProfileData>();
        }
    }
}