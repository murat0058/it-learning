namespace ITLearning.Contract.Data.Model.Branches
{
    public class BranchShortEditData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public EditActionEnum EditAction { get; set; }
    }

    public enum EditActionEnum
    {
        None = 0,
        Add = 1,
        Update = 2,
        Delete = 3
    }
}