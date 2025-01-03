﻿using JetBrains.Annotations;
using System.Security.Policy;

namespace ViewModelLayer.ViewModels.Comment
{
    public record CommentsByArtcleViewModel
    {

        public int Id { get; set; }

        public string? Title { get; set; }

        public int CommentCount { get; set; }

        public DateTime Created { get; set; }
        public bool Status { get; set; }

    }
}
