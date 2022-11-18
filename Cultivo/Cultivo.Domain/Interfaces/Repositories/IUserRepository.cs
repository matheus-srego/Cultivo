﻿using Cultivo.Domain.Models;

namespace Cultivo.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> DeleteUserWithPosts(int id);
    }
}
