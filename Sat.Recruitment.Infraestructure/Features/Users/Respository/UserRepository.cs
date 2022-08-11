using Sat.Recruitment.Domain.Features.Users.Entities;
using Sat.Recruitment.Domain.Features.Users.Repository;
using Sat.Recruitment.Infraestructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure.Features.Users.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly RecruitmentDbContext context;

        public UserRepository(RecruitmentDbContext context)  
        {
            this.context = context;
        }

        public Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<User>> GetFiltered(Expression<Func<User, bool>> expression)
        {
            return context.Users.Where(expression);
        }
     
        public async Task<User> Add(User entity)
        {
            context.Users.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
