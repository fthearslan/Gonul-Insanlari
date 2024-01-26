using BussinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BussinessLayer.Concrete
{
    public class ArticleManager : IArticleService
    {
        IArticleDAL _article;

        public ArticleManager(IArticleDAL article)
        {
            _article = article;
        }

        public void Add(Article entity)
        {
            _article.Insert(entity);
        }

        public void Delete(Article entity)
        {
            _article.Delete(entity);
        }

        public List<Article> GetAllIncludeDrafts()
        {
            return _article.GetAllIncludeDrafts().Where(x => x.Status == true).ToList();
        }

        public Article GetById(int id)
        {
            return _article.Get(x => x.ArticleID == id);
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

    }
}
