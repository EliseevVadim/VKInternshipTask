using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKInternshipTask.Application.Features.Users.Commands.CreateUser;

namespace VKInternshipTask.Tests.Common.Fakers
{
    public class FakeUsersSources
    {
        public static List<CreateUserCommand> GenerateUsersSourceData(int amount)
        {
            Randomizer.Seed = new Random(316);
            var users = new Faker<CreateUserCommand>()
                .RuleFor(command => command.Login, f => f.Name.FirstName())
                .RuleFor(command => command.Password, f => f.Random.String(8))
                .RuleFor(command => command.UserGroupCode, f => f.Parse("User"));
            return users.Generate(amount);
        }
    }
}
