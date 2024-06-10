using EntityLayer.Concrete.Entities;
using GonulInsanlari.Extensions.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModelLayer.ViewModels.Admin;

namespace GonulInsanlari.Areas.Admin.ViewComponents.Admin
{
    public class GetAdminsAll : ViewComponent
    {

        private readonly UserManager<AppUser> _userManager;

        public GetAdminsAll(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {


            var model = _userManager
              .GetUsersWithRolesAsync()
               .Result.ToList();

              var model2=  model.Select(x => new ListAdminsViewModel()
                {
                    Id = x.Id,
                    Username = x.UserName,
                    Roles = x.Roles.ToList(),
                    Status = x.Status,
                    ImagePath = x.ImagePath

                }).ToList();




            return View(model2);
        }
    }







}
