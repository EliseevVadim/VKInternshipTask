using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using VKInternshipTask.Domain.Entities;
using VKInternshipTask.Domain.Enums;
using VKInternshipTask.Persistence;

namespace VKInternshipTask.Tests.Common
{
    public class UsersAPIContextFactory
    {
        public static int AdminGroupId { get; private set; }
        public static int UserGroupId { get; private set; }
        public static int ActiveStateId { get; private set; }
        public static int BlockedStateId { get; private set; }

        public static UsersAPIDbContext Create()
        {
            var options = new DbContextOptionsBuilder<UsersAPIDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            UsersAPIDbContext context = new UsersAPIDbContext(options);
            context.Database.EnsureCreated();
            context.UsersGroups.AddRange(
                new UserGroup()
                {
                    Description = "System admin",
                    Code = UserGroupCode.Admin
                },
                new UserGroup()
                {
                    Description = "System user",
                    Code = UserGroupCode.User
                }
            );
            context.UsersStates.AddRange(
                new UserState()
                {
                    Description = "Active user",
                    Code = UserStateCode.Active
                },
                new UserState()
                {
                    Description = "Blocked user",
                    Code = UserStateCode.Blocked
                }
            );
            context.SaveChanges();
            AdminGroupId = context.UsersGroups.First(group => group.Code == UserGroupCode.Admin).Id;
            UserGroupId = context.UsersGroups.First(group => group.Code == UserGroupCode.User).Id;
            ActiveStateId = context.UsersStates.First(group => group.Code == UserStateCode.Active).Id;
            BlockedStateId = context.UsersStates.First(group => group.Code == UserStateCode.Blocked).Id;
            return context;
        }

        public static void Destroy(UsersAPIDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
