using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Dashboard
{
    public class DashboardContacts : ViewComponent
    {
        private readonly IContactService _contactManager;

        public DashboardContacts(IContactService contactManager)
        {
            _contactManager = contactManager;
        }

        public IViewComponentResult Invoke()
        {
           var contacts = _contactManager.
                   GetWhere(x => x.Status == true  && x.IsSeen==false && x.ContactStatus==ContactStatus.Received).
                    OrderByDescending(x => x.Created).
                   ToList();

            int Count = _contactManager
                .ListFilter()
                .Count;

            ViewData["Count"] = Count;
            
            return View(contacts);
        
        }
    }
}
