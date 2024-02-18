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
        public List<Assignment> GetList(int id)
        {
            using (var c = new Context())
            {

                return c.Assignments
                .Where(a => a.AssignmentId == id)
                .OrderByDescending(x => x.Created)
                 .AsNoTrackingWithIdentityResolution()
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
                           Due=a.Assignment.Due,
                           Created= a.Assignment.Created,
                           Title=a.Assignment.Title,

                   }).AsNoTrackingWithIdentityResolution()
                   .Take(6)
                   .ToList();
                 
            }
        }
    }
}
