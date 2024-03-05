using DataAccessLayer.Abstract.SubRepositories;
using DataAccessLayer.Concrete.Repositories;
using DataAccessLayer.Concrete.Providers;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete.Entities;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EFArticleDAL : GenericRepository<Article>, IArticleDAL
    {
        public List<Article> GetAllIncludeDrafts()
        {
            using (var db = new Context())
            {
                return  db.Articles
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

            using (var db = new Context())
            {

                return db.Articles
                     .Where(a => a.Id == id)
                     .Include(c => c.Category)
                     .Include(u => u.AppUser)
                     .FirstOrDefault();
            };

        }

        public List<Article> GetDraftsByUser(int userId)
        {

            using (var db = new Context())
            {
                return db.Articles
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
            using (var db = new Context())
            {
                return db.Articles
                    .Where(x => x.Id == id && x.Status == true)
                    .Include(a => a.AppUser)
                    .AsNoTrackingWithIdentityResolution()
                    .FirstOrDefault();

            }

        }

        public List<Article> ListReleased()
        {
            using (var db = new Context())
            {
                return db.Articles
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
            using (var db = new Context())
            {
                return db.Articles
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
            using (var db = new Context())
            {
                return db.Articles
                    .Where(a => a.CategoryID == id && a.IsDraft == false)
                    .OrderByDescending(a => a.Created).Select(a => new Article
                    {
                        Id = a.Id,
                        Title = a.Title,
                        AppUser = a.AppUser,

                    }).AsNoTrackingWithIdentityResolution()
                    .ToList();

            }


        }
    }

}

