using Brilliancy.Soccer.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Brilliancy.Soccer.WebApi.Providers
{
    public class ErrorHandlingMiddleware
    {
        /// <summary>
        /// Returns status code 200 and isSuccess = false if exception is known (CustomException). In other cases return 500
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Task HandleExceptionAsync(HttpContext context)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;
            if (exception is CustomException)
            {
                code = HttpStatusCode.OK;
            }

            var result = JsonConvert.SerializeObject(new { isSuccess = false, message = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
