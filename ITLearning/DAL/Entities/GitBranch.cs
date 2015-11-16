namespace ITLearning.Frontend.Web.DAL.Entities
{
    public class GitBranch
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string LastSHA { get; set; }
        public bool IsVisible { get; set; }

        public int RepositoryId { get; set; }
        public GitRepository Repository { get; set; }
    }
}