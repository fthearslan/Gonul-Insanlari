using AutoMapper;
using DataAccessLayer.Concrete.DTOs.Category;
using EntityLayer.Concrete.Entities;
using ViewModelLayer.ViewModels.Category;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class CategoryProfile:Profile
    {

        public CategoryProfile()
        {

            #region List

            CreateMap<CategoryDto, CategoryListViewModel>();
            CreateMap<CategoryCreateViewModel, Category>();

            #endregion

            #region Edit

            CreateMap<Category, CategoryEditViewModel>();
            CreateMap<CategoryEditViewModel, Category>();

            #endregion

            #region Details

            CreateMap<Category, CategoryDetailViewModel>();
            #endregion



        }


    }
}
