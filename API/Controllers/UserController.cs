using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VebtechTest.API;
using VebtechTest.Application.Users.Commands.AddRole;
using VebtechTest.Application.Users.Commands.CreateUser;
using VebtechTest.Application.Users.Commands.DeleteUser;
using VebtechTest.Application.Users.Commands.UpdateUser;
using VebtechTest.Application.Users.Queries.GetPaginatedUser;
using VebtechTest.Application.Users.Queries.GetUser;

namespace VebtechTest.Controllers;

public class UserController : ApiControllerBase
{

    [AllowAnonymous]
    [HttpGet("/login/{username}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public string Login(string username)
    {
        var claims = new List<Claim> 
        { 
            new Claim(ClaimTypes.Name, username) 
        };
        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    [HttpGet("list")]
    [ProducesResponseType(typeof(PaginatedList<UserItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<PaginatedList<UserItemDto>> GetPaginatedItemsAsync([FromQuery] GetPaginatedUserQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<UserDto> GetAsync(string? id)
    {
        return await Mediator.Send(new GetUserQuery { Id = id });
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<string> CreateAsync(CreateUserCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<string>> UpdateAsync(string id, UpdateUserCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(string id)
    {
        await Mediator.Send(new DeleteUserCommand { Id = id });

        return NoContent();
    }

    [HttpPost("add_role")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddRoleAsync(AddRoleCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
}