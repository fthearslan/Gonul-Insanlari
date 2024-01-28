using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class AdminGetNotes:ViewComponent
    {
        NoteManager noteManager = new NoteManager(new EFNoteDAL());
        UserManager<AppUser> userManager;

        public AdminGetNotes(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            var notes = noteManager.GetListByUser(user.Id).Take(6).ToList();
            var count = noteManager.GetListByUser(user.Id).Count;
            ViewBag.Count = count;
            return View(notes);
        }
    }
}
