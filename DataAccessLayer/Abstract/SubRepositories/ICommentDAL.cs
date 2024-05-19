using AutoMapper.Configuration.Conventions;
using DataAccessLayer.Abstract.Repositories;
using EntityLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract.SubRepositories
{
    public interface ICommentDAL : IRepository<Comment>
    {

        Task<List<Comment>> GetAllAsync();

        Task<Comment> GetByIdAsync(int _commentId);
        Task<List<Comment>> GetByArticleAsync(int _articleId);
    }
}
