using ITLearning.Contract.Enums;

namespace ITLearning.Contract.Data.Requests.Tasks
{
    public class GetTasksListRequestData
    {
        public string Query { get; set; }
        public TaskOwnerTypeEnum OwnerType { get; set; }
        public LanguageFilterEnum Language { get; set; }
        public TaskActivityStatusEnum ActivityStatus { get; set; }
    }
}