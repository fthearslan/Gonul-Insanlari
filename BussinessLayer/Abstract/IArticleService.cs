using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract
{
    public interface IArticleService:IGenericService<Article>
    {
        List<Article> ListWithCategory();
        Article GetWithVideos(int id);
        List<Article> GetDrafts();


    }
}
