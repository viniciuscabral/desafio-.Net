using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WsProjetoVinicius.Model
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (ex is ArgumentException) code = HttpStatusCode.UnprocessableEntity;
            else if (ex is ArgumentNullException) code = HttpStatusCode.UnprocessableEntity;
            else if (ex is NotFoundException) code = HttpStatusCode.NotFound;
            else if (ex is UnauthorizedException) code = HttpStatusCode.Unauthorized;
            else if (ex is DuplicateUserException) code = HttpStatusCode.Conflict;

            var result = JsonConvert.SerializeObject(new { message = ex.Message , errorCode = code });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}

