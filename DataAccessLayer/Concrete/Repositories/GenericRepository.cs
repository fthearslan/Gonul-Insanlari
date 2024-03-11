using DataAccessLayer.Abstract.Repositories;
using EntityLayer.Concrete.Entities;
using DataAccessLayer.Concrete.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Abstract;

namespace DataAccessLayer.Concrete.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T :BaseEntity
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

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbset.SingleOrDefaultAsync(filter);
        }

        public async Task InsertAsync(T entity)
        {
            await _dbset.AddAsync(entity);
            await db.SaveChangesAsync();
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


        public IQueryable<T> GetWhere(Expression<Func<T, bool>> filter)
        {
            return _dbset.Where(filter);
        }

     
    }
}
