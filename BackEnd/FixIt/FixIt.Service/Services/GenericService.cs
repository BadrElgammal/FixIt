using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Repositories;
using FixIt.Service.Abstracts;
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
        protected readonly IGenericRepository<T> _repository; 
        public GenericService(IGenericRepository<T> Repository)
        {
            _repository = Repository;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _repository.Find(predicate);
        }
        public void Delete(T entity)
        {
            _repository.Delete(entity);
            _repository.SaveToDB();
        }


        public List<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T GetById(object id)
        {
            return _repository.GetById(id);
        }

        public void Create(T entity)
        {
            _repository.Create(entity);
            _repository.SaveToDB();
        }


        public void Update(T entity)
        {
            _repository.Update(entity);
            _repository.SaveToDB();
        }
    }
}
