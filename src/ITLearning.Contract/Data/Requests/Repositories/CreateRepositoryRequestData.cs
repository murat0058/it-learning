namespace ITLearning.Contract.Data.Requests.Repositories
{
    public class CreateRepositoryRequestData
    {
        public string RepositoryName { get; set; }
        public string OwnerName { get; set; }
        public bool IsAnonymousPushAllowed { get; set; }
        public bool IsPublic { get; set; }
    }
}