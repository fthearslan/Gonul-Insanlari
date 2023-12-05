using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Dashboard
{
    public class DashboardContacts:ViewComponent
    {
        ContactManager contactManager = new ContactManager(new EFContactDAL());
        public IViewComponentResult Invoke()
        {
            var contacts = contactManager.ListFilter().Take(4).ToList();
            var Count = contactManager.ListFilter().Count;
            ViewBag.Count = Count;
            return View(contacts);
        }
    }
}
