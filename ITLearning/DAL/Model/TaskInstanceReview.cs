namespace ITLearning.Frontend.Web.DAL.Model
{
    public class TaskInstanceReview
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int TaskInstanceId { get; set; }
        public TaskInstance TaskInstance { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}