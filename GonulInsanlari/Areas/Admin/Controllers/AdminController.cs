using AutoMapper;
using BussinessLayer.Concrete.Validations.FluentValidation;
using BussinessLayer.Concrete.Validations.FluentValidation.Admin;
using EntityLayer.Concrete.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using ViewModelLayer.Models.Tools;
using ViewModelLayer.ViewModels.Admin;

namespace GonulInsanlari.Areas.Admin.Controllers
{

    [Area(nameof(Admin))]
    [Route("admin/u")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<AppRole> _roleManager;

        ResponseModel _response;
        public AdminController(UserManager<AppUser> userManager, IMapper mapper, RoleManager<AppRole> roleManager, ResponseModel response)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _response = response;
        }

        [Route("profile")]
        public async Task<IActionResult> GetProfile()
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);

            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            if (user is not null)
            {
                AdminProfileViewModel model = _mapper.Map<AdminProfileViewModel>(user);
                model.Roles = userRoles.ToList();

                ViewData["DayPassed"] = DateTime.Now.Subtract(model.Registered).Days;

                return View(model);

            }
            return BadRequest();
        }

        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            AdminEditViewModel model = _mapper.Map<AdminEditViewModel>(user);

            return Json(model);

        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> EditProfile(AdminEditViewModel model)
        {
            List<string> errors = new();

            if (ModelState.IsValid)
            {

                if (await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == model.UserName && x.Id != model.Id) is null)
                {
                    AppUser user = await _userManager.FindByIdAsync(model.Id.ToString());

                    user.UserName = model.UserName;
                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.Email = model.Email;
                    user.AboutMe = model.AboutMe;
                    user.Age = model.Age;
                    user.PhoneNumber = model.PhoneNumber;

                    await _userManager.UpdateAsync(user);

                    _response.success = true;
                    _response.responseMessage = "Informations have been successfully updated.";

                }
                else
                {
                    errors.Add("This username is in used.");
                }

            }
            else
            {


                foreach (var error in ModelState.Values)
                {
                    foreach (var item in error.Errors)
                    {
                        errors.Add(item.ErrorMessage);
                    }
                }
            }


            return Json(new
            {
                success = _response.success,
                responseMessage = _response.responseMessage,
                data = model,
                errors = errors


            });


        }


        [HttpGet]
        [Route("edit/picture")]
        public async Task<IActionResult> EditProfilePicture()
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user?.ImagePath is not null)
                return Json(user.ImagePath);

            return NotFound();

        }

        [HttpPost]
        [Route("edit/picture")]
        public async Task<IActionResult> EditProfilePicture(IFormFile file)
        {


            var user = await _userManager.GetUserAsync(HttpContext.User);


            _response.success = false;
            _response.responseMessage = "Something went wrong...";


            if (ImageUpload.CheckFileSize(file))
            {
                user.ImagePath = await ImageUpload.UploadAsync(file);

                await _userManager.UpdateAsync(user);

                _response.success = true;
                _response.responseMessage = "Profile picture has been succesfully changed.";
                _response.Data = user.ImagePath;
            }
            else
            {
                _response.success = false;
                _response.responseMessage = "Please, check image size.";
            }


            return Json(_response);

        }

        [HttpPost]
        [Route("changePassword")]
        public async Task<IActionResult> ChangePassword(AdminChangePasswordViewModel model)
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if(result.Succeeded)
            {
                _response.responseMessage = "Password has been successfully changed.";
                _response.success = true;
            }
            else
            {
                _response.responseMessage = "Something went wrong...";
            }

            return Json(_response);

        }
    }
}


