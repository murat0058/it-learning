namespace ITLearning.Contract.Data.Requests.Repositories
{
    public class CloneRepositoryRequestData
    {
        public string SourceRepositoryName { get; set; }
        public string NewRepositoryName { get; set; }
        public string OwnerName { get; set; }
        public string NewUserName { get; set; }
    }
}