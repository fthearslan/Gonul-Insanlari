using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using ViewModelLayer.ViewModels.Announcement;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Dashboard
{
    public class GetAnnouncements : ViewComponent
    {
        private readonly IAnnouncementService _manager;
        private readonly IMapper _mapper;
        public GetAnnouncements(IAnnouncementService manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public  IViewComponentResult Invoke()
        {
            var announcements =  _manager.GetForAdmin().Take(3).ToList();
    
            try
            {
                var modelList = _mapper.Map<List<DashboardAnnouncementViewModel>>(announcements);
                return View(modelList);
               
            }
            catch (AutoMapperMappingException)
            {
                return View();
            }
    

        }
    }
}
