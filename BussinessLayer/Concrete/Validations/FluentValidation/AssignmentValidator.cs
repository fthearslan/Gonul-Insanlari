using EntityLayer.Concrete.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete.Validations.FluentValidation
{
    public class AssignmentValidator : AbstractValidator<Assignment>
    {
        public AssignmentValidator()
        {
            RuleFor(a => a.Title).NotEmpty().MaximumLength(50).MinimumLength(5).WithMessage("Title must contain between 5 and 50 charachters.");
            RuleFor(a => a.Content).MaximumLength(15000).WithMessage("Too long for task.");
            RuleFor(a => a.Content).NotEmpty().MinimumLength(100).WithMessage("Too short for task.");
            RuleFor(a => a.Due).NotEmpty().WithMessage("Please, select a due date.");
            RuleFor(a => a.StartDate).NotNull().WithMessage("Please, select a start date.");
            RuleFor(a => a.StartDate).GreaterThan(a => a.Created).WithMessage("Please, select a valid start date.");
            RuleFor(a => a.StartDate).LessThan(a => a.Due).WithMessage("Start date cannot be greater than the due date.");
            RuleFor(a => a.UserAssignments).NotEmpty().WithMessage("Please select users for this task.");

            RuleSet("SubTask", () =>
            {
                RuleFor(s => s.Title).NotEmpty();
            });


        }

    }
}
