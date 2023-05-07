using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKInternshipTask.Application.Features.Users.Queries.GetUserInfo
{
    public class GetUserInfoQueryValidator : AbstractValidator<GetUserInfoQuery>
    {
        public GetUserInfoQueryValidator()
        {
            RuleFor(query => query.UserId).NotEmpty().GreaterThan(0);
        }
    }
}
