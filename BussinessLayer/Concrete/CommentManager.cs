﻿using BussinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete
{
    public class CommentManager : ICommentService
    {
        ICommentDAL _comment;

        public CommentManager(ICommentDAL comment)
        {
            _comment = comment;
        }

        public void Add(Comment entity)
        {
            _comment.Insert(entity);
        }

        public void Delete(Comment entity)
        {
            _comment.Delete(entity);
        }

        public Comment GetById(int id)
        {
            return _comment.Get(x => x.CommentID == id);
        }

        public List<Comment> List()
        {
            return _comment.List();
        }

        public List<Comment> ListFilter()
        {
            return _comment.ListFilter(x => x.Status == true);
        }

        public void Update(Comment entity)
        {
            _comment.Update(entity);
        }
    }
}