using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.ViewModels.Group
{
    public class PasswordEntryViewModel
    {
        public GroupBasicDataViewModel BasicDataViewModel { get; set; }
        public PasswordEntryDataViewModel PasswordEntryDataViewModel { get; set; }
    }
}
