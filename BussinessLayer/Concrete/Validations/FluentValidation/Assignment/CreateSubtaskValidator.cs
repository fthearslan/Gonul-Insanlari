using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Assignment;

namespace BussinessLayer.Concrete.Validations.FluentValidation.Assignment
{
    public class CreateSubtaskValidator : AbstractValidator<SubTaskCreateViewModel>
    {

        public CreateSubtaskValidator()
        {

            RuleFor(x => x.SubTaskDescription)
                .NotEmpty()
                .WithMessage("This field is required.");


        }






    }
}
