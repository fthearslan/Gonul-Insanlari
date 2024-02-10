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

        public Contact GetById(int id)
        {

            return _contact.Get(x => x.ID == id);
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
            return _contact.ListFilter(x => x.Status == true && x.IsSeen==false).OrderByDescending(x=>x.Created).ToList();
        }

        public void Update(Contact entity)
        {
            _contact.Update(entity);
        }
    }
}
