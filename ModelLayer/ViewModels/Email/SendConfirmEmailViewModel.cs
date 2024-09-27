using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Email
{
    public record SendConfirmEmailViewModel
    {

        public SendConfirmEmailViewModel(string username)
        {
            Username = username;
        }


        public SendConfirmEmailViewModel(string userName, string routeName, HttpContext httpContext)
        {
            Username = userName;
            RouteName = routeName;
            HttpContext = httpContext;

        }

        public string Username { get; set; }

        public string? Subject { get; set; }

        public HttpContext HttpContext { get; set; }

        public string RouteName { get; set; }
    }
}
