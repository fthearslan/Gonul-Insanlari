using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Admin;

namespace BussinessLayer.Concrete.Validations.FluentValidation.Admin
{
    public class EditAdminValidator : AbstractValidator<AdminEditViewModel>
    {
        public EditAdminValidator()
        {

            //RuleFor(x => x.Name).NotEmpty().WithMessage("Name field is required.");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Name is too short.");
            RuleFor(x => x.Name).MaximumLength(20).WithMessage("Name is too long.");

            //RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname field is required.");
            RuleFor(x => x.Surname).MinimumLength(2).WithMessage("Surname is too short.");
            RuleFor(x => x.Surname).MaximumLength(15).WithMessage("Surname is too long.");

            //RuleFor(x => x.UserName).NotEmpty().WithMessage("Username field is required.");
            RuleFor(x => x.UserName).MinimumLength(6).WithMessage("Username is too short.");
            RuleFor(x => x.UserName).MaximumLength(18).WithMessage("Username is too long.");
        
            //RuleFor(x => x.Email).NotEmpty().WithMessage("Email field is required.");
            RuleFor(x => x.Email).MinimumLength(12).WithMessage("Please tpye valid email adress.");
            RuleFor(x => x.Email).MaximumLength(30).WithMessage("Please tpye valid email adress.");

            //RuleFor(x => x.AboutMe).NotEmpty().WithMessage("About field is required.");
            RuleFor(x => x.AboutMe).MinimumLength(10).WithMessage("About is too short");

            RuleFor(x => x.AboutMe).MaximumLength(500).WithMessage("About is too long");

            //RuleFor(x => x.Age).NotEmpty().WithMessage("Age field is required.");

            RuleFor(x => x.Age).GreaterThanOrEqualTo(18).WithMessage("You must at least be 18.");


        }



    }
}
