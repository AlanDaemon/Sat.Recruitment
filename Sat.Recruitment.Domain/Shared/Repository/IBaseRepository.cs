using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain.Shared.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetFiltered(Expression<Func<T, bool>> expression);      
        Task<T> Add(T entity);   
    }
}
