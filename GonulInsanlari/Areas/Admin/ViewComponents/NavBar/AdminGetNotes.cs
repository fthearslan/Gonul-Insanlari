using BussinessLayer.Abstract;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.NavBar
{
    public class AdminGetNotes:ViewComponent
    {
        private readonly INoteService _noteManager;
        UserManager<AppUser> userManager;

        public AdminGetNotes(UserManager<AppUser> userManager, INoteService noteManager)
        {
            this.userManager = userManager;
            _noteManager=noteManager;
        }

        public IViewComponentResult Invoke()
        {
            var user = userManager.GetUserAsync(HttpContext.User).Result;
            var notes = _noteManager.GetListByUser(user.Id).Take(6).ToList();
            var count = _noteManager.GetListByUser(user.Id).Count;
            ViewBag.Count = count;
            return View(notes);
        }
    }
}
