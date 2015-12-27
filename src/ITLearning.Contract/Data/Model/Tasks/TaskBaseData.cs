using ITLearning.Contract.Data.Model.Branches;
using ITLearning.Contract.Data.Model.Groups;
using ITLearning.Contract.Data.Model.User;
using ITLearning.Contract.Enums;
using System.Collections.Generic;

namespace ITLearning.Contract.Data.Model.Tasks
{
    public class TaskBaseData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string RepositoryLink { get; set; }

        public bool IsActive { get; set; }

        public UserShortData Author { get; set; }

        public LanguageEnum Language { get; set; }

        public UserGroupData Group { get; set; }

        public IEnumerable<BranchShortData> Branches { get; set; }
    }
}