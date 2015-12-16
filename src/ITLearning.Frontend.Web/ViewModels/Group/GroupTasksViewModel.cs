using ITLearning.Contract.Data.Model.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace ITLearning.Frontend.Web.ViewModels.Group
{
    public class GroupTasksViewModel
    {
        public IEnumerable<TaskListItemData> Tasks { get; set; }
    }
}