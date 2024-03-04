using DataAccessLayer.Abstract.Repositories;
using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract.SubRepositories
{
    public interface IAssignmentDAL : IRepository<Assignment>
    {
        List<Assignment> GetAssignmentBar(int id);



        List<Assignment> GetListDashboard();

        Task PublishAsync(Assignment assignment);
    }
}
