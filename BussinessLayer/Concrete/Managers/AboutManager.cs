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
    public class AboutManager : IAboutService
    {
        IAboutDAL _about;

        public AboutManager(IAboutDAL about)
        {
            _about = about;
        }

        public async Task AddAsync(About entity)
        {
            await _about.InsertAsync(entity);
        }

        public void Delete(About entity)
        {
            _about.Delete(entity);
        }

        public async Task<About> GetByIdAsync(int id)
        {
            return await _about.GetAsync(x => x.ID == id);
        }

        public IQueryable<About> GetWhere(Expression<Func<About, bool>> filter)
        {
            return _about.GetWhere(filter);
        }

        public void InsertWithRelated(About entity)
        {
            _about.InsertWithRelated(entity);
        }

        public List<About> List()
        {
            return _about.List();
        }

        public List<About> ListFilter()
        {
            return _about.ListFilter(x => x.Status == true);
        }

        public void Update(About entity)
        {
            _about.Update(entity);
        }
    }
}
