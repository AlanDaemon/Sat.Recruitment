using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain.Shared.Repository;
using Sat.Recruitment.Infraestructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infraestructure.Features.Shared
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected RecruitmentDbContext Context { get; set; }
        public BaseRepository(RecruitmentDbContext context)
        {
            Context = context;
        }

        public async Task<T> GetById(int id) => await Context.Set<T>().FindAsync(id);              

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> Add(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public Task Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChangesAsync();
        }

        public Task Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
            return Context.SaveChangesAsync();
        }
    }
}
