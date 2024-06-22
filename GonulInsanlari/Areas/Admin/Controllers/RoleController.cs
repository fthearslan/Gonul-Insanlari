using AutoMapper;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using ViewModelLayer.Models.Tools;
using ViewModelLayer.ViewModels.Role;

namespace GonulInsanlari.Areas.Admin.Controllers
{

    [Area(nameof(Admin))]
    [Authorize(Roles = "SuperAdmin")]
    [Route("admin/roles")]
    public class RoleController : Controller
    {

        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private ResponseModel _response;


        public RoleController(RoleManager<AppRole> roleManager, IMapper mapper, UserManager<AppUser> userManager, ResponseModel response)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
            _response = response;
        }

        [Route("list")]
        public async Task<IActionResult> GetRoles()
        {


            var roles = await _roleManager.Roles.ToListAsync();

            if (roles is null)
                return NotFound();
          

            List<ListRolesViewModel> model = _mapper.Map<List<ListRolesViewModel>>(roles);

            model.First().IsActive = "active";

            return View(model);



        }


        [Route("remove")]
        public async Task<IActionResult> RemoveUser(string userId, string roleName)
        {
            var result = await _userManager.RemoveFromRoleAsync(await _userManager.FindByIdAsync(userId), roleName);

            _response.responseMessage = $"The user has successfully been removed from the role named {roleName}.";
            _response.success = true;

            if (!result.Succeeded)
            {
                _response.responseMessage = "Something went wrong...";
                _response.success = false;
            }


            return Json(_response);


        }






    }
}
