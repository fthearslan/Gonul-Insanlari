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


            RuleSet("Edit", () =>
            {



            });








        }



    }
}
