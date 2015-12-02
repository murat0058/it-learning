using ITLearning.Contract.Data.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.Data.Model.Groups
{
    public class GroupWithUsersData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate{ get; set; }
        public string Password { get; set; }

        public IEnumerable<UserProfileData> Users { get; set; }

        public int NoOfUsers
        {
            get
            {
                return Users != null ? Users.Count() : 0;
            }
        }
    }
}
