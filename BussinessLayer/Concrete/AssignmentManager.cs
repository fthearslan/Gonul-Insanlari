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
    public class AssignmentManager : IAssignmentService
    {
        IAssignmentDAL _assignment;

        public AssignmentManager(IAssignmentDAL assignment)
        {
            _assignment = assignment;
        }

        public void Add(Assignment entity)
        {
            _assignment.Insert(entity);
        }

        public void Delete(Assignment entity)
        {
            _assignment.Delete(entity);

        }

        public List<Assignment> GetAssignmentsByReceiver(int id)
        {

            return _assignment.ListFilter(x => x.Receiver.Id == id | x.Receiver==null && x.Status == true).OrderByDescending(x => x.Created).ToList();
        }

        public List<Assignment> GetAssignmentsWithSender(int id)
        {
            return _assignment.GetAssignmentsWithSender(id);
        }


        public Assignment GetById(int id)
        {
            return _assignment.Get(x => x.AssignmentId == id);
        }

        public List<Assignment> List()
        {
            return _assignment.List();
        }

        public List<Assignment> ListFilter()
        {
            return _assignment.ListFilter(x => x.Status == true);
        }

        public void Update(Assignment entity)
        {
            _assignment.Update(entity);
        }
    }
}
