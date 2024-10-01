using AutoMapper;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Authorization;
using GonulInsanlari.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using System.Runtime.InteropServices;
using ViewModelLayer.Models.Tools;
using ViewModelLayer.ViewModels.Comment;
using X.PagedList;

namespace GonulInsanlari.Areas.Admin.Controllers
{

    [Route("comments")]
    [Area(nameof(Admin))]
    [Authorize]

    public class CommentController : Controller
    {

        private readonly ICommentService _manager;
        private readonly IMapper _mapper;
        private readonly ILogger<CommentController> _logger;
        ResponseModel _response;
        public CommentController(ICommentService manager, IMapper mapper, ILogger<CommentController> logger, ResponseModel response)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
            _response = response;
        }

        #region Create

        #endregion

        #region Read

        [Route("list")]
        [HasPermission(PermissionType.Comment, Permission.Read)]

        public async Task<IActionResult> List(int pageNumber = 1)
        {
            List<Comment> comment = await _manager.GetAllAsync(CommentProgress.Pending, true);

            List<CommentListViewModel> model = new();

            try
            {

                model = _mapper.Map<List<CommentListViewModel>>(comment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();

                throw;
            }

            return View(await model.ToPagedListAsync(pageNumber, 10));

        }

        [Route("list/approved")]
        [HasPermission(PermissionType.Comment, Permission.Read)]

        public async Task<IActionResult> Approved(int pageNumber = 1)
        {

            List<Comment> comment = await _manager.GetAllAsync(CommentProgress.Approved, true);

            List<CommentListViewModel> model = new();

            try
            {

                model = _mapper.Map<List<CommentListViewModel>>(comment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();

                throw;
            }

            return View(await model.ToPagedListAsync(pageNumber, 10));

        }

        [Route("list/rejected")]
        [HasPermission(PermissionType.Comment, Permission.Read)]

        public async Task<IActionResult> Rejected(int pageNumber = 1)
        {
            List<Comment> comment = await _manager.GetAllAsync(CommentProgress.Rejected, true);

            List<CommentListViewModel> model = new();

            try
            {

                model = _mapper.Map<List<CommentListViewModel>>(comment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();

                throw;
            }

            return View(await model.ToPagedListAsync(pageNumber, 10));

        }

        [Route("list/disabled")]
        [HasPermission(PermissionType.Comment, Permission.Read)]

        public async Task<IActionResult> Disabled(int pageNumber = 1)
        {
            List<Comment> comment = await _manager.GetAllAsync(CommentProgress.Disabled, false);

            List<CommentListViewModel> model = new();

            try
            {

                model = _mapper.Map<List<CommentListViewModel>>(comment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();

                throw;
            }

            return View(await model.ToPagedListAsync(pageNumber, 10));

        }



        [Route("details/{articleTitle}")]
        [HasPermission(PermissionType.Comment, Permission.Read)]

        public async Task<IActionResult> GetDetails(string articleTitle, int pageNumber = 1)
        {

            var comments = await _manager.GetByArticleAsync(articleTitle);
            ViewData["ArticleTitle"] = articleTitle;
       

            if (comments is null)
                return NotFound();

            List<CommentListViewModel>? model = _mapper.Map<List<CommentListViewModel>?>(comments);

            return View(await model.ToPagedListAsync(pageNumber, 5));

        }

        [Route("search")]
        public async Task<IActionResult> Search(CommentSearchViewModel model)
        {
             
            List<Comment> results = await _manager.SearchAsync(model);

            List<CommentListViewModel> viewModel = new();

            viewModel = _mapper.Map<List<CommentListViewModel>>(results);

            return Json(viewModel);

        }


        #endregion

        #region Update

        [Route("update")]
        [HttpPost]
        [HasPermission(PermissionType.Comment, Permission.Update)]
        public async Task<IActionResult> ApproveOrReject(int commentId, string action)
        {
            Comment comment = await _manager.GetByIdAsync(commentId);

            if (comment is not null)
            {
                switch (action)
                {
                    case "approve":
                        comment.Progress = CommentProgress.Approved;
                        break;

                    case "reject":
                        comment.Progress = CommentProgress.Rejected;
                        break;

                    case "disable":
                        comment.Progress = CommentProgress.Disabled;
                        comment.Status = false;
                        break;

                    case "save":
                        comment.Progress = CommentProgress.Pending;
                        comment.Status = true;
                        break;
                }

                _manager.Update(comment);

                _response.success = true;
                _response.responseMessage = "Status of the comment has been successfully changed.";


            }
            else
            {

                _response.success = false;
                _response.responseMessage = "Something went wrong...";

            }

            return Json(new
            {
                success = _response.success,
                responseMessage = _response.responseMessage
            });
        }


        #endregion

        #region Delete

        [Route("delete/{id}")]
        [HttpPost]
        [HasPermission(PermissionType.Comment, Permission.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _manager.GetByIdAsync(id);

            if (comment is null)
                return NotFound();

            _manager.Delete(comment);

            return Json(200);

        }

        #endregion


    }
}
