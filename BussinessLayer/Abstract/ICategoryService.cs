using DataAccessLayer.Concrete;
using DataAccessLayer.DTOs;
using EntityLayer.Entities;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract
{
    public interface ICategoryService:IGenericService<Category>
    {
        List<CategoryDto> GetCategoriesWithArticle();

        Category GetDetails(int id);
    }
}
