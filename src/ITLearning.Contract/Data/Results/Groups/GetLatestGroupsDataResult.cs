using ITLearning.Contract.Data.Model.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.Data.Results.Groups
{
    public class GetLatestGroupsDataResult
    {
        public IEnumerable<GroupWithUsersData> Groups { get; set; }
    }
}
