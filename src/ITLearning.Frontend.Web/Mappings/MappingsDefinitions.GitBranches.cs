using AutoMapper;
using ITLearning.Backend.Database.Entities;
using ITLearning.Contract.Data.Model.Branches;

namespace ITLearning.Frontend.Web.Mappings
{
    public static partial class MappingsDefinitions
    {
        private static void CreateGitBranchMappings()
        {
            Mapper.CreateMap<GitBranch, BranchShortData>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DisplayName));
        }
    }
}
