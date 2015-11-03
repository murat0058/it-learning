using System.Collections.Generic;

namespace ITLearning.Frontend.Web.DAL.Model
{
    public class TaskInstance
    {
        public int Id { get; set; }

        public bool IsPrivate { get; set; }
        public bool IsDeleted { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }
        public int GitRepositoryId { get; set; }
        public GitRepository GitRepository { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<TaskInstanceReview> TaskInstanceReviews { get; set; }
    }
}