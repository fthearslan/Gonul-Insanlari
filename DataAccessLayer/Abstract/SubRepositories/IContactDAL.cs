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

        Task<List<Contact>> GetInboxAsync();

        Task<List<Contact>> GetSentboxAsync(string senderId);
        Task<List<Contact>> SearchByAsync(string search);

        ///<summary>
        /// isTodelete:Status of contact.(True if the status is false).
        /// 
        ///</summary>
        Task<List<Contact>> SearchByAsync(string search, string senderId, bool isDraft, bool isTodelete,bool isSent);


        Task<List<Contact>> GetDraftsAsync(string _senderId);

        Task<List<Contact>> GetTrashAsync();


    }
}
