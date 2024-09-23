using BussinessLayer.Abstract.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ViewModelLayer.Models.Tools;
using ViewModelLayer.ViewModels.Announcement;

namespace GonulInsanlari.Views.Shared
{
    public class Main_LayoutModel : PageModel
    {
        private readonly IAnnouncementService _announcementManager;

        public Main_LayoutModel(IAnnouncementService announcementManager)
        {
            _announcementManager = announcementManager;
        }

        public void OnGet()
        {
         
           DashboardAnnouncementViewModel? model =  _announcementManager
                .GetWhere(x => x.IsForAdmins == false && x.Status == true)
                .Select(x => new DashboardAnnouncementViewModel
                {
                    ID = x.Id,
                    Title = x.Title,
                    Details = x.Details,
                    Subject = x.Subject,
                    Created = x.Created

                })
                .OrderByDescending(x => x.Created)
                .SingleOrDefault();

            if (model is not null)
            {

                if (model.Title.Length > 50)
                    ViewData["announcement"] = model.Title.Substring(0, 50);

                ViewData["announcement"] = model.Title;

            }



        }
    }
}
