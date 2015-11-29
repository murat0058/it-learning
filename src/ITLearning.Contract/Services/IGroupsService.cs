using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Data.Results.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLearning.Contract.Enums;
using ITLearning.Contract.Data.Requests.Groups;

namespace ITLearning.Contract.Services
{
    public interface IGroupsService
    {
        CommonResult<CreateGroupResult> CreateGroup(CreateGroupRequestData request);
        
        CommonResult<GroupBasicDataResult> GetGroupById(int id);

        CommonResult<IEnumerable<GroupBasicDataResult>> GetGroupsBasicDataLimitedByNo(string userName, int noOfGroups);
        
        CommonResult<GroupAccessTypeEnum> GetUserAccessType(int id, string userName);
        
        CommonResult UpdateGroupAccess(GroupAccessUpdateRequestData request);
    }
}
