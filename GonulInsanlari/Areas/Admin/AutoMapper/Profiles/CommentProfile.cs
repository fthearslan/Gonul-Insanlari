using AutoMapper;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Comment;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class CommentProfile:Profile
    {
        public CommentProfile()
        {

            CreateMap<Comment,CommentListViewModel>()
                .ForMember(dest=>dest.Content,opt=>opt.MapFrom(src=>src.Content.Substring(0,10)));



            

        }
    }
}
