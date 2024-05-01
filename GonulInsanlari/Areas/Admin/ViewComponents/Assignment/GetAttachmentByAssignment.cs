using BussinessLayer.Abstract.Services;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Assignment
{
    public class GetAttachmentByAssignment : ViewComponent
    {
        private readonly IAssignmentService _manager;

        public GetAttachmentByAssignment(IAssignmentService manager)
        {
            _manager = manager;
        }

        public IViewComponentResult Invoke(int taskId)
        {

            
            var task = _manager.GetByIdAsync(taskId).Result;

            if (task?.Attachments != null)
            {

                ViewData["taskId"] = taskId;

                List<(string, DateTime)> files = new();

                foreach (var attachment in task.Attachments)
                {

                    files.Add((attachment.Path, attachment.CreatedDate));
                    
                }


                return View(files);

            }

            return View();

        }

    }
}
