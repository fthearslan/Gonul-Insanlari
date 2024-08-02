using AutoMapper;
using EntityLayer.Concrete.Entities;
using ViewModelLayer.ViewModels.Contact;

namespace GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers.ContactResolvers
{
    public class ContactToResolver : IValueResolver<Contact, ContactListViewModel, string>
    {


        string IValueResolver<Contact, ContactListViewModel, string>.Resolve(Contact source, ContactListViewModel destination, string destMember, ResolutionContext context)
        {

            if (source.Tos.Count >= 1)
                return source.Tos.First().EmailAddress;
            

            return null;

        }
    }
}
