using AutoMapper;
using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Authorization;
using GonulInsanlari.Enums;
using GonulInsanlari.Extensions.Admin;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using ViewModelLayer.Models.Tools;
using ViewModelLayer.ViewModels.Permission;
using ViewModelLayer.ViewModels.Role;

namespace GonulInsanlari.Areas.Admin.Controllers
{

    [Area(nameof(Admin))]
    [Route("admin/roles")]
    public class RoleController : Controller
    {

        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private ResponseModel _response;
        private readonly IConfiguration _config;

        public RoleController(RoleManager<AppRole> roleManager, IMapper mapper, UserManager<AppUser> userManager, ResponseModel response, IConfiguration config)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
            _response = response;
            _config = config;
        }

        [Route("list")]
        [HasPermission(Enums.PermissionType.Role, Permission.Read)]
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
            AppUser? user = await _userManager.FindByIdAsync(userId);


            if (user is null)
                return NotFound();

            IList<string> roles = await _userManager.GetRolesAsync(user);


            if (roles.Count <= 1)
                return BadRequest("This user cannot be removed, it has only one role.");

                var result = await _userManager.RemoveFromRoleAsync(await _userManager.FindByIdAsync(userId), roleName);

            if (!result.Succeeded)
                return BadRequest(result?.Errors?.First().Description); 

            return Ok();


        }


        [Route("permissions/{roleId}")]
        public async Task<IActionResult> GetPermissions(int roleId)
        {

            List<string>? rolePermissions = await _roleManager.GetPermissionsAsync(roleId);

            List<PermissionViewModel>? appPermissions = _config
                .GetSection("AppPermissions")
                .Get<List<PermissionViewModel>>();

            List<ListPermissionViewModel> model = new();

            appPermissions?.ForEach(x =>
            {

                x?.Permissions?.ForEach(p =>
                {

                    switch (rolePermissions.Contains(p))
                    {
                        case true:
                            model.Add(new()
                            {
                                Type = x.Type,
                                Permission = p,
                                Exists = true,
                            });
                            break;

                        case false:
                            model.Add(new()
                            {
                                Type = x.Type,
                                Permission = p,
                                Exists = false,
                            });

                            break;

                    }

                });


            });

            return Json(model);

        }

        [Route("permissions/manage")]
        public async Task<IActionResult> ManagePermissions(List<string> permissions, int roleId)
        {

            bool result;

            List<string> rolePermissions = await _roleManager.GetPermissionsAsync(roleId);

            if (rolePermissions is null)
            {
                result = _roleManager.AddPermission(permissions, roleId);

            }
            else
            {
                List<string> permissionsRemoved = rolePermissions
               .Except(permissions)
               .ToList();

                if (permissionsRemoved is not null)
                    _roleManager.RemovePermission(permissionsRemoved, roleId);



                List<string> permissionsAdded = new();
                permissions.ForEach(perm =>
            {

                if (!rolePermissions.Contains(perm))
                    permissionsAdded.Add(perm);

            });

                result = _roleManager.AddPermission(permissionsAdded, roleId);

            }

          if(result)
            return Ok();

          return BadRequest();
            

        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddRole(CreateRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = new();

                if (model.Name is not null)
                    if (await _roleManager.RoleExistsAsync(model.Name))
                        errors.Add("Role name is in used.");

                foreach (var errorValue in ModelState.Values)
                {
                    foreach (var error in errorValue.Errors)
                    {
                        errors.Add(error.ErrorMessage);

                    }
                }

                _response.success = false;
                _response.Data = errors;

                return Json(_response);

            }

            var result = await _roleManager.CreateAsync(new() { Name = model.Name, Description = model.RoleDescription });

            if (result.Succeeded)
                _response.success = true;

            if (!result.Succeeded)
                _response.Data = result.Errors;

            return Json(_response);


        }

        [HttpPost]
        [Route("delete/{roleId}")]
        public async Task<IActionResult> DeleteRole(int roleId)
        {

            AppRole? role = await _roleManager.Roles.SingleOrDefaultAsync(x => x.Id == roleId);
            if (role is null)
                return Json(_response.success = false);

            IList<AppUser> usersToMoved = await _userManager.GetUsersInRoleAsync(role.Name);

            if (usersToMoved is not null)
            {
                foreach (AppUser user in usersToMoved)
                {

                    var Result = await _userManager.AddToRoleAsync(user, "Admin");

                    if (!Result.Succeeded)
                        continue;

                }

            }


            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
                return Json(_response.success = true);

            _response.success = false;
            _response.Data = result.Errors;
            return Json(_response);

        }


        [HttpGet]
        [Route("getUsers/{Id}")]

        public async Task<IActionResult> GetUsersToAdd(int Id)
        {

            AppRole? role = await _roleManager.Roles.SingleOrDefaultAsync(x => x.Id == Id);

            if (role is null)
                return Json(404);


            var users = _userManager
              .Users.
              ToList();

            List<AddUserToRoleViewModel> model = new();

            foreach (AppUser user in users)
            {
                if (!await _userManager.IsInRoleAsync(user, role.Name))
                    model.Add(new() { Id = user.Id, Username = user.UserName, ImagePath = user.ImagePath, });

                continue;

            }

            return Json(model);
        }


        [HttpPost]
        [Route("addUser/{roleId}")]
        public async Task<IActionResult> AddUser(List<string> Users, int roleId)
        {

            if (Users is null)
                return Json(404);

            AppRole? role = await _roleManager.Roles.SingleOrDefaultAsync(x => x.Id == roleId);

            if (role is null)
                return Json(404);


            foreach (string userId in Users)
            {

                AppUser? user = await _userManager.GetUsersWithRoles(userId);

                if (user is null)
                    continue;

                if (user.Roles.Count >= 3)
                {
                    _response.responseMessage = "A user cannot have more than 3 roles at the same time.";
                    _response.success = false;

                    return Json(_response);
                }

                var result = await _userManager.AddToRoleAsync(user, role.Name);

                if (!result.Succeeded)
                    _response.responseMessage = "Something went wrong";

            }
            _response.success = true;

            return Json(_response);
        }


    }
}
