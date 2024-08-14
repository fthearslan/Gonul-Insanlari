using DataAccessLayer.Concrete.Providers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Newsletter;

namespace BussinessLayer.Concrete.Validations.FluentValidation.Newsletter
{
    public class CreateNewsletterSubscriberValidator : AbstractValidator<NewsletterSubscriberCreateViewModel>
    {


        public CreateNewsletterSubscriberValidator()
        {

            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Too short for name.");
            RuleFor(x => x.Name).MaximumLength(15).WithMessage("Too long for name.");
            RuleFor(x => x.Surname).MinimumLength(2).WithMessage("Too short for name.");
            RuleFor(x => x.Name).MaximumLength(15).WithMessage("Too long for name.");

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
