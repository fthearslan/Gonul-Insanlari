using AutoMapper;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.AutoMapper.Resolvers;
using ViewModelLayer.ViewModels.Category;

namespace GonulInsanlari.AutoMapper.Profiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {


            CreateMap<Category, CategoryDetailsUIViewModel>().ConvertUsing<CategoryArticleConverter>();

           
                

        }
    }
}
