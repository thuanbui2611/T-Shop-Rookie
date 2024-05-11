using System.Linq.Expressions;

namespace T_Shop.Domain.Repository
{
    public interface IGenericRepository<T>
    {
        T Add(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> All();
        bool Delete(Guid id);
        bool DeleteByEntity(T entity);
        bool DeleteRange(IEnumerable<T> entities);
        Task<List<T>> Find(Expression<Func<T, bool>> predicate);
        Task<T> FindOne(Expression<Func<T, bool>> predicate);
        Task<T> GetById(Guid id);
        T Update(T entity);
        Task<bool> Check(Expression<Func<T, bool>> predicate);
    }
}
