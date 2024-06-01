using AutoMapper;
using EntityLayer.Concrete.Entities;
using Microsoft.Build.Framework;
using ViewModelLayer.ViewModels.Contact;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class ContactProfile:Profile
    {


        public ContactProfile()
        {
            CreateMap<Contact, ContactListViewModel>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content.Substring(0, 30)));


            CreateMap<Contact, ContactDetailsViewModel>();



        }
    }
}
