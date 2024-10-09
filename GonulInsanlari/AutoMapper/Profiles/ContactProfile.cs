using AutoMapper;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.AutoMapper.Resolvers;
using ViewModelLayer.ViewModels.Contact;

namespace GonulInsanlari.AutoMapper.Profiles
{
    public class ContactProfile:Profile
    {
        public ContactProfile()
        {
            CreateMap<SubmitContactViewModel,Contact>()
                .ConvertUsing<ContactConverter>();
        }

    }
}
