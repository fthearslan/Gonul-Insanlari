using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.DTOs;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.EntityFramework
{
    public class EFCategoryDAL : GenericRepository<Category>, ICategoryDAL
    {
        public Category GetDetails(int id)
        {
            using (var c = new Context())
            {
                return c.Categories.Where(c=>c.CategoryID==id).Select(c => new Category
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
            using(var c= new Context()) 
            {


                return c.Categories.Select(x => new CategoryDto
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
