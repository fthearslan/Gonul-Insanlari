using Microsoft.AspNetCore.Identity;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.Models.Configuration;
using ViewModelLayer.Models.Newsletter;

namespace BussinessLayer.Abstract.Services
{
    public interface IEmailService
    {

        /// <summary>
        /// Send article to the subscribers asynchrnously.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task SendNewsletterAsync(WeeklyNewsletterModel model);





    }
}
