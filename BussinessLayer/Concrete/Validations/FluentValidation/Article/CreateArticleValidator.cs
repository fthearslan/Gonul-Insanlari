using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.ViewModels.Article;

namespace BussinessLayer.Concrete.Validations.FluentValidation.Article
{
    public class CreateArticleValidator : AbstractValidator<ArticleCreateViewModel>
    {
        public CreateArticleValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty.");
            RuleFor(x => x.Title).MinimumLength(3).WithMessage("Too short for title.");
            RuleFor(x => x.Title).Must((title) =>
            {
                using Context context = new Context();

                bool result = context.Articles
                 .Select(x => x.Title).ToString() == title;
                

                return !result;


            }).WithMessage("There already is an article with same title.");

            RuleFor(x => x.Title).MaximumLength(50).WithMessage("Title cannot contain more than 50 charachters.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("This field is required ");
            RuleFor(x => x.Content).MinimumLength(500).WithMessage("Too short for an article");
            RuleFor(x => x.ImagePath).NotEmpty().WithMessage("Please, select an image.");
            RuleFor(x => x.CategoryID).NotEmpty().WithMessage("Please, select a category.");

        }
    }
}
