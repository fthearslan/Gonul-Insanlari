using AutoMapper;
using BussinessLayer.Abstract.Services;
using DataAccessLayer.Concrete.DTOs.Assignment;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Assignment
{
    public class GetAssignmentsAll : ViewComponent
    {
        IAssignmentService _manager;
        IMapper _mapper;
        ILogger<GetAssignmentsAll> _logger;
        public GetAssignmentsAll(IAssignmentService manager, IMapper mapper, ILogger<GetAssignmentsAll> logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public IViewComponentResult Invoke()
        {
            var tasks = _manager.GetAll();

            List<AssignmentListViewModel> model = new();

            try
            {
                model = _mapper.Map<List<AssignmentListViewModel>>(tasks);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
            return View(model);

        }
    }
}
