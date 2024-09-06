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
                   GetWhere(x => x.Status == true && x.ContactStatus==ContactStatus.Received).
                    OrderByDescending(x => x.Created).
                    OrderByDescending(x=>x.IsSeen==false)
                   .ToList();


            ViewData["Count"] = contacts.Where(x=>x.IsSeen==false).Count();
            
            return View(contacts);
        
        }
    }
}
