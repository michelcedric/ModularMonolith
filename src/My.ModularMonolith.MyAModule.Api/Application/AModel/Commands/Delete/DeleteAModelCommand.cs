using System.ComponentModel.DataAnnotations;
using MediatR;

namespace My.ModularMonolith.MyAModule.Api.Application.AModel.Commands.Delete;

public record DeleteAModelCommand([Required] Guid Id) : IRequest;