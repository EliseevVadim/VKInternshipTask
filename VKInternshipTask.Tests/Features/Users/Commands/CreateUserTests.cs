using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VKInternshipTask.Application.Common.Exceptions;
using VKInternshipTask.Application.Features.Users.Commands.CreateUser;
using VKInternshipTask.Application.ViewModels;
using VKInternshipTask.Tests.Common;
using Xunit;

namespace VKInternshipTask.Tests.Features.Users.Commands
{
    public class CreateUserTests : TestBase
    {
        [Fact]
        public async Task CreateUser_Success()
        {
            // Arrange
            CreateUserCommandHandler handler = new CreateUserCommandHandler(Context, Mapper);

            //Act
            UserViewModel user = await handler.Handle(new CreateUserCommand()
            {
                Login = "TestUser1",
                Password = "TestPassword1",
                UserGroupCode = "User"
            }, CancellationToken.None);

            //Assert
            Assert.NotNull(await Context.Users.FindAsync(user.Id, CancellationToken.None));
            Assert.True(user.UserGroup.Id == UsersAPIContextFactory.UserGroupId);
            Assert.True(user.UserState.Id == UsersAPIContextFactory.ActiveStateId);
        }

        [Fact]
        public async Task CreateUserAdmin_Success()
        {
            // Arrange
            CreateUserCommandHandler handler = new CreateUserCommandHandler(Context, Mapper);

            //Act
            UserViewModel admin = await handler.Handle(new CreateUserCommand()
            {
                Login = "TestUser66",
                Password = "TestPassword1",
                UserGroupCode = "Admin"
            }, CancellationToken.None);

            //Assert
            Assert.NotNull(await Context.Users.FindAsync(admin.Id, CancellationToken.None));
            Assert.True(admin.UserGroup.Id == UsersAPIContextFactory.AdminGroupId);
            Assert.True(admin.UserState.Id == UsersAPIContextFactory.ActiveStateId);

            //Teardown
            Context.Users.Remove(Context.Users.First(user => user.Id == admin.Id));
        }

        [Fact]
        public async Task CreateUser_FailOnSecondAdmin()
        {
            // Arrange
            CreateUserCommandHandler handler = new CreateUserCommandHandler(Context, Mapper);
            UserViewModel user = await handler.Handle(new CreateUserCommand()
            {
                Login = "TestUser2",
                Password = "TestPassword2",
                UserGroupCode = "Admin"
            }, CancellationToken.None);

            //Act
            //Assert
            await Assert.ThrowsAsync<ConflictActionException>(async () =>
            {
                await handler.Handle(new CreateUserCommand()
                {
                    Login = "TestUser3",
                    Password = "TestPassword3",
                    UserGroupCode = "Admin"
                }, CancellationToken.None);
            });
        }

        [Fact]
        public async Task CreateUser_FailOnSameLoginFastCreated()
        {
            // Arrange
            CreateUserCommandHandler handler = new CreateUserCommandHandler(Context, Mapper);
            UserViewModel user = await handler.Handle(new CreateUserCommand()
            {
                Login = "TestUser4",
                Password = "TestPassword4",
                UserGroupCode = "User"
            }, CancellationToken.None);

            //Act
            //Assert
            await Assert.ThrowsAsync<ConflictActionException>(async () =>
            {
                await handler.Handle(new CreateUserCommand()
                {
                    Login = "TestUser4",
                    Password = "TestPassword5",
                    UserGroupCode = "User"
                }, CancellationToken.None);
            });
        }
    }
}
