using System;
using System.Linq;
using System.Linq.Expressions;
using PathSystem.DAL.Abstractions;
using PathSystem.DAL.Repositories.Interfaces;

namespace PathSystem.DAL.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationContext _context;

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public virtual void Add(T item)
        {
            _context.Add(item);
        }

        public void Delete(T item)
        {
            _context.Remove(item);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public virtual T GetById(int id)
        {
            return _context.Set<T>().SingleOrDefault(entity => entity.Id == id);
        }
    }
}
