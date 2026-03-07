using FixIt.Infrastructure.Abstracts;
using FixIt.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        //connect with DB
        readonly FIXITDbContext _db;
        public GenericRepository(FIXITDbContext db)
        {
            _db = db;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _db.Set<T>().Where(predicate).ToList();
        }
        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public T GetById(object id)
        {
            return _db.Set<T>().Find(id);
        }

        public void Create(T entity)
        {
            //Save inCache
            _db.Add(entity);
            //Use SaveToDB
        }

        public void Update(T entity)
        {
            //Save inCache
            _db.Update(entity);
            //use SaveToDB
        }

        public void Delete(T entity)
        {
            //Save inCache
            _db.Remove(entity);
        }
         
        public void SaveToDB()
        {
            _db.SaveChanges();
        }

    }
}
