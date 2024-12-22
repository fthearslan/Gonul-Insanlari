using DataAccessLayer.Concrete.Providers;
using EntityLayer.Concrete.Entities;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GonulInsanlari.Middlewares
{
    public class TotalVisitorCounterMiddleware
    {



        private readonly RequestDelegate _request;



        public TotalVisitorCounterMiddleware(RequestDelegate request)
        {
            _request = request;
        }



        public async Task Invoke(HttpContext _context)
        {

            string? visitorId = _context.Request.Cookies["VisitorId"];

            if (visitorId is null)
            {

                using Context _dbContext = new Context();

                await _dbContext.Visitors.AddAsync(new());

                await _dbContext.SaveChangesAsync();

                var consentFeature = _context.Features.Get<ITrackingConsentFeature>();

                consentFeature?.GrantConsent();

                _context.Response.Cookies.Append("VisitorId", Guid.NewGuid().ToString(), new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = true,
                    Secure = true,
                    MaxAge = TimeSpan.FromHours(1),
                    Expires = DateTimeOffset.Now.AddHours(1)
                });


                consentFeature?.WithdrawConsent();
            }

            await _request(_context);



        }


    }
}
