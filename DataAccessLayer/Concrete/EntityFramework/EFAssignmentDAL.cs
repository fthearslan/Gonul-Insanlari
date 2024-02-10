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
                 .Where(a => a.Receiver.Id == id && a.Status == true | a.Receiver == null && a.Status == true | a.Sender.Id == id && true)
                 .Include(a => a.Sender)
                 .Include(a => a.Receiver)
                 .OrderByDescending(x => x.Created)
                 .AsNoTrackingWithIdentityResolution()
                 .ToList();
            }
        }

        public List<Assignment> GetAssignmentsWithSender(int id)
        {
            using (var c = new Context())
            {
                return c.Assignments
                    .Include(x => x.Sender)
                    .Where(x => x.Receiver.Id == id | x.Sender.Id==id && x.Status == true && x.IsCompleted == false)
                    .OrderByDescending(x => x.Created)
                    .Take(6)
                    .ToList();
            }
        }
    }
}
