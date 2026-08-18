using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Auth.Commands.Register
{
   public record RegisterCommand(
    string Name,
    string Email,
    string Password) : IRequest<Guid>;
}