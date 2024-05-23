using BussinessLayer.Abstract.Services;
using DataAccessLayer.Abstract.SubRepositories;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Admin
{
    public class AdminProfileStatistics : ViewComponent
    {

        private readonly IArticleService _articleManager;
        private readonly IAnnouncementService _announceManager;
        private readonly IAssignmentService _assignmentManager;
        public AdminProfileStatistics(IArticleService articleManager, IAnnouncementService announceManager, IAssignmentService assignmentManager)
        {
            _articleManager = articleManager;
            _announceManager = announceManager;
            _assignmentManager = assignmentManager;
        }

        public IViewComponentResult Invoke(int userId)
        {

            ViewData["Articles"] = _articleManager.GetWhere(x => x.AppUserID == userId).Count();
            ViewData["Announcements"] = _announceManager.GetWhere(x => x.UserId == userId).Count();
            ViewData["Tasks"] = _assignmentManager.GetWhere(x => x.Publisher.Id == userId).Count();

            return View();
        }

    }
}
