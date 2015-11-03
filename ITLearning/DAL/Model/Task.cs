using ITLearning.Frontend.Web.Contract.Enums;
using System.Collections.Generic;

namespace ITLearning.Frontend.Web.DAL.Model
{
    public class Task
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Language Language { get; set; }
        public bool IsVisibleOnlyInGroup { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int TaskCategoryId { get; set; }
        public TaskCategory TaskCategory { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<TaskInstance> TaskInstances { get; set; }
    }
}