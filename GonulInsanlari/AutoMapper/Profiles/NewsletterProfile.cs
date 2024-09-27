using AutoMapper;
using EntityLayer.Concrete.Entities;
using ViewModelLayer.ViewModels.Newsletter;

namespace GonulInsanlari.AutoMapper.Profiles
{
    public class NewsletterProfile:Profile
    {

        public NewsletterProfile()
        {
            CreateMap<NewsletterSubscribeUIViewModel, NewsLetter>();
        }
    }
}
