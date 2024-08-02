using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.Models.Newsletter;
using ViewModelLayer.Models.Tools;

namespace BussinessLayer.Concrete.Validations.FluentValidation.Contact
{
    public class SendMailValidator : AbstractValidator<SendMailModel>
    {
        public SendMailValidator()
        {

            RuleFor(x => x.To).NotEmpty();
            RuleForEach(x => x.To).EmailAddress();
            RuleFor(x => x.Subject).NotEmpty();
            RuleFor(x => x.Content).NotEmpty();
            RuleFor(x => x.Attachments).Must((files) =>
            {
                if (files is null)
                    return true;

                return ImageUpload.CheckFileSizeAsync(files);
            
            });

        }
    }
}
