using AutoMapper;
using BussinessLayer.Abstract.Services;
using DataAccessLayer.Concrete.EntityFramework;
using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Configurations;
using EntityLayer.Concrete.Entities;
using FluentValidation;
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
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Identity;
using ViewModelLayer.Models.Tools;
using ViewModelLayer.ViewModels.Assignment;
using ViewModelLayer.ViewModels.TaskAttachment;
using X.PagedList;


namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize]
    [Route("assignments")]
    public class AssignmentController : Controller
    {

        #region DI Services 

        private readonly IAssignmentService _manager;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly UserManager<AppUser> _userManager;
        private ResponseModel _response;
        private readonly IHttpContextAccessor? _contextAccessor;

        AppUser _currentUser = new();
        public AssignmentController(IAssignmentService manager, IMapper mapper,UserManager<AppUser> userManager, IMemoryCache cache, ResponseModel response, IHttpContextAccessor contextAccessor)
        {
            _manager = manager;
            _mapper = mapper;
            _userManager = userManager;
            _cache = cache;
            _response = response;
            _contextAccessor = contextAccessor;
            _currentUser = _userManager.GetUserAsync(_contextAccessor?.HttpContext?.User).Result;

        }

        #endregion

        #region CREATE

        [HttpGet]
        [Route("add")]
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
        [Route("add")]
        public async Task<IActionResult> Add(AssignmentCreateViewModel model)
        {
            ViewData["Users"] = _cache.Get("UserList");

            if (ModelState.IsValid)
            {

                Assignment task = _mapper.Map<Assignment>(model);

                task.Publisher = await _userManager.GetUserAsync(HttpContext.User);
                task.Logs.Add(new TaskLog($"The task named {task.Title} created by ") { CreatedBy = task.Publisher });

                await _manager.PublishAsync(task);

                return RedirectToAction(nameof(List));

            }

            return View(model);

        }

        [HttpPost]
        [Route("add/user")]

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

                    await _manager.LogAsync(new($"User named {user.UserName} were addded by {_currentUser.UserName}") { Assignment = assignment }, _currentUser.Id);


                }

            }

        }

        [HttpPost]
        [Route("add/subtask")]

        public async Task AddSubTask(SubTaskCreateViewModel model)
        {

            if (ModelState.IsValid)
            {
                Assignment task = await _manager.GetByIdAsync(model.TaskId);

                SubTask subTask = new() { Assignment = task, Description = model.SubTaskDescription };

                task.SubTasks.Add(subTask);

                subTask.Assignment.Logs.Add(new($"New subtask '{subTask.Description.Substring(0, 15)}...' were added by {_currentUser.UserName}") { CreatedBy = _currentUser });
                _manager.AddSubTask(subTask);

            }
        }

        [HttpPost]
        [Route("add/file")]

        public async Task<JsonResult> AddAttachment(AddAttachmentViewModel model)
        {
            List<TaskAttachment> attachments = new List<TaskAttachment>();


            List<AttachmentResponseModel> response = new List<AttachmentResponseModel>();



            if (ImageUpload.CheckFileSizeAsync(model.Attachments))
            {

                var assignment = await _manager.GetByIdAsync(model.TaskId);

                var filePaths = await ImageUpload.UploadFileAsync(model.Attachments);


                foreach (var path in filePaths)
                {
                    if (assignment.Attachments.Any(x => x.Path == path))
                        continue;

                }

                foreach (var filePath in filePaths)
                    attachments.Add(new() { Path = filePath, Assignment = assignment });


                if (await _manager.AddAttachmentsAsync(attachments))
                {
                    if (attachments.Count > 1)
                        await _manager.LogAsync(new($"The files were uploaded by {_currentUser.UserName}") { Assignment = assignment }, _currentUser.Id);

                    if (attachments.Count == 1)
                        await _manager.LogAsync(new($"The file {attachments.First().Path} were uploaded by {_currentUser.UserName}") { Assignment = assignment, Attachment = attachments.First() }, _currentUser.Id);

                    _response.success = true;
                    _response.responseMessage = "Files have been successfully uploaded.";

                    foreach (var at in attachments)
                    {
                        response.Add(new()
                        {
                            Id = at.Id,
                            Path = at.Path,
                            CreatedDate = at.CreatedDate,
                        });

                    }

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
                data = response,

            });


        }

        #endregion

        #region READ

        [Route("list")]
        public async Task<IActionResult> List(int pageNumber = 1)
        {
            var tasks = await _manager.GetByProgress(Assignment.ProgressStatus.InProgress);

            List<AssignmentByProgressListViewModel> model = _mapper.Map<List<AssignmentByProgressListViewModel>>(tasks);

            return View(await model.ToPagedListAsync(pageNumber, 9));

        }

        [Route("details/{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var task = await _manager.GetByIdAsync(id);

            if (task != null)
            {

                AssignmentDetailsViewModel model = _mapper.Map<AssignmentDetailsViewModel>(task);

                ViewData["SubTasksAll"] = task.SubTasks?.Count;
                ViewData["SubTasksDone"] = task.SubTasks?.Where(s => s.Progress == SubTasksProgress.Done).Count();

                ViewBag.userExists = _manager.IsUser(task, _userManager.GetUserId(HttpContext.User));

                return View(model);

            }

            return NotFound();


        }

        #endregion

        #region UPDATE

        [HttpPost]
        [Route("changeProgress")]
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


            await _manager.LogAsync(new($"{_currentUser.UserName} changed the progress of subtask '{subtask?.Description.Substring(0, 15)}...' to {subtask?.Progress.ToString()}.")
            { Assignment = task },
            _currentUser.Id);
            _manager.Update(task);




        }

        #endregion

        #region DELETE

        [HttpPost]

        [Route("remove/user")]

        public async Task RemoveUser(int assignmentId, int userId)
        {
            var assignment = await _manager.GetByIdAsync(assignmentId);

            var user = assignment?.UserAssignments?.Find(a => a.UserId == userId);

            if (user != null)
            {
                assignment?.UserAssignments?.Remove(user);

                if (assignment != null)
                {

                    await _manager.LogAsync(new($"User named {user?.User?.UserName} deleted by {_currentUser.UserName}") { Assignment = assignment }, _currentUser.Id);

                    _manager.Update(assignment);


                }
            }

        }

        [HttpPost]
        [Route("remove/subtask")]

        public async Task RemoveSubTask(int assignmentId, Guid subtaskId)
        {

            Assignment? assignment = await _manager.GetByIdAsync(assignmentId);

            SubTask? subtask = assignment.SubTasks.Find(s => s.Id == subtaskId);

            if (subtask != null)
            {
                assignment.SubTasks.Remove(subtask);

                await _manager.LogAsync(new($"Subtask  '{subtask.Description.Substring(0, 15)}...' deleted by {_currentUser.UserName}") { Assignment = assignment }, _currentUser.Id);
                _manager.Update(assignment);

            }


        }

        [HttpPost]
        [Route("remove/file")]

        public async Task<JsonResult> DeleteAttachment(AttachmentDeleteViewModel model)
        {


            if (await _manager.DeleteAttachmentAsync(model.Path, model.TaskId))
            {

                Assignment? task = await _manager.GetByIdAsync(model.TaskId);

                if (task is not null)
                    await _manager.LogAsync(new($"File named {model.Path} were deleted by {_currentUser.UserName}") { Assignment = task }, _currentUser.Id);


                _response.success = true;
                _response.responseMessage = "File has been successfully deleted.";


            }
            else
            {
                _response.success = false;
                _response.responseMessage = "An error has occcured while deleting the file.";

            }

            return Json(new
            {
                success = _response.success,
                responseMessage = _response.responseMessage,

            });



        }

        #endregion


    }





}



