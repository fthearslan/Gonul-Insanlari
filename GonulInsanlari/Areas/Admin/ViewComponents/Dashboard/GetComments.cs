using BussinessLayer.Abstract;
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
            var comments = _manager.ListFilter().Take(5).ToList();
            var count = _manager.ListFilter().Count;
            ViewBag.Count = count;
            return View(comments);

        }
    }
}
