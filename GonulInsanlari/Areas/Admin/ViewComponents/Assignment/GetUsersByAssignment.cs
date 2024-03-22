using BussinessLayer.Abstract.Services;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Assignment
{
    public class GetUsersByAssignment : ViewComponent
    {
        private readonly IAssignmentService _manager;

        public GetUsersByAssignment(IAssignmentService manager)
        {
            _manager = manager;
        }

        public IViewComponentResult Invoke(int assignmentId)
        {
            
            var assignment = _manager.GetByIdAsync(assignmentId).Result;

            List<(int, string, string)> model = new();

            if (assignment?.UserAssignments != null)
            
                foreach (var user in assignment.UserAssignments)
                {
                    model.Add(new(user.User.Id, user.User.UserName, user.User.ImagePath));
                }

            ViewData["assignmentId"] = assignmentId;

            return View(model);
        }

    }
}
