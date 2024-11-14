using Microsoft.VisualStudio.TestTools.UnitTesting;
using GonulInsanlari.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;
using DataAccessLayer.Concrete.Providers;
using Microsoft.EntityFrameworkCore;
using EntityLayer.Concrete.Entities;

namespace GonulInsanlari.Areas.Admin.Controllers.Tests
{
    [TestClass()]
    public class DashboardControllerTests
    {
        [TestMethod()]
        public void GetVisitsTest()
        {


            using var c = new Context();
            List<Visitor> visitors = c.Visitors.Where(x => x.Visited.Year == DateTime.Now.Year).ToList();

          
            List<KeyValuePair<int, string>> mnths = new();
            visitors.ForEach(visitor=> { 
            
            
                mnths.Add( new (visitor.Visited.Month,visitor.Visited.ToString("MMMM")));


            });
            

            List<KeyValuePair<int, string>> months = mnths.Distinct().ToList();

            Dictionary<string,int> monthsAndcounts = new ();
            months.ForEach(month =>
            {
               
                

                monthsAndcounts.Add(month.Value, visitors.Count(x => x.Visited.Month == month.Key));


            });


            Assert.Fail();
        }
    }
}