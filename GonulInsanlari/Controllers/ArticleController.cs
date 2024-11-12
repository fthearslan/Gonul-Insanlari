using AutoMapper;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Runtime.CompilerServices;
using ViewModelLayer.ViewModels.Article;
using ViewModelLayer.ViewModels.Comment;
using X.PagedList;

namespace GonulInsanlari.Controllers
{


    [AllowAnonymous]
    [Route("article")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleManager;
        private readonly IMapper _mapper;

        public ArticleController(IArticleService articleManager, IMapper mapper)
        {
            _articleManager = articleManager;
            _mapper = mapper;
        }

        [Route("all")]
        public async Task<IActionResult> All(int pageNumber = 1)
        {

            IPagedList<ArticleListUIViewModel> model = await _articleManager.GetWhere(x => x.Status == true && x.IsDraft == false)
                .OrderByDescending(x => x.Created)
                .Select(x => new ArticleListUIViewModel()
                {
                    Id = x.Id,
                    CategoryName = x.Category.Name,
                    Title = x.Title,
                    Created = x.Created,
                    ImagePath = x.ImagePath,
                    SeenCount = x.SeenCount
                })
                .AsNoTrackingWithIdentityResolution()
                .ToPagedListAsync(pageNumber, 10);


            return View(model);


        }

        [Route("{articleSlug}/{articleId}")]
        public async Task<IActionResult> GetDetails(string articleSlug, int articleId)
        {

          

            Article? article = await _articleManager.GetWhere(x => x.Status == true && x.IsDraft == false)
                .Include(x => x.Comments)
             .SingleOrDefaultAsync(x => x.Id == articleId);

            if (article is null)
                return NotFound();

            
            article.SeenCount++;

            _articleManager.Update(article);

            
            ArticleDetailsUIViewModel model = _mapper.Map<ArticleDetailsUIViewModel>(article);


            return View(model);


        }






    }
}
