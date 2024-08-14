using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete.Managers;
using BussinessLayer.Concrete.Validations.FluentValidation;
using BussinessLayer.Concrete.Validations.FluentValidation.Admin;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.Models.Configuration;

namespace GonulInsanlari.Configurations.Services
{
    public static class ServiceRegistrator
    {

        public static void AddBussinessServices(this IServiceCollection Services)
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
            Services.AddHttpContextAccessor();

            ServiceProvider serviceProvider = Services.BuildServiceProvider();

            MailServerConfiguration mailServerConfiguration = serviceProvider
                  .GetRequiredService<IOptions<MailServerConfiguration>>()
                 .Value;

            SignInManager<AppUser> signInManager = serviceProvider.GetRequiredService<SignInManager<AppUser>>();

            var linkGenerator = serviceProvider.GetRequiredService<LinkGenerator>();

            var httpcontext
               = serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;

            Services.AddScoped<IEmailService>(x => new EmailManager(new NewsLetterManager(new EFNewsLetterDAL()), mailServerConfiguration, signInManager, linkGenerator));

        }

        public static void AddValidators(this IServiceCollection Services)
        {
            Services.AddFluentValidationAutoValidation();
            Services.AddValidatorsFromAssemblyContaining<EditAdminValidator>();


        }




    }
}
