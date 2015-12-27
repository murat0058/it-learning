using Microsoft.AspNet.Http;
using System.Drawing;
using System.IO;
using AutoMapper;
using System;
using ITLearning.Contract.DataAccess.Repositories;
using ITLearning.Contract.Services;
using ITLearning.Shared;
using ITLearning.Contract.Data.Model.User;
using ITLearning.Contract.Data.Results;
using ITLearning.Contract.Providers;
using ITLearning.Contract.Data.Requests;
using ITLearning.Contract.Data.Results.FileUpload;
using Microsoft.AspNet.Hosting;
using System.Collections.Generic;

namespace ITLearning.Backend.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppConfigurationProvider _configurationProvider;

        public UserService(IUserRepository userRepository, IAppConfigurationProvider configurationProvider)
        {
            _userRepository = userRepository;
            _configurationProvider = configurationProvider;
        }

        public CommonResult<UserProfileData> GetUserProfile(string userName)
        {
            return _userRepository.GetUserProfile(userName);
        }

        public CommonResult<UserProfileData> GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public CommonResult<UserProfileData> UpdateUserProfile(UpdateUserProfileRequest requestData)
        {
            return _userRepository.UpdateUserProfile(StaticManager.UserName, Mapper.Map<UserProfileData>(requestData));
        }

        public async System.Threading.Tasks.Task<CommonResult<UploadImageResult>> SaveProfileImage(IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString() + ".jpg";

            var filePath = _configurationProvider.GetProfileOriginalImagesFolderPath() + fileName;

            await file.SaveAsAsync(filePath);

            UpdateUserProfileImage(StaticManager.UserName, fileName);

            var image = Image.FromFile(filePath);

            return CommonResult<UploadImageResult>.Success(new UploadImageResult
            {
                Status = "success",
                Width = image.Width,
                Height = image.Height,
                Url = _configurationProvider.GetProfileOriginalImagesFolderInternalPath() + fileName
            });
        }

        public CommonResult<CropImageResult> CropProfileImage(CropImageData cropImageData)
        {
            var originalFilePath = _configurationProvider.GetHostingEnvironmentWWWRootPath() + cropImageData.ImageUrl;

            OnBeforeCropImage(cropImageData);

            var fileName = CropImage(StaticManager.UserName, originalFilePath, cropImageData);

            return CommonResult<CropImageResult>.Success(new CropImageResult
            {
                Status = "success",
                Url = _configurationProvider.GetProfileCroppedImagesFolderInternalPath() + fileName
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
            var folder = _configurationProvider.GetProfileCroppedImagesFolderPath();
            var croppedPath = Path.Combine(folder, fileName);

            if (File.Exists(croppedPath))
            {
                croppedPath = Path.Combine(folder, Guid.NewGuid().ToString());
            }

            targetImage.Save(croppedPath);

            UpdateUserProfileImage(userName, fileName);

            return fileName;
        }

        private CommonResult<UserProfileData> UpdateUserProfileImage(string userName, string fileName)
        {
            return _userRepository.UpdateUserProfileImage(userName, fileName);
        }

        public CommonResult<IEnumerable<UserProfileData>> GetAllUsersProfileData()
        {
            return _userRepository.GetAllUsersProfileData();
        }
    }
}