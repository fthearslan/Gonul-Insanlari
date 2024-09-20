using AutoMapper;
using EntityLayer.Concrete.Entities;
using ViewModelLayer.ViewModels.Article;
using ViewModelLayer.ViewModels.Category;
using ViewModelLayer.ViewModels.Contact;
using ViewModelLayer.ViewModels.Notification;

namespace GonulInsanlari.AutoMapper.Resolvers
{
    public class CategoryArticleConverter : ITypeConverter<Category, CategoryDetailsUIViewModel>
    {
    
        public CategoryDetailsUIViewModel Convert(Category source, CategoryDetailsUIViewModel dest, ResolutionContext context)
        {

            CategoryDetailsUIViewModel destination = new();

            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.Description = source.Description;
            destination.Created = source.Created;
            destination.ImagePath = source.ImagePath;


            destination.Articles = new();

            source.Articles?.ForEach(x =>
            {
                destination.Articles.Add(new ArticleListUIViewModel()
                {
                    Title = x.Title,
                    CategoryName = x.Category.Name,
                    Description = x.Content,
                    Created = x.Created,
                    ImagePath = x.ImagePath,
                    SeenCount = x.SeenCount
                });

            });

            return destination;

        }
    }
}
