using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.Data.Requests.Groups
{
    public class UserGroupRequest
    {
        public int GroupId { get; set; }

        public IEnumerable<int> Users { get; set; }
    }
}
