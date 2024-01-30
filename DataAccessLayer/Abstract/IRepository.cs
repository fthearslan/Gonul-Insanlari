using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRepository<T> where T : class
    {
        void Insert(T entity);
        void InsertWithRelated(T entity);
        void Delete(T entity);
        List<T> List();
        List<T> ListFilter(Expression<Func<T, bool>> filter);
        T Get(Expression<Func<T, bool>> filter);
        void Update(T entity);

    }
}
