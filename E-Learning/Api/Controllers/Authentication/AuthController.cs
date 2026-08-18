using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Authentication
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
    {

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterCommand command)
    {
        var userId = await mediator.Send(command);

        return Ok(new
        {
            userId
        });
    }
    }
}