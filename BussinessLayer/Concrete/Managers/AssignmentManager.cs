using BussinessLayer.Abstract.Services;
using DataAccessLayer.Abstract.SubRepositories;
using DataAccessLayer.Concrete.DTOs.Assignment;
using DataAccessLayer.Migrations;
using EntityLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete.Managers
{
    public class AssignmentManager : IAssignmentService
    {
        IAssignmentDAL _assignment;

        public AssignmentManager(IAssignmentDAL assignment)
        {
            _assignment = assignment;
        }

        public async Task AddAsync(Assignment entity)
        {
            await _assignment.InsertAsync(entity);
        }

        public void Delete(Assignment entity)
        {
            _assignment.Delete(entity);

        }


        public List<Assignment> GetListDashboard()
        {
            return _assignment.GetListDashboard();
        }

        public List<Assignment> GetAssignmentBar(int id)
        {
            return _assignment.GetAssignmentBar(id);
        }


        public async Task<Assignment> GetByIdAsync(int id)
        {
            return await _assignment.GetAsync(x => x.Id == id);
        }

        public void InsertWithRelated(Assignment entity)
        {
            _assignment.InsertWithRelated(entity);
        }

        public List<Assignment> List()
        {
            return _assignment.List();
        }

        public List<Assignment> ListFilter()
        {
            return _assignment.ListFilter(x => x.Status == true);
        }

        public void Update(Assignment entity)
        {
            _assignment.Update(entity);
        }

        public async Task PublishAsync(Assignment assignment)
        {
            await _assignment.PublishAsync(assignment);
        }

        public IQueryable<Assignment> GetWhere(Expression<Func<Assignment, bool>> filter)
        {
            return _assignment.GetWhere(filter);
        }

        public async Task<List<AssignmentByProgressDto>> GetByProgress(Assignment.ProgressStatus progress)
        {
            return await _assignment.GetByProgress(progress);
        }

        public List<AssignmentListDto> GetAll()
        {
            return _assignment.GetAll();
        }

        public void AddSubTask(SubTask task)
        {
            _assignment.AddSubTask(task);
        }

        public async Task<bool> AddAttachmentsAsync(List<TaskAttachment> taskAttachments)
        {

            return await _assignment.AddAttachmentsAsync(taskAttachments);

        }

        public bool IsUser(Assignment task, string _currentUserId)
        {
            return _assignment.IsUser(task, _currentUserId);

        }

        public async Task<bool> DeleteAttachmentAsync(string _path, int _taskId)
        {
            return await _assignment.DeleteAttachmentAsync(_path, _taskId);
        }

        public async Task LogAsync(TaskLog log)
        {
            await _assignment.LogAsync(log);

        }

        public async Task<List<TaskLog>> GetLogsByTaskAsync(int _taskId)
        {
            return await _assignment.GetLogsByTaskAsync(_taskId);

        }

        public async Task LogAsync(TaskLog log, int _userId)
        {
            await _assignment.LogAsync(log, _userId);

        }
    }
}
