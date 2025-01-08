using AutoMapper;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModelLayer.ViewModels.Article;
using ViewModelLayer.ViewModels.Comment;

namespace GonulInsanlari.ViewComponents
{
    public class CommentsByArticleComponent : ViewComponent
    {


        private readonly ICommentService _commentManager;
        private readonly IMapper _mapper;

        public CommentsByArticleComponent(ICommentService commentManager, IMapper mapper)
        {
            _commentManager = commentManager;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(int articleId)
        {

            List<Comment> comments = _commentManager.
                GetWhere(x => x.ArticleID == articleId && x.Status == true && x.Progress == CommentProgress.Approved)
                .Include(c => c.Replies)
              .OrderByDescending(x => x.Created)
                .AsNoTrackingWithIdentityResolution()
                .Take(5)
            .ToList();

            ViewData["Count"] = _commentManager.GetWhere(x => x.ArticleID == articleId && x.Status == true)
                .Include(x=>x.Replies)
                .Count();

            List<CommentByArticleUIViewModel> model = _mapper.Map<List<CommentByArticleUIViewModel>>(comments);



            @ViewData["articleId"] = articleId;

            return View(model);


        }
    }
}
