using AutoMapper;
using BussinessLayer.Abstract.Services;
using BussinessLayer.Concrete.Managers;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit.Tnef;
using ViewModelLayer.ViewModels.Comment;

namespace GonulInsanlari.Controllers
{
    [AllowAnonymous]
    [Route("comment")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentManager;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentManager = commentService;
            _mapper = mapper;
        }

        [Route("get/{articleId}")]
        public async Task<IActionResult> Get(int articleId, int skipCount)
        {

            List<Comment> comments = await _commentManager.
             GetWhere(x => x.ArticleID == articleId && x.Status == true && x.Progress == CommentProgress.Approved)
           .OrderByDescending(x => x.Created)
             .Skip(skipCount)
             .Take(5)
         .ToListAsync();

            List<CommentByArticleUIViewModel> model = _mapper.Map<List<CommentByArticleUIViewModel>>(comments);
          

            model.ForEach(x => { x.DisplayedDateTime = x.Created.ToShortDateString(); });


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
