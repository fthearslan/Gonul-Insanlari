using AutoMapper;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers;
using GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers.ContactResolvers;
using ViewModelLayer.ViewModels.Contact;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class ContactProfile : Profile
    {


        public ContactProfile()
        {
            CreateMap<Contact, ContactListViewModel>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content.Substring(0, 30)))
                .ForMember(dest => dest.To, opt => opt.MapFrom<ContactToResolver>());



            CreateMap<Contact, ContactDetailsViewModel>()
                .ForMember(dest => dest.Attachments, opt => opt.MapFrom<ContactAttachmentPathResolver>())
                .ForMember(dest => dest.Tos, opt => opt.MapFrom(src => src.Tos.Select(x => x.EmailAddress)));

          




        }
    }
}
