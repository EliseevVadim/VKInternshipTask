using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using VKInternshipTask.Application.Features.Users.Commands.CreateUser;
using VKInternshipTask.Application.Features.Users.Queries.GetUserInfo;
using VKInternshipTask.Application.ViewModels;
using VKInternshipTask.Tests.Common;
using Xunit;
using VKInternshipTask.Application.Common.Exceptions;

namespace VKInternshipTask.Tests.Features.Users.Queries
{
    public class GetUserInfoTests : TestBase
    {
        [Fact]
        public async Task GetUserInfo_Success()
        {
            // Arrange
            CreateUserCommandHandler setUpHandler = new CreateUserCommandHandler(Context, Mapper);
            GetUserInfoQueryHandler queryHandler = new GetUserInfoQueryHandler(Context, Mapper);
            UserViewModel user = await setUpHandler.Handle(new CreateUserCommand()
            {
                Login = "GetUser",
                Password = "TestPassword1",
                UserGroupCode = "User"
            }, CancellationToken.None);

            //Act
            var result = await queryHandler.Handle(new GetUserInfoQuery()
            {
                UserId = user.Id,
            }, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<UserViewModel>();
            result.Id.ShouldBe(user.Id);
        }

        [Fact]
        public async Task GetUserInfo_FailOnWrongId()
        {
            // Arrange
            GetUserInfoQueryHandler handler = new GetUserInfoQueryHandler(Context, Mapper);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(new GetUserInfoQuery()
                {
                    UserId = 316
                }, CancellationToken.None);
            });
        }
    }
}
