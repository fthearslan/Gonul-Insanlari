using DataAccessLayer.Concrete;
using DataAccessLayer.DTOs;
using EntityLayer;
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
    }
}
