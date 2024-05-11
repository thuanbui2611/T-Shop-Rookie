using Microsoft.EntityFrameworkCore;
using T_Shop.Infrastructure.Persistence;

namespace T_Shop.Infrastructure.Data.Queries
{
    public class BaseQuery<T> where T : class
    {
        protected DbSet<T> dbSet;

        public BaseQuery() { }

        public BaseQuery(ApplicationContext dbContext)
        {
            dbSet = dbContext.Set<T>();
        }
    }
}
