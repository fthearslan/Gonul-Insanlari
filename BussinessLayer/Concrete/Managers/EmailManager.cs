﻿using BussinessLayer.Abstract.Services;
using DataAccessLayer.Abstract.SubRepositories;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ViewModelLayer.Models.Configuration;
using ViewModelLayer.Models.Newsletter;
using System.Data;
using Microsoft.EntityFrameworkCore;
using ViewModelLayer.ViewModels.Newsletter;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Html;
using DataAccessLayer.Concrete.Providers;
using System.Security.Policy;
using Microsoft.Extensions.Hosting;
using ViewModelLayer.ViewModels.Email;
using Microsoft.AspNetCore.Mvc;
using ViewModelLayer.Models.Tools;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;
using MimeKit;

namespace BussinessLayer.Concrete.Managers
{
    public class EmailManager : IEmailService
    {
        private readonly INewsLetterService _newsletterManager;
        private readonly MailServerConfiguration _configuration;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly LinkGenerator _linkGenerator;
        private readonly UserManager<AppUser> _userManager;

        const string quote = "\"";

        public EmailManager(INewsLetterService newsletterManager, MailServerConfiguration configuration, SignInManager<AppUser> signInManager, LinkGenerator linkGenerator)
        {
            _newsletterManager = newsletterManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _linkGenerator = linkGenerator;
            _userManager = signInManager.UserManager;
        }

        #region InnerMethods

        public SmtpClient ConfigureClient()
        {

            return new SmtpClient(_configuration.HostName)
            {
                EnableSsl = _configuration.EnableSsl,
                UseDefaultCredentials = _configuration.UseDefaultCredentials,
                Port = _configuration.Port,
                Credentials = new NetworkCredential(_configuration.Username, _configuration.Password)
            };

        }

        public string GetBody(WeeklyNewsletterModel model)
        {
            BodyBuilder builder = new BodyBuilder();

            using (StreamReader reader = System.IO.File.OpenText(Path.Combine(Directory.GetCurrentDirectory(), "\\wwwroot\\inspinia-gh-pages\\email_templates\\Newsletter.html")))
            {
                builder.HtmlBody = reader.ReadToEnd();

            };


            string withTitle = builder.HtmlBody.Replace("{Title}", model.Title);

            string? withContent = withTitle?.Replace("{Content}", model.Description);

            string? withCategory = withContent?.Replace("{Category}", model.Category);

            string? withUrl = withCategory?.Replace("{Url}", $"https://www.google.com/articles/{model.Id}");


            return withUrl;


        }

        public string GetBody(string content)
        {

            BodyBuilder builder = new BodyBuilder();

            using (StreamReader reader = System.IO.File.OpenText(Path.Combine(Directory.GetCurrentDirectory(), "\\wwwroot\\inspinia-gh-pages\\email_templates\\Reply.html")))
            {
                builder.HtmlBody = reader.ReadToEnd();

            };


            return builder.HtmlBody.Replace("{Content}", content);


        }

        public async Task<AppUser> GetUser(string userName)
        {
            UserManager<AppUser> _userManager = _signInManager.UserManager;

            AppUser user = await _userManager.FindByNameAsync(userName);

            if (user is null)
                return null;

            return user;

        }
        public async Task<string> GetCallBackLink(AppUser user, SendConfirmEmailViewModel model)
        {

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            if (token is null)
                return null;

            return _linkGenerator.GetUriByRouteValues(model.HttpContext, model.RouteName, new
            {
                userId = user.Id,
                token = token
            }, model.HttpContext.Request.Scheme);




        }

        public string GetCallBackLink(SendConfirmEmailViewModel model)
        {

            return _linkGenerator.GetUriByRouteValues(model.HttpContext, model.RouteName, new { email = model.Username }, model.HttpContext.Request.Scheme);



        }


        public async Task<List<NewsletterSubscriberViewModel>> GetSubscribersAsync()
        {


            return await _newsletterManager
                .GetWhere(sub => sub.Status == true && sub.SubscriberStatus == SubscriberStatus.Active && sub.EmailConfirmed == true)
                .Select(sub => new NewsletterSubscriberViewModel()
                {
                    Name = sub.Name,
                    EmailAddress = sub.MailAddress

                })
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();




        }


        #endregion

        public async Task SendEmailAsync(SendMailModel model)
        {

            string? body = GetBody(model.Content);

            if (body is not null)
            {

                SmtpClient client = ConfigureClient();

                foreach (string email in model.To)
                {
                    MailMessage mail = new(_configuration.Username, email)
                    {
                        Body = body,
                        Subject = model.Subject,
                        IsBodyHtml = true,

                    };

                    if (model.ReplyTo is not null)
                        mail.ReplyToList.Add(new(email));

                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

                    if (model.Attachments is not null)
                    {
                        List<string> paths = await model.GetAttachmentPathsAsync();

                        if (paths is not null)
                            foreach (string? attachment in paths)
                                mail.Attachments.Add(new(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/", attachment)));

                    }

                    await client.SendMailAsync(mail);


                }

            }

        }

        public async Task SendNewsletterAsync(WeeklyNewsletterModel model)
        {

            string mailBody = GetBody(model);

            if (mailBody is not null)
            {
                List<NewsletterSubscriberViewModel> subscribers = await GetSubscribersAsync();

                if (subscribers is not null && subscribers.Count > 0)
                {

                    SmtpClient client = ConfigureClient();

                    List<ContactToCollection> TOs = new();

                    foreach (var subscriber in subscribers)
                    {
                        string body = mailBody.Replace("{Name}", subscriber.Name);

                        await client.SendMailAsync(new(_configuration.Username, subscriber.EmailAddress)
                        {

                            Body = body,
                            Subject = model.Subject,
                            IsBodyHtml = true

                        });

                        client.SendCompleted += (sender, e) =>
                        {
                            TOs.Add(new(subscriber.EmailAddress)
                            {
                                Name = subscriber.Name,
                                Surname = "Arslan"

                            });
                        };

                    }


                    using (var c = new Context())
                    {
                        c.Contacts.Add(new(ContactStatus.Newsletter)
                        {

                            Tos = TOs,
                            Content = model.Description,
                            Subject = ContactStatus.Newsletter.ToString(),
                            From = "Administration",
                            IsSeen = true,

                        });

                        c.SaveChanges();
                    }
                }

            }


        }

        public async Task SendResetPasswordLinkAsync(SendMailModel model)
        {

            string content = "<h3> Reset Password </h3>" + Environment.NewLine + "To reset your password, <a href=" + quote + model.Content + quote + "> click this link <a>.";

            string? body = GetBody(content);

            if (body is not null)
            {

                SmtpClient client = ConfigureClient();

                MailMessage mail = new(_configuration.Username, model.To.First())
                {
                    Body = body,
                    Subject = model.Subject,
                    IsBodyHtml = true,

                };

                await client.SendMailAsync(mail);
            }



        }

        public async Task<bool> SendConfirmationLinkAsync(SendConfirmEmailViewModel model)
        {
            AppUser user = await GetUser(model.Username);

            if (user is null)
                return false;

            string link = await GetCallBackLink(user, model);

            if (link is null)
                return false;

            string content = "<h3> Confirm your mail </h3>" + Environment.NewLine + "To confirm your mail, <a href=" + quote + link + quote + "> click this link <a>.";

            string? body = GetBody(content);

            if (body is null)
                return false;


            SmtpClient client = ConfigureClient();

            MailMessage mail = new(_configuration.Username, user.Email)
            {
                Body = body,
                Subject = "Confirm Your Email",
                IsBodyHtml = true,

            };

            await client.SendMailAsync(mail);

            return true;


        }

        public async Task<bool> SendSubscriptionConfirmationAsync(SendConfirmEmailViewModel model)
        {

            string? link = GetCallBackLink(model);

            if (link is null)
                return false;

            string content = "<h3> Confirm your subscription </h3>" + Environment.NewLine + "To confirm your subscription, <a href=" + quote + link + quote + "> click this link <a>.";

            string? body = GetBody(content);

            if (body is null)
                return false;

            SmtpClient client = ConfigureClient();

            MailMessage mail = new(_configuration.Username, model.Username)
            {
                Body = body,
                Subject = model.Subject,
                IsBodyHtml = true,

            };

            await client.SendMailAsync(mail);

            return true;
        }
    }
}
