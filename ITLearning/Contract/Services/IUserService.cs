using System.Threading.Tasks;
using ITLearning.Frontend.Web.Contract.Data.Requests;
using ITLearning.Frontend.Web.Contract.Data.Results;
using ITLearning.Frontend.Web.Contract.Data.Results.FileUpload;
using ITLearning.Frontend.Web.Contract.Data.Model.User;
using Microsoft.AspNet.Http;

namespace ITLearning.Frontend.Web.Contract.Services
{
    public interface IUserService
    {
        CommonResult<UserProfileData> GetUserProfile();
        CommonResult<UserProfileData> UpdateUserProfile(UpdateUserProfileRequestData requestData);
        Task<CommonResult<UploadImageResult>> SaveProfileImage(IFormFile file);
        CommonResult<CropImageResult> CropProfileImage(CropImageData cropImageData);
        CommonResult<UserProfileData> DeleteUserProfileImage();
    }
}