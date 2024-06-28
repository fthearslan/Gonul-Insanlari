using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Admin;

namespace BussinessLayer.Concrete.Validations.FluentValidation.Admin
{
   public class CreateAdminValidator:AbstractValidator<AdminRegisterViewModel>
    {

        public CreateAdminValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name field is required.");

            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Name is too short.");
            RuleFor(x => x.Name).MaximumLength(20).WithMessage("Name is too long.");

            RuleFor(x => x.Surname).MinimumLength(2).WithMessage("Surname is too short.");
            RuleFor(x => x.Surname).MaximumLength(15).WithMessage("Surname is too long.");

            RuleFor(x => x.UserName).MinimumLength(6).WithMessage("Username is too short.");
            RuleFor(x => x.UserName).MaximumLength(18).WithMessage("Username is too long.");

            RuleFor(x => x.Email).MinimumLength(12).WithMessage("Please tpye valid email adress.");
            RuleFor(x => x.Email).MaximumLength(30).WithMessage("Please tpye valid email adress.");
          
     
        }
    }
}
