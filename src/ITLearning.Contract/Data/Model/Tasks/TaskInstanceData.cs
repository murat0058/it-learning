using ITLearning.Contract.Data.Model.Branches;
using ITLearning.Contract.Data.Model.CodeReview;
using ITLearning.Contract.Data.Model.User;
using System.Collections.Generic;

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

        public IEnumerable<BranchShortData> Branches { get; set; }
    }
}