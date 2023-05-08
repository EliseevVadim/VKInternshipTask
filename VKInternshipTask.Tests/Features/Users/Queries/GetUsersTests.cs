using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VKInternshipTask.Application.Features.Users.Commands.CreateUser;
using VKInternshipTask.Application.Features.Users.Queries.GetUsers;
using VKInternshipTask.Application.ViewModels;
using VKInternshipTask.Tests.Common;
using VKInternshipTask.Tests.Common.Fakers;
using Xunit;

namespace VKInternshipTask.Tests.Features.Users.Queries
{
    public class GetUsersTests : TestBase
    {
        [Fact]
        public async Task GetUsers_Success()
        {
            //Arrange
            SetUpUsers();
            GetUsersQueryHandler handler = new GetUsersQueryHandler(Context, Mapper);

            //Act
            var result = await handler.Handle(new GetUsersQuery(), CancellationToken.None);

            //Assert
            result.ShouldBeOfType<UsersListViewModel>();
            result.CurrentCount.ShouldBe(10);
            result.TotalCount.ShouldBe(30);
            result.CurrentPage.ShouldBe(1);
            result.TotalPages.ShouldBe(3);
        }

        [Fact]
        public async Task GetUsersPaginated_Success()
        {
            //Arrange
            SetUpUsers();
            GetUsersQueryHandler handler = new GetUsersQueryHandler(Context, Mapper);

            //Act
            var result = await handler.Handle(new GetUsersQuery()
            {
                Page = 2,
                Size = 5
            }, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<UsersListViewModel>();
            result.CurrentCount.ShouldBe(5);
            result.TotalCount.ShouldBe(30);
            result.CurrentPage.ShouldBe(2);
            result.TotalPages.ShouldBe(6);
        }

        private void SetUpUsers()
        {
            Context.Users.RemoveRange(Context.Users.ToList());
            CreateUserCommandHandler setUpHandler = new CreateUserCommandHandler(Context, Mapper);
            var createUsersCommands = FakeUsersSources.GenerateUsersSourceData(30);
            createUsersCommands.ForEach(async (command) =>
            {
                await setUpHandler.Handle(command, CancellationToken.None);
            });
        }
    }
}
