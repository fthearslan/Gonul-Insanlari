using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EFAssignmentDAL : GenericRepository<Assignment>, IAssignmentDAL
    {
        public List<Assignment> GetListDashboard()
        {
            using (var c = new Context())
            {

                return c.Assignments
                .Where(a => a.Status == true && a.Progress == Assignment.ProgressStatus.InProgress)
                  .OrderByDescending(x => x.Created)
                .Select(x => new Assignment()
                {
                    AssignmentId = x.AssignmentId,
                    Due = x.Due,
                    Title = x.Title,
                    Progress = x.Progress,
                }).AsNoTrackingWithIdentityResolution()
                 .ToList();
            }
        }

        public List<Assignment> GetAssignmentBar(int userId)
        {
            using (var c = new Context())
            {


                return c.UserAssignment
                          .Where(u => u.UserId == userId && u.Assignment.Progress == Assignment.ProgressStatus.InProgress)
                     .Include(u => u.Assignment)
                       .OrderByDescending(x => x.Assignment.Created)
                       .Select(a => new Assignment()
                       {
                           AssignmentId = a.AssignmentId,
                           Content = a.Assignment.Content,
                           Due = a.Assignment.Due,
                           Created = a.Assignment.Created,
                           Title = a.Assignment.Title,

                       }).AsNoTrackingWithIdentityResolution()
                   .Take(6)
                   .ToList();

            }
        }

        public async Task PublishAsync(Assignment assignment)
        {

            using (var c = new Context())
            {
                c.Entry(assignment.Publisher).State = EntityState.Unchanged;
                await c.Assignments.AddAsync(assignment);
                await c.SaveChangesAsync();
            }

        }
    }
}
