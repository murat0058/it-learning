using ITLearning.Frontend.Web.Core.Identity.Models;
using System.Collections.Generic;

namespace ITLearning.Frontend.Web.DAL.Model
{
    public class GitRepositoryEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public bool IsBare { get; set; }
        public bool IsPublic { get; set; }
        public bool IsAnonymousPushAllowed { get; set; }

        public List<User> User { get; set; }

        public List<GitBranchEntity> Branches { get; set; }
    }
}