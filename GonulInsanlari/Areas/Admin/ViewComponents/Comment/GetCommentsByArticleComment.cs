using AutoMapper;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Comment;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Comment
{
    public class GetCommentsByArticleComment : ViewComponent
    {
        private readonly IArticleService _manager;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCommentsByArticleComment> _logger;

        public GetCommentsByArticleComment(IArticleService manager, IMapper mapper, ILogger<GetCommentsByArticleComment> logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public IViewComponentResult Invoke()
        {
            var articles = _manager.GetWhere(x=>x.Status==true).ToList();

            List<CommentsByArtcleViewModel> model = new();

            try
            {
                model = _mapper.Map<List<CommentsByArtcleViewModel>>(articles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            
            }

            return View(model);

        }
    }
}
