using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Category;
using ViewModelLayer.ViewModels.Role;

namespace BussinessLayer.Concrete.Validations.FluentValidation.Role
{
    public class CreateRoleValidator: AbstractValidator<CreateRoleViewModel>
    {

        public CreateRoleValidator()
        {
            RuleFor(x => x.Name).MaximumLength(15).WithMessage("Too long for role name.");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Too short for role name.");

            RuleFor(x => x.RoleDescription).MaximumLength(350).WithMessage("Too long for role description.");
            RuleFor(x => x.RoleDescription).MinimumLength(75).WithMessage("Too short for role description.");

        }
    }
}
