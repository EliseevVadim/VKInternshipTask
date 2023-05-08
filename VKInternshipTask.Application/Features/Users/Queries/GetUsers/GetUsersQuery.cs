using MediatR;
using VKInternshipTask.Application.ViewModels;

namespace VKInternshipTask.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<UsersListViewModel>
    {
        public int? Page { get; set; }
        public int? Size { get; set; }
    }
}
