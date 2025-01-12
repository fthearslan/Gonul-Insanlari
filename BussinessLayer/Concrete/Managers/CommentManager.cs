﻿using BussinessLayer.Abstract.Services;
using DataAccessLayer.Abstract.SubRepositories;
using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Comment;

namespace BussinessLayer.Concrete.Managers
{
    public class CommentManager : ICommentService
    {
        ICommentDAL _comment;

        public CommentManager(ICommentDAL comment)
        {
            _comment = comment;
        }

        public async Task AddAsync(Comment entity)
        {
            await _comment.InsertAsync(entity);
        }

        public void Delete(Comment entity)
        {
            _comment.Delete(entity);
        }

        public async Task<List<Comment>> GetAllAsync(CommentProgress progress, bool status)
        {
            return await _comment.GetAllAsync(progress, status);
        }

        public List<Comment> GetByArticle(int id)
        {
            return _comment.ListFilter(x => x.ArticleID == id);
        }

        public async Task<List<Comment>> GetByArticleAsync(int articleId)
        {
            return await _comment.GetByArticleAsync(articleId);

        }

        public async Task<List<Comment>> GetByArticleAsync(string articleTitle)
        {
            return await _comment.GetByArticleAsync(articleTitle);

        }


        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _comment.GetByIdAsync(id);
        }

        public IQueryable<Comment> GetWhere(Expression<Func<Comment, bool>> filter)
        {
            return _comment.GetWhere(filter);

        }

        public void InsertWithRelated(Comment entity)
        {
            _comment.InsertWithRelated(entity);
        }

        public List<Comment> List()
        {
            return _comment.List();
        }

        public List<Comment> ListFilter()
        {
            return _comment.ListFilter(x => x.Status == true).OrderByDescending(x => x.Created).ToList();
        }

        public async Task<List<Comment>> SearchAsync(CommentSearchViewModel model)
        {

            if (model.Progress is not null)
                return await _comment.SearchByAsync(model.Search, model.Progress);

            if (model.ArticleTitle is not null)
                return await _comment.SearchByAsync(model.Search, model.ArticleTitle);

            return await _comment.SearchByAsync(model.Search);


        }





        public void Update(Comment entity)
        {
            _comment.Update(entity);
        }
    }
}
