using AutoMapper;
using BussinessLayer.Abstract.Services;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.ViewComponents
{
    public class LeaveCommentComponent:ViewComponent
    {

        private readonly ICommentService _commentManager;

        private readonly IMapper _mapper;

        public LeaveCommentComponent(ICommentService commentManager, IMapper mapper)
        {
            _commentManager = commentManager;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(int articleId)
        {

            ViewData["articleId"] = articleId;

            return View();

        }


    }
}
