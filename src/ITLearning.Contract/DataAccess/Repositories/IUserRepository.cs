using ITLearning.Contract.Data.Model.User;
using ITLearning.Contract.Data.Results;
using System.Collections.Generic;

namespace ITLearning.Contract.DataAccess.Repositories
{
    public interface IUserRepository
    {
        CommonResult<UserProfileData> GetUserById(int id);
        CommonResult<UserProfileData> GetUserProfile(string userName);
        CommonResult<UserProfileData> UpdateUserProfile(string userName, UserProfileData requestData);
        CommonResult<UserProfileData> UpdateUserProfileImage(string userName, string fileName);
        CommonResult<IEnumerable<UserProfileData>> GetAllUsersProfileData();
        CommonResult<string> GetUserPassword(string userName);
    }
}