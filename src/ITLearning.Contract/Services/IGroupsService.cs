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

        CommonResult<CreateGroupResult> CreateGroup(CreateGroupRequest request);

        CommonResult DeleteGroup(DeleteGroupRequest request);

        CommonResult<GroupBasicData> GetBasicData(GroupBasicDataRequest request);

        CommonResult<GroupAccessTypeResult> GetAccessType(GroupAccessTypeRequest request);

        CommonResult<GetLatestGroupsBasicDataResult> GetLatestGroupsBasicData(GetLatestGroupsBasicDataRequest request);
    }
        
}
