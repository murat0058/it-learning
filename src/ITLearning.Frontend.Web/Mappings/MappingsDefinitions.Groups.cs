using AutoMapper;
using ITLearning.Backend.Database.Entities;
using ITLearning.Contract.Data.Model.Groups;
using ITLearning.Contract.Data.Requests.Groups;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Data.Results.Groups;
using ITLearning.Frontend.Web.ViewModels.Group;

namespace ITLearning.Frontend.Web.Mappings
{
    public static partial class MappingsDefinitions
    {
        private static void CreateGroupsMappings()
        {
            Mapper.CreateMap<CreateGroupViewModel, CreateGroupRequest>();

            Mapper.CreateMap<UpdateGroupViewModel, UpdateGroupRequest>();

            Mapper.CreateMap<GetGroupsListViewModel, GetGroupListRequest>();

            Mapper.CreateMap<GroupData, GroupBasicData>();

            Mapper.CreateMap<GroupData, UserGroupData>();

            Mapper.CreateMap<GroupData, GroupWithUsersData>();

            Mapper.CreateMap<GroupData, GroupBasicDataViewModel>();

            Mapper.CreateMap<GroupWithUsersData, GroupBasicDataViewModel>();

            Mapper.CreateMap<GroupWithUsersData, UpdateGroupViewModel>();

            Mapper.CreateMap<Group, GroupData>()
                .ForMember(dest => dest.Owner, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                .ForMember(dest => dest.Tasks, opt => opt.Ignore());

            Mapper.CreateMap<GroupListItem, UserGroupData>();

            Mapper.CreateMap<Group, UserGroupData>();

            Mapper.CreateMap<CommonResult<GetTasksForGroupResult>, GroupTasksViewModel>()
                .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src.Item.Tasks));
        }
    }
}