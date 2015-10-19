using ITLearning.Frontend.Web.Core.Identity.Models;
using System.Collections.Generic;

namespace ITLearning.Frontend.Web.DAL.Model
{
    public class GitBranchEntity
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string LastSHA { get; set; }
        public bool IsVisible { get; set; }

        public int RepositoryId { get; set; }
        public GitRepositoryEntity Repository { get; set; }
    }
}