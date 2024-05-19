using AutoMapper;
using BussinessLayer.Abstract.Services;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public CommentController(ICommentService manager, IMapper mapper, ILogger<CommentController> logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        #region Create

        #endregion

        #region Read

        [Route("list")]
        public async Task<IActionResult> List()
        {
            List<Comment> comment = await _manager.GetAllAsync();

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

            return View(model);

        }


        #endregion

        #region Update

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
