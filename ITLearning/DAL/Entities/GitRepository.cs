using System.Collections.Generic;

namespace ITLearning.Frontend.Web.DAL.Entities
{
    public class GitRepository
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public bool IsBare { get; set; }
        public bool IsAnonymousPushAllowed { get; set; }

        public int SourceGitRepositoryId { get; set; }
        public GitRepository SourceGitRepository { get; set; }
        public int TaskInstanceId { get; set; }
        public TaskInstance TaskInstance { get; set; }

        public ICollection<GitBranch> Branches { get; set; }
    }
}