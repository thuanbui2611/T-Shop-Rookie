﻿using Microsoft.EntityFrameworkCore;
using T_Shop.Domain.Repository;
using T_Shop.Infrastructure.Persistence;
using T_Shop.Infrastructure.Persistence.IdentityModels;

namespace T_Shop.Infrastructure.Data.Queries;
public class UserQueries : BaseQuery<ApplicationUser>, IUserQueries
{
    public UserQueries(ApplicationContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> CheckIfUserExisted(Guid userId)
    {
        return await dbSet
            .AnyAsync(x => x.Id.Equals(userId));
    }

}
