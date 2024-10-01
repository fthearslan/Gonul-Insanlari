using AutoMapper;
using EntityLayer.Concrete.Entities;
using ViewModelLayer.ViewModels.Comment;

namespace GonulInsanlari.AutoMapper.Profiles
{
    public class CommentProfile:Profile
    {
        public CommentProfile()
        {

            CreateMap<Comment, CommentByArticleUIViewModel>();
            CreateMap<CommentSubmitUIViewModel,Comment>();

                
        }
    }
}
