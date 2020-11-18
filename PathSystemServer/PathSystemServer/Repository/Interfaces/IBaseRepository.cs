using System;
using System.Linq;
using System.Linq.Expressions;

namespace PathSystemServer.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        void Add(T item);
        void Delete(T item);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        T GetById(int id);
    }
}