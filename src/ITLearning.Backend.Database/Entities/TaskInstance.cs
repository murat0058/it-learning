using System;

namespace ITLearning.Backend.Database.Entities
{
    public class TaskInstance
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public bool CodeReviewExist { get; set; }
        public bool IsFinished { get; set; }

        public virtual Task Task { get; set; }
        public virtual GitRepository GitRepository { get; set; }
        public virtual User User { get; set; }
        public virtual TaskInstanceReview TaskInstanceReview { get; set; }
    }
}