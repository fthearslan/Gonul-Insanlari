using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.About;

namespace BussinessLayer.Concrete.Validations.FluentValidation.About
{
   public class EditAboutValidator:AbstractValidator<EdtiAboutViewModel>
    {


        public EditAboutValidator()
        {


            RuleFor(x => x.Title).MinimumLength(5).WithMessage("Too short for title...");
            RuleFor(x => x.Title).MaximumLength(30).WithMessage("Too long for title...");

            RuleFor(x => x.Details).MinimumLength(100).WithMessage("Too short for details...");


        }






    }
}
