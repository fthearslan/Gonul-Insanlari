using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IArticleDAL : IRepository<Article>
    {
        List<Article> ListReleased();
        Article GetDetailsByUser(int id);

        List<Article> GetAllWithoutDrafts();

        List<Article> GetAllIncludeDrafts();

        List<Article> GetDraftsByUser(int userId);

        Article GetByIdInclude(int id);

    }
}
