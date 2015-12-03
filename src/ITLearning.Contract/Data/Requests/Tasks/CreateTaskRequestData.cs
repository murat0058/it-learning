using ITLearning.Contract.Data.Model;
using ITLearning.Contract.Data.Model.Branches;
using ITLearning.Contract.Data.Model.Groups;
using ITLearning.Contract.Enums;
using System.Collections.Generic;

namespace ITLearning.Contract.Data.Requests.Tasks
{
    public class CreateTaskRequestData
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string RepositoryLink { get; set; }

        public bool IsActive { get; set; }

        public LanguageEnum SelectedLanguage { get; set; }
        public IEnumerable<EnumDisplayData> AvailableLanguages { get; set; }

        public IEnumerable<UserGroupData> UserGroups { get; set; }

        public int SelectedGroupId { get; set; }

        public IEnumerable<BranchShortData> Branches { get; set; }
    }
}