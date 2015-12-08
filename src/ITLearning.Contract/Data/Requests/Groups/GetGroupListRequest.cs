using ITLearning.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.Data.Requests.Groups
{
    public class GetGroupListRequest
    {
        public string UserName { get; set; }
        public string Query { get; set; }
        public GroupOwnerTypeEnum OwnerType { get; set; }
        public GroupAccessEnum AccessType { get; set; }
    }
}
