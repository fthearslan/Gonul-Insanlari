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


            RuleFor(x => x.NameSurname).MinimumLength(3).WithMessage("Ad ve soyad için çok kısa...");

            RuleFor(x => x.NameSurname).MaximumLength(35).WithMessage("Ad ve soyad  için çok uzun...");


            RuleFor(x => x.Subject).MinimumLength(2).WithMessage("Konu için çok kısa...");
            RuleFor(x => x.Subject).MaximumLength(30).WithMessage("Konu için çok uzun...");


            RuleFor(x => x.Content).MinimumLength(5).WithMessage("Mesaj için çok kısa...");

            RuleFor(x => x.Content).MaximumLength(2000).WithMessage("Mesaj için çok uzun...");



            RuleFor(x=>x.From).EmailAddress();




        }

    }
}
