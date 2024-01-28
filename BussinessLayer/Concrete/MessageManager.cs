using BussinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        IMessageDAL _message;

        public MessageManager(IMessageDAL message)
        {
            _message = message;
        }

        public void Add(Message entity)
        {
            _message.Insert(entity);
        }

        public void Delete(Message entity)
        {
            _message.Delete(entity);
        }

        public Message GetById(int id)
        {
            return _message.Get(x => x.MessageID == id);
        }

        public List<Message> GetByWriter(int id)
        {
            return _message.ListFilter(x => x.Receiver.Id == id);
        }

        public List<Message> GetListWithSender(int id)
        {
            return _message.GetListhWithSender(id);
        }

        public List<Message> List()
        {
            return _message.List();
        }

        public List<Message> ListFilter()
        {
            return _message.ListFilter(x => x.Status == true);
        }

        public void Update(Message entity)
        {
            _message.Update(entity);
        }
    }
}
