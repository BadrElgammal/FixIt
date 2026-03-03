using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixIt.Infrastructure.InfraBases
{
    internal interface IGenericRepository<T> where T : class
    {
        public List<T> GetAll();
        public T GetById(int id);
        public void Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public void SaveToDB();


    }
}
