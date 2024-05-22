﻿using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract.Services
{
    public interface ICommentService : IGenericService<Comment>
    {
        Task<List<Comment>> GetAllAsync(CommentProgress progress, bool status);

        Task<List<Comment>> GetByArticleAsync(int articleId);

        Task<List<Comment>> SearchAsync(string search, CommentProgress progress, bool status);
    }
}
