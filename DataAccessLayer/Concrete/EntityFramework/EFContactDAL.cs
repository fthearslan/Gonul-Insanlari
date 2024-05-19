using DataAccessLayer.Abstract.SubRepositories;
using DataAccessLayer.Concrete.Providers;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EFContactDAL : GenericRepository<Contact>, IContactDAL
    {
        public async Task<List<Contact>> GetDraftsAsync(string _senderId)
        {
            using (var c = new Context())
            {
                return await c.Contacts
                    .Where(x => x.SenderId == _senderId && x.IsDraft == true && x.Status==true)
                    .OrderByDescending(x => x.Created)
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();
            }


        }

        public async Task<List<Contact>> GetInboxAsync()
        {

            using (var c = new Context())
            {

                return await c.Contacts.
                    Where(x => x.Status == true && x.IsSent==false && x.IsDraft==false).
                    OrderByDescending(x => x.Created).
                    AsNoTrackingWithIdentityResolution().
                    ToListAsync();

            }



        }

        public async Task<List<Contact>> GetSentboxAsync(string senderId)
        {

            using (var c = new Context())
            {
                return await c.Contacts
                    .Where(x => x.SenderId == senderId && x.Status == true)
                    .Where(x => x.IsSent == true && x.IsDraft == false)
                    .OrderByDescending(x => x.Created)
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();
            }

        }

        public async Task<List<Contact>> GetTrashAsync()
        {

            using (var c = new Context())
            {
                return await c.Contacts
                    .Where(x => x.Status == false)
                    .OrderByDescending(x => x.Created)
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();
            }


        }

        public async Task<List<Contact>> SearchByAsync(string search)
        {

            using (var c = new Context())
            {
                return await c.Contacts
                    .Where(x => x.Subject.Contains(search) || x.NameSurname.Contains(search) || x.EmailAddress.Contains(search))
                    .OrderBy(x => x.Subject.Contains(search))
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();

            }

        }
        public async Task<List<Contact>> SearchByAsync(string search, string senderId, bool isDraft, bool isTodelete, bool isSent)
        {

            using (var c = new Context())
            {


                return await c.Contacts
                    .Where(x => x.SenderId == senderId)
                    .Where(x => x.IsDraft == isDraft && x.IsSent == isSent)
                    .Where(x => x.Status == !isTodelete)
                    .Where(x => x.Subject.Contains(search) || x.NameSurname.Contains(search) || x.EmailAddress.Contains(search))
                    .OrderBy(x => x.Subject.Contains(search))
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();


            }

        }
    }
}
