﻿using AutoMapper;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Permissions;
using System.Security.Policy;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Assignment
{
    public class GetSubTasksByAssignment : ViewComponent
    {


        private readonly IAssignmentService _manager;
        private readonly UserManager<AppUser> _userManager;
        public GetSubTasksByAssignment(IAssignmentService manager, IMemoryCache cache, UserManager<AppUser> userManager)
        {
            _manager = manager;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke(int assignmentId)
        {

            var _currentUserId = _userManager.GetUserId(HttpContext.User);

            var task = _manager.GetByIdAsync(assignmentId).Result;

            ViewBag.userExist = task.UserAssignments.Any(a => a.UserId.ToString() == _currentUserId);
            ViewBag.isPublisher=(task.Publisher.Id.ToString() == _currentUserId) ;

            
            List<SubTaskViewModel> model = new();

            foreach (var subtask in task.SubTasks)
            {
                model.Add(new SubTaskViewModel()
                {
                    Id = subtask.Id,
                    Description = subtask.Description,
                    Progress = subtask.Progress.ToString()
                });
            }

            return View(model);
        }


    }
}