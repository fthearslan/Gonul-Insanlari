using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.DTOs.Assignment
{
    public record AssignmentListDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Publisher { get; set; }

        public DateTime Created { get; set; }

        public int SubTasks { get; set; }

        public string Progress { get; set; }

        public int UserCount { get; set; }


    }
}
