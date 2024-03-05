using BussinessLayer.Abstract.Services;
using DataAccessLayer.Abstract.SubRepositories;
using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete.Managers
{
    public class NewsLetterManager : INewsLetterService
    {
        INewsLetterDAL _news;

        public NewsLetterManager(INewsLetterDAL news)
        {
            _news = news;
        }

        public async Task AddAsync(NewsLetter entity)
        {
            await _news.InsertAsync(entity);
        }
        public void Delete(NewsLetter entity)
        {
            _news.Delete(entity);
        }

        public async Task<NewsLetter> GetByIdAsync(int id)
        {
            return await _news.GetAsync(x => x.Id == id);
        }

        public IQueryable<NewsLetter> GetWhere(Expression<Func<NewsLetter, bool>> filter)
        {
            return _news.GetWhere(filter);
        }

        public void InsertWithRelated(NewsLetter entity)
        {
            _news.InsertWithRelated(entity);
        }

        public List<NewsLetter> List()
        {
            return _news.List();
        }

        public List<NewsLetter> ListFilter()
        {
            return _news.ListFilter(x => x.Status == true);
        }

        public void Update(NewsLetter entity)
        {
            _news.Update(entity);
        }
    }
}
