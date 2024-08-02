using AutoMapper;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModelLayer.ViewModels.Comment;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Comment
{
    public class GetCommentsByArticleComment : ViewComponent
    {
        private readonly IArticleService _manager;
        private readonly IMapper _mapper;


        public GetCommentsByArticleComment(IArticleService manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;

        }

        public IViewComponentResult Invoke()
        {
            var articles = _manager.GetWhere(x => x.Status == true)
                .Include(x => x.Comments)
                .ToList();

            List<CommentsByArtcleViewModel> model = new();


            model = _mapper.Map<List<CommentsByArtcleViewModel>>(articles);


            return View(model);

        }
    }
}
