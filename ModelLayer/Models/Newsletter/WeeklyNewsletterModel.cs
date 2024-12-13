using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.Models.Newsletter
{
    public record WeeklyNewsletterModel
    {
      

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Category { get; set; }

        public string ImagePath     { get; set; }
        public DateTime Created { get; set; }

        public string Subject { get; set; } = "New Article";

        public HttpContext Context { get; set; }

        public string Route { get; set; }


    }
}
