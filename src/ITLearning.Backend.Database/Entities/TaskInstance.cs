using System.Collections.Generic;

namespace ITLearning.Backend.Database.Entities
{
    public class TaskInstance
    {
        public int Id { get; set; }

        public bool IsPrivate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsCompleted { get; set; }

        public Task Task { get; set; }
        public GitRepository GitRepository { get; set; }
        public User User { get; set; }

        //TODO AB change relation to one-to-one
        public ICollection<TaskInstanceReview> TaskInstanceReviews { get; set; }

        //TODO AB add property for creation date
    }
}