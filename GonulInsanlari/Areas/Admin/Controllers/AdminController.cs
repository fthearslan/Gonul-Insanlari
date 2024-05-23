using AutoMapper;
using EntityLayer.Concrete.Entities;
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

            IList<string> userRoles =await _userManager.GetRolesAsync(user);
            
           
            if (user is not null)
            {
                AdminProfileViewModel model = new();

                try
                {
                    model = _mapper.Map<AdminProfileViewModel>(user);
                    model.Roles = userRoles.ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);

                    return BadRequest();
                }

                return View(model);

            }

            return BadRequest();
        }
    }
}
