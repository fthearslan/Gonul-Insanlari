using EntityLayer.Concrete.Entities;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels.Comment
{
    public record CommentSearchViewModel
    {

        public string Search { get; set; }

        public CommentProgress? Progress { get; set; }

        public string? ArticleTitle { get; set; }




    }
}
