﻿using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Comment;

namespace BussinessLayer.Concrete.Validations.FluentValidation.Comment
{
    public class ReplyCommentValidator:AbstractValidator<CommentReplyViewModel>
    {


        public ReplyCommentValidator()
        {

            RuleFor(c => c.NameSurname).MinimumLength(2).WithMessage("Too short for name and surname");
            RuleFor(c => c.NameSurname).MaximumLength(50).WithMessage("Too long for name and surname");

            RuleFor(c => c.Email).EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("Please, type a valid email address.");
            RuleFor(c => c.Content).MaximumLength(1000).WithMessage("Too long for comment.");
            RuleFor(c => c.Content).Custom((content, context) =>
            {


                List<string> prohibitedWords = new List<string>() { "kufur", "kotusoz" };

                prohibitedWords.ForEach(word =>
                {

                    if ( content is not null && content.Contains(word))
                        context.AddFailure("Content", "Your comment contains prohibited words.");

                });

            });


        }

    }
}
