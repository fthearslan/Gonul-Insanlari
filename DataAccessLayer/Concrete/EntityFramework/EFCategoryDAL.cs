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
       
        public Category GetDetails(int id)
        {
            using (var db= new Context())
            {
                return db.Categories.Where(c => c.CategoryID == id).Select(c => new Category
                {
                    CategoryID = c.CategoryID,
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

                    CategoryID = x.CategoryID,
                    Name = x.Name,
                    Status = x.Status,
                    ArticleCount = x.Articles.Count

                }).AsNoTrackingWithIdentityResolution()
                .ToList();


            }

        }
    }
}
