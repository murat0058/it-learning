using ITLearning.Frontend.Web.Core.Identity.Models;
using System.Collections.Generic;

namespace ITLearning.Frontend.Web.DAL.Model
{
    public class GitRepositoryEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<GitBranchEntity> Branches { get; set; }
    }
}