using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Entity;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence;

namespace T_Shop.Infrastructure.Data.Queries;
public class ColorQueries : BaseQuery<Color>, IColorQueries
{
    public ColorQueries(ApplicationContext dbContext) : base(dbContext)
    {

    }

    public async Task<List<Color>> GetColorsAsync()
    {
        return await dbSet
            .OrderBy(c => c.Name)
            .ToListAsync();
    }
    public async Task<Color> GetColorByIdAsync(Guid id)
    {
        return await dbSet
            .Where(c => c.Id.Equals(id))
            .FirstOrDefaultAsync();
    }

    public async Task<bool> CheckIsColorExisted(string name)
    {
        return await dbSet
            .AnyAsync(t => t.Name.ToLower().Equals(name.ToLower()));
    }

}
