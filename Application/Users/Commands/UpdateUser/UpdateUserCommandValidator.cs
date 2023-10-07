using VebtechTest.Application.Users.Commands.CreateUser;

namespace VebtechTest.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(IApplicationDbContext context) 
    {
        Include(new CreateUserCommandValidator(context));
    }
}
