using GonulInsanlari.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Policy;

namespace GonulInsanlari.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext _context)
        {

            try
            {
                await _next.Invoke(_context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong... {ex.Message}");

                if (_context.Response.StatusCode != 404)
                    _context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

              

            }


        }


    }
}
