using AutoMapper;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.Tools;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Contact;
using Microsoft.Build.Framework;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class ContactProfile:Profile
    {


        public ContactProfile()
        {
            CreateMap<Contact, ContactInboxViewModel>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content.Substring(0, 30)));


            CreateMap<Contact, ContactDetailsViewModel>();



        }
    }
}
