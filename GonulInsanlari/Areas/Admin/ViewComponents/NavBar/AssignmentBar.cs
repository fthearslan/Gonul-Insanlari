using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete.Entities;
using EntityLayer.Concrete.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ViewModelLayer.ViewModels.Assignment;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class AssignmentBar : ViewComponent
    {
        UserManager<AppUser> userManager;
        private readonly IAssignmentService _manager;
        private readonly IMapper _mapper;
        private readonly ILogger<AssignmentBar> _logger;
        public AssignmentBar(UserManager<AppUser> UserManager, IAssignmentService manager,IMapper mapper, ILogger<AssignmentBar> logger)
        {
            userManager = UserManager;
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public IViewComponentResult Invoke()
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            var assignments = _manager.GetAssignmentBar(user.Id);
            try
            {
                var model = _mapper.Map<List<AssignmentBarViewModel>>(assignments);
                ViewBag.Count = "You have " + model.Count + " assignments";
                return View(model);
            }
            catch (AutoMapperMappingException)
            {
                _logger.LogError("Mapping exception has been thrown at AssignmentBar.cs...");
                return View();
            }
          
        }

    }
}
