using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Newsletter;

namespace BussinessLayer.Concrete.Validations.FluentValidation.Newsletter
{
    public class ConfirmSubscriptionValidator:AbstractValidator<NewsletterConfirmSubscriptionViewModel>
    {

        public ConfirmSubscriptionValidator()
        {
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Too short for name...");
            RuleFor(x => x.Name).MaximumLength(10).WithMessage("Too long for name...");


            RuleFor(x => x.Surname).MinimumLength(2).WithMessage("Too short for name...");
            RuleFor(x => x.Surname).MaximumLength(10).WithMessage("Too short for name...");


        }



    }
}
