﻿using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
public interface IAssignmentDAL:IRepository<Assignment>
    {
        List<Assignment> GetAssignmentsWithSender(int id);
    }
}
