

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OkalaExchange.Contracts;
using OkalaExchange.Domain.SeedWork;
using System.Net;

namespace OkalaExchange.ApplicationService.Common.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }

            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var response = httpContext.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;



            var errorDetails = new ErrorDetails
            {
                ErrorCode = exception is BaseException baseExDetails ? baseExDetails.ErrorCode : "INTERNAL_SERVER_ERROR",
                ErrorMessage = exception.Message
            };

            var baseResponse = new BaseResponse<object>
            {
                Success = false,
                Message = "An error occurred while processing your request.",
                Error = errorDetails
            };

            _logger.LogError($"Something went wrong: {JsonConvert.SerializeObject(baseResponse)}");

            return response.WriteAsync(JsonConvert.SerializeObject(baseResponse));
        }
    }
}
