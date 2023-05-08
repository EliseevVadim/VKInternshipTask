using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VKInternshipTask.Application.Common.Exceptions;
using VKInternshipTask.Application.Features.Users.Commands.CreateUser;
using VKInternshipTask.Application.Features.Users.Commands.DeleteUser;
using VKInternshipTask.Application.ViewModels;
using VKInternshipTask.Tests.Common;
using Xunit;

namespace VKInternshipTask.Tests.Features.Users.Commands
{
    public class DeleteUserTests : TestBase
    {
        [Fact]
        public async Task DeleteUser_Success()
        {
            // Arrange
            CreateUserCommandHandler setUpHandler = new CreateUserCommandHandler(Context, Mapper);
            DeleteUserCommandHandler deletionHandler = new DeleteUserCommandHandler(Context);
            UserViewModel userForDelete = await setUpHandler.Handle(new CreateUserCommand()
            {
                Login = "UserForDelete",
                Password = "TestPassword1",
                UserGroupCode = "User"
            }, CancellationToken.None);
            int userForDeleteId = userForDelete.Id;

            //Act
            await deletionHandler.Handle(new DeleteUserCommand()
            {
                UserId = userForDeleteId
            }, CancellationToken.None);

            //Assert
            Assert.True(Context.Users
                .First(user => user.Id == userForDeleteId)
                .UserStateId == UsersAPIContextFactory.BlockedStateId);
        }

        [Fact]
        public async Task DeleteUser_FailOnWrongId()
        {
            // Arrange
            DeleteUserCommandHandler deletionHandler = new DeleteUserCommandHandler(Context);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await deletionHandler.Handle(new DeleteUserCommand()
                {
                    UserId = 316
                }, CancellationToken.None);
            });
        }

        [Fact]
        public async Task DeleteUser_FailOnAlreadyDeleted()
        {
            // Arrange
            CreateUserCommandHandler setUpHandler = new CreateUserCommandHandler(Context, Mapper);
            DeleteUserCommandHandler deletionHandler = new DeleteUserCommandHandler(Context);
            UserViewModel userForDoubleDelete = await setUpHandler.Handle(new CreateUserCommand()
            {
                Login = "userForDoubleDelete",
                Password = "TestPassword1",
                UserGroupCode = "User"
            }, CancellationToken.None);
            int userForDeleteId = userForDoubleDelete.Id;
            await deletionHandler.Handle(new DeleteUserCommand()
            {
                UserId = userForDeleteId
            }, CancellationToken.None);

            //Act
            //Assert
            await Assert.ThrowsAsync<ConflictActionException>(async () =>
            {
                await deletionHandler.Handle(new DeleteUserCommand()
                {
                    UserId = userForDeleteId
                }, CancellationToken.None);
            });
        }
    }
}
