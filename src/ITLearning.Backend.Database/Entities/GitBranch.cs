namespace ITLearning.Backend.Database.Entities
{
    public class GitBranch
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string LastSHA { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }

        public virtual GitRepository Repository { get; set; }
    }
}