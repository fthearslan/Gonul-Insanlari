using DataAccessLayer.Abstract.SubRepositories;
using DataAccessLayer.Concrete.DTOs.Category;
using DataAccessLayer.Concrete.Repositories;
using DataAccessLayer.Concrete.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete.Entities;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EFCategoryDAL : GenericRepository<Category>, ICategoryDAL
    {
        public List<Category> GetCategoriesWithArticleCount(int takeCount)
        {
            using var c = new Context();

            return c.Categories
                      .Where(x => x.Status == true)
                      .Include(x => x.Articles)
                      .Select(x => new Category
                      {
                          Name = x.Name,
                          Articles = x.Articles
                     
                      }).OrderByDescending(x => x.Articles.Count)
                      .Take(takeCount)
                      .ToList();


        }

        public Category GetDetails(int id)
        {
            using (var db = new Context())
            {
                return db.Categories.Where(c => c.Id == id).Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    ImagePath = c.ImagePath,
                }).AsNoTrackingWithIdentityResolution()
                .FirstOrDefault();
            }

        }

        public List<CategoryDto> GetList()
        {
            using (var db = new Context())
            {


                return db.Categories.Select(x => new CategoryDto
                {

                    CategoryID = x.Id,
                    Name = x.Name,
                    Status = x.Status,
                    ArticleCount = x.Articles.Count

                }).AsNoTrackingWithIdentityResolution()
                .ToList();


            }

        }
    }
}
