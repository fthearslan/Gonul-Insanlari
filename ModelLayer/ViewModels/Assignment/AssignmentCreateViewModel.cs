using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ViewModelLayer.ViewModels.Assignment
{
    public record AssignmentCreateViewModel
    {
        public int Id { get; set; }

      
        public string Title { get; set; } = null!;

     
        public string Content { get; set; } = null!;

        public DateTime Created => DateTime.Now;
 
        public DateTime Due { get; set; }

        DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                var hours = value.Date.AddHours(DateTime.Now.Hour);
                _startDate = hours.AddMinutes(DateTime.Now.Minute);
            }
        }
        public IFormFileCollection? Files { get; set; }

        public List<int> Users { get; set; } = null!;


    }



}
