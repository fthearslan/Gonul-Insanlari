using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Comment;

namespace BussinessLayer.Abstract.Services
{
    public interface ICommentService : IGenericService<Comment>
    {
        Task<List<Comment>> GetAllAsync(CommentProgress progress, bool status);

        Task<List<Comment>> GetByArticleAsync(int articleId);

        Task<List<Comment>> GetByArticleAsync(string articleTitle);


        Task<List<Comment>> SearchAsync(CommentSearchViewModel model);
    }
}
