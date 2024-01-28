using AutoMapper;
using EntityLayer.Entities;
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

            #region Create
            CreateMap<Article, ArticleCreateViewModel>();
            CreateMap<ArticleCreateViewModel, Article>()
                .ForMember(art => art.ImagePath, opt => opt.MapFrom<ImagePathResolver>());
            #endregion
            
            #region Edit
            CreateMap<ArticleEditViewModel, Article>();
            CreateMap<Article, ArticleEditViewModel>();
            #endregion

            #region List
            CreateMap<ArticleListViewModel, Article>();
            CreateMap<Article, ArticleListViewModel>()
                .ForMember(mod => mod.CommentCount, opt => opt.MapFrom(art => art.Comments.Count));
            #endregion
            
            #region Details
            CreateMap<Article, ArticleDetailsViewModel>();
            CreateMap<Article, ArticleByCategoryViewModel>().ForMember(a => a.AppUserName, opt => opt.MapFrom(art => art.AppUser.UserName));
            #endregion

            #region All
            CreateMap<Article, ArticleAllViewModel>();
            #endregion

        }
    }
}
