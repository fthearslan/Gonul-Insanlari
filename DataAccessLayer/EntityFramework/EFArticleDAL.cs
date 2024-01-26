using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFArticleDAL : GenericRepository<Article>, IArticleDAL
    {
        public List<Article> GetAllIncludeDrafts()
        {
            using (var c = new Context())
            {
                return c.Articles
                    .Include(a => a.Category)
                    .Include(a => a.AppUser)
                    .Include(a => a.Comments)
                    .OrderByDescending(c => c.Created)
                    .AsNoTrackingWithIdentityResolution()
                    .ToList();
            }

        }

        public Article GetByIdInclude(int id)
        {

            using (var c = new Context())
            {

                return c.Articles
                     .Where(a => a.ArticleID == id)
                     .Include(c => c.Category)
                     .Include(u => u.AppUser)
                     .FirstOrDefault();
            };

        }

        public List<Article> GetDraftsByUser(int userId)
        {

            using (var c = new Context())
            {
                return c.Articles
                    .Where(a => a.IsDraft == true)
                    .Include(a => a.Category)
                    .Include(a => a.AppUser)
                    .OrderByDescending(c => c.Created)
                    .AsNoTrackingWithIdentityResolution()
                    .ToList();

            }

        }

        public Article GetDetailsByUser(int id)
        {
            using (var c = new Context())
            {
                return c.Articles
                    .Where(x => x.ArticleID == id && x.Status == true)
                    .Include(a => a.AppUser)
                    .AsNoTrackingWithIdentityResolution()
                    .FirstOrDefault();

            }

        }

        public List<Article> ListReleased()
        {
            using (var c = new Context())
            {
                return c.Articles
                    .Where(x => x.Status == true && x.IsDraft == false)
                    .Include(a => a.Category)
                    .Include(a => a.Comments)
                    .Include(a => a.AppUser)
                    .OrderByDescending(a => a.Created)
                    .AsNoTrackingWithIdentityResolution()
                    .ToList();
            }
        }

        public List<Article> GetAllWithoutDrafts()
        {
            using (var c = new Context())
            {
                return c.Articles
                    .Where(a => a.IsDraft == false)
                    .Include(a => a.Category)
                    .Include(a => a.AppUser)
                    .OrderByDescending(a => a.Created)
                    .AsNoTrackingWithIdentityResolution()
                    .ToList();
            }

        }

        public List<Article> GetByCategory(int id)
        {
            using (var c = new Context())
            {
                return c.Articles
                    .Where(a => a.CategoryID==id && a.IsDraft == false)
                    .OrderByDescending(a => a.Created).Select(a => new Article
                    {
                        ArticleID=a.ArticleID,
                        Title = a.Title,
                        AppUser = a.AppUser,

                    }).AsNoTrackingWithIdentityResolution()
                    .ToList();
                   
            }


        }
    }

}

