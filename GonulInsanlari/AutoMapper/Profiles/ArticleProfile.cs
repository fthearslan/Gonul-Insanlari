using AutoMapper;
using EntityLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ViewModelLayer.ViewModels.Article;

namespace GonulInsanlari.AutoMapper.Profiles
{
    public class ArticleProfile : Profile
    {

        public ArticleProfile()
        {


            CreateMap<Article, ArticleListUIViewModel>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id));
    


            CreateMap<Article, ArticleDetailsUIViewModel>()
                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id));
            

        }


    }
}
