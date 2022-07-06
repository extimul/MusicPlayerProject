using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ServerApp.WebApp.Base.Controllers;

[ApiController]
[Produces("application/json")]
public class BaseApiController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => (_mediator ?? HttpContext.RequestServices.GetRequiredService<IMediator>()) 
                                    ?? throw new InvalidOperationException("MediatR is null");
}