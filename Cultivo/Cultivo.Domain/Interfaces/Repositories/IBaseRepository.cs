﻿using Cultivo.Domain.Models;
using System.Linq.Expressions;

namespace Cultivo.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetOneByCriteriaAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> ListUsers();
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);
    }
}
