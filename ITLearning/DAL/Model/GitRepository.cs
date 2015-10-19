using System.Collections.Generic;

namespace ITLearning.Frontend.Web.DAL.Model
{
    public class GitRepository
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public bool IsBare { get; set; }
        public bool IsPublic { get; set; }
        public bool IsAnonymousPushAllowed { get; set; }
        public bool IsDeleted { get; set; }
        public string SourceRepositoryName { get; set; }

        public ICollection<GitRepositoryUser> Users { get; set; }

        public ICollection<GitBranch> Branches { get; set; }
    }
}