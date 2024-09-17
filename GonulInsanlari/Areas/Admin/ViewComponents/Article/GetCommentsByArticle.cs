using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using ViewModelLayer.ViewModels.Comment;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Article
{
    public class GetCommentsByArticle : ViewComponent
    {
        private readonly ICommentService _manager;
        private readonly IMapper _mapper;
        public GetCommentsByArticle(ICommentService manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(int id)
        {
            var comments = _manager.GetByArticleAsync(id).Result;

            List<CommentListViewModel> model = _mapper.Map<List<CommentListViewModel>>(comments);

            if (comments?.Count == 0)
            {
                TempData["Warning"] = "There is no comment for this article.";
            }

       

            return View(model);

        }
    }
}
