using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Contact
{
    public class ContactFolders : ViewComponent
    {
        private readonly IContactService _manager;

        public ContactFolders(IContactService manager, UserManager<AppUser> userManager)
        {
            _manager = manager;

        }

        public IViewComponentResult Invoke()
        {

            ViewData["Count"] = _manager.GetWhere(x => x.Status == true && x.IsSeen == false && x.ContactStatus ==ContactStatus.Received)
                  .AsNoTrackingWithIdentityResolution()
                  .Count();



            return View();

        }
    }
}
