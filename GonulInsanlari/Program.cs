using BussinessLayer.Abstract;
using BussinessLayer.Concrete;
using BussinessLayer.Concrete.Validations;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Providers;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using GonulInsanlari.Areas.Admin.AutoMapper.Profiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Rotativa.AspNetCore;
using System.Configuration;
using System.Reflection;
using GonulInsanlari.Middlewares;
using Serilog;
using ViewModelLayer.Models.Tools;
using GonulInsanlari.Areas.Admin.Authorization;
using ViewModelLayer.Models.Configuration;
using TableDependency.SqlClient;
using GonulInsanlari.Subscriptions;
using GonulInsanlari.Configurations;
using GonulInsanlari.Hubs;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));


}
);
builder.Services.AddDbContext<Context>();

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.SignIn.RequireConfirmedEmail = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);
    opt.User.RequireUniqueEmail = true;

}).AddDefaultTokenProviders().AddEntityFrameworkStores<Context>();

builder.Services.Configure<MailServerConfiguration>(builder.Configuration.GetSection(MailServerConfiguration.Server));

builder.Services.AddScoped<MailServerConfiguration>();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        );
});

builder.Services.Configure<CookiePolicyOptions>(options => {
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});



builder.Services.AddBussinessServices();


builder.Services.AddValidators();

builder.Services.AddSingleton<IMemoryCache, MemoryCache>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<ResponseModel>();


builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

builder.Services.AddSingleton<NotificationHub>();

builder.Services.AddSingleton<NotificationSubscription>();


builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/login/admin";

}
    );

builder.Services.ConfigureApplicationCookie(opt => opt.AccessDeniedPath = new PathString("/error/accessDenied"));

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.AddSerilog(logger);





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseStatusCodePagesWithReExecute("/error/notFound", "?code={0}");

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseDatabaseSubscription<NotificationSubscription>("Notifications");

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthentication();

app.UseSession();

app.UseAuthorization();

app.UseMiddleware(typeof(TotalVisitorCounterMiddleware));


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

 
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapHub<NotificationHub>("/notificationHub");



//app.UseUserConfiguration();


app.Run();

