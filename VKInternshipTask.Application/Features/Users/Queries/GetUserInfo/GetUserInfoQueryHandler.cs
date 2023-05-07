using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKInternshipTask.Application.Common.Exceptions;
using VKInternshipTask.Application.Common.Interfaces;
using VKInternshipTask.Application.ViewModels;
using VKInternshipTask.Domain.Entities;

namespace VKInternshipTask.Application.Features.Users.Queries.GetUserInfo
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserViewModel>
    {
        private IUsersAPIDbContext _context;
        private IMapper _mapper;

        public GetUserInfoQueryHandler(IUsersAPIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            User? user = await _context.Users
                .Include(user => user.UserGroup)
                .Include(user => user.UserState)
                .FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);
            if (user == null)
                throw new NotFoundException(nameof(User), request.UserId);
            return _mapper.Map<UserViewModel>(user);
        }
    }
}
