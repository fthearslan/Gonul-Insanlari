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
    public class ContactManager : IContactService
    {
        IContactDAL _contact;

        public ContactManager(IContactDAL contact)
        {
            _contact = contact;
        }


        public async Task AddAsync(Contact entity)
        {
            await _contact.InsertAsync(entity);

        }

        public void Delete(Contact entity)
        {
            _contact.Delete(entity);
        }

        public async Task<Contact> GetByIdAsync(int id)
        {

            return await _contact.GetAsync(x => x.Id == id);
        }

        public async Task<List<Contact>> GetInbox()
        {

            return await _contact.GetInbox();

        }

        public IQueryable<Contact> GetWhere(Expression<Func<Contact, bool>> filter)
        {

            return _contact.GetWhere(filter);
        }

        public void InsertWithRelated(Contact entity)
        {
            _contact.InsertWithRelated(entity);
        }

        public List<Contact> List()
        {
            return _contact.List();
        }

        public List<Contact> ListFilter()
        {
            return _contact.ListFilter(x => x.Status == true && x.IsSeen == false).OrderByDescending(x => x.Created).ToList();
        }

        public async Task<List<Contact>> SearchByAsync(string search)
        {
            return await _contact.SearchByAsync(search);

        }

        public void Update(Contact entity)
        {
            _contact.Update(entity);
        }
    }
}
