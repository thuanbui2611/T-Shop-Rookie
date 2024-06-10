using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence;

namespace T_Shop.Infrastructure.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationContext _context;
        internal DbSet<T> dbSet;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual T Add(T entity)
        {
            dbSet.Add(entity);
            return entity;
        }

        public virtual IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            if (entities.Any())
            {
                dbSet.AddRange(entities);
                return entities;
            }
            else
            {
                throw new Exception(message: "An error occours when adding ranged of entities");
            }
        }

        public virtual bool Delete(Guid id)
        {
            var entity = dbSet.Find(id);
            if (entity == null)
            {
                return false;
            }
            else
                dbSet.Remove(entity);
            return true;
        }

        public virtual bool DeleteByEntity(T entity)
        {
            dbSet.Remove(entity);
            return true;
        }

        public virtual bool DeleteRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
            return true;
        }

        public virtual async Task<IEnumerable<T>> All()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<List<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T> FindOne(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<bool> Check(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.AnyAsync(predicate);
        }

        public T Update(T entity)
        {
            dbSet.Update(entity);
            return entity;
        }
    }
}
