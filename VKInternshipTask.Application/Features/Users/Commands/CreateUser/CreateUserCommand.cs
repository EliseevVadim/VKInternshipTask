using MediatR;
using VKInternshipTask.Application.ViewModels;

namespace VKInternshipTask.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<UserViewModel>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserGroupCode { get; set; }
    }
}
