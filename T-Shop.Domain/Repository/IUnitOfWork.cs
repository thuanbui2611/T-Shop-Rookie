namespace T_Shop.Domain.Repository
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
        void Detach<T>(T entity) where T : class;
        void DetachRange<T>(IEnumerable<T> entities) where T : class;
        void Dispose();
        IGenericRepository<T> GetBaseRepo<T>() where T : class;
    }
}
