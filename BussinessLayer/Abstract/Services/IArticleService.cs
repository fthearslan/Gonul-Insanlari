using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract.Services
{
    public interface IArticleService : IGenericService<Article>
    {
        List<Article> ListReleased();
        Article GetDetailsByUser(int id);
        List<Article> GetDraftsByUser(int userId);

        List<Article> GetAllIncludeDrafts();

        Article GetByIdInclude(int id);
        List<Article> GetAllWithoutDrafts();

        List<Article> ListByCategory(int id);


    }
}
