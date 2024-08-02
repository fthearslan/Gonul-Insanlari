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
using DataAccessLayer.Concrete.Providers;

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

        public string GetBody(SendMailModel model)
        {

            BodyBuilder builder = new BodyBuilder();

            using (StreamReader reader = System.IO.File.OpenText(SendMailModel.Path))
            {
                builder.HtmlBody = reader.ReadToEnd();

            };


            return builder.HtmlBody.Replace("{Content}", model.Content);


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

        public async Task SendEmailAsync(SendMailModel model)
        {

            string? body = GetBody(model);

            if (body is null)
                throw new MailBodyIsNullException();

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
