using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Dashboard
{
    public class GetArticles:ViewComponent
    {
        private readonly IArticleService _manager;
        private readonly IMapper _mapper;
        private readonly ILogger<GetArticles> _logger;

        public GetArticles(IArticleService manager, IMapper mapper, ILogger<GetArticles> logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        public IViewComponentResult Invoke()
        {
            var articles = _manager.ListReleased().Take(10).ToList();
            

            return View(articles);
        }
    }
}
