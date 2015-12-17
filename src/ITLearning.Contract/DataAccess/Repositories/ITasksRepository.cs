using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Data.Results.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.DataAccess.Repositories
{
    public interface ITasksRepository
    {
        CommonResult<IEnumerable<TaskInstancesForUserResult>> GetForUser(string userName);
    }
}
