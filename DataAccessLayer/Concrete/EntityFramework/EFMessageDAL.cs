using DataAccessLayer.Abstract.SubRepositories;
using DataAccessLayer.Concrete.Repositories;
using DataAccessLayer.Concrete.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete.Entities;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EFMessageDAL : GenericRepository<Message>, IMessageDAL
    {
    
        public List<Message> GetListhWithSender(int id)
        {
            using(var db= new Context())
            {

                return db.Messages.Include(x => x.Sender).Where(x => x.Receiver.Id == id && x.IsDraft == false && x.Status == true && x.IsSeen == false).OrderByDescending(x => x.Created).ToList();

            }

        }
    }
}
