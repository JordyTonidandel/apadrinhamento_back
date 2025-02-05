using EncantoApadrinhamento.Core.CustomException;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;

namespace EncantoApadrinhamento.Api.Configurations.ExceptionHandler
{
    public static class ExceptionHandlerConfiguration
    {
        public static void UseExceptionHandlerConfiguration(this WebApplication webApplication)
        {
            webApplication.UseExceptionHandler("/error");
            _ = webApplication.Map("/error", (HttpContext http) =>
            {
                var error = http.Features?.Get<IExceptionHandlerFeature>()?.Error;

                if (error != null)
                {
                    var problemDetails = new ProblemDetails
                    {
                        Title = "An error occurred",
                        Status = (int)HttpStatusCode.InternalServerError,
                        Detail = error.Message
                    };

                    if (error is BadHttpRequestException badHttpRequestException)
                    {
                        problemDetails.Title = "Invalid request";
                        problemDetails.Status = badHttpRequestException.StatusCode > 0 ? badHttpRequestException.StatusCode : (int)HttpStatusCode.BadRequest;
                        problemDetails.Detail = badHttpRequestException.Message;
                    }

                    if (error is DomainException domainException)
                    {
                        problemDetails.Title = domainException.Message;
                        problemDetails.Status = (int)HttpStatusCode.BadRequest;
                        problemDetails.Detail = string.Join(",", domainException.Errors == null ? "Erros não indentificados." : domainException.Errors.ToString());
                    }

                    if (error is SecurityTokenException securityTokenException)
                    {
                        problemDetails.Title = "Unauthorized";
                        problemDetails.Status = (int)HttpStatusCode.Unauthorized;
                        problemDetails.Detail = securityTokenException.Message;
                    }

                    if (error is InvalidOperationException invalidOperationException)
                    {
                        problemDetails.Title = "Invalid operation";
                        problemDetails.Status = (int)HttpStatusCode.BadRequest;
                        problemDetails.Detail = invalidOperationException.Message;
                    }

                    if (error is ArgumentException argumentException)
                    {
                        problemDetails.Title = "Invalid argument";
                        problemDetails.Status = (int)HttpStatusCode.BadRequest;
                        problemDetails.Detail = argumentException.Message;
                    }

                    ILogger? logger = http.RequestServices.GetService(typeof(ILogger<Program>)) as ILogger<Program>;
                    logger?.LogError(error, message: error.Message);

                    http.Response.StatusCode = problemDetails.Status.Value;
                    http.Response.WriteAsJsonAsync(problemDetails, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }
            });
        }
    }
}
