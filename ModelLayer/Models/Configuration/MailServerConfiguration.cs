using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.Models.Configuration
{
    public record MailServerConfiguration
    {

        public const string Server = "MailServer";

        public string HostName { get; set; } = null!;

        public int Port { get; set; } 

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
        public bool UseDefaultCredentials { get; set; }
        public bool EnableSsl { get; set; }




    }
}
