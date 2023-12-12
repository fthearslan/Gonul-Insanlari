using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
public interface IArticleDAL:IRepository<Article>
    {
        List<Article> ListWithCategory();
        Article GetWithVideos(int id);
    }
}
