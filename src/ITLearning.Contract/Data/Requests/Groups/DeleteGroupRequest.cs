using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.Data.Requests.Groups
{
    public class DeleteGroupRequest
    {
        public string UserName { get; set; }
        public int GroupId { get; set; }
    }
}
