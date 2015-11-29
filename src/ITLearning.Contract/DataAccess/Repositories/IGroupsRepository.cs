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
        CommonResult<CreateGroupResult> CreateGroup(CreateGroupRequestData requestData);

        CommonResult<IEnumerable<GroupBasicData>> GetGroupsBasicData();
        CommonResult<GroupBasicDataResult> GetGroupById(int id);

        CommonResult<IEnumerable<GroupBasicData>> GetGroupsByUserName(string userName);
    }
}
