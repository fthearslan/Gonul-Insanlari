using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.DTOs;
using EntityLayer;
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

                }).AsNoTrackingWithIdentityResolution().ToList();


            }

        }
    }
}
