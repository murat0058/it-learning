using ITLearning.Frontend.Web.DAL.Model.JunctionTables;
using System.Collections.Generic;

namespace ITLearning.Frontend.Web.DAL.Model
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
        public bool IsPrivate { get; set; }

        public ICollection<Task> Tasks { get; set; }
        public ICollection<UserGroup> Users { get; set; }
    }
}