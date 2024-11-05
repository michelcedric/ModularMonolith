using System.ComponentModel.DataAnnotations;

namespace My.ModularMonolith.Common.Exceptions;

public class BusinessValidationException : ValidationException
{
    public int ErrorCode { get; }

    public BusinessValidationException(int errorCode, string propertyName, string message)
        : base(new ValidationResult(message, [propertyName]), null, null)
    {
        ErrorCode = errorCode;
    }

    public BusinessValidationException(int errorCode, string message)
        : base(new ValidationResult(message, ["GlobalValidation"]), null, null)
    {
        ErrorCode = errorCode;
    }
}