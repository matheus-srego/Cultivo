using Cultivo.Domain.Interfaces.Repositories;
using Cultivo.Domain.Models;
using Cultivo.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultivo.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> _query;
        protected readonly CultivoContext _context;

        public BaseRepository(CultivoContext context)
        {
            _context = context;
            _query = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _query.FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _query.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _query.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
