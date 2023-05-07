using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKInternshipTask.Domain.Enums;

namespace VKInternshipTask.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Login).NotEmpty().MaximumLength(255);
            RuleFor(command => command.Password).NotEmpty().MinimumLength(3);
            RuleFor(command => command.UserGroupCode)
                .NotEmpty()
                .Must(GroupCodeIsValid);
        }

        private bool GroupCodeIsValid(string code)
        {
            return Enum.TryParse<UserGroupCode>(code, out _);
        }
    }
}
