using AutoMapper;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.Tools;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public AdminController(UserManager<AppUser> userManager, IMapper mapper, ILogger<AdminController> logger, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _roleManager = roleManager;
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



    }
}
