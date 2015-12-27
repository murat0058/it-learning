using ITLearning.Contract.Enums;
using System;
using System.Collections.Generic;

namespace ITLearning.Backend.Database.Entities
{
    public class Task
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public LanguageEnum Language { get; set; }
        public DateTime DateOfCreation { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
        public virtual GitRepository GitRepository { get; set; }

        public virtual ICollection<TaskInstance> TaskInstances { get; set; }
    }
}