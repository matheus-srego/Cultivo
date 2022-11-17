using Cultivo.Domain.Models;
using FluentValidation;
using System.Linq.Expressions;

namespace Cultivo.Domain.Interfaces.Services
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<T> GetOneByCriteriaAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> ListUsers();
        Task<T> CreateAsync<TValidator>(T entity) where TValidator : AbstractValidator<T>;
        Task<T> UpdateAsync<TValidator>(T entity) where TValidator : AbstractValidator<T>;
        Task<T> DeleteAsync(int id);

        void Validate(T entity, AbstractValidator<T> validator);
    }
}
