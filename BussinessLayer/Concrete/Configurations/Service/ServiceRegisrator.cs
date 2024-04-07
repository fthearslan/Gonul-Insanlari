using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete.Managers;
using BussinessLayer.Concrete.Validations.FluentValidation;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete.Configurations.Service
{
    public static class ServiceRegisrations
    {

        public static IServiceCollection AddBussinessServices(this IServiceCollection Services)
        {
            Services.AddScoped<ICategoryService>(x => new CategoryManager(new EFCategoryDAL()));
            Services.AddScoped<IArticleService>(x => new ArticleManager(new EFArticleDAL()));
            Services.AddScoped<IAboutService>(x => new AboutManager(new EFAboutDAL()));
            Services.AddScoped<IAnnouncementService>(x => new AnnouncementManager(new EFAnnouncementDAL()));
            Services.AddScoped<IAssignmentService>(x => new AssignmentManager(new EFAssignmentDAL()));
            Services.AddScoped<ICommentService>(x => new CommentManager(new EFCommentDAL()));
            Services.AddScoped<IContactService>(x => new ContactManager(new EFContactDAL()));
            Services.AddScoped<INoteService>(x => new NoteManager(new EFNoteDAL()));
            Services.AddScoped<INotificationService>(x => new NotificationManager(new EFNotificationDAL()));
            Services.AddScoped<IMessageService>(x => new MessageManager(new EFMessageDAL()));
            Services.AddScoped<INewsLetterService>(x => new NewsLetterManager(new EFNewsLetterDAL()));
       
            return Services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection Services)
        {
            Services.AddScoped<AbstractValidator<Article>, ArticleValidator>();
            Services.AddScoped<AbstractValidator<Category>, CategoryValidator>();
            Services.AddScoped<AbstractValidator<Announcement>, AnnouncementValidator>();
            Services.AddScoped<AbstractValidator<Assignment>, AssignmentValidator>();

            return Services;

        }

    }
}
