using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment.CustomAttributes;
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

        public DateTime StartDate { get; set; }
        public IFormFile? Attachment { get; set; }
        [Required]
        public List<int> Users { get; set; } = null!;

 
        List<string> _subTasks;
       
        [Required,ListStringLength(200,ErrorMessage ="Tasks must contain between 10 and 200 charachters.",MinimumLength =10)]
        
        public List<string> SubTasks
        {
            get
            {
                return _subTasks;
            }
            set
            {
                List<string> strings = new List<string>();

                foreach (var item in value)
                {
                    foreach (var obj in item.Split(','))
                        strings.Add(obj);
                };

                _subTasks = strings;
            }

        }

       
    }



}
