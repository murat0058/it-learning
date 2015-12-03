using ITLearning.Contract.Data.Model.User;
using ITLearning.Contract.Data.Requests;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Data.Results.FileUpload;
using Microsoft.AspNet.Http;
using System.Threading.Tasks;

namespace ITLearning.Contract.Services
{
    public interface IUserService
    {
        CommonResult<UserProfileData> GetUserProfile();
        CommonResult<UserProfileData> UpdateUserProfile(UpdateUserProfileRequest requestData);
        Task<CommonResult<UploadImageResult>> SaveProfileImage(IFormFile file);
        CommonResult<CropImageResult> CropProfileImage(CropImageData cropImageData);
        CommonResult<UserProfileData> DeleteUserProfileImage();
    }
}