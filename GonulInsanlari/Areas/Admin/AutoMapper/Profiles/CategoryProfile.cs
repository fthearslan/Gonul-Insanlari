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

            #region List

            CreateMap<CategoryDto,CategoryListViewModel>();
            CreateMap<CategoryCreateViewModel, Category>();

            #endregion

            #region Edit

            CreateMap<Category, CategoryEditViewModel>();
            CreateMap<CategoryEditViewModel, Category>();

            #endregion

            #region Details

            CreateMap<Category,CategoryDetailViewModel>();
            #endregion



        }


    }
}
