using BussinessLayer.Abstract.Services;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment.TaskLogs;
using Microsoft.AspNetCore.Mvc;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Assignment
{
    public class AssignmentGetTaskLogs : ViewComponent
    {
        IAssignmentService _manager;

        public AssignmentGetTaskLogs(IAssignmentService manager)
        {
            _manager = manager;
        }

        public IViewComponentResult Invoke(int _taskId)
        {

            var logs = _manager.GetLogsByTaskAsync(_taskId).Result;

            List<TaskLogListVievModel> model = new();

            foreach (var log in logs)
            {
                model.Add(new TaskLogListVievModel()
                {
                    Id = log.Id,
                    Description = log.Description,
                    Createdby = log?.CreatedBy?.UserName,
                    CreatedDate = log?.CreatedDate,
                    CreatedByImage=log?.CreatedBy?.ImagePath,
                    AttachmentTitle=log?.Attachment?.Path
                    
                });
            }

            return View(model);




        }
    }
}
