using BussinessLayer.Abstract.Services;
using DataAccessLayer.Abstract.SubRepositories;
using EntityLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Contact;

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

        public async Task<List<Contact>> GetContactsAsync(ContactStatus? contactStatus, string? senderId, bool? status)
        {

            if (senderId is not null && contactStatus is not null)
                return await _contact.GetContactsAsync((ContactStatus)contactStatus, senderId);

            if (status is not null && senderId is not null)
                return await _contact.GetContactsAsync((bool)status, senderId);

            if (contactStatus is not null)
                return await _contact.GetContactsAsync((ContactStatus)contactStatus);
              
            return null;


        }


        public IQueryable<Contact> GetWhere(Expression<Func<Contact, bool>> filter)
        {

            return _contact.GetWhere(filter);
        }

        public async Task<Contact> GetWithReply(int id)
        {
            return await _contact.GetWhere(contact => contact.Id == id)
                            .Include(x => x.RepliedTo)
                            .SingleOrDefaultAsync();

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
            return _contact
                .ListFilter(x => x.Status == true && x.IsSeen == false)
                .OrderByDescending(x => x.Created)
                .ToList();
        }

        public async Task<List<Contact>> SearchByAsync(string search)
        {
            return await _contact.SearchByAsync(search);

        }

        public async Task<List<Contact>> SearchByAsync(string search, ContactStatus status, string? senderId)
        {

            if (status == ContactStatus.Received | status == ContactStatus.Newsletter)
                return await _contact.SearchByAsync(search, status);

            if (senderId is not null && status == ContactStatus.Trash)
                return await _contact.SearchByAsync(search, senderId, true);


            if (senderId is not null && status != ContactStatus.Received | status != ContactStatus.Newsletter)
                return await _contact.SearchByAsync(search, status, senderId);



            return await _contact.SearchByAsync(search, status);



        }

        public void Update(Contact entity)
        {
            _contact.Update(entity);
        }
    }
}
