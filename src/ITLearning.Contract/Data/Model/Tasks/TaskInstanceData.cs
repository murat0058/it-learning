using ITLearning.Contract.Data.Model.User;

namespace ITLearning.Contract.Data.Model.Tasks
{
    public class TaskInstanceData
    {
        public UserShortData User { get; set; }

        public string CreateDate { get; set; }

        public bool CodeReviewExist { get; set; }

        public bool Finished { get; set; }

        public string FinishDate { get; set; }

        public string RepositoryLink { get; set; }

        public CodeReviewData CodeReview { get; set; }
    }
}