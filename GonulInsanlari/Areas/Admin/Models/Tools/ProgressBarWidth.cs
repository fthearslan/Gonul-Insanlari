using EntityLayer.Concrete.Entities;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;

namespace GonulInsanlari.Areas.Admin.Models.Tools
{
    public static class ProgressBarWidth
    {
        public static int GetWidth(DateTime Created, DateTime Due)
        {
            var days = Due.Subtract(Created).Days;
            var past = DateTime.Now.Subtract(Created).Days;
            decimal value = 100 * past / days;
            var width = (int)Math.Round(value, 0);
            return width;

        }

        public static int GetWidthBySubTasks(List<SubTask> subTasks)
        {
            if (subTasks.Count > 0)
            {
                var all = subTasks.Count;
                var done = subTasks.Where(s => s.Progress == SubTasksProgress.Done).Count();
                decimal width = (done * 100) / all;
                return (int)Math.Round(width, 0);

            }
            return 0;
        }
        public static int GetWidthBySubTasks(int _tasksAll, int _tasksDone)
        {
            if (_tasksAll > 0)
            {

                decimal width = (_tasksDone * 100) / _tasksAll;
                return (int)Math.Round(width, 0);

            }

            return 0;
        }




    }
}
