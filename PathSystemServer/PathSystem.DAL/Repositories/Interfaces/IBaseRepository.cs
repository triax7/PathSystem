using System;
using System.Linq;
using System.Linq.Expressions;
using PathSystem.DAL.Abstractions;

namespace PathSystem.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        void Add(T item);
        void Delete(T item);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        T GetById(int id);
    }
}