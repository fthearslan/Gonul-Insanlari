using EntityLayer.Concrete.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete.Validations.FluentValidation
{
    public class CategoryValidator : AbstractValidator<Category>
    {

        public CategoryValidator()
        {
            #region Name

            RuleFor(c => c.Name).NotEmpty().WithMessage("This field is required.");
            RuleFor(c => c.Name).MinimumLength(2).WithMessage("Too short for name.");
            RuleFor(c => c.Name).MaximumLength(30).WithMessage("Too long for name.");

            #endregion

            #region Description

            RuleFor(c => c.Description).NotEmpty().WithMessage("This field is required.");
            RuleFor(c => c.Description).MinimumLength(20).WithMessage("Too short for description.");
            RuleFor(c => c.Description).MaximumLength(10000).WithMessage("Too long for description.");

            #endregion

            #region Image

            RuleFor(c => c.ImagePath).NotEmpty().WithMessage("This field is required.");

            #endregion

        }
    }
}
