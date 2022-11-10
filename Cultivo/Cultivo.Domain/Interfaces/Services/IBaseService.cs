using Cultivo.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cultivo.Domain.Interfaces.Services
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<T> GetOneByCriteria(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync<TValidator>(T entity) where TValidator : AbstractValidator<T>;
        Task<T> UpdateAsync<TValidator>(T entity) where TValidator : AbstractValidator<T>;
        Task<T> DeleteAsync(int id);

        void Validate(T entity, AbstractValidator<T> validator);
    }
}
