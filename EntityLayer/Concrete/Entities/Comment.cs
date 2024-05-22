using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Entities
{
    [DebuggerDisplay("Name={NameSurname},Progress={Progress},Status={Status}")]
    public class Comment : BaseEntity
    {
        [StringLength(maximumLength: 50)]
        public string NameSurname { get; set; }

        [StringLength(maximumLength: 300)]
        public string Content { get; set; }
        public int? ArticleID { get; set; }
        public string? EditedBy { get; set; }
        public Article Article { get; set; }
        public CommentProgress Progress { get; set; }

    }
    public enum CommentProgress
    {
        Pending,
        Approved,
        Rejected,
        Disabled
    }
}
