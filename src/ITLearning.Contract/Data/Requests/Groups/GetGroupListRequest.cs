using ITLearning.Contract.Enums;

namespace ITLearning.Contract.Data.Requests.Groups
{
    public class GetGroupListRequest
    {
        public string UserName { get; set; }
        public string Query { get; set; }
        public GroupOwnerTypeEnum OwnerType { get; set; }
        public GroupAccessEnum AccessType { get; set; }
        public bool AllForUser { get; set; }
    }
}