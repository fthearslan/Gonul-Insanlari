using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.DTOs.Category;
using EntityLayer.Concrete.Entities;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract.Services
{
    public interface ICategoryService : IGenericService<Category>
    {
        List<CategoryDto> GetCategoriesWithArticle();

        Category GetDetails(int id);

        List<Category> GetCategoriesWithArticleCount(int takeCount);

        Task<Category> GetByNameAsync(string categoryName);

    


    }
}
