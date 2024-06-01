using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Category;

namespace BussinessLayer.Concrete.Validations.FluentValidation.Category
{
  public class CreateCategoryValidator : AbstractValidator<CategoryCreateViewModel>
    {

        public CreateCategoryValidator()
        {

            RuleFor(c => c.Name).NotEmpty().WithMessage("This field is required.");
            RuleFor(c => c.Name).MinimumLength(2).WithMessage("Too short for name.");
            RuleFor(c => c.Name).MaximumLength(30).WithMessage("Too long for name.");


            RuleFor(c => c.Description).NotEmpty().WithMessage("This field is required.");
            RuleFor(c => c.Description).MinimumLength(100).WithMessage("Too short for description.");
            RuleFor(c => c.Description).MaximumLength(10000).WithMessage("Too long for description.");


            RuleFor(c => c.ImagePath).NotEmpty().WithMessage("This field is required.");
        }


    }
}
