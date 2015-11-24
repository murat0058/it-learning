using ITLearning.Contract.Data.Requests;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Data.Results.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.Services
{
    public interface IGroupsService
    {
        CommonResult<CreateGroupResult> CreateGroup(CreateGroupRequestData request);
    }
}
