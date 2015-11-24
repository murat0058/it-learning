using ITLearning.Backend.Database.Entities.JunctionTables;
using System.Collections.Generic;

namespace ITLearning.Backend.Database.Entities
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
        public bool IsPrivate { get; set; }
        public User Owner { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<UserGroup> Users { get; set; }
    }
}