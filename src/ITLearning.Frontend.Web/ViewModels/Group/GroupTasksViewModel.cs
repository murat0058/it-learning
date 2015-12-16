using ITLearning.Contract.Data.Model.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace ITLearning.Frontend.Web.ViewModels.Group
{
    public class GroupTasksViewModel
    {
        public IEnumerable<TaskListItemData> Item { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}