using ITLearning.Frontend.Web.Core.Identity.Models;

namespace ITLearning.Frontend.Web.DAL.Model
{
    public class GitRepositoryUser
    {
        public int GitRepositoryId { get; set; }
        public GitRepository GitRepository { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}