using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Contract.Data.Model.Groups
{
    public class GroupListedData
    {
        public IEnumerable<GroupListItem> Groups { get; set; }
    }
}
