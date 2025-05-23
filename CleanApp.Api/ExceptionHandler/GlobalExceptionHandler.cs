﻿using System.Net;
using App.Application;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CleanApp.Api.ExceptionHandler
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, 
            CancellationToken cancellationToken)
        {
            var errorAsDto =ServiceResult.Fail(exception.Message, HttpStatusCode.InternalServerError);

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(errorAsDto, cancellationToken: cancellationToken);

            return true;

        }
    }
}
