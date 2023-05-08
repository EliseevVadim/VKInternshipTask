using MediatR;
using VKInternshipTask.Application.ViewModels;

namespace VKInternshipTask.Application.Features.Users.Queries.GetUserInfo
{
    public class GetUserInfoQuery : IRequest<UserViewModel>
    {
        public int UserId { get; set; }
    }
}
