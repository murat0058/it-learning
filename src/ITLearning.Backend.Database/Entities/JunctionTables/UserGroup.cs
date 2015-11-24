namespace ITLearning.Backend.Database.Entities.JunctionTables
{
    public class UserGroup
    {
        public int Id { get; set; }
        public User User { get; set; }

        public Group Group { get; set; }
    }
}