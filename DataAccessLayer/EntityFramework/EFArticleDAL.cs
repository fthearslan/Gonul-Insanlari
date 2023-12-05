using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFArticleDAL : GenericRepository<Article>, IArticleDAL
    {
        public List<Article> ListWithCategory()
        {
            using (var c = new Context())
            {
                return c.Articles.Include(a => a.Category).OrderByDescending(a=>a.Created).Where(x=>x.Status==true).ToList();
            }
        }
    }
}
