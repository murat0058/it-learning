using ITLearning.Frontend.Web.DAL.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace ITLearning.Frontend.Web.Core.Identity.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<BranchEntity> Branches { get; set; }

        public List<RepositoryEntity> Repositories { get; set; }
    }
}