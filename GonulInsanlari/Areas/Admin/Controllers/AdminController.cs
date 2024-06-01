using AutoMapper;
using BussinessLayer.Concrete.Validations.FluentValidation;
using BussinessLayer.Concrete.Validations.FluentValidation.Admin;
using EntityLayer.Concrete.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ILogger<AdminController> _logger;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IValidator<AdminEditViewModel> _validator;
        
        ResponseModel _response;
        public AdminController(UserManager<AppUser> userManager, IMapper mapper, ILogger<AdminController> logger, RoleManager<AppRole> roleManager, ResponseModel response, IValidator<AdminEditViewModel> validator)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _roleManager = roleManager;
            _response = response;

           _validator = validator;
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

        //[HttpPost]
        //[Route("edit")]
        //public async Task<IActionResult> EditProfile(AdminEditViewModel model)
        //{

        //    if (ModelState.IsValid)
        //        if (_userManager.Users.Where(x => x.UserName == model.UserName && x.Id != model.Id) is null)
        //        {
        //            AppUser user = _mapper.Map<AppUser>(model);

        //            var result = await _validator.ValidateAsync(user);
        //            if (result.IsValid)
        //            {
        //                await _userManager.UpdateAsync(user);

        //                _response.success = true;
        //                _response.responseMessage = "Informations have been successfully updated.";

        //            }
        //            else
        //            {
        //                foreach (var error in result.Errors)
        //                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        //            }



        //        };

        //    return Json(_response);


        //        }
        //}
    }
}

