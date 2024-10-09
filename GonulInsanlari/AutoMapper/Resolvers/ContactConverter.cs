using AutoMapper;
using EntityLayer.Concrete.Entities;
using ViewModelLayer.ViewModels.Contact;

namespace GonulInsanlari.AutoMapper.Resolvers
{
    public class ContactConverter : ITypeConverter<SubmitContactViewModel, Contact>
    {


        private readonly IConfiguration _configuration;

        public ContactConverter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Contact Convert(SubmitContactViewModel source, Contact destination, ResolutionContext context)
        {

            string? emailAddress = _configuration.GetValue<string>("AppMail:Address");

            Contact contact = new Contact();

            contact.NameSurname = source.NameSurname;
            contact.From = source.From;
            contact.Subject = source.Subject;
            contact.Content = source.Content;
            contact.ContactStatus = ContactStatus.Received;
            contact.Tos.Add(new(emailAddress));

            return contact;


        }



    }
}
