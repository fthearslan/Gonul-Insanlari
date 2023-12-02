﻿using BussinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer;
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

        public void Add(Contact entity)
        {
            _contact.Insert(entity);

        }

        public void Delete(Contact entity)
        {
            _contact.Delete(entity);
        }

        public Contact GetById(int id)
        {

            return _contact.Get(x => x.ID == id);
        }

        public List<Contact> List()
        {
            return _contact.List();
        }

        public List<Contact> ListFilter()
        {
            return _contact.ListFilter(x => x.Status == true);
        }

        public void Update(Contact entity)
        {
            _contact.Update(entity);
        }
    }
}