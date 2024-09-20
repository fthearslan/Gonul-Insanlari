using DataAccessLayer.Abstract.Repositories;
using DataAccessLayer.Concrete.DTOs.Category;
using EntityLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract.SubRepositories
{
    public interface ICategoryDAL : IRepository<Category>
    {
        List<CategoryDto> GetList();
        Category GetDetails(int id);

        List<Category> GetCategoriesWithArticleCount(int takeCount);

        Task<Category> GetByNameAsync(string categoryName);

       
       



    }
}
