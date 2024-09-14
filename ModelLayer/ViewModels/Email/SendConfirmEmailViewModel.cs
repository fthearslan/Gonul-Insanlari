﻿using Microsoft.AspNetCore.Http;
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


        public SendConfirmEmailViewModel(string username,string callbackAction,string callbackController,HttpContext httpContext)
        {
            Username = username;
            CallBackAction=callbackAction;
            CallBackController=callbackController;
            HttpContext = httpContext;
        }


        public string Username { get; set; }

        public string? Subject { get; set; }

        public HttpContext HttpContext { get; set; }
        public string CallBackAction { get; set; }

        public string CallBackController { get; set; }

    }
}