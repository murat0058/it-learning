using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AutoMapper;
using ITLearning.Frontend.Web.ViewModels.User;
using ITLearning.Contract.Services;
using ITLearning.Contract.Data.Requests;
using ITLearning.Contract.Data.Model.User;
using System.Collections.Generic;
using Microsoft.Data.Entity.Internal;

namespace ITLearning.Frontend.Web.Controllers
{
    public static class Logger
    {
        public static List<string> errors = new List<string>();
    }

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
        public IActionResult UpdateProfile(UpdateUserProfileRequest requestData)
        {
            var result = _userService.UpdateUserProfile(requestData);

            return View("Profile", Mapper.Map<UserProfileViewModel>(result.Item));
        }

        [HttpGet("GetErrors")]
        public IActionResult GetErrors()
        {
            return Json(Logger.errors.Join(","));
        }

        [HttpPost("UploadImage")]
        public async Task<string> UploadImage(IFormFile img)
        {
            try
            {
                var result = await _userService.SaveProfileImage(img);

                return JsonConvert.SerializeObject(result.Item);
            }
            catch (System.Exception ex)
            {
                Logger.errors.Add(ex.ToString());


                throw;
            }
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