using AutoMapper;
using EntityLayer.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Announcement;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class AnnouncementProfile:Profile
    {
        public AnnouncementProfile()
        {

            #region List

            CreateMap<Announcement, AnnouncementListViewModel>();
           
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

        }
    }
}
