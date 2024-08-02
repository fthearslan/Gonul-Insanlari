using AutoMapper;
using EntityLayer.Concrete.Entities;
using ViewModelLayer.ViewModels.Article;
using ViewModelLayer.ViewModels.Contact;

namespace GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers
{
    public class ContactAttachmentPathResolver : IValueResolver<Contact, ContactDetailsViewModel, List<string>>
    {
        public List<string> Resolve(Contact source, ContactDetailsViewModel destination, List<string> destMember, ResolutionContext context)
        {


            source?.Attachments?.ForEach(x =>
            {
                destination?.Attachments?.Add(x.Path);

            });

            return destination.Attachments;


        }
    }
}
