﻿using AutoMapper;
using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Article;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Article
{
    public class GetListArticle : ViewComponent
    {
        ArticleManager _articleManager = new ArticleManager(new EFArticleDAL());
        UserManager<AppUser> _userManager;
        IMapper _mapper;
        public GetListArticle(UserManager<AppUser> userManager,IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            var articles = _articleManager.GetAllWithoutDrafts();
            List<ArticleAllViewModel> model = _mapper.Map<List<ArticleAllViewModel>>(articles);
            return View(model);

        }

    }
}