using ITLearning.Frontend.Web.DAL.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace ITLearning.Frontend.Web.Core.Identity.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<GitBranchEntity> Branches { get; set; }

        public List<GitRepositoryEntity> Repositories { get; set; }
    }
}