using DataAccessLayer.Abstract.SubRepositories;
using DataAccessLayer.Concrete.Providers;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EFContactDAL : GenericRepository<Contact>, IContactDAL
    {
        public async Task<List<Contact>> GetContactsAsync(ContactStatus status)
        {

            using var c = new Context();

            return await c.Contacts
                 .Where(contact => contact.ContactStatus == status)
                     .OrderByDescending(x => x.Created)
                 .AsNoTrackingWithIdentityResolution()
                 .ToListAsync();

        }

        public async Task<List<Contact>> GetContactsAsync(ContactStatus status, string senderId)
        {

            using var c = new Context();

            return await c.Contacts
                 .Where(contact => contact.ContactStatus == status && contact.SenderId == senderId)
                     .OrderByDescending(x => x.Created)
                 .AsNoTrackingWithIdentityResolution()
                 .ToListAsync();

        }

        public async Task<List<Contact>> GetContactsAsync(bool status, string senderId)
        {
            using var c = new Context();

            return await c.Contacts
                 .Where(contact => contact.Status == status && contact.SenderId == senderId)
                     .OrderByDescending(x => x.Created)
                 .AsNoTrackingWithIdentityResolution()
                 .ToListAsync();


        }
      
        public async Task<List<Contact>> SearchByAsync(string search)
        {

            using (var c = new Context())
            {
                return await c.Contacts
                    .Where(x => x.Subject.Contains(search) || x.From.Contains(search))
                    .OrderBy(x => x.Subject.Contains(search))
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();

            }

        }

        public async Task<List<Contact>> SearchByAsync(string search, ContactStatus status)
        {

            using var c = new Context();

            return await c.Contacts
                    .Where(contact => contact.Tos.Any(x=>x.EmailAddress.Contains(search)) | contact.From.Contains(search) | contact.Subject.Contains(search) && contact.ContactStatus == status)
                    .OrderByDescending(contact => contact.Created)
                    .OrderByDescending(contact => contact.Subject.Contains(search))
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();

        }
        public async Task<List<Contact>> SearchByAsync(string search, ContactStatus status, string senderId)
        {

            using var c = new Context();

            return await c.Contacts
                .Where(contact => contact.ContactStatus == status && contact.SenderId == senderId)
                    .Where(contact => contact.Tos.Any(x => x.EmailAddress.Contains(search)) | contact.From.Contains(search) | contact.Subject.Contains(search))
                    .OrderByDescending(contact => contact.Created)
                    .OrderByDescending(contact => contact.Subject.Contains(search))
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();



        }
        public async Task<List<Contact>> SearchByAsync(string search, string senderId, bool isTodelete)
        {
            using var c = new Context();

            return await c.Contacts
                    .Where(contact => contact.Tos.Any(x => x.EmailAddress.Contains(search)) | contact.From.Contains(search) | contact.Subject.Contains(search) && contact.SenderId == senderId && contact.Status == !isTodelete)
                    .OrderByDescending(contact => contact.Created)
                .OrderByDescending(contact => contact.Subject.Contains(search))
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();

        }

  
    }
}
