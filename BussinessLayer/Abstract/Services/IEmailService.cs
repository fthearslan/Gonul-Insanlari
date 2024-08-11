using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.Models.Configuration;
using ViewModelLayer.Models.Newsletter;
using ViewModelLayer.ViewModels.Email;
using ViewModelLayer.ViewModels.Newsletter;

namespace BussinessLayer.Abstract.Services
{
    public interface IEmailService
    {

        /// <summary>
        /// It configures port number, host name and credentials such as username and password.
        /// </summary>
        /// <returns>SmtpClient</returns>
        protected SmtpClient ConfigureClient();


        /// <summary>
        /// Gets the mailbody in required format with a template.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected string GetBody(WeeklyNewsletterModel model);


        /// <summary>
        /// Gets the mailbody in required format with a template.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        protected string GetBody(string content);
        protected Task<AppUser> GetUser(string userName);
        protected Task<string> GetCallBackLink(AppUser user, ConfirmEmailViewModel model);

        /// <summary>
        /// Gets all the subscriber.
        /// </summary>
        /// <returns></returns>
        protected Task<List<NewsletterSubscriberViewModel>> GetSubscribersAsync();

        /// <summary>   
        /// Send article to the subscribers asynchrnously.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task SendNewsletterAsync(WeeklyNewsletterModel model);


        /// <summary>
        /// Sends email to certain user.
        /// /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task SendEmailAsync(SendMailModel model);


        Task SendResetPasswordLinkAsync(SendMailModel model);

        Task<bool> SendConfirmationLinkAsync(ConfirmEmailViewModel model);


    }
}
