using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract.Services
{
    public interface IContactService : IGenericService<Contact>
    {

        Task<List<Contact>> GetInboxAsync();
        Task<List<Contact>> GetSentboxAsync(string senderId);

        Task<List<Contact>> SearchByAsync(string search);

        /// <summary>
        /// isTodelete: Status of contact.(True if the status is false).
        /// </summary>
        /// <param name="search"></param>
        /// <param name="senderId"></param>
        /// <param name="isdraft"></param>
        /// <param name="isTodelete"></param>
        /// <returns></returns>
        Task<List<Contact>> SearchByAsync(string search, string senderId,bool isdraft,bool isTodelete,bool isSent);

        Task<List<Contact>> GetDraftsAsync(string _senderId);

        Task<List<Contact>> GetTrashAsync();


    }
}
