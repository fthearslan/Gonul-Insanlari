using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using BussinessLayer.Concrete.Validations;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete.Entities;
using FluentValidation;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment;
using Humanizer;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using X.PagedList;


namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class AssignmentController : Controller
    {
        private readonly IAssignmentService _manager;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly ILogger<AssignmentController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly AbstractValidator<Assignment> _validator;

        public AssignmentController(IAssignmentService manager, IMapper mapper, ILogger<AssignmentController> logger, UserManager<AppUser> userManager, IMemoryCache cache, AbstractValidator<Assignment> Validator)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _cache = cache;
            _validator = Validator;
        }

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
                return View(); // Error Page
            }

            return View(await model.ToPagedListAsync(pageNumber, 9));

        }

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
                var user = await _userManager.GetUserAsync(HttpContext.User);

                Assignment task = new();

                try
                {
                    task = _mapper.Map<Assignment>(model);

                }
                catch (AutoMapperMappingException ex)
                {

                    _logger.LogError($"{ex.Message} on AssignmentController.");
                    return View(); // => Error Page...

                }

                task.Publisher = user;

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

        public async Task<IActionResult> GetDetails(int id)
        {
            var task = await _manager.GetByIdAsync(id);
            if (task != null)
            {

                AssignmentDetailsViewModel model = new();

                try
                {
                    model = _mapper.Map<AssignmentDetailsViewModel>(task); ;
                }
                catch (AutoMapperMappingException ex)
                {
                    _logger.LogError(ex.Message);
                    return RedirectToAction(); // Error Page...
                }

                return View(model); // Error Page...

            }

            return View();
        
        }


    }
}
