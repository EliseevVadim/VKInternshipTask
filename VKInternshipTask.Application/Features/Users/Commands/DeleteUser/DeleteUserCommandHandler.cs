using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKInternshipTask.Application.Common.Exceptions;
using VKInternshipTask.Application.Common.Interfaces;
using VKInternshipTask.Domain.Entities;
using VKInternshipTask.Domain.Enums;

namespace VKInternshipTask.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private IUsersAPIDbContext _context;

        public DeleteUserCommandHandler(IUsersAPIDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _context.Users
                .Include(user => user.UserState)
                .FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);
            if (user == null)
                throw new NotFoundException(nameof(User), request.UserId);
            if (user.UserState.Code == UserStateCode.Blocked)
                throw new ConflictActionException("User has already been deleted");
            user.UserState.Code = UserStateCode.Blocked;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
