using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Repositories;
using FixIt.Service.Abstracts;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Service.Services
{
    public class GenericService<T> : IService<T> where T : class
    {
        protected readonly IGenericRepositoryAsync<T> _repository; 
        public GenericService(IGenericRepositoryAsync<T> Repository)
        {
            _repository = Repository;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _repository.Find(predicate);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task DeleteRangeAsync(ICollection<T> entities)
        {
            await _repository.DeleteRangeAsync(entities);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }


        public IDbContextTransaction BeginTransaction()
        {
            return _repository.BeginTransaction();
        }

        public void Commit()
        {
            _repository.Commit();
        }

        public void RollBack()
        {
            _repository.RollBack();
        }

        public IQueryable<T> GetTableNoTracking()
        {
            return _repository.GetTableNoTracking();
        }

        public IQueryable<T> GetTableAsTracking()
        {
            return _repository.GetTableAsTracking();
        }

        public async Task<T> AddAsync(T entity)
        {
            return await _repository.AddAsync(entity);
        }

        public async Task AddRangeAsync(ICollection<T> entities)
        {
            await _repository.AddRangeAsync(entities);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task UpdateRangeAsync(ICollection<T> entities)
        {
            await _repository.UpdateRangeAsync(entities);
        }

        public async Task DeleteAsync(T entity)
        {
            await _repository.DeleteAsync(entity);
        }
    }
}
