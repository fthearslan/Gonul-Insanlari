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
        public async Task<List<Contact>> GetInbox()
        {

            using (var c = new Context())
            {

                return await c.Contacts.
                    Where(x => x.Status == true).
                    OrderByDescending(x => x.Created).
                    AsNoTrackingWithIdentityResolution().
                    ToListAsync();

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
    }
}
