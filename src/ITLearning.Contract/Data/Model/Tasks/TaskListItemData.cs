namespace ITLearning.Contract.Data.Model.Tasks
{
    public class TaskListItemData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public string Language { get; set; }
        public string GroupName { get; set; }
        public bool IsCompleted { get; set; }
    }
}