using MediatR;
using VKInternshipTask.Application.ViewModels;

namespace VKInternshipTask.Application.Features.Users.Queries.GetAuthorizedUser
{
    public class GetAuthorizedUserQuery : IRequest<UserViewModel?>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
