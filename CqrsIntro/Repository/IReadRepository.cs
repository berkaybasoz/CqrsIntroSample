using System;
using System.Linq;
using System.Linq.Expressions;

namespace CqrsIntro.Repository
{
    public interface IReadRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        T Get(Expression<Func<T, bool>> predicate);
    }
}