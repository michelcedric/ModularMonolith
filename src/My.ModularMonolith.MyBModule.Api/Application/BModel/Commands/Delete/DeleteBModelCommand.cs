using System.ComponentModel.DataAnnotations;
using MediatR;

namespace My.ModularMonolith.MyBModule.Api.Application.BModel.Commands.Delete;

public record DeleteBModelCommand([Required] Guid Id) : IRequest;