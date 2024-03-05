using BussinessLayer.Abstract.Services;
using DataAccessLayer.Abstract.SubRepositories;
using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete.Managers
{
    public class MessageManager : IMessageService
    {
        IMessageDAL _message;

        public MessageManager(IMessageDAL message)
        {
            _message = message;
        }

        public async Task AddAsync(Message entity)
        {
            await _message.InsertAsync(entity);
        }

        public void Delete(Message entity)
        {
            _message.Delete(entity);
        }

        public async Task<Message> GetByIdAsync(int id)
        {
            return await _message.GetAsync(x => x.Id == id);
        }

        public List<Message> GetByWriter(int id)
        {
            return _message.ListFilter(x => x.Receiver.Id == id);
        }

        public List<Message> GetListWithSender(int id)
        {
            return _message.GetListhWithSender(id);
        }

        public IQueryable<Message> GetWhere(Expression<Func<Message, bool>> filter)
        {
            return _message.GetWhere(filter);

        }

        public void InsertWithRelated(Message entity)
        {
            _message.InsertWithRelated(entity);
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
