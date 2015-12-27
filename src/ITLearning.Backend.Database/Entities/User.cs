using ITLearning.Backend.Database.Entities.JunctionTables;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace ITLearning.Backend.Database.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageName { get; set; }

        public virtual ICollection<Group> OwnedGroups { get; set; }
        public virtual ICollection<UserGroup> Groups { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<TaskInstance> TaskInstances { get; set; }
    }
}