using ITLearning.Contract.Enums;
using System.Collections.Generic;

namespace ITLearning.Backend.Database.Entities
{
    public class Task
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Language Language { get; set; }
        public bool IsVisibleOnlyInGroup { get; set; }
        public Group Group { get; set; }
        public User User { get; set; }

        public ICollection<TaskInstance> TaskInstances { get; set; }
    }
}