using System;
using ITLearning.Contract.Enums;

namespace ITLearning.Frontend.Web.ViewModels.Group
{
    public class SingleGroupViewModel
    {
        public int GroupId { get; set; }
        public GroupBasicDataViewModel BasicDataViewModel { get; set; }
        public GroupAccessTypeEnum AccessType { get; set; }
    }
}