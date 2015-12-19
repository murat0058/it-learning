using ITLearning.Contract.Data.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.ViewModels.Administration
{
    public class UsersListViewModel
    {
        public IEnumerable<UserProfileData> Users { get; set; }
    }
}
