using System.ComponentModel.DataAnnotations;
using My.ModularMonolith.MyAModule.Domain.Entities;

namespace My.ModularMonolith.MyAModule.Api.Application.AModel.Commands.Create;

public record CreateAModelCommand([Required]string Name, string? Description) : IRequest<ModelA>;