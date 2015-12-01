using ITLearning.Contract.Data.Model.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.Data.Results.Groups
{
    public class GetLatestGroupsBasicDataResult
    {
        public IEnumerable<GroupWithUsersBasicData> Groups { get; set; }
    }
}
