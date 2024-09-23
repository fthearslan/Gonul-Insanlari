using AutoMapper;
using BussinessLayer.Abstract.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModelLayer.ViewModels.Article;

namespace GonulInsanlari.ViewComponents
{
    public class LandingPageArticlesComponent : ViewComponent
    {
        private readonly IArticleService _articleManager;
        private readonly IMapper _mapper;
        public LandingPageArticlesComponent(IArticleService articleManager, IMapper mapper)
        {
            _articleManager = articleManager;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {

            List<EntityLayer.Concrete.Entities.Article> articles = _articleManager.GetWhere(x => x.Status == true && x.IsDraft == false)
                .OrderByDescending(x => x.SeenCount)
                .AsNoTrackingWithIdentityResolution()
                .Take(10)
                .ToList();

            List<ArticleListUIViewModel> model = _mapper.Map<List<ArticleListUIViewModel>>(articles);

            return View(model);

        }

    }
}
