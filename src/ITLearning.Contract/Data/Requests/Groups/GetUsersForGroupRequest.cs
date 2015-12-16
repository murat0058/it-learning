using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.Data.Requests.Groups
{
    public class GetUsersForGroupRequest
    {
        public string OwnerName { get; set; }
        public int GroupId { get; set; }
        public bool IsRequestForManagement { get; set; }
    }
}
