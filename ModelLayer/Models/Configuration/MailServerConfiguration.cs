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

        public string? Host { get; set; }

        public string? Port { get; set; }

        public string? User { get; set; }

        public string? Password { get; set; }




    }
}
