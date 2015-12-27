using ITLearning.Contract.Data.Model;
using ITLearning.Contract.Data.Model.Branches;
using ITLearning.Contract.Data.Model.Groups;
using ITLearning.Contract.Enums;
using System.Collections.Generic;

namespace ITLearning.Contract.Data.Requests.Tasks
{
    public class EditTaskRequestData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string RepositoryLink { get; set; }

        public bool IsActive { get; set; }

        public EnumDisplayData SelectedLanguage { get; set; }
        public IEnumerable<EnumDisplayData> AvailableLanguages { get; set; }

        public LanguageEnum EditedLanguage { get; set; }

        public UserGroupData SelectedGroup { get; set; }

        public IEnumerable<BranchShortData> Branches { get; set; }
    }
}