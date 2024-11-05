using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using My.ModularMonolith.Api.Filters.Models;
using My.ModularMonolith.Common.Exceptions;

namespace My.ModularMonolith.Api.Filters;

[ExcludeFromCodeCoverage]
public class GlobalExceptionFilters :  IExceptionFilter
{
    private readonly ILogger _logger;

        public GlobalExceptionFilters(ILogger<GlobalExceptionFilters> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                if (context.Exception is BusinessValidationException validationException)
                {
                    var modelStateDictionary = new ModelStateDictionary();
                    modelStateDictionary.AddModelError(validationException.ValidationResult.MemberNames.FirstOrDefault() ?? "GlobalValidation",
                        validationException.ValidationResult.ErrorMessage ?? "Unknow reasons");

                    var problemDetails = new ValidationProblemDetails(modelStateDictionary)
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "One or more validation errors occurred.",
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                    };

                    var traceId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;
                    problemDetails.Extensions["traceId"] = traceId;
                    problemDetails.Extensions["errorCode"] = validationException.ErrorCode;

                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else if (context.Exception is SqlException sqlException)
                {
                    var message = context.Exception.Message;
                    if (sqlException.Number == 547)
                    {
                        var linkedObject = TryGetRelatedObject(sqlException) ?? "another object";
                        message = $"Impossible to delete because is linked to {linkedObject}.";
                    }
                    var genericError = new GenericError(message,sqlException.GetType().Name,Guid.NewGuid());
                    var errorServerObjectResult = new ErrorServerObjectResult(genericError);
                    context.Result = errorServerObjectResult;
                }
                else
                {
                    var message = $"Message : {context.Exception.Message}; InnerExceptionMessage: {context.Exception.InnerException?.Message}";
                    var genericError = new GenericError(message, context.Exception.GetType().Name, Guid.NewGuid());
                    var errorServerObjectResult = new ErrorServerObjectResult(genericError);
                    _logger.LogError("Exception of type {genericError.ErrorType} occured with id {genericError.ErrorId}. Reasons : {genericError.Message}", genericError.ErrorType, genericError.ErrorId, genericError.Message);
                    context.Result = errorServerObjectResult;
                }
            }
        }

        private static string? TryGetRelatedObject(SqlException sqlException)
        {
            try
            {
                return sqlException.Message.Split("table")[1].Split('.')[1].Split(',')[0].Replace('"', '\0');
            }
            catch
            {
                return null;
            }
        }
    }