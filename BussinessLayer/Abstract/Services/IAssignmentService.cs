using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract.Services
{
    public interface IAssignmentService : IGenericService<Assignment>
    {
        List<Assignment> GetAssignmentBar(int publisherId);
        Task<List<Assignment>> GetByProgress(Assignment.ProgressStatus progress);

        List<Assignment> GetListDashboard();
        Task PublishAsync(Assignment assignment);
    }
}
