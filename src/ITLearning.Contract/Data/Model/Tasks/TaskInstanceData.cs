using ITLearning.Contract.Data.Model.CodeReview;
using ITLearning.Contract.Data.Model.User;

namespace ITLearning.Contract.Data.Model.Tasks
{
    public class TaskInstanceData
    {
        public int Id { get; set; }

        public UserShortData User { get; set; }

        public string StartDate { get; set; }

        public bool CodeReviewExist { get; set; }

        public bool IsFinished { get; set; }

        public string FinishDate { get; set; }

        public string RepositoryLink { get; set; }

        public CodeReviewData CodeReview { get; set; }
    }
}