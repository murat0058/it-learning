using System;
using ITLearning.Contract.Enums;
using ITLearning.Contract.Data.Model.Tasks;

namespace ITLearning.Frontend.Web.ViewModels.Group
{
    public class SingleGroupViewModel
    {
        public int GroupId { get; set; }
        public GroupBasicDataViewModel BasicDataViewModel { get; set; }
        public GroupTasksViewModel GroupTasks { get; set; }
        public GroupAccessTypeEnum AccessType { get; set; }
    }
}