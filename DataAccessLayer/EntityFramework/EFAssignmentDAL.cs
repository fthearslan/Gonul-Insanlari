using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFAssignmentDAL : GenericRepository<Assignment>, IAssignmentDAL
    {
        public List<Assignment> GetAssignmentsWithSender(int id)
        {
            using (var c = new Context())
            {
              return  c.Assignments.Include(x=>x.Sender).Where(x=>x.Receiver.Id== id | x.Receiver==null && x.Status==true && x.IsCompleted==false).OrderByDescending(x=>x.Created).Take(6).ToList();
            }
        }
    }
}
