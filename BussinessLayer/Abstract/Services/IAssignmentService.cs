using DataAccessLayer.Concrete.DTOs.Assignment;
using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract.Services
{
    public interface IAssignmentService : IGenericService<Assignment>
    {
        List<Assignment> GetAssignmentBar(int publisherId);
        Task<List<AssignmentByProgressDto>> GetByProgress(Assignment.ProgressStatus progress);
        List<AssignmentListDto> GetAll();
        List<Assignment> GetListDashboard();
        Task PublishAsync(Assignment assignment);

       void AddSubTask(SubTask task);
    }
}
