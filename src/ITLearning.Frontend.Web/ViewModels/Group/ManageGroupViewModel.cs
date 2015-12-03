using ITLearning.Contract.Enums;

namespace ITLearning.Frontend.Web.ViewModels.Group
{
    public class ManageGroupViewModel
    {
        public int GroupId { get; set; }
        public GroupBasicDataViewModel BasicDataViewModel { get; set; }
        public UpdateGroupViewModel UpdateGroupViewModel { get; set; }
        public GroupAccessTypeEnum AccessType { get; set; }
    }
}
