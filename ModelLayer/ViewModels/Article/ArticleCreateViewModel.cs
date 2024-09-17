using System.Security.Policy;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using ViewModelLayer.Models.Tools;
using Microsoft.AspNetCore.Http;

namespace ViewModelLayer.ViewModels.Article
{
    public record ArticleCreateViewModel
    {
       
        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime Created { get; set; } = DateTime.Now;


        public IFormFile ImagePath { get; set; } = null!;

        public int CategoryID { get; set; }

        public bool Status { get; set; } = true;

        public bool IsDraft { get; set; } = false;


        /// <summary>
        /// To convert raw url.
        /// </summary>
        /// <param name="Url"></param>
      
    }




}

