using DataAccessLayer.Concrete.Providers;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Newsletter;

namespace BussinessLayer.Concrete.Validations.FluentValidation.Newsletter
{
    public class SubscribeNewsletterValidator:AbstractValidator<NewsletterSubscribeUIViewModel>
    {

        public SubscribeNewsletterValidator()
        {

            RuleFor(x => x.MailAddress).EmailAddress(EmailValidationMode.AspNetCoreCompatible);

            RuleFor(x => x.MailAddress).MinimumLength(11).WithMessage("Please, type valid email.");

            RuleFor(x => x.MailAddress).Must((email) =>
            {
                using Context c = new Context();

                if (c.NewsLetters.Any(x => x.MailAddress == email))
                    return false;

                return true;

            }).WithMessage("There already is a subscriber with same email adress.");


        }
    }
}
