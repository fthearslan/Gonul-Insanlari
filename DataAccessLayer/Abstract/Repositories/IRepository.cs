using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract.Repositories
{
    public interface IRepository<T> where T : class
    {

        Task InsertAsync(T entity);
        void InsertWithRelated(T entity);
        void Delete(T entity);
        List<T> List();
        IQueryable<T> GetWhere(Expression<Func<T, bool>> filter);
        List<T> ListFilter(Expression<Func<T, bool>> filter);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        void Update(T entity);
        void Test();

    }
}
