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
        public List<Assignment> GetAssignmentsWithReceiver(int id)
        {
            using (var c = new Context())
            {

                return  c.Assignments
                .Where(a=>a.AssignmentId==id && a.Status==true && a.Progress!=Assignment.ProgressStatus.Cancelled)
                .Include(a=>a.UserAssignments)
                .Include(a=>a.Publisher)
                .OrderByDescending(x => x.Created)
                 .AsNoTrackingWithIdentityResolution()
                 .ToList();
            }
        }

        public List<Assignment> GetAssignmentsWithSender(int publisherId)
        {
            using (var c = new Context())
            {
                return c.Assignments
                    .Include(a=>a.Publisher)
                    .Include(a=>a.UserAssignments)
                    .ThenInclude(a=>a.User)
                    .Where(a=>a.Publisher.Id==publisherId && a.Status == true && a.Progress != Assignment.ProgressStatus.Cancelled)
                    .OrderByDescending(x => x.Created)
                    .Take(6)
                    .ToList();
            }
        }
    }
}
