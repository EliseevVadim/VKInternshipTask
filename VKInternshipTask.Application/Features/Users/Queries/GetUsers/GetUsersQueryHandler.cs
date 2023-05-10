using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VKInternshipTask.Application.Common.Exceptions;
using VKInternshipTask.Application.Common.Interfaces;
using VKInternshipTask.Application.ViewModels;
using VKInternshipTask.Domain.Entities;
using VKInternshipTask.Domain.Enums;

namespace VKInternshipTask.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, UsersListViewModel>
    {
        private readonly IUsersAPIDbContext _context;
        private readonly IMapper _mapper;

        private const int DEFAULT_PAGE = 1;
        private const int DEFAULT_SIZE = 10;

        public GetUsersQueryHandler(IUsersAPIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UsersListViewModel> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            int page = request.Page ?? DEFAULT_PAGE;
            int size = request.Size ?? DEFAULT_SIZE;
            UserState? activeState = await _context.UsersStates
                .FirstOrDefaultAsync(state => state.Code == UserStateCode.Active);
            if (activeState == null)
                throw new NotFoundException(nameof(UserGroup), "Active");
            var users = await _context.Users
                .Include(user => user.UserGroup)
                .Include(user => user.UserState)
                .Where(user => user.UserStateId == activeState.Id)
                .OrderBy(user => user.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            int totalCount = await _context.Users
                .Where(user => user.UserStateId == activeState.Id)
                .CountAsync(cancellationToken);
            int totalPages = (int)Math.Ceiling(totalCount / (double)size);
            return new UsersListViewModel()
            {
                Users = users,
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                CurrentCount = users.Count
            };
        }
    }
}
