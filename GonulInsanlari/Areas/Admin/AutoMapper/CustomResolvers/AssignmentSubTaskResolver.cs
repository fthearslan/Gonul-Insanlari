using AutoMapper;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment;

namespace GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers
{
    //public class AssignmentSubTaskResolver : IValueResolver<Assignment, AssignmentDetailsViewModel, List<SubTaskViewModel>>
    //{
    //    public List<SubTaskViewModel> Resolve(Assignment source, AssignmentDetailsViewModel destination, List<SubTaskViewModel> destMember, ResolutionContext context)
    //    {

    //        foreach (var subtask in source.SubTasks)
    //        {
    //            destination.SubTasks.Add(new SubTaskViewModel()
    //            {
    //                Id = subtask.Id,
    //                Description = subtask.Description,
    //                Progress = subtask.Progress.ToString(),
    //            });
    //        }

    //        return destination.SubTasks;
    //    }
    //}
}
