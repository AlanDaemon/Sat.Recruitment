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

        public new async Task Add(User entity)
        {
            await base.Add(entity);
        }

        public new Task<int> CountAll()
        {
            throw new NotImplementedException();
        }

        public new Task<int> CountWhere(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public new Task<User> FirstOrDefault(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public new Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public new Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public new async Task<IEnumerable<User>> GetWhere(Expression<Func<User, bool>> predicate)
        {
            return await base.GetWhere(predicate);
        }

        public new Task Remove(User entity)
        {
            throw new NotImplementedException();
        }

        public new Task Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
