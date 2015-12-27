namespace ITLearning.Frontend.Web.Mappings
{
    public static partial class MappingsDefinitions
    {
        public static void CreateMappings()
        {
            CreateTasksMappings();
            CreateGroupsMappings();
            CreateAdministrationMappings();
            CreateUserMappings();
            CreateNewsMappings();
            CreateGitBranchMappings();
        }
    }
}