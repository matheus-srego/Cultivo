using Cultivo.Domain.Constants;
using Cultivo.Domain.Interfaces.Repositories;
using Cultivo.Domain.Interfaces.Services;
using Cultivo.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cultivo.Service.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly IBaseRepository<T> _baseRepository;

        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<T> GetOneByCriteriaAsync(Expression<Func<T, bool>> expression) => await _baseRepository.GetOneByCriteriaAsync(expression);
        public async Task<T> GetByIdAsync(int id) => await _baseRepository.GetByIdAsync(id);
        public async Task<IEnumerable<T>> ListUsers() => await _baseRepository.ListUsers();
        public async Task<T> DeleteAsync(int id) => await _baseRepository.DeleteAsync(id);

        public async Task<T> CreateAsync<TValidator>(T entity) where TValidator : AbstractValidator<T>
        {
            Validate(entity, Activator.CreateInstance<TValidator>());
            return await _baseRepository.CreateAsync(entity);
        }

        public async Task<T> UpdateAsync<TValidator>(T entity) where TValidator : AbstractValidator<T>
        {
            Validate(entity, Activator.CreateInstance<TValidator>());
            return await _baseRepository.UpdateAsync(entity);
        }

        public void Validate(T entity, AbstractValidator<T> validator)
        {
            if (entity == null)
                throw new Exception(Exceptions.MESSAGE_REGISTER);

            validator.ValidateAndThrow(entity);
        }
    }
}
