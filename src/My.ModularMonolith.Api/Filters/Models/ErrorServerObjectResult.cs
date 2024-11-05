using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace My.ModularMonolith.Api.Filters.Models;

[ExcludeFromCodeCoverage]
[DefaultStatusCode(DefaultStatusCode)]
public class ErrorServerObjectResult : ObjectResult
{
    private const int DefaultStatusCode = StatusCodes.Status500InternalServerError;

    public ErrorServerObjectResult(GenericError genericError) : base(genericError)
    {
        StatusCode = DefaultStatusCode;
    }
}