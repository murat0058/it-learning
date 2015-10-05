using ITLearning.Frontend.Web.Core.Identity.Models;

namespace ITLearning.Frontend.Web.DAL.Model
{
    public class BranchEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public bool CanPull { get; set; }
        public bool CanPush { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int RepositoryId { get; set; }
        public RepositoryEntity Repository { get; set; }
    }
}