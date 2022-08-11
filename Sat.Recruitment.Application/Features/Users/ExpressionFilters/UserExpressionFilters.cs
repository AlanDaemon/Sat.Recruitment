using Sat.Recruitment.Domain.Features.Users.Entities;
using System;
using System.Linq.Expressions;

namespace Users.Domain.Features.Users
{
    public class UserExpressionFilters : IUserExpressionFilters
    {
        public Expression<Func<User, bool>> Exists(User user)
        {
            return x => (x.Email == user.Email || x.Phone == user.Phone ||
                        (x.Address == user.Address && x.Name == user.Name));
        }
    }
}
