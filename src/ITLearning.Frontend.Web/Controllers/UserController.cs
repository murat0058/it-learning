using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AutoMapper;
using ITLearning.Frontend.Web.ViewModels.User;
using ITLearning.Contract.Services;
using ITLearning.Contract.Data.Requests;
using ITLearning.Contract.Data.Model.User;
using ITLearning.Contract.Providers;
using ITLearning.Frontend.Web.Controllers.Base;
using ITLearning.Shared;

namespace ITLearning.Frontend.Web.Controllers
{
    [Route("User")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IAppConfigurationProvider _configurationProvider;

        public UserController(IUserService userService, IAppConfigurationProvider configurationProvider)
        {
            _userService = userService;
            _configurationProvider = configurationProvider;
        }

        [HttpGet("PublicProfile/{userName}")]
        public IActionResult PublicProfile(string userName)
        {
            var result = _userService.GetUserProfile(userName);

            return View(result.Item);
        }

        [HttpGet("Profile")]
        public IActionResult Profile()
        {
            var result = _userService.GetUserProfile(StaticManager.UserName);

            return View(Mapper.Map<UserProfileViewModel>(result.Item));
        }

        [HttpPost("UpdateProfile")]
        public IActionResult UpdateProfile(UpdateUserProfileRequest requestData)
        {
            var result = _userService.UpdateUserProfile(requestData);

            return View("Profile", Mapper.Map<UserProfileViewModel>(result.Item));
        }

        [HttpPost("UploadImage")]
        public async Task<string> UploadImage(IFormFile img)
        {
            var result = await _userService.SaveProfileImage(img);

            return JsonConvert.SerializeObject(result.Item);
        }

        [HttpPost("CropImage")]
        public string CropImage(string imgUrl, int imgInitW, int imgInitH, double imgW, double imgH, int imgY1, int imgX1, int cropH, int cropW)
        {
            var result = _userService.CropProfileImage(new CropImageData
            {
                ImageUrl = imgUrl,
                ImageOriginalWidth = imgInitW,
                ImageOriginalHeight = imgInitH,
                ImageScaledWidth = (int)imgW,
                ImageScaledHeight = (int)imgH,
                ImageCropStartPointY = imgY1,
                ImageCropStartPointX = imgX1,
                ImageCropHeight = cropH,
                ImageCropWidth = cropW
            });

            return JsonConvert.SerializeObject(result.Item);
        }

        [HttpPost("DeleteImage")]
        public IActionResult DeleteImage()
        {
            var result = _userService.DeleteUserProfileImage();

            return RedirectToAction("Profile");
        }
    }
}