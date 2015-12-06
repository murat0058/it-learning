using ITLearning.Contract.Data.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.Data.Results.Groups
{
    public class GetUsersForGroupResult
    {
        public IEnumerable<UserData> Users { get; set; }
    }
}
