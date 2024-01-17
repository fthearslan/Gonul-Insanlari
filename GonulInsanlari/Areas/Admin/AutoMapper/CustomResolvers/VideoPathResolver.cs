using AutoMapper;
using EntityLayer;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Article;
using GonulInsanlari.Models;

namespace GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers
{
    public class VideoPathResolver : IValueResolver<ArticleCreateViewModel, Article, string?>
    {
        public string? Resolve(ArticleCreateViewModel source, Article destination, string? destMember, ResolutionContext context)
        {
            
            return   ImageUpload.UploadAsync(source.VideoPath).Result;
           

        }

       


    }
}
