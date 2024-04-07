using Microsoft.VisualStudio.TestTools.UnitTesting;
using GonulInsanlari.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.Abstract.Services;
using AutoMapper;
using DataAccessLayer.Concrete.EntityFramework;
using BussinessLayer.Concrete.Managers;
using EntityLayer.Concrete.Entities;
using NuGet.Protocol.Plugins;
using DataAccessLayer.Concrete.Providers;
using Microsoft.EntityFrameworkCore;
using FluentValidation.Results;
using GonulInsanlari.Areas.Admin.Models.ViewModels.Assignment;
using Microsoft.AspNetCore.Http;

namespace GonulInsanlari.Areas.Admin.Controllers.Tests
{
    [TestClass()]
    public class AssignmentControllerTests
    {

        //private readonly IAssignmentService _manager;
        //private readonly IMapper _mapper;

        //public AssignmentControllerTests(IAssignmentService manager, IMapper mapper)
        //{
        //    _manager = manager;
        //    _mapper = mapper;
        //}

        [TestMethod()]
        public async Task AddAttachmentTest()
        {
            using var db = new Context();

            //var task = await _manager.GetByIdAsync(38);
            var task = db.Assignments.FirstOrDefault(); 
            EFAssignmentDAL eF = new();
            
            List<TaskAttachment> files = new List<TaskAttachment>();

            for (int i = 0; i < 10; i++)
            {

                files.Add(new()
                {
                    Assignment = task,
                    Path = $"TestPath => {i.ToString()} ",

                });

            }


          var result =  await eF.AddAttachmentsAsync(files);
            Assert.AreEqual(10, result);
        }


    }
}