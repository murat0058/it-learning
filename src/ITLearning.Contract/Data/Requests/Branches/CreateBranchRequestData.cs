namespace ITLearning.Contract.Data.Requests.Branches
{
    public class CreateBranchRequestData
    {
        public string RepositoryName { get; set; }
        public string BranchName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
    }
}
