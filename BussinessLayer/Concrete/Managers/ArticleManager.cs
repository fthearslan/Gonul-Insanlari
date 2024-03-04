using BussinessLayer.Abstract.Services;
using DataAccessLayer.Abstract.SubRepositories;
using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BussinessLayer.Concrete.Managers
{
    public class ArticleManager : IArticleService
    {
        IArticleDAL _article;

        public ArticleManager(IArticleDAL article)
        {
            _article = article;
        }

        public async Task AddAsync(Article entity)
        {
            await _article.InsertAsync(entity);
        }

        public void Delete(Article entity)
        {
            _article.Delete(entity);
        }

        public List<Article> GetAllIncludeDrafts()
        {
            return _article.GetAllIncludeDrafts().Where(x => x.Status == true).ToList();
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            return await _article.GetAsync(x => x.ArticleID == id);
        }

        public Article GetByIdInclude(int id)
        {

            return _article.GetByIdInclude(id);

        }

        public List<Article> GetDraftsByUser(int userId)
        {
            return _article.GetDraftsByUser(userId).ToList();

        }

        public Article GetDetailsByUser(int id)
        {
            return _article.GetDetailsByUser(id);
        }

        public List<Article> List()
        {
            return _article.List();
        }

        public List<Article> ListFilter()
        {
            return _article.ListFilter(x => x.Status == true).OrderByDescending(x => x.Created).ToList();
        }

        public List<Article> ListReleased()
        {
            return _article.ListReleased();
        }


        public void Update(Article entity)
        {
            _article.Update(entity);
        }

        public List<Article> GetAllWithoutDrafts()
        {
            return _article.GetAllWithoutDrafts();
        }

        public List<Article> ListByCategory(int id)
        {
            return _article.GetByCategory(id);
        }

        public void InsertWithRelated(Article entity)
        {
            _article.InsertWithRelated(entity);
        }

        public IQueryable<Article> GetWhere(Expression<Func<Article, bool>> filter)
        {
            return _article.GetWhere(filter);
        }
    }
}
