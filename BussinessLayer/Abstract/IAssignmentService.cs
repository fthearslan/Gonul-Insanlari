using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract
{
    public interface IAssignmentService:IGenericService<Assignment>
    {
        List<Assignment> GetAssignmentsWithSender(int id);
        //List<Assignment> GetAssignmentsByReceiverDashboard(int id);

        List<Assignment> GetAssignmentsByReceiver(int id);
    }
}
