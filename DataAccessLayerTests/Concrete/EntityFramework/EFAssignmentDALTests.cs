using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Concrete.Providers;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using EntityLayer.Concrete.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Concrete.EntityFramework.Tests
{
    [TestClass()]
    public class EFAssignmentDALTests
    {
        //    [TestMethod()]
        //    public async Task DeleteAttachmentTest()
        //    {
        //        Context context = new Context();
        //        EFAssignmentDAL manager = new EFAssignmentDAL();

        //        var attachments = context.TaskAttachment.ToList();

        //        var attachment = attachments.First();



        //        Assert.AreEqual(true, await manager.DeleteAttachmentAsync(attachment.Path,attachment.Assignment.Id));

        //    }
        [TestMethod()]
        public async Task AddTaskLog()
        {
            using (var context = new Context())
            {

                EFAssignmentDAL manager = new EFAssignmentDAL();


                var assignment = await context.Assignments.Include(x => x.Logs).FirstAsync();
                var user = context.Users.FirstOrDefault(x => x.UserName == "fthearslan15");

                var tasklog = new TaskLog($"The user name {user.UserName} changed the progress.")
                {
                    CreatedBy = user.Name,
                    Assignment = assignment,
                };

                Assert.IsTrue(await manager.LogAsync(tasklog));









            }
        }



    }
}