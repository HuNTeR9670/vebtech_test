﻿using Microsoft.AspNetCore.Authorization;

namespace VebtechTest.API.Common;

[Authorize]
[ApiController]
[ApiExceptionFilter]
[Route("api/v1/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}

