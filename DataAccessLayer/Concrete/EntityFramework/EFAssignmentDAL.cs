using DataAccessLayer.Abstract.SubRepositories;
using DataAccessLayer.Concrete.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using DataAccessLayer.Concrete.DTOs.Assignment;
using System.Globalization;
using AutoMapper;
using System.Threading.Tasks.Sources;
using System.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using EntityLayer.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using AutoMapper.Internal;
using System.Dynamic;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EFAssignmentDAL : GenericRepository<Assignment>, IAssignmentDAL
    {


        public List<Assignment> GetListDashboard()
        {
            using var db = new Context();
            return db.Assignments
            .Where(a => a.Status == true && a.Progress == Assignment.ProgressStatus.InProgress)
              .OrderByDescending(x => x.Created)
            .Select(x => new Assignment()
            {
                Id = x.Id,
                Due = x.Due,
                Title = x.Title,
                Progress = x.Progress,
            }).AsNoTrackingWithIdentityResolution()
             .ToList();

        }

        public List<Assignment> GetAssignmentBar(int userId)
        {
            using var db = new Context();
            return db.UserAssignment
                      .Where(u => u.UserId == userId && u.Assignment.Progress == Assignment.ProgressStatus.InProgress)
                 .Include(u => u.Assignment)
                   .OrderByDescending(x => x.Assignment.Created)
                   .Select(a => new Assignment()
                   {
                       Id = a.AssignmentId,
                       Content = a.Assignment.Content,
                       Due = a.Assignment.Due,
                       Title = a.Assignment.Title,
                       SubTasks = a.Assignment.SubTasks,
                       Created = a.Assignment.Created

                   }).AsNoTrackingWithIdentityResolution()
               .Take(6)
               .ToList();
        }

        public async Task PublishAsync(Assignment assignment)
        {

            using var db = new Context();

            db.Entry(assignment.Publisher).State = EntityState.Unchanged;
            await db.Assignments.AddAsync(assignment);
            await db.SaveChangesAsync();

        }

        public async Task<List<AssignmentByProgressDto>> GetByProgress(Assignment.ProgressStatus progress)
        {
            using var db = new Context();
            return await db.Assignments
                   .Where(a => a.Progress == progress)
                   .Select(a => new AssignmentByProgressDto()
                   {
                       Id = a.Id,
                       Title = a.Title,
                       Content = a.Content,
                       Created = a.Created,
                       Publisher = a.Publisher.UserName,
                       SubTasks = a.SubTasks.Count,
                       SubTasksDone = a.SubTasks.Where(s => s.Progress == SubTasksProgress.Done).Count(),
                       UserImagePaths = a.UserAssignments.Select(x => x.User.ImagePath).ToList()
                   })
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public List<AssignmentListDto> GetAll()
        {
            using var db = new Context();

            return db.Assignments
            .IgnoreAutoIncludes()
            .Select(a => new AssignmentListDto()
            {
                Id = a.Id,
                Title = a.Title,
                Created = a.Created,
                Publisher = a.Publisher.UserName,
                SubTasks = a.SubTasks.Count,
                Progress = a.Progress.ToString(),
                UserCount = a.UserAssignments.Count,

            })
            .OrderByDescending(x => x.Created)
            .ToList();

        }

        public void AddSubTask(SubTask task)
        {
            using var c = new Context();
            {
                c.Entry(task.Assignment).State = EntityState.Unchanged;
                c.Entry(task.Assignment.Publisher).State = EntityState.Unchanged;
                c.SubTask.Add(task);
                c.SaveChanges();

            }

        }

        public async Task<bool> AddAttachmentsAsync(List<TaskAttachment> taskAttachments)
        {
            if (taskAttachments is null)
                return false;

            using var c = new Context();
            {
                foreach (var attachment in taskAttachments)
                    c.Entry(attachment.Assignment).State = EntityState.Modified;

                await c.TaskAttachment.AddRangeAsync(taskAttachments);

                if (await c.SaveChangesAsync() > 0)
                    return true;
                else
                    return false;

            }
        }




    }


}
