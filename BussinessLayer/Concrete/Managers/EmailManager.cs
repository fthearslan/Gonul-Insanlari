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
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ViewModelLayer.Models.Configuration;
using ViewModelLayer.Models.Newsletter;
using IModel = RabbitMQ.Client.IModel;

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

        public Task SendNewsletterAsync(WeeklyNewsletterModel model)
        {
            Console.WriteLine($"{_configuration?.User} + {_configuration?.Password}");
            return Task.CompletedTask;
        }
    }
}
