using Microsoft.VisualStudio.TestTools.UnitTesting;
using GonulInsanlari.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System.Resources;
using MimeKit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace GonulInsanlari.Areas.Admin.Controllers.Tests
{



    [TestClass()]
    public class LoginControllerTests
    {


        [TestMethod()]
        public async Task ExampleTest()
        {


            string subscriber = "fthearslan12@gmail.com";
            string sender = "ginsanlari@gmail.com";
            string password = "jcla wrrg gfeg trjp";

            SmtpClient client = new SmtpClient("smtp.gmail.com")
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Port = 587,
                Credentials = new NetworkCredential(sender, password)
            };

            //var root = env.WebRootPath;

            //var path1 = root + Path.DirectorySeparatorChar.ToString() + "inspinia-gh-pages" + Path.DirectorySeparatorChar.ToString() + "email_templates" + Path.DirectorySeparatorChar.ToString() + "Newsletter.html";

            var path1 = "C:\\Users\\Admin\\source\\repos\\GonulInsanlariCoreMVC\\GonulInsanlari\\wwwroot\\inspinia-gh-pages\\email_templates\\Newsletter.html";

            var builder = new BodyBuilder();
            using (StreamReader reader = System.IO.File.OpenText(path1))
            {
                builder.HtmlBody = reader.ReadToEnd();

            };

            string articleContent = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?";
            string url = "http://www.youtube.com";

            var body = builder.HtmlBody.Replace("{Name}", "Fatih Arslan");

            var body1 = body.Replace("{Title}", "Why were we created?");
            var body2 = body1.Replace("{Content}", articleContent);
            var body3 = body2.Replace("{Url}", url);


            await client.SendMailAsync(new(sender, subscriber)
            {
                Body = body3,
                Subject = "Weekly Articles",
                IsBodyHtml = true
            });

           




        }
    }
}