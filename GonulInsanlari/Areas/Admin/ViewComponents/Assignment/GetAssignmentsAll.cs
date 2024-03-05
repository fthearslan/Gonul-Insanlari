using AutoMapper;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Assignment
{
    public class GetAssignmentsAll:ViewComponent
    {
        IAssignmentService _manager;
        IMapper _mapper;

        public GetAssignmentsAll(IAssignmentService manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            var tasks = _manager.List();
            
            return View();

        }
    }
}
