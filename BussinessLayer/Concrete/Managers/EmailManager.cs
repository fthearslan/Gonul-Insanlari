using BussinessLayer.Abstract.Services;
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
using MimeKit;
using System.Data;
using GonulInsanlari.Exceptions;
using Microsoft.EntityFrameworkCore;
using ViewModelLayer.ViewModels.Newsletter;
using GonulInsanlari.Exceptions.Newsletter;
using BussinessLayer.Concrete.Exceptions.Newsletter;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Html;

namespace BussinessLayer.Concrete.Managers
{
    public class EmailManager : IEmailService
    {
        private readonly INewsLetterService _newsletterManager;
        private readonly MailServerConfiguration _configuration;

        public EmailManager(INewsLetterService newsletterManager, MailServerConfiguration configuration)
        {
            _newsletterManager = newsletterManager;
            _configuration = configuration;
        }

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

            using (StreamReader reader = System.IO.File.OpenText(WeeklyNewsletterModel.Path))
            {
                builder.HtmlBody = reader.ReadToEnd();

            };


            string withTitle = builder.HtmlBody.Replace("{Title}", model.Title);

            string? withContent = withTitle?.Replace("{Content}", model.Description);

            string? withCategory = withContent?.Replace("{Category}", model.Category);

            string? withUrl = withCategory?.Replace("{Url}", $"https://www.google.com/articles/{model.Id}");


            return withUrl;


        }

        public string GetBody(MailReplyModel model)
        {

            BodyBuilder builder = new BodyBuilder();

            using (StreamReader reader = System.IO.File.OpenText(MailReplyModel.Path))
            {
                builder.HtmlBody = reader.ReadToEnd();

            };

            string? withName = builder.HtmlBody.Replace("{Name}", model.Name);

            string? withContent = withName.Replace("{Content}", model.Content);

            return withContent;

        }

        public async Task<List<NewsletterSubscriberViewModel>> GetSubscribersAsync()
        {


            return await _newsletterManager
                .GetWhere(sub => sub.Status == true)
                .Select(sub => new NewsletterSubscriberViewModel()
                {
                    Name = sub.Name,
                    EmailAddress = sub.MailAddress

                })
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();




        }

        public async Task SendEmailAsync(MailReplyModel model)
        {
            string? body = GetBody(model);

            if (body is null)
                throw new MailBodyIsNullException();

            SmtpClient client = ConfigureClient();

            await client.SendMailAsync(new(_configuration.Username, model.Email)
            {

                Body = body,
                Subject = model.Subject,
                IsBodyHtml = true

            });

        }


        public async Task SendNewsletterAsync(WeeklyNewsletterModel model)
        {

            string mailBody = GetBody(model);

            if (mailBody is null)
                throw new MailBodyIsNullException();

            List<NewsletterSubscriberViewModel> subscribers = await GetSubscribersAsync();

            if (subscribers is null)
                throw new SubscribersAreNullException();
            if (subscribers.Count == 0)
                throw new SubscribersAreNullException();



            SmtpClient client = ConfigureClient();

            foreach (var subscriber in subscribers)
            {

                await client.SendMailAsync(new(_configuration.Username, subscriber.EmailAddress)
                {

                    Body = mailBody.Replace("{Name}", subscriber.Name),
                    Subject = model.Subject,
                    IsBodyHtml = true

                });


            }



        }
    }
}
