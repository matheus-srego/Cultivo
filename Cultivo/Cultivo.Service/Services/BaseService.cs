using Cultivo.Domain.Interfaces.Repositories;
using Cultivo.Domain.Interfaces.Services;
using Cultivo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<T> GetByIdAsync(int id) => await _baseRepository.GetByIdAsync(id);

        public async Task<T> CreateAsync(T entity) => await _baseRepository.CreateAsync(entity);

        public async Task<T> UpdateAsync(T entity) => await _baseRepository.UpdateAsync(entity);

        public async Task<T> DeleteAsync(int id) => await _baseRepository.DeleteAsync(id);
    }
}
