using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract.Services
{
    public interface IGenericService<T> where T : class
    {
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> GetByIdAsync(int id);
        List<T> List();
        List<T> ListFilter();

        IQueryable<T> GetWhere(Expression<Func<T, bool>> filter);

        void InsertWithRelated(T entity);

    }
}
