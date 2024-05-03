using DataAccessLayer.Abstract.Repositories;
using DataAccessLayer.Concrete.DTOs.Assignment;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract.SubRepositories
{
    public interface IAssignmentDAL : IRepository<Assignment>
    {
        List<Assignment> GetAssignmentBar(int id);

        Task<List<AssignmentByProgressDto>> GetByProgress(Assignment.ProgressStatus progress);

        List<Assignment> GetListDashboard();

        Task PublishAsync(Assignment assignment);

        List<AssignmentListDto> GetAll();

        void AddSubTask(SubTask task);

        Task<bool> AddAttachmentsAsync(List<TaskAttachment> taskAttachments);

        bool IsUser(Assignment task, string _currentUserId);

        Task<bool> DeleteAttachmentAsync(string _path, int _taskId);

        Task LogAsync(TaskLog log);

        Task LogAsync(TaskLog log,int _userId);

        Task<List<TaskLog>> GetLogsByTaskAsync(int _taskId);

    }
}
