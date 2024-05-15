using BussinessLayer.Abstract.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Contact
{
    public class ContactFolders:ViewComponent
    {
        private readonly IContactService _manager;

        public ContactFolders(IContactService manager)
        {
            _manager = manager;
        }

        public IViewComponentResult Invoke()
        {

          ViewData["Count"] = _manager.GetWhere(x=>x.Status==true && x.IsSeen==false)
                .AsNoTrackingWithIdentityResolution()
                .Count();


            return View();

        }
    }
}
