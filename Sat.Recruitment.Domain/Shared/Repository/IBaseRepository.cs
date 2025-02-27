﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain.Shared.Repository
{
    public interface IBaseRepository<T> where T : class
    {

        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Remove(T entity);

    }
}
