using ITLearning.Contract.Data.Model.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.Data.Results.Groups
{
    public class GetTasksForGroupResult
    {
        public IEnumerable<TaskListItemData> Tasks { get; set; }
    }
}
