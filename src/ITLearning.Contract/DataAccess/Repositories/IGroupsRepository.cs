using ITLearning.Contract.Data.Model.Groups;
using ITLearning.Contract.Data.Requests;
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
    }
}
