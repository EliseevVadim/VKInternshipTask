using FluentValidation;

namespace VKInternshipTask.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty().GreaterThan(0);
        }
    }
}
