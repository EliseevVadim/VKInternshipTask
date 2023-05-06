using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
