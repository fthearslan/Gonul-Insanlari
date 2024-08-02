using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.Models.Tools;
using ViewModelLayer.ViewModels.Admin;

namespace ViewModelLayer.Models.Newsletter
{
    public record SendMailModel
    {
        public SendMailModel()
        {
            AttachmentExists = new();
        }

        public const string Path = "C:\\Users\\Admin\\source\\repos\\GonulInsanlariCoreMVC\\GonulInsanlari\\wwwroot\\inspinia-gh-pages\\email_templates\\Reply.html";
        
        public int Id { get; set; }
       
        List<string>? to;
        
        public List<string> To
        {
            get
            {
                return to;
            }
            set
            {
                if (value.Count == 1)
                    to = value[0].Split(',').ToList();

                if (value.Count > 1)
                {

                    string val = string.Join(',', value);

                    List<string> list = new() { val };

                    to = list;


                }

            }
        }
        
        public string Subject { get; set; } = null!;
        
        public string Content { get; set; } = null!;
        
        public int? ReplyTo { get; set; }
        
        public List<string>? AttachmentExists { get; set; } 
        
        public ContactStatus Status { get; set; }
        
        public IList<IFormFile>? Attachments { get; set; }


        #region Methods

        public async Task<List<string>> GetAttachmentPathsAsync()
        {
            if (Attachments is null)
                return null;

            return await ImageUpload.UploadFileAsync(Attachments);


        }

        public List<ContactToCollection> GetContactTos()
        {

            List<ContactToCollection> contactTos = new();

            to?.ForEach(adr => contactTos.Add(new(adr)));

            return contactTos;

        }


        #endregion


    }
}
