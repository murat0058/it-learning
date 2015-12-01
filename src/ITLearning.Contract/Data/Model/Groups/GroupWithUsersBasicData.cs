using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.Data.Model.Groups
{
    public class GroupWithUsersBasicData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate{ get; set; }
        public int NoOfUsers { get; set; }
    }
}
