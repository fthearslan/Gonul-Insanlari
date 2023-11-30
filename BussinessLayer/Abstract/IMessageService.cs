using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract
{
   public interface IMessageService : IGenericService<Message>
    {
        List<Message> GetListWithSender(int id);
    }
}
