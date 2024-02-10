using BussinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Migrations;
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

        public async Task AddAsync(Assignment entity)
        {
           await _assignment.InsertAsync(entity);
        }

        public void Delete(Assignment entity)
        {
            _assignment.Delete(entity);

        }

        //public List<Assignment> GetAssignmentsByReceiverDashboard(int id)
        //{
        //    return _assignment.ListFilter(x => x.Receiver.Id == id | x.Receiver == null && x.Status == true).OrderByDescending(x => x.Created).ToList();
        //}

        public List<Assignment> GetAssignmentsByReceiver(int id)
        {
            return  _assignment.GetAssignmentsWithReceiver(id);
        }

        public List<Assignment> GetAssignmentsWithSender(int id)
        {
            return _assignment.GetAssignmentsWithSender(id);
        }


        public Assignment GetById(int id)
        {
            return _assignment.Get(x => x.AssignmentId == id);
        }

        public void InsertWithRelated(Assignment entity)
        {
            _assignment.InsertWithRelated(entity);
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
