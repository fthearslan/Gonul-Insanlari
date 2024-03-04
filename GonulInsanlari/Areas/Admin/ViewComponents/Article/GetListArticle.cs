using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Article;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Article
{
    public class GetListArticle : ViewComponent
    {

        private readonly IArticleService _articleManager;
        IMapper _mapper;
        public GetListArticle(IMapper mapper, IArticleService manager)
        {
            _mapper = mapper;
            _articleManager = manager;
        }

        public IViewComponentResult Invoke()
        {
            var articles = _articleManager.GetAllWithoutDrafts();
            List<ArticleAllViewModel> model = _mapper.Map<List<ArticleAllViewModel>>(articles);
            return View(model);

        }

    }
}
