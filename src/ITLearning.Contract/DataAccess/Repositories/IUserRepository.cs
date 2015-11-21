using ITLearning.Contract.Data.Model.User;
using ITLearning.Contract.Data.Results;

namespace ITLearning.Contract.DataAccess.Repositories
{
    public interface IUserRepository
    {
        CommonResult<UserProfileData> GetUserProfile(string userName);
        CommonResult<UserProfileData> UpdateUserProfile(string userName, UserProfileData requestData);
        CommonResult<UserProfileData> UpdateUserProfileImage(string userName, string fileName);
    }
}