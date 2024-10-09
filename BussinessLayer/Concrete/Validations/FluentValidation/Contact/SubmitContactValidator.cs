using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Contact;

namespace BussinessLayer.Concrete.Validations.FluentValidation.Contact
{
  public class SubmitContactValidator:AbstractValidator<SubmitContactViewModel>
    {
        public SubmitContactValidator()
        {


            RuleFor(x => x.NameSurname).MinimumLength(3).WithMessage("Too short for a name.");

            RuleFor(x => x.NameSurname).MaximumLength(35).WithMessage("Too long for a name.");


            RuleFor(x => x.Subject).MinimumLength(2).WithMessage("Too short for a subject.");
            RuleFor(x => x.Subject).MaximumLength(30).WithMessage("Too long for a subject.");


            RuleFor(x => x.Content).MinimumLength(5).WithMessage("Too short for a message");

            RuleFor(x => x.Content).MaximumLength(2000).WithMessage("Too short for a message.");



            RuleFor(x=>x.From).EmailAddress();




        }

    }
}
