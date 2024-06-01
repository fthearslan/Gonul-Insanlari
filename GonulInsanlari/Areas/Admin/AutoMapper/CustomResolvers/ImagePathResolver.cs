using AutoMapper;
using EntityLayer.Concrete.Entities;
using ViewModelLayer.Models.Tools;
using ViewModelLayer.ViewModels.Article;

namespace GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers
{
    public class ImagePathResolver: IValueResolver<ArticleCreateViewModel, Article, string>
    {
        public string Resolve(ArticleCreateViewModel source, Article destination, string? destMember, ResolutionContext context)
        {
            return ImageUpload.UploadAsync(source.ImagePath).Result;


        }
    }
}
