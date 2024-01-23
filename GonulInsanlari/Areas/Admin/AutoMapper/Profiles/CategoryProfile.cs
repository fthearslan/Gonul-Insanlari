using AutoMapper;
using DataAccessLayer.DTOs;
using EntityLayer;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Category;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class CategoryProfile:Profile
    {

        public CategoryProfile()
        {
            
            CreateMap<CategoryDto,CategoryListViewModel>();
            CreateMap<CategoryCreateViewModel, Category>();


        }


    }
}
