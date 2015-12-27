namespace ITLearning.Contract.Data.Results.Tasks
{
    public class CreatedTaskResultData
    {
        public int Id { get; set; }
        public string RepositoryLink { get; set; }
        public bool ShouldActivateTask { get; set; }
    }
}