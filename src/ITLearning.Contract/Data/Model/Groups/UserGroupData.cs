using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.Data.Model.Groups
{
    public class UserGroupData
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
