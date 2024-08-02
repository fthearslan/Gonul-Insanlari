using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Contact;

namespace BussinessLayer.Abstract.Services
{
    public interface IContactService : IGenericService<Contact>
    {
        Task<List<Contact>> GetContactsAsync(ContactStatus? contactStatus, string? senderId, bool? status);

        Task<List<Contact>> SearchByAsync(string search);

        Task<List<Contact>> SearchByAsync(string search,ContactStatus status,string? senderId);

        Task<Contact> GetWithReply(int id);

    }
}
