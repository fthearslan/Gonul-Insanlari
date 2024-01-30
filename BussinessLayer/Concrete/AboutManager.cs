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
    public class AboutManager : IAboutService
    {
        IAboutDAL _about;

        public AboutManager(IAboutDAL about)
        {
            _about = about;
        }

        public void Add(About entity)
        {
            _about.Insert(entity);
        }

        public void Delete(About entity)
        {
            _about.Delete(entity);
        }

        public About GetById(int id)
        {
            return _about.Get(x => x.ID == id);
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
