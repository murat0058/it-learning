using ITLearning.Contract.Data.Model.Groups;
using ITLearning.Contract.Data.Requests.Groups;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Data.Results.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.DataAccess.Repositories
{
    public interface IGroupsRepository
    {
        CommonResult<GroupData> Get(int groupId, bool withOwner = false, bool withUsers = false, bool withTasks = false);
        CommonResult<IEnumerable<GroupData>> GetAll(bool withOwner = false, bool withUsers = false, bool withTasks = false);
        CommonResult<CreateGroupResult> Create(CreateGroupRequest request);
        CommonResult Update(UpdateGroupRequest request);
        CommonResult Delete(int groupId);
        CommonResult AddUsers(UserGroupRequest request);
        CommonResult RemoveUsers(UserGroupRequest request);
    }
}
