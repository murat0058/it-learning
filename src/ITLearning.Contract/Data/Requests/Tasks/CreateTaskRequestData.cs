using ITLearning.Contract.Data.Model;
using ITLearning.Contract.Data.Model.Groups;
using ITLearning.Contract.Enums;
using System.Collections.Generic;

namespace ITLearning.Contract.Data.Requests.Tasks
{
    public class CreateTaskRequestData
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public LanguageEnum SelectedLanguage { get; set; }

        public int SelectedGroupId { get; set; }

        public List<EnumDisplayData> AvailableLanguages { get; set; }

        public List<UserGroupData> UserGroups { get; set; }
    }
}