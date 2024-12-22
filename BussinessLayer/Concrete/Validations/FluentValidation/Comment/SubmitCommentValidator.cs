using FluentValidation;
using FluentValidation.Validators;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Comment;

namespace BussinessLayer.Concrete.Validations.FluentValidation.Comment
{
    public class SubmitCommentValidator : AbstractValidator<CommentSubmitUIViewModel>
    {
        public SubmitCommentValidator()
        {

            RuleFor(c => c.NameSurname).MinimumLength(2).WithMessage("Ad ve soyad için çok kısa...");
            RuleFor(c => c.NameSurname).MaximumLength(50).WithMessage("Ad ve soyad için çok uzun...");

            RuleFor(c => c.Email).EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("Lütfen geçerli bir mail adresi giriniz...");
            RuleFor(c=>c.Content).MaximumLength(1000).WithMessage("Yorum için çok uzun...");
            RuleFor(c => c.Content).Custom((content,context) =>
            {
             

                List<string> prohibitedWords =new List<string>(){ "FETO", "feto","pic","oc","aptal","salak","serefsiz","hain"};

                prohibitedWords.ForEach(word =>
                {
                    if (content.Contains(word))
                        context.AddFailure("Content", "Yorumunuz uygunsuz sözcükler içeriyor...");

                });

            });

        }
    }
}
