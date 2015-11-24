namespace ITLearning.Backend.Database.Entities
{
    public class TaskInstanceReview
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public TaskInstance TaskInstance { get; set; }
        public User User { get; set; }
    }
}