using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using DataAccessLayer.Migrations;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Dashboard
{
    public class GetAssignments:ViewComponent
    {
        UserManager<AppUser> _userManager;

        private readonly IAssignmentService _manager;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAssignments> _logger;

        public GetAssignments(UserManager<AppUser> userManager, IAssignmentService manager, IMapper mapper, ILogger<GetAssignments> logger)
        {
            _userManager = userManager;
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public IViewComponentResult Invoke()
        {
            var user =  _userManager.GetUserAsync(HttpContext.User).Result;
            var assignments = _manager.GetListDashboard();
            try
            {
              
                var model = _mapper.Map<List<AssignmentDashboardViewModel>>(assignments);
                return View(model);

            }
            catch (AutoMapperMappingException)
            {
                _logger.LogError("Mapping exception has been thrown at GetAssignment.cs");
           
            }
            return View();
        }
    }
}
