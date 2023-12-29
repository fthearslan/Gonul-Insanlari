using BussinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<Article> GetAll()
        {
            return _article.GetAll().Where(x => x.Status == true).ToList();
        }

        public Article GetById(int id)
        {
            return _article.Get(x => x.ArticleID == id);
        }

        public List<Article> GetDrafts()
        {
            return _article.ListReleased().Where(x => x.IsDraft == true).ToList();
        }

        public Article GetWithVideos(int id)
        {
            return _article.GetWithVideos(id);
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

        //public async Task UpdateAsync(Article entity)
        //{
        //   await _article.Update(entity);
        //}
        public void Update(Article entity)
        {
            _article.Update(entity);
        }
    }
}
