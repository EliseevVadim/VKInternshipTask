using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using VKInternshipTask.Application.Common.Exceptions;
using VKInternshipTask.Application.Common.Interfaces;
using VKInternshipTask.Application.ViewModels;
using VKInternshipTask.Domain.Entities;
using VKInternshipTask.Domain.Enums;

namespace VKInternshipTask.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserViewModel>
    {
        private IUsersAPIDbContext _context;
        private IMapper _mapper;

        public CreateUserCommandHandler(IUsersAPIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            UserGroupCode groupCode = (UserGroupCode)Enum.Parse(typeof(UserGroupCode), request.UserGroupCode);
            if (groupCode == UserGroupCode.Admin)
                await CheckIsAdminAlreadyExistsAsync(cancellationToken);
            UserGroup? requestedGroup = await _context.UsersGroups
                .FirstOrDefaultAsync(group => group.Code == groupCode);
            if (requestedGroup == null)
                throw new NotFoundException(nameof(UserGroup), request.UserGroupCode);
            UserState? activeState = await _context.UsersStates
                .FirstOrDefaultAsync(state => state.Code == UserStateCode.Active);
            if (activeState == null)
                throw new NotFoundException(nameof(UserGroup), "Active");
            DateTime creationDate = DateTime.Now;
            await CheckUserWithSimilarLoginAsync(request.Login, creationDate, cancellationToken);
            User user = CreateUser(request, requestedGroup.Id, activeState.Id, creationDate);
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UserViewModel>(user);
        }

        private async Task CheckIsAdminAlreadyExistsAsync(CancellationToken cancellationToken)
        {
            bool adminAlreadyExists = await _context.Users
                    .Include(user => user.UserGroup)
                    .AnyAsync(user => user.UserGroup.Code == UserGroupCode.Admin, cancellationToken);
            if (adminAlreadyExists)
                throw new ConflictActionException("Admin user already exists");
        }

        private async Task CheckUserWithSimilarLoginAsync(string login, DateTime creationDate, CancellationToken cancellationToken)
        {
            User? userWithSimilarLogin = await _context.Users
                .Where(user => user.Login == login)
                .OrderByDescending(user => user.CreatedDate)
                .FirstOrDefaultAsync(cancellationToken);
            if (userWithSimilarLogin != null &&
                userWithSimilarLogin.CreatedDate.Subtract(creationDate) <= TimeSpan.FromSeconds(5))
                throw new ConflictActionException("User with same login created right now. Wait a little bit");
        }

        private User CreateUser(CreateUserCommand request, int groupId, int stateId, DateTime creationDate)
        {
            SHA256 hashingAlgorithm = SHA256.Create();
            byte[] hash = hashingAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            string hashedPassword = Convert.ToBase64String(hash);
            return new User()
            {
                Login = request.Login,
                Password = hashedPassword,
                CreatedDate = creationDate,
                UserGroupId = groupId,
                UserStateId = stateId
            };
        }
    }
}
