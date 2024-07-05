using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete.Validations.FluentValidation;
using BussinessLayer.Concrete.Validations.FluentValidation.Admin;
using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using FluentValidation;
using GonulInsanlari.Areas.Admin.Authorization;
using GonulInsanlari.Enums;
using GonulInsanlari.Extensions.Admin;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Debugger.Contracts.HotReload;
using Org.BouncyCastle.Crypto;
using System.Net.Mail;
using System.Runtime.Intrinsics.X86;
using ViewModelLayer.Models.Tools;
using ViewModelLayer.ViewModels.Admin;
using X.PagedList;

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

            if (user is null)
                return NotFound();

            IList<string> userRoles = await _userManager.GetRolesAsync(user);



            AdminProfileViewModel model = _mapper.Map<AdminProfileViewModel>(user);
            model.Roles = userRoles;

            ViewData["DayPassed"] = DateTime.Now.Subtract(model.Registered).Days;

            return View(model);


        }


        [HasPermission(PermissionType.User, Permission.Read)]
        [Route("user/{username}")]
        public async Task<IActionResult> GetDetails(string userName)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);

            if (user is null)
                return NotFound();



            AdminProfileViewModel model = _mapper.Map<AdminProfileViewModel>(user);
            model.IsUser = await _userManager.GetUserAsync(User) == user;
            model.Roles = await _userManager.GetRolesAsync(user);

            return View(model);

        }

        [HttpPost]
        [Route("register/new")]
        [HasPermission(PermissionType.User, Permission.Create)]
        public async Task<IActionResult> Register(AdminRegisterViewModel model)
        {
            List<string> modelErrors = new();

            if (ModelState.IsValid)
            {

                AppUser user = _mapper.Map<AppUser>(model);

                IdentityResult result = await _userManager.CreateAsync(user, "Admin123@");

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        modelErrors.Add(error.Description);
                    return BadRequest(modelErrors);

                }
             
                return StatusCode(200);

            }

            foreach (var Errors in ModelState.Values)
                foreach (var error in Errors.Errors)
                {
                    modelErrors.Add(error.ErrorMessage);
                }

            return BadRequest(modelErrors);

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
        [HasPermission(PermissionType.User, Permission.Update)]
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
            if (result.Succeeded)
            {
                _response.responseMessage = "Password has been successfully changed.";
                _response.success = true;
            }
            else
            {
                List<string> errors = new();

                foreach (var error in result.Errors.ToList())
                {
                    errors.Add(error.Description);
                }

                _response.success = false;
                _response.Data = errors;
            }

            return Json(_response);

        }



        [HttpGet]
        [Route("getuserlogins/{userId}")]
        public async Task<IActionResult> GetUserLogins(int userId)
        {

            var user =
               await _userManager.Users.Include(x => x.UserLogin)
                .SingleOrDefaultAsync(x => x.Id == userId);

            List<AdminUserLoginsViewModel> userLogs = new();


            user?.UserLogin.ForEach(x => userLogs.Add(new AdminUserLoginsViewModel { Description = x.Description, Type = x.Type.ToString() }));


            return Json(userLogs);
        }



        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchLogins(AdminSearchLoginViewModel model)
        {


            var user =
               await _userManager.Users.Include(x => x.UserLogin)
                .SingleOrDefaultAsync(x => x.Id == model.Id);

            List<UserLogin>? userLogins = new();

            if (model.Search is not null)
            {
                userLogins = user?.UserLogin.
                   Where(x => x.Description.Contains(model.Search) || x.Type.ToString().Contains(model.Search) || x.Date.ToString().Contains(model.Search))
                   .ToList();


            }

            List<AdminUserLoginsViewModel> values = new();


            userLogins?
                .ForEach(x => values.Add(new() { Description = x.Description, Type = x.Type.ToString() }));


            return Json(values);

        }



        [HttpGet]
        [Route("users")]
        [HasPermission(PermissionType.User, Permission.Read)]
        public async Task<IActionResult> GetUsers(int pageNumber = 1)
        {
            var users = await _userManager.GetUsersWithRolesAsync();

            List<AdminListViewModel> model = new();
            foreach (var user in await users.Include(x => x.UserLogin).ToListAsync())
            {

                model.Add(new()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Surname = user.Surname,
                    UserName = user.UserName,
                    ImagePath = user.ImagePath,
                    PhoneNumber = user.PhoneNumber,
                    Status = user.Status,
                    LastLogin = user?.UserLogin?
                    .Where(x => x.Type == LoginType.Login)
                    .OrderByDescending(x => x.Date)
                    .FirstOrDefault()?
                    .Date
                });
            }

            return View(await model.ToPagedListAsync(pageNumber, 10));

        }



        [HttpPost]
        [Route("users/delete/{id}")]
        [HasPermission(PermissionType.User, Permission.Update | Permission.Delete)]
        public async Task<IActionResult> EnableOrDisable(string id)
        {

            var user = await _userManager.FindByIdAsync(id);

            user.Status = user.Status switch
            {
                true => false,
                false => true
            };

            using (var c = new Context())
            {
                c.Users.Update(user);

                if (c.SaveChanges() > 0)
                {
                    _response.responseMessage = "The status of the user has been successfully changed.";
                    _response.success = true;
                }
                else
                {
                    _response.responseMessage = "Something went wrong";
                    _response.success = false;

                }


            }


            return Json(_response);

        }

        // DeleteUser()...














    }
}


