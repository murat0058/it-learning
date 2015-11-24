using System.Collections.Generic;

namespace ITLearning.Backend.Database.Entities
{
    public class TaskInstance
    {
        public int Id { get; set; }

        public bool IsPrivate { get; set; }
        public bool IsDeleted { get; set; }

        public Task Task { get; set; }
        public GitRepository GitRepository { get; set; }
        public User User { get; set; }

        public ICollection<TaskInstanceReview> TaskInstanceReviews { get; set; }
    }
}