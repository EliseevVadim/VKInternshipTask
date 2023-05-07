using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKInternshipTask.Application.ViewModels;

namespace VKInternshipTask.Application.Features.Users.Queries.GetUserInfo
{
    public class GetUserInfoQuery : IRequest<UserViewModel>
    {
        public int UserId { get; set; }
    }
}
