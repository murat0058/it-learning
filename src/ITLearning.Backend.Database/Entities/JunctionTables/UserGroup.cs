namespace ITLearning.Backend.Database.Entities.JunctionTables
{
    public class UserGroup
    {
        public int Id { get; set; }
        public virtual User User { get; set; }

        public virtual Group Group { get; set; }
    }
}