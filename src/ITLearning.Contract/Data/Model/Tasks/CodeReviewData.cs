using ITLearning.Contract.Data.Model.Branches;
using System.Collections.Generic;

namespace ITLearning.Contract.Data.Model.Tasks
{
    public class CodeReviewData
    {
        public int NumberOfActivityDays { get; set; }
        public int ArchitectureRate { get; set; }
        public int OptymizationRate { get; set; }
        public int CleanCodeRate { get; set; }
        public string Comment { get; set; }
        public IEnumerable<BranchShortData> Branches { get; set; }
    }
}
