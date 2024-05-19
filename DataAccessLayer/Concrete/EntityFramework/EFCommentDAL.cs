using DataAccessLayer.Abstract.SubRepositories;
using DataAccessLayer.Concrete.Providers;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EFCommentDAL : GenericRepository<Comment>, ICommentDAL
    {
        public async Task<List<Comment>> GetAllAsync()
        {

            using (var c = new Context())
            {
                return await c.Comments
                    .OrderByDescending(x => x.Created)
                    .OrderByDescending(x => x.IsApproved)
                    .OrderByDescending(x => x.Status)
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();

            };




        }

        public async Task<List<Comment>> GetByArticleAsync(int _articleId)
        {
            using (var c = new Context())
            {
                return await c.Comments
                   .Where(x => x.ArticleID == _articleId)
                    .AsNoTrackingWithIdentityResolution()
                    .ToListAsync();

            };


        }

        public async Task<Comment> GetByIdAsync(int _commentId)
        {
            using (var c = new Context())
            {
                return await c.Comments
                    .AsNoTrackingWithIdentityResolution()
                    .SingleOrDefaultAsync(x => x.Id == _commentId);


            };

        }
    }
}
