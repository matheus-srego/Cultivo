using Cultivo.Domain.Models;

namespace Cultivo.Domain.Interfaces.Services
{
    public interface IUserService : IBaseService<User>
    {
        Task<User> GetUserWithPosts(string email);
        // ---
    }
}
