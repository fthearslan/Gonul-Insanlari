using AutoMapper;
using BussinessLayer.Abstract;
using BussinessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class AssignmentController : Controller
    {
        private readonly IAssignmentService _manager;
        private readonly IMapper _mapper;
        private readonly ILogger<AssignmentController> _logger;
        private readonly UserManager<AppUser> _userManager;

        public AssignmentController(IAssignmentService manager, IMapper mapper, ILogger<AssignmentController> logger, UserManager<AppUser> userManager)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult List()
        {
            var assingments = _manager.GetAssignmentsWithSender(4);
            
            return View(assingments);
        }
    }
}
