using System.ComponentModel.DataAnnotations;

namespace My.ModularMonolith.MyAModule.Api.Application.AModel.Commands.Update;

public record UpdateAModelCommand([Required] Guid Id, [Required] string Name, string? Description) : IRequest;