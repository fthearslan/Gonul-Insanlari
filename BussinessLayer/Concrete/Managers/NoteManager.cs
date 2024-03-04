using BussinessLayer.Abstract.Services;
using DataAccessLayer.Abstract.SubRepositories;
using EntityLayer.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete.Managers
{
    public class NoteManager : INoteService
    {
        INoteDAL _note;

        public NoteManager(INoteDAL note)
        {
            _note = note;
        }
        public async Task AddAsync(Note entity)
        {
            await _note.InsertAsync(entity);
        }
        public void Delete(Note entity)
        {
            _note.Delete(entity);
        }

        public async Task<Note> GetByIdAsync(int id)
        {
            return await _note.GetAsync(x => x.Id == id);
        }

        public List<Note> GetListByUser(int id)
        {
            return _note.ListFilter(x => x.CreatedBy.Id == id && x.Status == true).OrderByDescending(x => x.Created).ToList();
        }

        public IQueryable<Note> GetWhere(Expression<Func<Note, bool>> filter)
        {
            return _note.GetWhere(filter);

        }

        public void InsertWithRelated(Note entity)
        {
            _note.InsertWithRelated(entity);
        }

        public List<Note> List()
        {
            return _note.List();
        }

        public List<Note> ListFilter()
        {
            return _note.ListFilter(x => x.Status == true);
        }

        public void Update(Note entity)
        {
            _note.Update(entity);
        }
    }
}
