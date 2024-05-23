using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Dashboard
{
    public class GetComments : ViewComponent
    {
        private readonly ICommentService _manager;

        public GetComments(ICommentService manager)
        {
            _manager = manager;
        }

        public IViewComponentResult Invoke()
        {

            var comments = _manager.GetAllAsync(EntityLayer.Concrete.Entities.CommentProgress.Pending, true).Result.Take(5).ToList();
            int count;
            if (comments is not null)
            {
                count = comments.Count();

            }
            else
            {
                count = 0;
            }
            ViewBag.Count = count;
            return View(comments);

        }
    }
}
