using EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete.Validations
{
    public class AnnouncementValidator:AbstractValidator<Announcement>
    {
        public AnnouncementValidator()
        {
            #region Create 
          
            RuleFor(a=>a.Title).NotEmpty().WithMessage("Title cannot be empty.");
            RuleFor(a=>a.Title).MinimumLength(5).WithMessage("Title cannot contain less than 5  charachters.");
            RuleFor(a=>a.Title).MaximumLength(40).WithMessage("Title cannot contain more than 40 charachters.");
            RuleFor(a=>a.Subject).MinimumLength(5).WithMessage("Title cannot contain less than 5  charachters.");
            RuleFor(a => a.Subject).MaximumLength(30).WithMessage("Title cannot contain more than 40 charachters.");
            RuleFor(a => a.Subject).NotEmpty().WithMessage("Subject cannot be empty.");
            RuleFor(a => a.Title).MaximumLength(10).WithMessage("Title cannot contain more than 40 charachters.");
            RuleFor(a => a.Details).MinimumLength(100).WithMessage("Too short.");
            RuleFor(a => a.Details).MaximumLength(1000).WithMessage("Too long.");
            RuleFor(a => a.Details).NotEmpty().WithMessage("Details cannot be empty.");
           
            #endregion 
        }
    }
}
