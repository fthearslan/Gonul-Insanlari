using DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        Context db = new Context();
        
        DbSet<T> _dbset;

        public GenericRepository()
        {
            _dbset = db.Set<T>();
        }

        public void Delete(T entity)
        {
            var DeletedEntity = db.Entry(entity);
            DeletedEntity.State = EntityState.Deleted;
            db.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _dbset.SingleOrDefault(filter);
        }

        public void Insert(T entity)
        {
            var AddedEntity = db.Entry(entity);
            AddedEntity.State = EntityState.Added;
            db.SaveChanges();
        }

        public List<T> List()
        {
            return _dbset.ToList();

        }

        public List<T> ListFilter(Expression<Func<T, bool>> filter)
        {
            return _dbset.Where(filter).ToList();
        }

        public void Update(T entity)
        {
            var UpdatedEntity = db.Entry(entity);
            UpdatedEntity.State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
