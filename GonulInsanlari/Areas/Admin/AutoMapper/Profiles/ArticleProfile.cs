using AutoMapper;
using EntityLayer;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Article;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class ArticleProfile : Profile
    {

        public ArticleProfile()
        {
            CreateMap<Article, ArticleCreateViewModel>();
            CreateMap<ArticleCreateViewModel, Article>();
            CreateMap<ArticleEditViewModel, Article>();
            CreateMap<Article, ArticleEditViewModel>();

            CreateMap<ArticleListViewModel, Article>();
            CreateMap<Article, ArticleListViewModel>()
                .ForMember(mod => mod.CommentCount, opt => opt.MapFrom(art => art.Comments.Count));
            CreateMap<Article, ArticleDetailsViewModel>();

            CreateMap<Article, ArticleAllViewModel>();
        }
    }
}
