using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKInternshipTask.Application.ViewModels;

namespace VKInternshipTask.Application.Features.Users.Queries.GetAuthorizedUser
{
    public class GetAuthorizedUserQuery : IRequest<UserViewModel?>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
