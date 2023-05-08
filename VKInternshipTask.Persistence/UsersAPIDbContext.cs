using Microsoft.EntityFrameworkCore;
using VKInternshipTask.Application.Common.Interfaces;
using VKInternshipTask.Domain.Entities;

namespace VKInternshipTask.Persistence
{
    public class UsersAPIDbContext : DbContext, IUsersAPIDbContext
    {
        public UsersAPIDbContext()
            : base() { }

        public UsersAPIDbContext(DbContextOptions<UsersAPIDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserGroup> UsersGroups { get; set; } = null!;
        public DbSet<UserState> UsersStates { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
