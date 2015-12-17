using System;
using System.Linq;
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
using ITLearning.Contract.Data.Requests.News;
using ITLearning.Contract.Data.Results.Groups;
using ITLearning.Contract.Data.Results;

namespace ITLearning.Shared.Mappings
{
    public static class MappingsProvider
    {
        public static void ConfigureMappings()
        {
            Mapper.CreateMap<SignUpViewModel, SignUpModel>();
            Mapper.CreateMap<LoginViewModel, LoginModel>();
            Mapper.CreateMap<NewsData, NewsThumbnailViewModel>();
            Mapper.CreateMap<NewsData, SingleNewsViewModel>();

            Mapper.CreateMap<NewsData, CreateUpdateNewsViewModel>()
                .ForMember(dest => dest.TagsString, opt => opt.ResolveUsing(src => src.Tags.Aggregate((prev, next) => $"{prev} {next}")));

            Mapper.CreateMap<NewsListRequest, NewsListViewModel>();
            Mapper.CreateMap<CreateUpdateNewsViewModel, CreateNewsRequest>();
            Mapper.CreateMap<CreateUpdateNewsViewModel, EditNewsRequest>();
            Mapper.CreateMap<DeleteNewsViewModel, DeleteNewsRequest>();

            Mapper.CreateMap<User, UserProfileData>();
            Mapper.CreateMap<UserProfileData, User>();
            Mapper.CreateMap<UserProfileData, UserProfileViewModel>();
            Mapper.CreateMap<UpdateUserProfileRequest, UserProfileData>();

            Mapper.CreateMap<UserProfileData, UserData>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => GetUserName(src)))
                .ForMember(dest => dest.ProfileImagePath, opt => opt.MapFrom(src => src.ProfileImagePath ?? "default.jpg"));

            Mapper.CreateMap<CreateGroupViewModel, CreateGroupRequest>();
            Mapper.CreateMap<UpdateGroupViewModel, UpdateGroupRequest>();
            Mapper.CreateMap<GetGroupsListViewModel, GetGroupListRequest>();

            Mapper.CreateMap<GroupData, GroupBasicData>();
            Mapper.CreateMap<GroupData, GroupWithUsersData>();
            Mapper.CreateMap<GroupData, GroupBasicDataViewModel>();
            Mapper.CreateMap<GroupWithUsersData, GroupBasicDataViewModel>();
            Mapper.CreateMap<GroupWithUsersData, UpdateGroupViewModel>();
            Mapper.CreateMap<Group, GroupData>()
                .ForMember(dest => dest.Owner, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                .ForMember(dest => dest.Tasks, opt => opt.Ignore());

            Mapper.CreateMap<CommonResult<GetTasksForGroupResult>, GroupTasksViewModel>();
        }

        private static string GetUserName(UserProfileData src)
        {
            if (string.IsNullOrEmpty(src.FirstName) || string.IsNullOrEmpty(src.LastName))
            {
                return src.UserName;
            }
            else
            {
                return $"{src.FirstName} {src.LastName}";
            }
        }
    }
}
