using ITLearning.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.ViewModels.Group
{
    public class GetGroupsListViewModel
    {
        public string Query { get; set; }
        public GroupOwnerTypeEnum OwnerType { get; set; }
        public GroupAccessEnum AccessType { get; set; }

    }
}
