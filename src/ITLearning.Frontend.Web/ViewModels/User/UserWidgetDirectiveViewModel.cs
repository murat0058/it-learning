using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.ViewModels.User
{
    public class UserWidgetDirectiveViewModel
    {
        public int DirectiveId { get; set;}
        public string DirectiveString { get; set; }
        public string TabType { get; set; }
        public string TabIcon { get; set; }
        public string TabTitle { get; set; }
    }
}
