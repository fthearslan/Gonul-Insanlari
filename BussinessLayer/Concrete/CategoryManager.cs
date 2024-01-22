using BussinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.DTOs;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDAL _category;

        public CategoryManager(ICategoryDAL category)
        {
            _category = category;
        }

        public void Add(Category entity)
        {
            _category.Insert(entity);
        }

        public void Delete(Category entity)
        {
            _category.Delete(entity);
        }

        public Category GetById(int id)
        {
            return _category.Get(x => x.CategoryID == id);
        }

        public List<CategoryDto> GetCategoriesWithArticle()
        {
            return _category.GetList();
                            
        }

        public List<Category> List()
        {
            return _category.List();
        }

        public List<Category> ListFilter()
        {
            return _category.ListFilter(x => x.Status == true);
        }

        public void Update(Category entity)
        {
            _category.Update(entity);
        }
    }
}
