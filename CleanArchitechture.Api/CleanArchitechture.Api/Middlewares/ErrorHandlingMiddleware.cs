using System.Net;
using BookKeeping.Core.Exceptions;
using CleanArchitechture.Core.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CleanArchitechture.Api.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            ApiResult<object> errorResponse;
            var errors = FilterErrorMessage(ex);
            Console.WriteLine(ex);
            switch (ex)
            {
                case CustomException exception:
                    errorResponse = new ApiResult<object>(){Error = exception.Errors,IsSuccess = false};
                    errorResponse.Message = exception.Message;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.MessageDisplayType = exception.MessageDisplayType;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse = new ApiResult<object>(){ Message = ex.Message, Error = errors,IsSuccess = false};
                    break;
            }

            var settings = new JsonSerializerSettings { ContractResolver = new CamelCaseContractResolver() };

            var errorJson = JsonConvert.SerializeObject(errorResponse, Formatting.Indented, settings);

            await response.WriteAsync(errorJson);
        }
    }

    #region Supported Methods

    private static List<string> FilterErrorMessage(Exception ex)
    {
        var errors = new List<string>();
        var e = ex;
        while (e != null)
        {
            errors.Add(e.Message);
            e = e.InnerException;
        }

        return errors.Count > 1 ? errors : null;
    }

    public class CamelCaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return char.ToLower(propertyName[0]) + propertyName[1..];
        }
    }

    #endregion
}

public static class GlobalErrorHanding
{
    public static void UseGlobalErrorHandlingMiddleware(this IApplicationBuilder appBuilder)
    {
        appBuilder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}