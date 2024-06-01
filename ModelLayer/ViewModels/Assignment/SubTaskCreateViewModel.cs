using System.ComponentModel.DataAnnotations;

namespace ViewModelLayer.ViewModels.Assignment
{
    public record SubTaskCreateViewModel
    {
        public int TaskId { get; set; }

        [Required, StringLength(150, ErrorMessage = "Subtask must contain between 5 and 150 chrachters.", MinimumLength = 15)]
        public string SubTaskDescription { get; set; }

    }
}
