using Microsoft.VisualStudio.TestTools.UnitTesting;
using GonulInsanlari.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using EntityLayer;
using Microsoft.Extensions.Caching.Memory;

namespace GonulInsanlari.Areas.Admin.Controllers.Tests
{
    [TestClass()]
    public class ArticleControllerTests
    {
        UserManager<AppUser> userManager;
        IMemoryCache memoryCache;

        public ArticleControllerTests(UserManager<AppUser> userManager, IMemoryCache memoryCache)
        {
            this.userManager = userManager;
            this.memoryCache = memoryCache;
        }

        [TestMethod()]
        public void AddArticleTest()
        {
           
            ArticleController cont = new ArticleController(userManager,memoryCache);
          var result = cont.AddArticle();
           
        
        }
    }
}