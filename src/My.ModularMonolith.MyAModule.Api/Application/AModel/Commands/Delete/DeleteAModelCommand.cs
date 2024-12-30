using System.ComponentModel.DataAnnotations;

namespace My.ModularMonolith.MyAModule.Api.Application.AModel.Commands.Delete;

public record DeleteAModelCommand([Required] Guid Id) : IRequest;