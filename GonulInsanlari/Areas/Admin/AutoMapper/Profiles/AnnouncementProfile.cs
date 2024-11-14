using AutoMapper;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Identity;
using ViewModelLayer.ViewModels.Announcement;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class AnnouncementProfile:Profile
    {
        public AnnouncementProfile()
        {

            #region List

            CreateMap<Announcement, AnnouncementListViewModel>()
                .ForMember(dest=>dest.User,opt=>opt.MapFrom(src=>src.User.UserName));

           
            #endregion

            #region Create
            CreateMap<AnnouncementCreateViewModel, Announcement>();
            #endregion

            #region Edit

            CreateMap<Announcement, AnnouncementEditViewModel>();
            CreateMap<AnnouncementEditViewModel, Announcement>()
                .ForMember(an => an.User , opt => opt.Ignore())
            .ForMember(an => an.Created, opt => opt.Ignore());


            #endregion

            #region Details

            CreateMap<Announcement, AnnouncementDetailsViewModel>()
                .ForMember(mod => mod.AppUser, opt => opt.MapFrom(an => an.User.UserName));
          

            #endregion

            #region Dashboard
            CreateMap<Announcement, DashboardAnnouncementViewModel>(); 

            #endregion


        }
    }
}
