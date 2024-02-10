using AutoMapper;
using BussinessLayer.Abstract;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Article;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Article
{
    public class GetDrafts : ViewComponent
    {
        private readonly IArticleService _articleManager;
        UserManager<AppUser> _userManager;
        IMapper _mapper;

        public GetDrafts(UserManager<AppUser> userManager, IMapper mapper,IArticleService manager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _articleManager = manager;
        }

        public IViewComponentResult Invoke()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var drafts = _articleManager.GetDraftsByUser(Convert.ToInt32(userId));
            List<ArticleAllViewModel> model = _mapper.Map<List<ArticleAllViewModel>>(drafts);
            return View(model);
        }



    }
}
