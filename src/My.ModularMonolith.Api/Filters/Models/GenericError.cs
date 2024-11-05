using System.Diagnostics.CodeAnalysis;

namespace My.ModularMonolith.Api.Filters.Models;

[ExcludeFromCodeCoverage]
public record GenericError(string Message,string ErrorType, Guid ErrorId);