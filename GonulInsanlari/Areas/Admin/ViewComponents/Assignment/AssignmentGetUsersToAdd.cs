﻿using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Extensions.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Threading.Tasks;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Assignment
{
    public class AssignmentGetUsersToAdd : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAssignmentService _manager;
        public AssignmentGetUsersToAdd(UserManager<AppUser> userManager, IAssignmentService manager)
        {
            _userManager = userManager;
            _manager = manager;
        }

        public IViewComponentResult Invoke(int assignmentId)
        {
            var assignment = _manager.GetByIdAsync(assignmentId).Result;

            List<AddUserToTaskVIewModel> model = new();


            foreach (var user in _userManager.GetUsersWithRolesAsync().Result)
            {

                if (!_manager.IsUser(assignment, user.Id.ToString()))
                    model.Add(new()
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        ImagePath = user.ImagePath,
                        Role = user.Roles[0]
                    }); 

            }
          

            ViewData["assignmentId"] = assignmentId;
            
            return View(model);

        }


    }
    public record AddUserToTaskVIewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string ImagePath { get; set; } = null!;

        public string Role { get; set; } = null!;

    }


}
