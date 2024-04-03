using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment.CustomAttributes;
using GonulInsanlari.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using X.PagedList;

namespace GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment
{
    public record AssignmentCreateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        public DateTime Created => DateTime.Now;
        [Required]
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
                _startDate= hours.AddMinutes(DateTime.Now.Minute);
            }
        }
        public IFormFileCollection? Files { get; set; }

        [Required]
        public List<int> Users { get; set; } = null!;


    }



}
