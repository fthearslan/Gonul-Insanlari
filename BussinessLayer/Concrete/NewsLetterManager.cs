using BussinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete
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

        public async  Task<NewsLetter> GetByIdAsync(int id)
        {
            return await  _news.GetAsync(x => x.ID == id);
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
            return _news.ListFilter(x => x.Status == "True");
        }

        public void Update(NewsLetter entity)
        {
            _news.Update(entity);
        }
    }
}
