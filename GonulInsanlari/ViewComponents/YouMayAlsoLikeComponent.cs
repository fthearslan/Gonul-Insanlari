using AutoMapper;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using ViewModelLayer.ViewModels.Article;

namespace GonulInsanlari.ViewComponents
{
    public class YouMayAlsoLikeComponent : ViewComponent
    {

        private readonly IArticleService _articleManager;
        private readonly IMapper _mapper;

        public YouMayAlsoLikeComponent(IArticleService articleManager, IMapper mapper)
        {
            _articleManager = articleManager;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(string categoryName)
        {

            List<Article> articles = _articleManager
                 .GetWhere(x => x.Category.Name == categoryName && x.Status == true && x.IsDraft == false)
                 .OrderByDescending(x => x.Created)
                 .AsNoTrackingWithIdentityResolution()
                 .Take(4)
                 .ToList();

            List<ArticleListUIViewModel> model = _mapper.Map<List<ArticleListUIViewModel>>(articles);

            return View(model);


        }
    }
}
