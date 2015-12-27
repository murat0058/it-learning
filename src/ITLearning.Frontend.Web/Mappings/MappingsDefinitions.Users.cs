using AutoMapper;
using ITLearning.Backend.Database.Entities;
using ITLearning.Contract.Data.Model.User;
using ITLearning.Contract.Data.Requests;
using ITLearning.Frontend.Web.ViewModels.User;

namespace ITLearning.Frontend.Web.Mappings
{
    public static partial class MappingsDefinitions
    {
        private static void CreateUserMappings()
        {
            Mapper.CreateMap<User, UserProfileData>();

            Mapper.CreateMap<User, UserShortData>();

            Mapper.CreateMap<UserProfileData, User>();

            Mapper.CreateMap<UserProfileData, UserProfileViewModel>();

            Mapper.CreateMap<UpdateUserProfileRequest, UserProfileData>();

            Mapper.CreateMap<UserProfileData, UserData>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => GetUserName(src)))
                .ForMember(dest => dest.ProfileImagePath, opt => opt.MapFrom(src => src.ProfileImagePath ?? "default.jpg"));

        }

        private static string GetUserName(UserProfileData src)
        {
            if (string.IsNullOrEmpty(src.FirstName) || string.IsNullOrEmpty(src.LastName))
            {
                return src.UserName;
            }
            else
            {
                return $"{src.FirstName} {src.LastName}";
            }
        }
    }
}