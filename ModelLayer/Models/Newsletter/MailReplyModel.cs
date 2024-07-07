using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.Models.Newsletter
{
    public record MailReplyModel
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Subject { get; set; } = null!;
        public string Content { get; set; } = null!;

        public const string Path = "C:\\Users\\Admin\\source\\repos\\GonulInsanlariCoreMVC\\GonulInsanlari\\wwwroot\\inspinia-gh-pages\\email_templates\\Reply.html";

    }
}
