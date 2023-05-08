using FluentValidation;

namespace VKInternshipTask.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
    {
        public GetUsersQueryValidator()
        {
            RuleFor(query => query.Page).GreaterThan(0);
            RuleFor(query => query.Size).GreaterThan(0);
        }
    }
}
