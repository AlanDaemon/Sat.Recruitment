using Sat.Recruitment.Domain.Features.Users.Entities;
using Sat.Recruitment.Domain.Features.Users.Repository;
using Sat.Recruitment.Infraestructure.Database;
using Sat.Recruitment.Infraestructure.Features.Shared;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure.Features.Users.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(RecruitmentDbContext context) : base(context) { }
                    
        public new async Task<User> GetById(int id)
        {
            return await base.GetById(id);
        }

        public new async Task<IEnumerable<User>> GetWhere(Expression<Func<User, bool>> predicate)
        {
            return await base.GetWhere(predicate);
        }

        public new async Task Add(User entity)
        {            
            await base.Add(entity);
        }

        public new async Task Remove(User entity)
        {
            await base.Remove(entity);
        }

        public new async Task Update(User entity)
        {
            await base.Update(entity);
        }
    }
}
