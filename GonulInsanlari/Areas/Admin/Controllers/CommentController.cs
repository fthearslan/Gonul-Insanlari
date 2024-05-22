using AutoMapper;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.Tools;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using System.Runtime.InteropServices;
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
        public async Task<IActionResult> Disabled(int pageNumber = 1)
        {
            List<Comment> comment = await _manager.GetAllAsync(CommentProgress.Disabled,false);

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



        [Route("details/{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var comments = await _manager.GetByArticleAsync(id);

            return View(comments);

        }

        [Route("search")]
        public async Task<IActionResult> Search(string search, CommentProgress progress, bool status)
        {

            List<Comment> results = await _manager.SearchAsync(search, progress, status);

            List<CommentListViewModel> model = new();

            try
            {
                model = _mapper.Map<List<CommentListViewModel>>(results);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();

            }

            return Json(model);
        }


        #endregion

        #region Update

        [Route("update")]
        [HttpPost]
        public async Task<IActionResult> ApproveOrReject(int commentId, string action)
        {

            Comment comment = await _manager.GetByIdAsync(commentId);

            if (comment is not null)
            {
                switch (action)
                {
                    case "approve":
                        comment.Progress = CommentProgress.Approved;
                        _manager.Update(comment);
                        break;
                    case "reject":
                        comment.Progress = CommentProgress.Rejected;
                        _manager.Update(comment);
                        break;
                    case "disable":
                        comment.Progress = CommentProgress.Disabled;
                        comment.Status = false;
                        _manager.Update(comment);
                        break;
                    case "delete":
                        _manager.Delete(comment);
                        break;
                    case "save":
                        comment.Progress = CommentProgress.Pending;
                        comment.Status = true;
                        break;
                }

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
        public async Task<IActionResult> Delete(int id)
        {

            var comment = await _manager.GetByIdAsync(id);
            return RedirectToAction(nameof(List));
        }

        #endregion


    }
}
