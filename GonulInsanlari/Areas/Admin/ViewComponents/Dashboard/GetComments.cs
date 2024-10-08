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

            var comments = _manager.GetAllAsync(EntityLayer.Concrete.Entities.CommentProgress.Pending, true)
                .Result
                .Take(5)
                .ToList();

      
          
            ViewData["Count"]= comments.Count;
            return View(comments);

        }
    }
}
