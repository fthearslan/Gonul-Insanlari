using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract
{
    public interface IAssignmentService:IGenericService<Assignment>
    {
        List<Assignment> GetAssignmentBar(int publisherId);
        List<Assignment> GetList(int id);
    }
}
