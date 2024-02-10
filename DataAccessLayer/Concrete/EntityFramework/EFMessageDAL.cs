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
    public class EFMessageDAL : GenericRepository<Message>, IMessageDAL
    {
        public List<Message> GetListhWithSender(int id)
        {
            using (var c = new Context())
            {
                return c.Messages.Include(x => x.Sender).Where(x => x.Receiver.Id == id && x.IsDraft == false && x.Status == true && x.IsSeen == false).OrderByDescending(x => x.Created).ToList();
            }
        }
    }
}
