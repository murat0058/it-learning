using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.Data.Results.Tasks
{
    public class TaskInstancesForUserResult
    {
        public string UserName { get; set; }
        public int TaskId { get; set; }
        public int TaskInstanceId { get; set; }

        public bool IsPrivate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsCompleted { get; set; }
    }
}
