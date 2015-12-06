using ITLearning.Contract.Data.Model.Branches;
using ITLearning.Contract.Data.Model.Groups;
using ITLearning.Contract.Data.Model.User;
using ITLearning.Contract.Enums;
using System.Collections.Generic;

namespace ITLearning.Contract.Data.Model.Tasks
{
    public class TaskData
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string RepositoryLink { get; set; }

        public bool IsActive { get; set; }

        public LanguageEnum SelectedLanguage { get; set; }

        public UserGroupData UserGroup { get; set; }

        public IEnumerable<BranchShortData> Branches { get; set; }

        public UserShortData Author { get; set; }

        public IEnumerable<TaskInstanceData> TaskInstances { get; set; }
    }
}