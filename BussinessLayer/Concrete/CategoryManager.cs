using BussinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.DTOs;
using EntityLayer.Entities;
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

        public async Task AddAsync(Category entity)
        {
            await _category.InsertAsync(entity);
        }

        public void Delete(Category entity)
        {
            _category.Delete(entity);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _category.GetAsync(x => x.CategoryID == id);
        }

        public List<CategoryDto> GetCategoriesWithArticle()
        {
            return _category.GetList();

        }

        public Category GetDetails(int id)
        {
            return _category.GetDetails(id);
        }

        public void InsertWithRelated(Category entity)
        {
            _category.InsertWithRelated(entity);
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
