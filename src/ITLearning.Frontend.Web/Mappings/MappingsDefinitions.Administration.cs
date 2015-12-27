using AutoMapper;
using ITLearning.Contract.Data.Model.Administration;
using ITLearning.Frontend.Web.Core.Identity.Models;
using ITLearning.Frontend.Web.ViewModels.Administration;
using ITLearning.Frontend.Web.ViewModels.Identity;

namespace ITLearning.Frontend.Web.Mappings
{
    public static partial class MappingsDefinitions
    {
        private static void CreateAdministrationMappings()
        {
            Mapper.CreateMap<SignUpViewModel, SignUpModel>();
            Mapper.CreateMap<LoginViewModel, LoginModel>();

            Mapper.CreateMap<ClaimsViewModel, ClaimsData>();
            Mapper.CreateMap<ClaimsData, ClaimsViewModel>();
        }
    }
}
