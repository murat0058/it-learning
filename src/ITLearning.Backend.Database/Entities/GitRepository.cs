using System.Collections.Generic;

namespace ITLearning.Backend.Database.Entities
{
    public class GitRepository
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public bool IsBare { get; set; }
        public bool IsAnonymousPushAllowed { get; set; }
        public bool IsDeleted { get; set; }

        public virtual GitRepository SourceGitRepository { get; set; }
        public int? TaskInstanceId { get; set; }
        public virtual TaskInstance TaskInstance { get; set; }
        public int? TaskId { get; set; }
        public virtual Task Task { get; set; }

        public virtual ICollection<GitBranch> Branches { get; set; }
    }
}