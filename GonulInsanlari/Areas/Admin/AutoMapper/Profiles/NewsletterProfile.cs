using AutoMapper;
using EntityLayer.Concrete.Entities;
using ViewModelLayer.ViewModels.Newsletter;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class NewsletterProfile:Profile
    {

        public NewsletterProfile()
        {

            CreateMap<NewsLetter, NewsletterListSubscribersViewModel>()
                .ForMember(dest => dest.NameSurname, opt => opt.MapFrom(src => string.Concat(src.Name," ",src.Surname)));


            CreateMap<NewsletterSubscriberCreateViewModel, NewsLetter>();
                

        }
    }
}
