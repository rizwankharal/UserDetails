﻿using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using UserDetails.Logger;
using UserDetails.Models;

namespace UserDetails.ExceptionHandlerMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _logger;
        public ExceptionMiddleware(RequestDelegate next, ILoggerService logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleException(httpContext, ex);
            }
        }
        private async Task HandleException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ErrorInfo()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware."
            }.ToString());
        }


    }
}
