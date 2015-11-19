using ITLearning.Frontend.Web.Contract.Data.Requests;
using ITLearning.Frontend.Web.Contract.Data.Model.User;
using ITLearning.Frontend.Web.Contract.Services;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AutoMapper;
using ITLearning.Frontend.Web.ViewModels.User;

namespace ITLearning.Frontend.Web.Controllers
{
    [Route("User")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Profile")]
        public IActionResult Profile()
        {
            var result = _userService.GetUserProfile();

            return View(Mapper.Map<UserProfileViewModel>(result.Item));
        }

        [HttpPost("UpdateProfile")]
        public IActionResult UpdateProfile(UpdateUserProfileRequestData requestData)
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
            var result = _userService.CropProfileImage(new CropImageData {
                ImageUrl = imgUrl,
                ImageOriginalWidth = imgInitW,
                ImageOriginalHeight = imgInitH,
                ImageScaledWidth = (int)imgW,
                ImageScaledHeight = (int)imgH,
                ImageCropStartPointY = imgY1,
                ImageCropStartPointX =  imgX1,
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