﻿using ITLearning.Backend.Database.Entities.JunctionTables;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace ITLearning.Backend.Database.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageName { get; set; }
        public ICollection<Group> OwnedGroups { get; set; }
        public ICollection<UserGroup> Groups { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<TaskInstance> TaskInstances { get; set; }
        public ICollection<TaskInstanceReview> TaskInstanceReviews { get; set; }
    }
}