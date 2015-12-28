using System.Collections.Generic;

namespace ITLearning.Contract.Data.Results.Branches
{
    public class EditBranchesResultData
    {
        public List<string> BranchesToDelete { get; set; }
        public List<string> BranchesToAdd { get; set; }

        public EditBranchesResultData()
        {
            BranchesToDelete = new List<string>();
            BranchesToAdd = new List<string>();
        }
    }
}