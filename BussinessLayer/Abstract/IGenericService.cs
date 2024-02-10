using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract
{
   public interface IGenericService<T> where T : class
    {
       Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetById(int id);
        List<T> List();
        List<T> ListFilter();

        void InsertWithRelated(T entity);

    }
}
