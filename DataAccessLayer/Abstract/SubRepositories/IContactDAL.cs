using AutoMapper.Configuration.Conventions;
using DataAccessLayer.Abstract.Repositories;
using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract.SubRepositories
{
    public interface IContactDAL : IRepository<Contact>
    {

        Task<List<Contact>> GetContactsAsync(ContactStatus status);
        Task<List<Contact>> GetContactsAsync(ContactStatus status, string senderId);
        Task<List<Contact>> GetContactsAsync(bool status, string senderId);
   
        Task<List<Contact>> SearchByAsync(string search);

        Task<List<Contact>> SearchByAsync(string search, ContactStatus status);

        Task<List<Contact>> SearchByAsync(string search, ContactStatus status,string senderId);

        Task<List<Contact>> SearchByAsync(string search, string senderId, bool isTodelete);
   

    }
}
