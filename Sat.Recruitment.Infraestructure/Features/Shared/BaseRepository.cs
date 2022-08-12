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
    public abstract class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected RecruitmentDbContext context { get; set; }
        public BaseRepository(RecruitmentDbContext context)
        {
            this.context = context;
        }
        public async Task<T> GetById(int id) => await this.context.Set<T>().FindAsync(id);

        public Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
            => this.context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<T> Add(T entity)
        {
            await this.context.Set<T>().AddAsync(entity);
            await this.context.SaveChangesAsync();
            return entity;
        }

        public Task Update(T entity)
        {
            this.context.Entry(entity).State = EntityState.Modified;
            return this.context.SaveChangesAsync();
        }

        public Task Remove(T entity)
        {
            this.context.Set<T>().Remove(entity);
            return this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await this.context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await this.context.Set<T>().Where(predicate).ToListAsync();
        }

        public Task<int> CountAll() => this.context.Set<T>().CountAsync();

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate)
            => this.context.Set<T>().CountAsync(predicate);

    }
}
