

using AutoMapper;
using BussinessLayer.Abstract.Services;
using DataAccessLayer.Concrete.EntityFramework;
using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using FluentValidation;
using GonulInsanlari.Areas.Admin.Models.Tools;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment;
using GonulInsanlari.Areas.Admin.ViewComponents.Assignment;
using GonulInsanlari.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using X.PagedList;


namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize]
    public class AssignmentController : Controller
    {

        #region DI Services 

        private readonly IAssignmentService _manager;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly ILogger<AssignmentController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly AbstractValidator<Assignment> _validator;
        private ResponseModel _response;
        public AssignmentController(IAssignmentService manager, IMapper mapper, ILogger<AssignmentController> logger, UserManager<AppUser> userManager, IMemoryCache cache, AbstractValidator<Assignment> Validator, ResponseModel response)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _cache = cache;
            _validator = Validator;
            _response = response;
        }

        #endregion

        #region Create 

        [HttpGet]
        public IActionResult Add()
        {


            List<SelectListItem> users = (from x in _userManager.Users.Select(u => new AppUser()
            {
                Id = u.Id,
                UserName = u.UserName,
            })
                                          select new SelectListItem
                                          {
                                              Text = x.UserName,
                                              Value = x.Id.ToString(),
                                          }).ToList();

            ViewData["Users"] = users;
            _cache.Set("UserList", users);
            return View();


        }

        [HttpPost]
        public async Task<IActionResult> Add(AssignmentCreateViewModel model)
        {
            ViewData["Users"] = _cache.Get("UserList");

            if (ModelState.IsValid)
            {


                Assignment task = new();

                try
                {

                    task = _mapper.Map<Assignment>(model);

                }
                catch (AutoMapperMappingException ex)
                {

                    _logger.LogError($"{ex.Message} on AssignmentController.");
                    return BadRequest();
                }

                task.Publisher = await _userManager.GetUserAsync(HttpContext.User);

                var result = _validator.Validate(task);

                if (result.IsValid)
                {
                    await _manager.PublishAsync(task);
                    return RedirectToAction(nameof(List));
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return View();

        }

        [HttpPost]
        public async Task AddUser(int taskId, int userId)
        {

            var assignment = await _manager.GetByIdAsync(taskId);

            if (assignment != null)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user != null)
                {
                    assignment.UserAssignments.Add(new()
                    {
                        User = user,
                        UserId = userId,
                    });

                    assignment.Modified = DateTime.Now;
                    _manager.Update(assignment);
                }

            }

        }

        [HttpPost]
        public async Task AddSubTask(SubTaskCreateViewModel model)
        {
            ViewData["Error"] = "Error was thrown!";
            if (ModelState.IsValid)
            {
                Assignment task = await _manager.GetByIdAsync(model.TaskId);

                SubTask subTask = new() { Assignment = task, Description = model.SubTaskDescription };

                task.SubTasks.Add(subTask);

                var result = _validator.Validate(task, opt => opt.IncludeRuleSets("SubTask"));

                if (result.IsValid)
                    _manager.AddSubTask(subTask);

                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }


            }
        }

        [HttpPost]
        public async Task<JsonResult> AddAttachment(AddAttachmentViewModel model)
        {

            if (await ImageUpload.CheckFileSizeAsync(model.Attachments))
            {

                var assignment = await _manager.GetByIdAsync(model.TaskId);

                var filePaths = await ImageUpload.UploadFileAsync(model.Attachments);

                List<TaskAttachment> attachments = new List<TaskAttachment>();

                foreach (var filePath in filePaths)
                    attachments.Add(new() { Path = filePath, Assignment = assignment });


                if (await _manager.AddAttachmentsAsync(attachments))
                {
                    _response.success = true;
                    _response.responseMessage = "Files have been successfully uploaded.";
                }
                else
                {
                    _response.success = false;
                    _response.responseMessage = "Error has occured while uploading files!";
                }

            }
            else
            {
                _response.success = false;
                _response.responseMessage = "Please check file sizes!";

            }

            return Json(new
            {
                success = _response.success,
                responseMessage = _response.responseMessage,

            });

        }

        #endregion

        #region Read

        public async Task<IActionResult> List(int pageNumber = 1)
        {
            var tasks = await _manager.GetByProgress(Assignment.ProgressStatus.InProgress);

            List<AssignmentByProgressListViewModel> model = new();

            try
            {
                model = _mapper.Map<List<AssignmentByProgressListViewModel>>(tasks);
            }
            catch (AutoMapperMappingException)
            {
                 return BadRequest();
            }

            return View(await model.ToPagedListAsync(pageNumber, 9));

        }
        public async Task<IActionResult> GetDetails(int id)
        {
            var task = await _manager.GetByIdAsync(id);
            if (task != null)
            {

                AssignmentDetailsViewModel model = new();

                try
                {
                    model = _mapper.Map<AssignmentDetailsViewModel>(task);
                }
                catch (AutoMapperMappingException ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest();
                }

                ViewData["SubTasksAll"] = task.SubTasks?.Count;
                ViewData["SubTasksDone"] = task.SubTasks?.Where(s => s.Progress == SubTasksProgress.Done).Count();

                ViewBag.userExists = _manager.IsUser(task, _userManager.GetUserId(HttpContext.User));


                return View(model);

            }

            return NotFound();


        }

        #endregion

        #region Update 

        [HttpPost]
        public async Task ChangeProgress(AssingmentProgressChangeViewModel obj)
        {
            var task = await _manager.GetByIdAsync(obj.TaskId);

            SubTask? subtask = task.SubTasks.Find(s => s.Id.ToString() == obj.SubTaskId);

            if (subtask != null)

                switch (obj.Progress)
                {
                    case "Pending":

                        subtask.Progress = SubTasksProgress.Pending;

                        break;

                    case "InProgress":

                        subtask.Progress = SubTasksProgress.InProgress;
                        break;

                    case "Done":

                        subtask.Progress = SubTasksProgress.Done;
                        break;

                }

            _manager.Update(task);

        }

        #endregion

        #region Delete

        [HttpPost]
        public async Task RemoveUser(int assignmentId, int userId)
        {
            var assignment = await _manager.GetByIdAsync(assignmentId);

            var user = assignment?.UserAssignments?.Find(a => a.UserId == userId);

            if (user != null)
            {
                assignment?.UserAssignments?.Remove(user);

                if (assignment != null)
                    _manager.Update(assignment);
            }

        }

        [HttpPost]
        public async Task RemoveSubTask(int assignmentId, Guid subtaskId)
        {

            Assignment? assignment = await _manager.GetByIdAsync(assignmentId);

            SubTask? subtask = assignment.SubTasks.Find(s => s.Id == subtaskId);

            if (subtask != null)
                assignment.SubTasks.Remove(subtask);

            _manager.Update(assignment);


        }

        #endregion


    }





}



