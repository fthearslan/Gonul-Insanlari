﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Announcement;

namespace BussinessLayer.Concrete.Validations.FluentValidation.Announcement
{
   public class CreateAnnouncementValidator: AbstractValidator<AnnouncementCreateViewModel>
    {
        public CreateAnnouncementValidator()
        {
            RuleFor(a => a.Title).MinimumLength(5).WithMessage("Title cannot contain less than 5  charachters.");
            RuleFor(a => a.Title).MaximumLength(40).WithMessage("Title cannot contain more than 40 charachters.");
            
            RuleFor(a => a.Subject).MinimumLength(1).WithMessage("To short for subject.");
            RuleFor(a => a.Subject).MaximumLength(30).WithMessage("Subject cannot contain more than 30 chracachters.");
            
            RuleFor(a => a.Details).MinimumLength(100).WithMessage("Too short.");
            RuleFor(a => a.Details).MaximumLength(15000).WithMessage("Too long.");

           


        }

    }
}
