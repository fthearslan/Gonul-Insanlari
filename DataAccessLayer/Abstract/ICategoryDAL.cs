using DataAccessLayer.DTOs;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICategoryDAL : IRepository<Category>
    {
       List<CategoryDto> GetList();
        Category GetDetails(int id);

    }
}
