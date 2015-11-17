using ITLearning.Frontend.Web.Contract.Data.Results;
using ITLearning.Frontend.Web.Contract.Data.User;

namespace ITLearning.Frontend.Web.Contract.DAL.Repositories
{
    public interface IUserRepository
    {
        CommonResult<UserProfileData> GetUserProfile(string userName);
        CommonResult<UserProfileData> UpdateUserProfile(string userName, UserProfileData requestData);
        CommonResult<UserProfileData> UpdateUserProfileImage(string userName, string fileName);
    }
}