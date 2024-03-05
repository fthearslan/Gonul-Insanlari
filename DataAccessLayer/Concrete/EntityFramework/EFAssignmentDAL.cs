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

        public async Task<List<Assignment>> GetByProgress(Assignment.ProgressStatus progress)
        {
            using var db = new Context();
            return await db.Assignments
                   .Where(a => a.Progress == progress)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

       
    }
}
