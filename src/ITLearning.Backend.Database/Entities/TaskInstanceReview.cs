namespace ITLearning.Backend.Database.Entities
{
    public class TaskInstanceReview
    {
        public int Id { get; set; }

        public int ArchitectureRate { get; set; }
        public int OptymizationRate { get; set; }
        public int CleanCodeRate { get; set; }
        public string Comment { get; set; }

        public virtual TaskInstance TaskInstance { get; set; }
    }
}