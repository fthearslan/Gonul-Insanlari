using AutoMapper;
using EntityLayer;
using GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Article;
using GonulInsanlari.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class ArticleProfile : Profile
    {

        public ArticleProfile()
        {
            CreateMap<Article, ArticleCreateViewModel>();

            CreateMap<ArticleCreateViewModel, Article>()
                .ForMember(art => art.VideoPath, opt => opt.MapFrom<VideoPathResolver>())
                .ForMember(art => art.ImagePath, opt => opt.MapFrom<ImagePathResolver>());

            CreateMap<ArticleEditViewModel, Article>().ForMember(x => x.VideoPath, opt => opt.Ignore()).ForMember(art=>art.VideoPath,opt=>opt.MapFrom(mod=>mod.PathString));

            CreateMap<Article, ArticleEditViewModel>().ForMember(x => x.VideoPath, opt => opt.Ignore())
                .ForMember(mod=>mod.PathString,opt=>opt.MapFrom(art=>art.VideoPath));// article videopath===> vm Pathstring


            CreateMap<ArticleListViewModel, Article>();
            CreateMap<Article, ArticleListViewModel>()
                .ForMember(mod => mod.CommentCount, opt => opt.MapFrom(art => art.Comments.Count));
            CreateMap<Article, ArticleDetailsViewModel>();

            CreateMap<Article, ArticleAllViewModel>();
        }
    }
}
