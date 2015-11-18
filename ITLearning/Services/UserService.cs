using ITLearning.Frontend.Web.Contract.DAL.Repositories;
using ITLearning.Frontend.Web.Contract.Data.Results;
using ITLearning.Frontend.Web.Contract.Data.Results.FileUpload;
using ITLearning.Frontend.Web.Contract.Data.Model.User;
using ITLearning.Frontend.Web.Contract.Services;
using Microsoft.AspNet.Http;
using Microsoft.Dnx.Runtime;
using Microsoft.Net.Http.Headers;
using System.Drawing;
using System.IO;
using ITLearning.Frontend.Web.Contract.Data.Requests;
using AutoMapper;
using ITLearning.Frontend.Web.Common;

namespace ITLearning.Frontend.Web.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationEnvironment _hostingEnvironment;
        private readonly IUserRepository _userRepository;

        public UserService(IApplicationEnvironment hostingEnvironment, IUserRepository userRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _userRepository = userRepository;
        }

        public CommonResult<UserProfileData> GetUserProfile()
        {
            return _userRepository.GetUserProfile(StaticManager.UserName);
        }

        public CommonResult<UserProfileData> UpdateUserProfile(UpdateUserProfileRequestData requestData)
        {
            return _userRepository.UpdateUserProfile(StaticManager.UserName, Mapper.Map<UserProfileData>(requestData));
        }

        public async System.Threading.Tasks.Task<CommonResult<UploadImageResult>> SaveProfileImage(IFormFile file)
        {
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition)
                                                        .FileName
                                                        .Trim('"');
            var filePath = _hostingEnvironment.ApplicationBasePath + "\\wwwroot\\" + fileName;

            await file.SaveAsAsync(filePath);

            UpdateUserProfileImage(StaticManager.UserName, fileName);

            var image = Image.FromFile(filePath);

            return CommonResult<UploadImageResult>.Success(new UploadImageResult
            {
                Status = "success",
                Width = image.Width,
                Height = image.Height,
                Url = "..//" + fileName
            });
        }

        public CommonResult<CropImageResult> CropProfileImage(CropImageData cropImageData)
        {
            var originalFilePath = _hostingEnvironment.ApplicationBasePath + "\\wwwroot\\" + cropImageData.ImageUrl.Remove(0, 4);

            OnBeforeCropImage(cropImageData);

            var fileName = CropImage(StaticManager.UserName, originalFilePath, cropImageData);

            return CommonResult<CropImageResult>.Success(new CropImageResult
            {
                Status = "success",
                Url = "../Cropped/" + fileName
            });
        }

        public CommonResult<UserProfileData> DeleteUserProfileImage()
        {
            return _userRepository.UpdateUserProfileImage(StaticManager.UserName, string.Empty);
        }

        private void OnBeforeCropImage(CropImageData cropImageData)
        {
            cropImageData.ImageScaledHeight = cropImageData.ImageScaledHeight == 0 ? cropImageData.ImageOriginalHeight : cropImageData.ImageScaledHeight;
        }

        private string CropImage(string userName, string originalFilePath, CropImageData cropImageData)
        {
            var originalImage = Image.FromFile(originalFilePath);

            var resizedOriginalImage = new Bitmap(originalImage, cropImageData.ImageScaledWidth, cropImageData.ImageScaledHeight);
            var targetImage = new Bitmap(cropImageData.ImageCropWidth, cropImageData.ImageCropHeight);

            using (var graphics = Graphics.FromImage(targetImage))
            {
                graphics.DrawImage(resizedOriginalImage, 
                    new Rectangle(0,
                                  0, 
                                  cropImageData.ImageCropWidth, 
                                  cropImageData.ImageCropHeight
                    ), 
                    new Rectangle(cropImageData.ImageCropStartPointX, 
                                  cropImageData.ImageCropStartPointY, 
                                  cropImageData.ImageCropWidth, 
                                  cropImageData.ImageCropHeight
                    ), 
                    GraphicsUnit.Pixel);
            }

            var fileName = Path.GetFileName(originalFilePath);
            var folder = _hostingEnvironment.ApplicationBasePath + "\\wwwroot\\Cropped\\";
            var croppedPath = Path.Combine(folder, fileName);

            targetImage.Save(croppedPath);

            UpdateUserProfileImage(userName, fileName);

            return fileName;
        }

        private CommonResult<UserProfileData> UpdateUserProfileImage(string userName, string fileName)
        {
            return _userRepository.UpdateUserProfileImage(userName, fileName);
        }
    }
}