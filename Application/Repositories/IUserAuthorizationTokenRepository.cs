﻿using Application.Entities;
using Application.Repositories.BasicRepositories;

namespace Application.Repositories
{
    public interface IUserAuthorizationTokenRepository : 
        IAddedRepository<UserAuthorizationToken>, 
        IRemovableRepository<UserAuthorizationToken>,
        ISearchRepository<UserAuthorizationToken>
    {
        Task<UserAuthorizationToken> GetByUserIdAsync(long userId);
        Task<UserAuthorizationToken> GetByRefreshTokenAsync(string refreshToken);
    }
}
