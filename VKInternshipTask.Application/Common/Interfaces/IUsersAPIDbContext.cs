using Microsoft.EntityFrameworkCore;
using VKInternshipTask.Domain.Entities;

namespace VKInternshipTask.Application.Common.Interfaces
{
    public interface IUsersAPIDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UsersGroups { get; set; }
        public DbSet<UserState> UsersStates { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
