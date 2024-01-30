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
    public class GenericRepository<T>: IRepository<T> where T : class
    {
        Context db = new Context();
        
        DbSet<T> _dbset;

        public GenericRepository()
        {
            _dbset = db.Set<T>();
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
            db.SaveChanges();
        }

        public T  Get(Expression<Func<T, bool>> filter)
        {
            return  _dbset.SingleOrDefault(filter);
        }

        public void Insert(T entity)
        {
           _dbset.Add(entity);
            db.SaveChanges();
        }

        public void InsertWithRelated(T entity)
        {
            var refs = db.Entry(entity).References.ToList();
            if (refs.Count > 0)
                foreach (var item in refs)
                {
                    item.EntityEntry.State = EntityState.Unchanged;
                }
            _dbset.Add(entity);
            db.SaveChanges();
        }

        public List<T> List()
        {
            return _dbset.
                AsNoTrackingWithIdentityResolution()
                .ToList();
        }

        public List<T> ListFilter(Expression<Func<T, bool>> filter)
        {
            return _dbset.Where(filter)
                .AsNoTrackingWithIdentityResolution()
                .ToList();
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
            db.SaveChanges();
        }
    }
}
