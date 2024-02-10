using BussinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete
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

        public Note GetById(int id)
        {
            return _note.Get(x => x.Id == id);
        }

        public List<Note> GetListByUser(int id)
        {
            return _note.ListFilter(x => x.CreatedBy.Id == id && x.Status == true).OrderByDescending(x => x.Created).ToList();
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
