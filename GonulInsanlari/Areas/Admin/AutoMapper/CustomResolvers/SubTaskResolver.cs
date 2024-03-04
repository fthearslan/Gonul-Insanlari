using AutoMapper;
using EntityLayer.Concrete.Entities;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Article;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment;

namespace GonulInsanlari.Areas.Admin.AutoMapper.CustomResolvers
{
    public class SubTaskResolver : IValueResolver<AssignmentCreateViewModel, Assignment, List<SubTask>>
    {
        public List<SubTask> Resolve(AssignmentCreateViewModel source, Assignment destination, List<SubTask> destMember, ResolutionContext context)
        {
            foreach (var subtask in source?.SubTasks)
            {
                destination.SubTasks.Add(new SubTask()

                {
                    Description = subtask,
                    Assignment = destination,
                    Progress = SubTasksProgress.Pending,

                });

            }

            return destination.SubTasks;


        }
    }
}
