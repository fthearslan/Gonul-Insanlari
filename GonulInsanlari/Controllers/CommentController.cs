using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete.Managers;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ViewModelLayer.ViewModels.Comment;

namespace GonulInsanlari.Controllers
{
    [AllowAnonymous]
    [Route("comment")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentManager;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        public CommentController(ICommentService commentService, IMapper mapper, IMemoryCache cache)
        {
            _commentManager = commentService;
            _mapper = mapper;
            _cache = cache;
        }

        [Route("get/{articleId}")]
        public async Task<IActionResult> Get(int articleId, int skipCount)
        {

            int count = _cache.GetOrCreate("skipCount", entry =>
             {
                 entry.SetValue(skipCount);
                
                 return skipCount;
             
             });


            List<Comment> comments = await _commentManager.
             GetWhere(x => x.ArticleID == articleId && x.Status == true && x.Progress == CommentProgress.Approved)
             .Include(x => x.Replies)
           .OrderByDescending(x => x.Created)
             .Skip(count)
             .Take(5)
         .ToListAsync();


            List<CommentByArticleUIViewModel> model = _mapper.Map<List<CommentByArticleUIViewModel>>(comments);


            model.ForEach(x =>
            {

                x.DisplayedDateTime = x.Created.ToString("MM/dd/yyyy HH:mm");

                x.Replies.ForEach(y => y.DisplayedDateTime = y.Created.ToString("MM/dd/yyyy HH:mm"));


            });

            _cache.Set("skipCount",count+5);

            return Json(model);

        }


        [HttpPost]
        [Route("submit")]
        public async Task<IActionResult> Submit(CommentSubmitUIViewModel input)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetModelErrors());

            Comment comment = _mapper.Map<Comment>(input);

            await _commentManager.AddAsync(comment);

            return StatusCode(200);


        }







    }
}
