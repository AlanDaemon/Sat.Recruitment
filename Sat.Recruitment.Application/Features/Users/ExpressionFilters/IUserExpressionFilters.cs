using Sat.Recruitment.Domain.Features.Users.Entities;
using System;
using System.Linq.Expressions;

namespace Users.Domain.Features.Users
{
    public interface IUserExpressionFilters
    {
        Expression<Func<User, bool>> Exists(User user);
    }
}