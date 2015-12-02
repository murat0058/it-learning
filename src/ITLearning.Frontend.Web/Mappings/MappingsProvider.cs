using AutoMapper;
using ITLearning.Backend.Database.Entities;
using ITLearning.Backend.Database.Entities.JunctionTables;
using ITLearning.Contract.Data.Model.Groups;
using ITLearning.Contract.Data.Model.News;
using ITLearning.Contract.Data.Model.User;
using ITLearning.Contract.Data.Requests;
using ITLearning.Contract.Data.Requests.Groups;
using ITLearning.Frontend.Web.Core.Identity.Models;
using ITLearning.Frontend.Web.ViewModels.Group;
using ITLearning.Frontend.Web.ViewModels.Identity;
using ITLearning.Frontend.Web.ViewModels.News;
using ITLearning.Frontend.Web.ViewModels.User;

namespace ITLearning.Shared.Mappings
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
            Mapper.CreateMap<UpdateUserProfileRequest, UserProfileData>();

            Mapper.CreateMap<CreateGroupViewModel, CreateGroupRequest>();
            Mapper.CreateMap<UpdateGroupViewModel, UpdateGroupRequest>();
            Mapper.CreateMap<GroupData, GroupBasicData>();
            Mapper.CreateMap<GroupData, GroupWithUsersData>();
            Mapper.CreateMap<GroupWithUsersData, GroupBasicDataViewModel>();
            Mapper.CreateMap<GroupWithUsersData, UpdateGroupViewModel>();
            Mapper.CreateMap<Group, GroupData>()
                .ForMember(dest => dest.Owner, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore());
            //  .ForMember(dest => dest.Tasks, opt => opt.Ignore());
        }
    }
}
