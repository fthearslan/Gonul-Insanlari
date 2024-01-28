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

            CreateMap<Announcement, AnnouncementListViewModel>()
               /* .ForMember(mod => mod.CreatedBy,opt=>opt.MapFrom(a=>a.CreatedBy.UserName))*/;



            #endregion

            #region Create
            CreateMap<AnnouncementCreateViewModel, Announcement>();
            #endregion

        }
    }
}
