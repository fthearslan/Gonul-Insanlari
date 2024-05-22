﻿using AutoMapper;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Comment;

namespace GonulInsanlari.Areas.Admin.AutoMapper.Profiles
{
    public class CommentProfile:Profile
    {
        public CommentProfile()
        {

            CreateMap<Comment, CommentListViewModel>();
                

            CreateMap<Article, CommentsByArtcleViewModel>()
                .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Comments.Count));


        }
    }
}
