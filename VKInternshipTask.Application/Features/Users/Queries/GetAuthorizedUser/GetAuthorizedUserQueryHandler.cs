using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using VKInternshipTask.Application.Common.Interfaces;
using VKInternshipTask.Application.ViewModels;
using VKInternshipTask.Domain.Entities;

namespace VKInternshipTask.Application.Features.Users.Queries.GetAuthorizedUser
{
    public class GetAuthorizedUserQueryHandler : IRequestHandler<GetAuthorizedUserQuery, UserViewModel?>
    {
        private IUsersAPIDbContext _context;
        private IMapper _mapper;

        public GetAuthorizedUserQueryHandler(IUsersAPIDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserViewModel?> Handle(GetAuthorizedUserQuery request, CancellationToken cancellationToken)
        {
            SHA256 hashingAlgorithm = SHA256.Create();
            byte[] hash = hashingAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            string hashedPassword = Convert.ToBase64String(hash);
            User? authorizedUser = await _context.Users
                .FirstOrDefaultAsync(user => user.Login == request.Login && user.Password == hashedPassword);
            return authorizedUser == null ? null : _mapper.Map<UserViewModel>(authorizedUser);
        }
    }
}
