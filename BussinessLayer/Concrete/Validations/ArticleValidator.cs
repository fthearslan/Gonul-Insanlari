using EntityLayer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete.Validations
{
    public class ArticleValidator : AbstractValidator<Article>
    {
        public ArticleValidator()
        {
           
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty.");
            RuleFor(x => x.Title).MinimumLength(10).WithMessage("Title cannot contain less than 10 charachters.");
            RuleFor(x => x.Title).MaximumLength(50).WithMessage("Title cannot contain more than 50 charachters.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("This field is required.");
            RuleFor(x => x.Content).MinimumLength(500).WithMessage("Too short for an article.");
            RuleFor(x => x.ImagePath).NotEmpty().WithMessage("Please, select an image.");
            RuleFor(x => x.CategoryID).NotEmpty().WithMessage("Please, select a category.");
           
            RuleSet("Draft", () =>
            {
                RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty.");
                RuleFor(x => x.Content).NotEmpty().WithMessage("Content cannot be empty.");
                RuleFor(x => x.Content).MinimumLength(100).WithMessage("Too short for draft.");
                RuleFor(x => x.Title).MinimumLength(10).WithMessage("Title cannot contain less than 10 charachters.");
                RuleFor(x => x.ImagePath).NotEmpty().WithMessage("Please, select an image.");
                RuleFor(x => x.CategoryID).NotEmpty().WithMessage("Please, choose a category.");
            });
        
        }
    }
}
