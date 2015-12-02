using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Data.Results.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLearning.Contract.Enums;
using ITLearning.Contract.Data.Requests.Groups;
using ITLearning.Contract.Data.Model.Groups;

namespace ITLearning.Contract.Services
{
    public interface IGroupsService
    {

        CommonResult<GroupBasicData> GetData(GetGroupRequest request);

        CommonResult<GroupWithUsersData> GetDataWithUsers(GetGroupRequest request);

        CommonResult<GetLatestGroupsDataResult> GetLatestGroupsData(GetLatestGroupsBasicDataRequest request);

        CommonResult<GroupAccessTypeResult> GetAccessType(GroupAccessTypeRequest request);

        CommonResult<CreateGroupResult> CreateGroup(CreateGroupRequest request);

        CommonResult DeleteGroup(DeleteGroupRequest request);

        CommonResult UpdateGroup(UpdateGroupRequest request);

        CommonResult TryAddUserToGroup(AddUserToGroupRequest request);
    }
        
}
