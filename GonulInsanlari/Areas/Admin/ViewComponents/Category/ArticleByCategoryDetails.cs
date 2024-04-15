using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Article;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Category;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Category
{
    public class ArticleByCategoryDetails : ViewComponent
    {
        private readonly IArticleService _manager;
        private readonly IMapper _mapper;
        ILogger<ArticleByCategoryDetails> _logger;
        public ArticleByCategoryDetails(IMapper mapper, ILogger<ArticleByCategoryDetails> logger, IArticleService manager)
        {
            _mapper = mapper;
            _logger = logger;
            _manager = manager;
        }

        public IViewComponentResult Invoke(int id)
        {
            List<ArticleByCategoryViewModel> model = new();

            var list = _manager.ListByCategory(id);
            if (list.Any())
                try
                {
                    model = _mapper.Map<List<ArticleByCategoryViewModel>>(list);
                }
                catch (AutoMapperMappingException ex)
                {
                    _logger.LogError(ex.Message);
                }

            return View(model);



        }


    }
}
