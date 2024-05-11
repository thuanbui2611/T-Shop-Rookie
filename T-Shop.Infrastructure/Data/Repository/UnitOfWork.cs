using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence;

namespace T_Shop.Infrastructure.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _context;
        private Dictionary<string, object> _repositories;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> GetBaseRepo<T>() where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (_repositories.ContainsKey(type) == false)
            {
                var repositoryInstance = new GenericRepository<T>(_context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<T>)_repositories[type];
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Detach<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public void DetachRange<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
