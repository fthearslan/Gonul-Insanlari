using BussinessLayer.Abstract.Services;
using DataAccessLayer.Abstract.SubRepositories;
using DataAccessLayer.Concrete.DTOs.Category;
using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete.Managers
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

        public IQueryable<Category> GetWhere(Expression<Func<Category, bool>> filter)
        {
            return _category.GetWhere(filter);
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
