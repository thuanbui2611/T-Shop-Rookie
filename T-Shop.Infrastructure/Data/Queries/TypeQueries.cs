using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence;

namespace T_Shop.Infrastructure.Data.Queries;
public class TypeQueries : BaseQuery<TypeProduct>, ITypeQueries
{
    public TypeQueries(ApplicationContext dbContext) : base(dbContext)
    {

    }

    public async Task<List<TypeProduct>> GetTypesAsync()
    {
        return await dbSet
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<TypeProduct> GetTypeByIdAsync(Guid id)
    {
        return await dbSet
            .Where(t => t.Id.Equals(id))
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<bool> CheckIsTypeExisted(string name)
    {
        return await dbSet
            .AnyAsync(t => t.Name.ToLower().Equals(name.ToLower()));
    }

}
