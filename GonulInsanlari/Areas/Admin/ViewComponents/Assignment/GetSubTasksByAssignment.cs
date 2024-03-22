using AutoMapper;
using BussinessLayer.Abstract.Services;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Permissions;
using System.Security.Policy;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Assignment
{
    public class GetSubTasksByAssignment : ViewComponent
    {


        private readonly IAssignmentService _manager;
        public GetSubTasksByAssignment(IAssignmentService manager)
        {
            _manager = manager;
        }

        public IViewComponentResult Invoke(int assignmentId)
        {
            var task = _manager.GetByIdAsync(assignmentId).Result;


                List<SubTaskViewModel> model = new();

                foreach (var subtask in task.SubTasks)
                {
                    model.Add(new SubTaskViewModel()
                    {
                        Id = subtask.Id,
                        Description = subtask.Description,
                        Progress = subtask.Progress.ToString()
                    });
                }

                return View(model);
        }


    }
}
