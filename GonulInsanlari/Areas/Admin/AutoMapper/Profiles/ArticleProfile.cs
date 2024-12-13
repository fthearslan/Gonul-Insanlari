using AutoMapper;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers;
using GonulInsanlari.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ViewModelLayer.Models.Newsletter;
using ViewModelLayer.Models.Tools;
using ViewModelLayer.ViewModels.Article;
using X.PagedList;

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

            CreateMap<Article, WeeklyNewsletterModel>()
                   .ForMember(model => model.Description, opt => opt.MapFrom(article =>  GetString.GetRawString(article.Content).Substring(0,150)))
                   .ForMember(model => model.Category, opt => opt.MapFrom(article => article.Category.Name))
                   .ForMember(model => model.Subject, opt => opt.Ignore());
     
                   
            


            #endregion

            #region Edit
            CreateMap<ArticleEditViewModel, Article>();
            CreateMap<Article, ArticleEditViewModel>();
            #endregion

            #region List
            CreateMap<ArticleListViewModel, Article>();
            CreateMap<Article, ArticleListViewModel>()
                .ForMember(mod => mod.CommentCount, opt => opt.MapFrom(art => art.Comments.Count))
                .ForMember(mod => mod.UserImagePath, opt => opt.MapFrom(art => art.AppUser.ImagePath));
            #endregion

            #region Details
            CreateMap<Article, ArticleDetailsViewModel>()
                .ForMember(dest=>dest.CategoryName,opt=>opt.MapFrom(src=>src.Category.Name));

            CreateMap<Article, ArticleByCategoryViewModel>().ForMember(a => a.AppUserName, opt => opt.MapFrom(art => art.AppUser.UserName));
            #endregion

            #region All

            CreateMap<Article, ArticleAllViewModel>()
                .ForMember(dest=>dest.CommentCount,opt=>opt.MapFrom(src=>src.Comments.Count))
                .ForMember(dest=>dest.Category,opt=>opt.MapFrom(src=>src.Category.Name));
            
            #endregion

        }
    }
}
